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

 
        [HttpGet("{datarackID}")]
        public async Task<ActionResult<IEnumerable<RackUnitFlatDTO>>> GetRackUnitsByDataRack(int datarackID)
        {
            var today = DateTime.Today;
            var rackUnits = await _rackUnitRepository.GetAllRackUnitsByDataRackId(datarackID);

            var rackUnitDTOs = rackUnits.Select(ru =>
            {
                // Check for any reservations active today
                var currentReservation = ru.ReservedRackUnits
                    .FirstOrDefault(rru => rru.Reservation.StartDate <= today && rru.Reservation.EndDate >= today);

                return new RackUnitFlatDTO
                {
                    RackUnitID = ru.RackUnitID,
                    UnitNumber = ru.UnitNumber,
                    StartDate = currentReservation?.Reservation.StartDate.ToString("yyyy-MM-dd"),
                    EndDate = currentReservation?.Reservation.EndDate.ToString("yyyy-MM-dd"),
                    ReservationBackground = currentReservation?.Reservation.Background,
                    DisplayName = currentReservation?.Reservation.User?.Displayname,
                    StudieEmail = currentReservation?.Reservation.User?.StudieEmail,
                    EquipmentName = currentReservation != null ? ru.EquipmentRackUnits.FirstOrDefault()?.Equipment?.Name : null,
                    EquipmentModel = currentReservation != null ? ru.EquipmentRackUnits.FirstOrDefault()?.Equipment?.Model : null,
                    EquipmentType = currentReservation != null ? ru.EquipmentRackUnits.FirstOrDefault()?.Equipment?.Type : null
                };
            }).ToList();

            return Ok(rackUnitDTOs);
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
            return Ok("Rack unit successfully created");
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
