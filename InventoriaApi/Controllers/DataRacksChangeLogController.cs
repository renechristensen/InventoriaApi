using Microsoft.AspNetCore.Mvc;
using InventoriaApi.Services.RepositoryInterfaces;
using InventoriaApi.DTOs.ReceivedDTOs;
using InventoriaApi.DTOs.ResponseDTO;
using System;
using System.Linq;
using System.Threading.Tasks;
using InventoriaApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace InventoriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataRacksChangeLogController : ControllerBase
    {
        private readonly IDataRacksChangeLogRepository _dataRacksChangeLogRepository;

        public DataRacksChangeLogController(IDataRacksChangeLogRepository dataRacksChangeLogRepository)
        {
            _dataRacksChangeLogRepository = dataRacksChangeLogRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<DataRacksChangeLogDTO>>> GetAllChangeLogs()
        {
            var logs = await _dataRacksChangeLogRepository.ReadAllRecords();
            var logsDto = logs.Select(log => new DataRacksChangeLogDTO
            {
                DataRacksChangeLogID = log.DataRacksChangeLogID,
                DataRackID = log.DataRackID,
                ChangedByUserID = log.ChangedByUserID,
                ChangeType = log.ChangeType,
                ChangeTimestamp = log.ChangeTimestamp,
                ChangeDescription = log.ChangeDescription
            }).ToList();

            return Ok(logsDto);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateChangeLog(CreateDataRacksChangeLogDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newLog = new DataRacksChangeLog
            {
                DataRackID = dto.DataRackID,
                ChangedByUserID = dto.ChangedByUserID,
                ChangeType = dto.ChangeType,
                ChangeTimestamp = DateTime.UtcNow, // or use DTO if client sends timestamp
                ChangeDescription = dto.ChangeDescription
            };

            await _dataRacksChangeLogRepository.CreateRecord(newLog);
            return Ok(new { Message = "Change log created successfully." });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateChangeLog(int id, UpdateDataRacksChangeLogDTO dto)
        {
            var log = await _dataRacksChangeLogRepository.ReadRecordByID(id);
            if (log == null)
            {
                return NotFound($"Change log with ID {id} not found.");
            }

            log.ChangeType = dto.ChangeType ?? log.ChangeType;
            log.ChangeDescription = dto.ChangeDescription ?? log.ChangeDescription;
            await _dataRacksChangeLogRepository.UpdateRecord(log);

            return Ok(new { Message = "Change log updated successfully." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteChangeLog(int id)
        {
            await _dataRacksChangeLogRepository.DeleteRecord(id);
            return Ok(new { Message = "Change log deleted successfully." });
        }
    }
}
