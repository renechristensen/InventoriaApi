using Microsoft.AspNetCore.Mvc;
using InventoriaApi.Services.RepositoryInterfaces;
using InventoriaApi.DTOs.ReceivedDTOs;
using InventoriaApi.DTOs.ResponseDTO;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InventoriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentalSettingController : ControllerBase
    {
        private readonly IEnvironmentalSettingRepository _environmentalSettingRepository;

        public EnvironmentalSettingController(IEnvironmentalSettingRepository environmentalSettingRepository)
        {
            _environmentalSettingRepository = environmentalSettingRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<EnvironmentalSettingDTO>> GetEnvironmentalSetting(int id)
        {
            var setting = await _environmentalSettingRepository.ReadRecordByID(id);
            if (setting == null)
            {
                return NotFound();
            }

            return new EnvironmentalSettingDTO
            {
                EnvironmentalSettingsID = setting.EnvironmentalSettingsID,
                ServerRoomID = setting.ServerRoomID,
                TemperatureUpperLimit = setting.TemperatureUpperLimit,
                TemperatureLowerLimit = setting.TemperatureLowerLimit,
                HumidityUpperLimit = setting.HumidityUpperLimit,
                HumidityLowerLimit = setting.HumidityLowerLimit,
                LatestChange = setting.LatestChange
            };
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnvironmentalSetting([FromBody] CreateEnvironmentalSettingDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newSetting = new EnvironmentalSetting
            {
                ServerRoomID = dto.ServerRoomID,
                TemperatureUpperLimit = dto.TemperatureUpperLimit,
                TemperatureLowerLimit = dto.TemperatureLowerLimit,
                HumidityUpperLimit = dto.HumidityUpperLimit,
                HumidityLowerLimit = dto.HumidityLowerLimit,
                LatestChange = DateTime.UtcNow
            };

            await _environmentalSettingRepository.CreateRecord(newSetting);
            return CreatedAtAction(nameof(GetEnvironmentalSetting), new { id = newSetting.EnvironmentalSettingsID }, newSetting);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEnvironmentalSetting(int id, [FromBody] UpdateEnvironmentalSettingDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var setting = await _environmentalSettingRepository.ReadRecordByID(id);
            if (setting == null)
            {
                return NotFound();
            }

            setting.TemperatureUpperLimit = dto.TemperatureUpperLimit ?? setting.TemperatureUpperLimit;
            setting.TemperatureLowerLimit = dto.TemperatureLowerLimit ?? setting.TemperatureLowerLimit;
            setting.HumidityUpperLimit = dto.HumidityUpperLimit ?? setting.HumidityUpperLimit;
            setting.HumidityLowerLimit = dto.HumidityLowerLimit ?? setting.HumidityLowerLimit;
            setting.LatestChange = DateTime.UtcNow;  // Automatically update on change

            await _environmentalSettingRepository.UpdateRecord(setting);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnvironmentalSetting(int id)
        {
            await _environmentalSettingRepository.DeleteRecord(id);
            return NoContent();
        }

        [HttpGet]
        public async Task<ActionResult<List<EnvironmentalSettingDTO>>> GetAllEnvironmentalSettings()
        {
            var settings = await _environmentalSettingRepository.ReadAllRecordsWithServerRoom();
            if (settings == null || !settings.Any())
            {
                return NotFound();
            }

            var settingsDto = settings.Select(setting => new EnvironmentalSettingDTO
            {
                EnvironmentalSettingsID = setting.EnvironmentalSettingsID,
                ServerRoomID = setting.ServerRoomID,
                ServerRoomName = setting.ServerRoom?.ServerRoomName,
                TemperatureUpperLimit = setting.TemperatureUpperLimit,
                TemperatureLowerLimit = setting.TemperatureLowerLimit,
                HumidityUpperLimit = setting.HumidityUpperLimit,
                HumidityLowerLimit = setting.HumidityLowerLimit,
                LatestChange = setting.LatestChange
            }).ToList();

            return settingsDto;
        }

    }
}
