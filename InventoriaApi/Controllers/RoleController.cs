using Microsoft.AspNetCore.Mvc;
using InventoriaApi.Services.RepositoryInterfaces;
using InventoriaApi.DTOs.ReceivedDTOs;
using InventoriaApi.DTOs.ResponseDTO;
using System.Threading.Tasks;
using InventoriaApi.Models;

namespace InventoriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;

        public RoleController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RoleDTO>> GetRole(int id)
        {
            var role = await _roleRepository.ReadRecordByID(id);
            if (role == null)
            {
                return NotFound();
            }

            return new RoleDTO
            {
                RoleID = role.RoleID,
                RoleName = role.RoleName
            };
        }

        [HttpPost]
        public async Task<ActionResult> CreateRole(CreateRoleDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newRole = new Role
            {
                RoleName = dto.RoleName
            };

            await _roleRepository.CreateRecord(newRole);
            return CreatedAtAction(nameof(GetRole), new { id = newRole.RoleID }, newRole);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRole(int id, UpdateRoleDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var role = await _roleRepository.ReadRecordByID(id);
            if (role == null)
            {
                return NotFound($"Role with ID {id} not found.");
            }

            role.RoleName = dto.RoleName ?? role.RoleName;

            await _roleRepository.UpdateRecord(role);
            return Ok("Role updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRole(int id)
        {
            await _roleRepository.DeleteRecord(id);
            return NoContent();
        }

        [HttpGet("GetAllRoles")]
        public async Task<ActionResult<IEnumerable<UserRoleDTO>>> GetAllUserRoles()
        {
            var roles = await _roleRepository.ReadAllRecords();
            if (roles == null || !roles.Any())
            {
                return NotFound("No roles found.");
            }

            var roleDTOs = roles.Select(r => new RoleDTO
            {
                RoleID = r.RoleID,
                RoleName = r.RoleName
            }).ToList();

            return Ok(roleDTOs);
        }
    }
}
