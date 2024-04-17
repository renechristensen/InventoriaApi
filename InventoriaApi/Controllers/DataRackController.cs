using Microsoft.AspNetCore.Mvc;
using InventoriaApi.Services.RepositoryInterfaces;
using InventoriaApi.DTOs.ReceivedDTOs;
using System;
using System.Linq;
using System.Threading.Tasks;
using InventoriaApi.Models;
using Microsoft.AspNetCore.Authorization;
using InventoriaApi.DTOs.ResponseDTO;

namespace InventoriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataRackController : ControllerBase
    {
        private readonly IDataRackRepository _dataRackRepository;

        public DataRackController(IDataRackRepository dataRackRepository)
        {
            _dataRackRepository = dataRackRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<DataRackTableRecordsDTO>>> GetAllDataRackTableRecords()
        {
            try
            {
                var dataRacks = await _dataRackRepository.ReadAllRecordsWithDetailsAsync();
                var dataRackDTOs = dataRacks.Select(dr => new DataRackTableRecordsDTO
                {
                    DataRackID = dr.DataRackID,
                    ServerRoomName = dr.ServerRoom?.ServerRoomName,
                    RackStartupDate = dr.CreationDate,
                    RackStatus = dr.Status,
                    TotalUnits = dr.TotalUnits,
                    AvailableUnits = dr.AvailableUnits,
                    DataCenterName = dr.ServerRoom?.DataCenter?.Name,
                    CompanyName = dr.ServerRoom?.DataCenter?.Company?.Name,
                    DatarackName = dr.datarackName,
                    RackPlacement = dr.RackPlacement,
                    Roles = dr.RackAccessPermissions.Select(rap => rap.Role.RoleName).ToList()
                }).ToList();

                return Ok(dataRackDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving data racks: " + ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DataRackTableRecordsDTO>> GetDataRackById(int id)
        {
            try
            {
                var dataRack = await _dataRackRepository.ReadDataRackRecordByID(id);
                if (dataRack == null)
                {
                    return NotFound($"Data rack with ID {id} not found.");
                }

                var dataRackDTO = new DataRackTableRecordsDTO
                {
                    DataRackID = dataRack.DataRackID,
                    ServerRoomName = dataRack.ServerRoom?.ServerRoomName,
                    RackStartupDate = dataRack.CreationDate,
                    RackStatus = dataRack.Status,
                    TotalUnits = dataRack.TotalUnits,
                    AvailableUnits = dataRack.AvailableUnits,
                    DataCenterName = dataRack.ServerRoom?.DataCenter?.Name,
                    CompanyName = dataRack.ServerRoom?.DataCenter?.Company?.Name,
                    DatarackName = dataRack.datarackName,
                    RackPlacement = dataRack.RackPlacement,
                    Roles = dataRack.RackAccessPermissions.Select(rap => rap.Role.RoleName).ToList()
                };

                return Ok(dataRackDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while retrieving the data rack: " + ex.Message);
            }
        }



        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateDataRack(CreateDataRackDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                await _dataRackRepository.CreateRecord(new DataRack
                {
                    datarackName =dto.datarackName,
                    ServerRoomID = dto.ServerRoomID,
                    RackPlacement = dto.RackPlacement,
                    TotalUnits = dto.TotalUnits,
                    AvailableUnits = dto.AvailableUnits,
                    Status = dto.Status,
                    CreationDate = DateTime.UtcNow // Assuming you're setting the creation date to the current time
                });

                return Ok("Data rack created successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while creating the data rack: " + ex.Message);
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateDataRack(int id, UpdateDataRackDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataRack = await _dataRackRepository.ReadRecordByID(id);
            if (dataRack == null)
            {
                return NotFound($"Data rack with ID {id} not found.");
            }

            // Updating properties
            dataRack.datarackName = dto.datarackName ?? dataRack.datarackName;
            dataRack.RackPlacement = dto.RackPlacement ?? dataRack.RackPlacement;
            dataRack.TotalUnits = dto.TotalUnits > 0 ? dto.TotalUnits : dataRack.TotalUnits;
            dataRack.AvailableUnits = dto.AvailableUnits >= 0 ? dto.AvailableUnits : dataRack.AvailableUnits;
            dataRack.Status = dto.Status ?? dataRack.Status;

            try
            {
                await _dataRackRepository.UpdateRecord(dataRack);
                return Ok("Data rack updated successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while updating the data rack: " + ex.Message);
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteDataRack(int id)
        {
            try
            {
                await _dataRackRepository.DeleteRecord(id);
                return Ok("Data rack deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the data rack: " + ex.Message);
            }
        }
    }
}
