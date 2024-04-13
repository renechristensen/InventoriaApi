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
    public class EquipmentRackUnitController : ControllerBase
    {
        private readonly IEquipmentRackUnitRepository _equipmentRackUnitRepository;

        public EquipmentRackUnitController(IEquipmentRackUnitRepository equipmentRackUnitRepository)
        {
            _equipmentRackUnitRepository = equipmentRackUnitRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentRackUnitDTO>> GetEquipmentRackUnit(int id)
        {
            var equipmentRackUnit = await _equipmentRackUnitRepository.ReadRecordByID(id);
            if (equipmentRackUnit == null)
            {
                return NotFound();
            }

            return new EquipmentRackUnitDTO
            {
                EquipmentRackUnitID = equipmentRackUnit.EquipmentRackUnitID,
                EquipmentID = equipmentRackUnit.EquipmentID,
                RackUnitID = equipmentRackUnit.RackUnitID
            };
        }

        [HttpPost]
        public async Task<IActionResult> CreateEquipmentRackUnit(CreateEquipmentRackUnitDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newEquipmentRackUnit = new EquipmentRackUnit
            {
                EquipmentID = dto.EquipmentID,
                RackUnitID = dto.RackUnitID
            };

            await _equipmentRackUnitRepository.CreateRecord(newEquipmentRackUnit);
            return CreatedAtAction(nameof(GetEquipmentRackUnit), new { id = newEquipmentRackUnit.EquipmentRackUnitID }, newEquipmentRackUnit);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEquipmentRackUnit(int id, UpdateEquipmentRackUnitDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var equipmentRackUnit = await _equipmentRackUnitRepository.ReadRecordByID(id);
            if (equipmentRackUnit == null)
            {
                return NotFound($"Equipment Rack Unit with ID {id} not found.");
            }

            equipmentRackUnit.EquipmentID = dto.EquipmentID;
            equipmentRackUnit.RackUnitID = dto.RackUnitID;

            await _equipmentRackUnitRepository.UpdateRecord(equipmentRackUnit);
            return Ok("Equipment Rack Unit updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipmentRackUnit(int id)
        {
            await _equipmentRackUnitRepository.DeleteRecord(id);
            return NoContent();
        }
    }
}
