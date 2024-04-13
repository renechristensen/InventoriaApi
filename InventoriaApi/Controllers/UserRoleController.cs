using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using InventoriaApi.Services.RepositoryInterfaces;
using InventoriaApi.Services.Repositories;
using InventoriaApi.Models;
using InventoriaApi.DTOs.ReceivedDTOs;
using InventoriaApi.DTOs.ResponseDTO;
using System.Security.Cryptography;

namespace InventoriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : Controller
    {
        private IUserRepository _userRepository;
        private IUserRoleRepository _userRoleRepository;
        private IRoleRepository _roleRepository;
        private IConfiguration _config;
        public UserRoleController(IConfiguration config, IUserRepository userRepository, IUserRoleRepository userRoleRepository, IRoleRepository roleRepository)
        {
            _config = config;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
            _roleRepository = roleRepository;
        }

        [HttpPost("AssignUserRole")]
        public async Task<IActionResult> AssignUserRole([FromBody] AssignUserRoleDTO assignRoleDTO)
        {

            // Check if the user exists 
            var user = await _userRepository.ReadRecordByID(assignRoleDTO.UserId);
            if (user == null)
            {
                return NotFound($"User with ID {assignRoleDTO.UserId} not found.");
            }

            //check that the role exists
            var roleExists = await _roleRepository.ReadRecordToVerify(assignRoleDTO.RoleId);
            if (!roleExists)
            {
                return NotFound($"Role with ID {assignRoleDTO.RoleId} not found.");
            }

            //check if role is already assigned
            var roleAlreadyAssigned = await _userRoleRepository.UserHasRole(user.UserID, assignRoleDTO.RoleId);
            if (roleAlreadyAssigned)
            {
                return BadRequest($"User already has the role.");
            }

            // Assign the role to the user
            try
            {
                await _userRoleRepository.AssignRoleToUser(user.UserID, assignRoleDTO.RoleId);
                return Ok($"Role with ID {assignRoleDTO.RoleId} successfully assigned to user {user.Displayname}.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while assigning the role: {ex.Message}");
            }
        }
    }
}
