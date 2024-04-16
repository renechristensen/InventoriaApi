using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
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
    public class ServerRoomController : ControllerBase
    {
        private readonly IServerRoomRepository _serverRoomRepository;

        public ServerRoomController(IServerRoomRepository serverRoomRepository)
        {
            _serverRoomRepository = serverRoomRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<ServerRoomResponseDTO>>> GetAllServerRooms()
        {
            try
            {
                var serverRooms = await _serverRoomRepository.ReadAllRecordsWithDetailsAsync();
                var serverRoomDTOs = serverRooms.Select(sr => new ServerRoomResponseDTO
                {
                    ServerRoomID = sr.ServerRoomID,
                    ServerRoomName = sr.ServerRoomName,
                    RackCapacity = sr.RackCapacity,
                    StartupDate = sr.StartupDate,
                    DataCenterID = sr.DataCenterID,
                    DataCenterName = sr.DataCenter?.Name
                }).ToList();

                return Ok(serverRoomDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving server rooms: " + ex.Message);
            }
        }

        [HttpPost("CreateServerRoom")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateServerRoom(CreateServerRoomDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _serverRoomRepository.CreateRecord(new ServerRoom
                {
                    DataCenterID = dto.DataCenterID,
                    ServerRoomName = dto.ServerRoomName,
                    RackCapacity = dto.RackCapacity,
                    StartupDate = dto.StartupDate
                });

                return Ok("Server room created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the server room: " + ex.Message);
            }
        }

        [HttpPut("UpdateServerRoom/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateServerRoom(int id, UpdateServerRoomDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var serverRoom = await _serverRoomRepository.ReadRecordByID(id);
            if (serverRoom == null)
            {
                return NotFound($"Server room with ID {id} not found.");
            }

            // Updating properties
            serverRoom.ServerRoomName = dto.ServerRoomName ?? serverRoom.ServerRoomName;
            serverRoom.RackCapacity = dto.RackCapacity > 0 ? dto.RackCapacity : serverRoom.RackCapacity;
            serverRoom.StartupDate = dto.StartupDate ?? serverRoom.StartupDate;

            try
            {
                await _serverRoomRepository.UpdateRecord(serverRoom);
                return Ok("Server room updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the server room: " + ex.Message);
            }
        }

        [HttpDelete("DeleteServerRoom/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteServerRoom(int id)
        {
            try
            {
                await _serverRoomRepository.DeleteRecord(id);
                return Ok("Server room deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the server room: " + ex.Message);
            }
        }
    }
}
