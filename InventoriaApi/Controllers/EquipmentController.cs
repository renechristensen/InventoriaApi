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
    public class EquipmentController : ControllerBase
    {
        private readonly IEquipmentRepository _equipmentRepository;

        public EquipmentController(IEquipmentRepository equipmentRepository)
        {
            _equipmentRepository = equipmentRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EquipmentDTO>> GetEquipment(int id)
        {
            var equipment = await _equipmentRepository.ReadRecordByID(id);
            if (equipment == null)
            {
                return NotFound();
            }

            return new EquipmentDTO
            {
                EquipmentID = equipment.EquipmentID,
                Name = equipment.Name,
                Model = equipment.Model,
                Type = equipment.Type
            };
        }

        [HttpPost]
        public async Task<IActionResult> CreateEquipment(CreateEquipmentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool areUnitsAvailable = await _equipmentRepository.AreRackUnitsAvailable(dto.RackUnitIDs);
            if (!areUnitsAvailable)
            {
                return BadRequest("One or more rack units are not available.");
            }

            var newEquipment = new Equipment
            {
                Name = dto.Name,
                Model = dto.Model,
                Type = dto.Type,
                EquipmentRackUnits = dto.RackUnitIDs.Select(id => new EquipmentRackUnit { RackUnitID = id }).ToList()
            };

            await _equipmentRepository.CreateRecord(newEquipment);
            var equipmentDto = new EquipmentDTO
            {
                EquipmentID = newEquipment.EquipmentID,
                Name = newEquipment.Name,
                Model = newEquipment.Model,
                Type = newEquipment.Type,
                RackUnitIDs = newEquipment.EquipmentRackUnits.Select(e => e.RackUnitID).ToList()
            };

            return CreatedAtAction(nameof(GetEquipment), new { id = newEquipment.EquipmentID }, equipmentDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEquipment(int id, UpdateEquipmentDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var equipment = await _equipmentRepository.ReadRecordByID(id);
            if (equipment == null)
            {
                return NotFound($"Equipment with ID {id} not found.");
            }

            equipment.Name = dto.Name ?? equipment.Name;
            equipment.Model = dto.Model ?? equipment.Model;
            equipment.Type = dto.Type ?? equipment.Type;

            await _equipmentRepository.UpdateRecord(equipment);
            return Ok("Equipment updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEquipment(int id)
        {
            await _equipmentRepository.DeleteRecord(id);
            return NoContent();
        }

        [HttpDelete("ByRackUnit/{rackUnitId}")]
        public async Task<IActionResult> DeleteEquipmentByRackUnitId(int rackUnitId)
        {
            bool deleted = await _equipmentRepository.DeleteEquipmentByRackUnitId(rackUnitId);
            if (deleted)
            {
                return Ok("Equipment deleted successfully.");
            }
            return NotFound("Equipment not found for the specified rack unit ID.");
        }
    }
}
