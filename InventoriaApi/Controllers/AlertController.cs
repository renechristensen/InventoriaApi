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
    public class AlertController : ControllerBase
    {
        private readonly IAlertRepository _alertRepository;

        public AlertController(IAlertRepository alertRepository)
        {
            _alertRepository = alertRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<AlertResponseDTO>>> GetAllAlerts()
        {
            var alerts = await _alertRepository.ReadAllRecordsWithDetailsAsync();
            var alertDTOs = alerts.Select(a => new AlertResponseDTO
            {
                AlertID = a.AlertID,
                AlertTypeID = a.AlertTypeID,
                AlertTypeName = a.AlertType.TypeName,
                AlertTypeDescription = a.AlertType.Description,
                ThresholdExceeded = a.ThresholdExceeded,
                EnvironmentalReadingID = a.EnvironmentalReadingID,
                Temperature = a.EnvironmentalReading.Temperature,
                Humidity = a.EnvironmentalReading.Humidity,
                ReadingTimestamp = a.EnvironmentalReading.ReadingTimestamp,
                AlertTimestamp = a.AlertTimestamp,
                Resolved = a.Resolved,
                EnvironmentalSettingsID = a.EnvironmentalReading.EnvironmentalSettingsID,
                TemperatureUpperLimit = a.EnvironmentalReading.EnvironmentalSetting.TemperatureUpperLimit,
                TemperatureLowerLimit = a.EnvironmentalReading.EnvironmentalSetting.TemperatureLowerLimit,
                HumidityUpperLimit = a.EnvironmentalReading.EnvironmentalSetting.HumidityUpperLimit,
                HumidityLowerLimit = a.EnvironmentalReading.EnvironmentalSetting.HumidityLowerLimit,
                LatestChange = a.EnvironmentalReading.EnvironmentalSetting.LatestChange,
                ServerRoomID = a.EnvironmentalReading.EnvironmentalSetting.ServerRoom.ServerRoomID,
                ServerRoomName = a.EnvironmentalReading.EnvironmentalSetting.ServerRoom.ServerRoomName,
                RackCapacity = a.EnvironmentalReading.EnvironmentalSetting.ServerRoom.RackCapacity,
                StartupDate = a.EnvironmentalReading.EnvironmentalSetting.ServerRoom.StartupDate
            }).ToList();

            return Ok(alertDTOs);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateAlert(CreateAlertDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var envReadingExists = await _alertRepository.EnvironmentalReadingExists(dto.EnvironmentalReadingID);
            if (!envReadingExists)
            {
                return BadRequest("Invalid EnvironmentalReadingID: No corresponding environmental reading found.");
            }
            var alert = new Alert
            {
                AlertTypeID = dto.AlertTypeID,
                ThresholdExceeded = dto.ThresholdExceeded,
                EnvironmentalReadingID = dto.EnvironmentalReadingID,
                AlertTimestamp = dto.AlertTimestamp,
                Resolved = dto.Resolved
            };

            await _alertRepository.CreateRecord(alert);
            return Ok(new { message = "Alert created successfully." });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateAlert(int id, UpdateAlertDTO dto)
        {
            var alert = await _alertRepository.ReadRecordByID(id);
            if (alert == null)
            {
                return NotFound($"Alert with ID {id} not found.");
            }

            alert.ThresholdExceeded = dto.ThresholdExceeded ?? alert.ThresholdExceeded;
            alert.Resolved = dto.Resolved ?? alert.Resolved;

            await _alertRepository.UpdateRecord(alert);
            return Ok(new { message = "Alert updated successfully." });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> DeleteAlert(int id)
        {
            var alert = await _alertRepository.ReadRecordByID(id);
            if (alert == null)
            {
                return NotFound($"Alert with ID {id} not found.");
            }

            await _alertRepository.DeleteRecord(id);
            return Ok(new { message = "Alert deleted successfully." });
        }
    }
}
