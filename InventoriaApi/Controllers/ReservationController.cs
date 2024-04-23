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
        [HttpGet("user/{userId}")]
        public async Task<ActionResult<List<ReservationDTO>>> GetReservationsByUser(int userId)
        {
            var reservations = await _reservationRepository.GetReservationsByUserId(userId);
            if (reservations == null || !reservations.Any())
            {
                return NotFound($"No reservations found for user with ID {userId}.");
            }

            var reservationDtos = reservations.Select(reservation => new ReservationDTO
            {
                ReservationID = reservation.ReservationID,
                UserID = reservation.UserID,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                Background = reservation.Background,
                ReservedRackUnitIDs = reservation.ReservedRackUnits.Select(ru => ru.RackUnitID).ToList()
            }).ToList();

            return Ok(reservationDtos);
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

            // Check that the end date is strictly after the start date
            if (dto.EndDate <= dto.StartDate)
            {
                return BadRequest("The end date must be after the start date, and reservations on the same day are not allowed.");
            }

            // Normalize dates to remove time part if necessary
            DateTime startDate = dto.StartDate.Date;
            DateTime endDate = dto.EndDate.Date;

            // Check if the rack units are available during the given dates
            bool isAvailable = await _reservationRepository.IsRackUnitAvailable(dto.RackUnitIDs, startDate, endDate);
            if (!isAvailable)
            {
                return BadRequest("One or more rack units are not available for the specified dates.");
            }

            var newReservation = new Reservation
            {
                UserID = dto.UserID,
                StartDate = startDate,
                EndDate = endDate,
                Background = dto.Background,
                ReservedRackUnits = dto.RackUnitIDs.Select(id => new ReservedRackUnit { RackUnitID = id }).ToList()
            };

            await _reservationRepository.CreateRecord(newReservation);

            var reservationDto = new ReservationDTO
            {
                ReservationID = newReservation.ReservationID,
                UserID = newReservation.UserID,
                StartDate = startDate,
                EndDate = endDate,
                Background = newReservation.Background,
                ReservedRackUnitIDs = newReservation.ReservedRackUnits.Select(ru => ru.RackUnitID).ToList()
            };

            return CreatedAtAction(nameof(GetReservation), new { id = newReservation.ReservationID }, reservationDto);
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