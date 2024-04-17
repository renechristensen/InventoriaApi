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
    public class ReservationController : ControllerBase
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationController(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDTO>> GetReservation(int id)
        {
            var reservation = await _reservationRepository.ReadRecordByID(id);
            if (reservation == null)
            {
                return NotFound();
            }

            return new ReservationDTO
            {
                ReservationID = reservation.ReservationID,
                UserID = reservation.UserID,
                StartDate = (DateTime)reservation.StartDate,
                EndDate = (DateTime)reservation.EndDate,
                Background = reservation.Background
            };
        }

        [HttpPost]
        public async Task<IActionResult> CreateReservation(CreateReservationDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newReservation = new Reservation
            {
                UserID = dto.UserID,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Background = dto.Background
            };

            await _reservationRepository.CreateRecord(newReservation);
            return CreatedAtAction(nameof(GetReservation), new { id = newReservation.ReservationID }, newReservation);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateReservation(int id, UpdateReservationDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var reservation = await _reservationRepository.ReadRecordByID(id);
            if (reservation == null)
            {
                return NotFound($"Reservation with ID {id} not found.");
            }

            reservation.UserID = dto.UserID;
            reservation.StartDate = dto.StartDate;
            reservation.EndDate = dto.EndDate;
            reservation.Background = dto.Background;

            await _reservationRepository.UpdateRecord(reservation);
            return Ok("Reservation updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            await _reservationRepository.DeleteRecord(id);
            return NoContent();
        }
    }
}
