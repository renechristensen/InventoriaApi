using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using InventoriaApi.Services.RepositoryInterfaces;
using InventoriaApi.Services.Repositories;
using InventoriaApi.Models;
using InventoriaApi.DTOs.ResponseDTO;
using System.Security.Cryptography;
using InventoriaApi.DTOs.ReceivedDTOs;
using System.Threading.Tasks;
using System;

namespace InventoriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private IUserRoleRepository _userRoleRepository;

        public UserRoleController(IUserRoleRepository userRoleRepository)
        {
            _userRoleRepository = userRoleRepository;
        }

        // Get user roles by user ID
        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<UserRoleDTO>>> GetUserRoles(int userId)
        {
            var userRoles = await _userRoleRepository.ReadAllUserRolesByUserID(userId);
            if (userRoles == null || userRoles.Count == 0)
            {
                return NotFound("No roles found for the user.");
            }

            var userRoleDTOs = userRoles.Select(ur => new UserRoleDTO
            {
                UserRoleID = ur.UserRoleID,
                UserID = ur.UserID,
                RoleID = ur.RoleID,
                RoleName = ur.Role.RoleName
            }).ToList();

            return Ok(userRoleDTOs);
        }

        // Assign a role to a user
        [HttpPost("Assign")]
        public async Task<IActionResult> AssignUserRole([FromBody] AssignUserRoleDTO assignRoleDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (await _userRoleRepository.UserHasRole(assignRoleDTO.UserId, assignRoleDTO.RoleId))
            {
                return BadRequest("User already has the assigned role.");
            }

            try
            {
                await _userRoleRepository.AssignRoleToUser(assignRoleDTO.UserId, assignRoleDTO.RoleId);
                return Ok("Role assigned successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // Remove a role from a user
        [HttpDelete("{userRoleId}")]
        public async Task<IActionResult> RemoveUserRole(int userRoleId)
        {
            try
            {
                var result = await _userRoleRepository.DeleteRecord(userRoleId);
                if (!result)
                {
                    return NotFound("UserRole not found.");
                }

                return Ok("UserRole removed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("GetAllUserRoles")]
        public async Task<ActionResult<IEnumerable<UserRoleDTO>>> GetAllUserRoles()
        {
            var userRoles = await _userRoleRepository.ReadAllUserRoles();
            if (userRoles == null || !userRoles.Any())
            {
                return NotFound("No user roles found.");
            }

            var userRoleDTOs = userRoles.Select(ur => new UserRoleDTO
            {
                UserRoleID = ur.UserRoleID,
                UserID = ur.UserID,
                RoleID = ur.RoleID,
                RoleName = ur.Role.RoleName
            }).ToList();

            return Ok(userRoleDTOs);
        }
    }
}
