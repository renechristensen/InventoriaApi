using Microsoft.AspNetCore.Mvc;
using InventoriaApi.Services.RepositoryInterfaces;
using InventoriaApi.DTOs.ReceivedDTOs;
using InventoriaApi.DTOs.ResponseDTO;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using InventoriaApi.Models;

namespace InventoriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataCenterController : ControllerBase
    {
        private readonly IDataCenterRepository _dataCenterRepository;

        public DataCenterController(IDataCenterRepository dataCenterRepository)
        {
            _dataCenterRepository = dataCenterRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<DataCenterResponseDTO>>> GetAllDataCenters()
        {
            var dataCenters = await _dataCenterRepository.ReadAllRecordsWithDetailsAsync();
            var dataCenterDTOs = dataCenters.Select(dc => new DataCenterResponseDTO
            {
                DataCenterID = dc.DataCenterID,
                Name = dc.Name,
                Address = dc.Address,
                Description = dc.Description,
                CompanyID = dc.CompanyID,
                CompanyName = dc.Company.Name
            }).ToList();

            return Ok(dataCenterDTOs);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateDataCenter(CreateDataCenterDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataCenter = new DataCenter
            {
                Name = dto.Name,
                Address = dto.Address,
                Description = dto.Description,
                CompanyID = dto.CompanyID
            };

            await _dataCenterRepository.CreateRecord(dataCenter);
            return Ok(new { message = "Data center created successfully." });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateDataCenter(int id, UpdateDataCenterDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var dataCenter = await _dataCenterRepository.ReadRecordByID(id);
            if (dataCenter == null)
            {
                return NotFound($"Data center with ID {id} not found.");
            }

            dataCenter.Name = dto.Name ?? dataCenter.Name;
            dataCenter.Address = dto.Address ?? dataCenter.Address;
            dataCenter.Description = dto.Description ?? dataCenter.Description;

            await _dataCenterRepository.UpdateRecord(dataCenter);
            return Ok(new { message = "Data center updated successfully." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteDataCenter(int id)
        {
            var dataCenter = await _dataCenterRepository.ReadRecordByID(id);
            if (dataCenter == null)
            {
                return NotFound($"Data center with ID {id} not found.");
            }

            await _dataCenterRepository.DeleteRecord(id);
            return Ok(new { message = "Data center deleted successfully." });
        }
    }
}

