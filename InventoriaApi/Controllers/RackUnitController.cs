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
    public class RackUnitController : ControllerBase
    {
        private readonly IRackUnitRepository _rackUnitRepository;

        public RackUnitController(IRackUnitRepository rackUnitRepository)
        {
            _rackUnitRepository = rackUnitRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<RackUnitDTO>> GetRackUnit(int id)
        {
            var rackUnit = await _rackUnitRepository.ReadRecordByID(id);
            if (rackUnit == null)
            {
                return NotFound();
            }

            return new RackUnitDTO
            {
                RackUnitID = rackUnit.RackUnitID,
                DataRackID = rackUnit.DataRackID,
                UnitNumber = rackUnit.UnitNumber
            };
        }

        [HttpPost]
        public async Task<IActionResult> CreateRackUnit(CreateRackUnitDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newRackUnit = new RackUnit
            {
                DataRackID = dto.DataRackID,
                UnitNumber = dto.UnitNumber
            };

            await _rackUnitRepository.CreateRecord(newRackUnit);
            return CreatedAtAction(nameof(GetRackUnit), new { id = newRackUnit.RackUnitID }, newRackUnit);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRackUnit(int id, UpdateRackUnitDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var rackUnit = await _rackUnitRepository.ReadRecordByID(id);
            if (rackUnit == null)
            {
                return NotFound($"Rack Unit with ID {id} not found.");
            }

            rackUnit.DataRackID = dto.DataRackID;
            rackUnit.UnitNumber = dto.UnitNumber;

            await _rackUnitRepository.UpdateRecord(rackUnit);
            return Ok("Rack Unit updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRackUnit(int id)
        {
            await _rackUnitRepository.DeleteRecord(id);
            return NoContent();
        }
    }
}
