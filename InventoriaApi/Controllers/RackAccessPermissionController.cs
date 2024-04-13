using Microsoft.AspNetCore.Mvc;
using InventoriaApi.Services.RepositoryInterfaces;
using InventoriaApi.DTOs.ReceivedDTOs;
using InventoriaApi.DTOs.ResponseDTO;
using System;
using System.Linq;
using System.Threading.Tasks;
using InventoriaApi.Models;

namespace InventoriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RackAccessPermissionController : ControllerBase
    {
        private readonly IRackAccessPermissionRepository _rackAccessPermissionRepository;

        public RackAccessPermissionController(IRackAccessPermissionRepository rackAccessPermissionRepository)
        {
            _rackAccessPermissionRepository = rackAccessPermissionRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RackAccessPermissionDTO>> GetRackAccessPermission(int id)
        {
            var rackAccessPermission = await _rackAccessPermissionRepository.ReadRecordByID(id);
            if (rackAccessPermission == null)
            {
                return NotFound();
            }

            return new RackAccessPermissionDTO
            {
                RackAccessPermissionID = rackAccessPermission.RackAccessPermissionID,
                DataRackID = rackAccessPermission.DataRackID,
                RoleID = rackAccessPermission.RoleID
            };
        }

        [HttpPost]
        public async Task<IActionResult> CreateRackAccessPermission(CreateRackAccessPermissionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newRackAccessPermission = new RackAccessPermission
            {
                DataRackID = dto.DataRackID,
                RoleID = dto.RoleID
            };

            await _rackAccessPermissionRepository.CreateRecord(newRackAccessPermission);
            return CreatedAtAction(nameof(GetRackAccessPermission), new { id = newRackAccessPermission.RackAccessPermissionID }, newRackAccessPermission);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRackAccessPermission(int id, UpdateRackAccessPermissionDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rackAccessPermission = await _rackAccessPermissionRepository.ReadRecordByID(id);
            if (rackAccessPermission == null)
            {
                return NotFound($"Rack Access Permission with ID {id} not found.");
            }

            rackAccessPermission.DataRackID = dto.DataRackID;
            rackAccessPermission.RoleID = dto.RoleID;

            await _rackAccessPermissionRepository.UpdateRecord(rackAccessPermission);
            return Ok("Rack Access Permission updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRackAccessPermission(int id)
        {
            await _rackAccessPermissionRepository.DeleteRecord(id);
            return NoContent();
        }
    }
}
