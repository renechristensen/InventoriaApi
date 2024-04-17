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
            var rackUnits = await _rackUnitRepository.GetAllRackUnitsByDataRackId(datarackID);

            var rackUnitDTOs = 
                // basically, check if the rack unit has any reservation. If it does, use those. If not, pretend one exists to simplify making this dto.This one was a brain twister
                // this list CAN IN THEORY include multiple reservedunits pointing at the same unit, do to the possibility of having multiple reservations at different times.
                rackUnits.SelectMany(ru =>
                (ru.ReservedRackUnits.Any() ? ru.ReservedRackUnits : new List<ReservedRackUnit> { new ReservedRackUnit { Reservation = new Reservation() } })
                .Select(rru => new RackUnitFlatDTO
                {
                    RackUnitID = ru.RackUnitID,
                    UnitNumber = ru.UnitNumber,
                    StartDate = rru.Reservation.StartDate?.ToString("yyyy-MM-dd"),
                    EndDate = rru.Reservation.EndDate?.ToString("yyyy-MM-dd"),
                    ReservationBackground = rru.Reservation.Background,
                    UserName = rru.Reservation.User?.Displayname,
                    UserEmail = rru.Reservation.User?.StudieEmail,
                    EquipmentName = ru.EquipmentRackUnits.FirstOrDefault()?.Equipment?.Name,
                    EquipmentModel = ru.EquipmentRackUnits.FirstOrDefault()?.Equipment?.Model,
                    EquipmentType = ru.EquipmentRackUnits.FirstOrDefault()?.Equipment?.Type
                }))
                .ToList();

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
