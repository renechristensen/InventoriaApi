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
    public class ReservedRackUnitController : ControllerBase
    {
        private readonly IReservedRackUnitRepository _reservedRackUnitRepository;

        public ReservedRackUnitController(IReservedRackUnitRepository reservedRackUnitRepository)
        {
            _reservedRackUnitRepository = reservedRackUnitRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservedRackUnitDTO>> GetReservedRackUnit(int id)
        {
            var reservedRackUnit = await _reservedRackUnitRepository.ReadRecordByID(id);
            if (reservedRackUnit == null)
            {
                return NotFound();
            }

            return new ReservedRackUnitDTO
            {
                ReservedRackUnitID = reservedRackUnit.ReservedRackUnitID,
                ReservationID = reservedRackUnit.ReservationID,
                RackUnitID = reservedRackUnit.RackUnitID
            };
        }

        [HttpPost]
        public async Task<ActionResult> CreateReservedRackUnit(CreateReservedRackUnitDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newReservedRackUnit = new ReservedRackUnit
            {
                ReservationID = dto.ReservationID,
                RackUnitID = dto.RackUnitID
            };

            await _reservedRackUnitRepository.CreateRecord(newReservedRackUnit);
            return CreatedAtAction(nameof(GetReservedRackUnit), new { id = newReservedRackUnit.ReservedRackUnitID }, newReservedRackUnit);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservedRackUnit(int id, UpdateReservedRackUnitDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reservedRackUnit = await _reservedRackUnitRepository.ReadRecordByID(id);
            if (reservedRackUnit == null)
            {
                return NotFound($"Reserved rack unit with ID {id} not found.");
            }

            reservedRackUnit.ReservationID = dto.ReservationID;
            reservedRackUnit.RackUnitID = dto.RackUnitID;

            await _reservedRackUnitRepository.UpdateRecord(reservedRackUnit);
            return Ok("Reserved rack unit updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservedRackUnit(int id)
        {
            await _reservedRackUnitRepository.DeleteRecord(id);
            return NoContent();
        }
    }
}
