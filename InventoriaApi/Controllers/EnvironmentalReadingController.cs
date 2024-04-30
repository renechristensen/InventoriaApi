using Microsoft.AspNetCore.Mvc;
using InventoriaApi.Services.RepositoryInterfaces;
using InventoriaApi.DTOs.ReceivedDTOs;
using InventoriaApi.DTOs.ResponseDTO;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using InventoriaApi.Models;

namespace InventoriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentalReadingController : ControllerBase
    {
        private readonly IEnvironmentalReadingRepository _environmentalReadingRepository;

        public EnvironmentalReadingController(IEnvironmentalReadingRepository environmentalReadingRepository)
        {
            _environmentalReadingRepository = environmentalReadingRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<EnvironmentalReadingDTO>>> GetAllEnvironmentalReadings()
        {
            var readings = await _environmentalReadingRepository.ReadAllRecords();
            return Ok(readings.Select(er => new EnvironmentalReadingDTO
            {
                EnvironmentalReadingID = er.EnvironmentalReadingID,
                Temperature = er.Temperature,
                Humidity = er.Humidity,
                ReadingTimestamp = er.ReadingTimestamp
            }));
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnvironmentalReading([FromBody] CreateEnvironmentalReadingDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Get the Danish datetime using the TimeUtil class
            var danishDatetime = TimeUtil.GetDanishDatetime();

            var environmentalReading = new EnvironmentalReading
            {
                Temperature = (float)dto.Temperature,
                Humidity = (float)dto.Humidity,
                ReadingTimestamp = danishDatetime, // Use Danish datetime
                EnvironmentalSettingsID = dto.EnvironmentalSettingsID 
            };

            await _environmentalReadingRepository.CreateRecord(environmentalReading);
            return CreatedAtAction(nameof(GetAllEnvironmentalReadings), new { id = environmentalReading.EnvironmentalReadingID }, environmentalReading);
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEnvironmentalReading(int id, [FromBody] UpdateEnvironmentalReadingDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var environmentalReading = await _environmentalReadingRepository.ReadRecordByID(id);
            if (environmentalReading == null)
            {
                return NotFound();
            }

            environmentalReading.Temperature = dto.Temperature ?? environmentalReading.Temperature;
            environmentalReading.Humidity = dto.Humidity ?? environmentalReading.Humidity;
            environmentalReading.ReadingTimestamp = dto.ReadingTimestamp ?? environmentalReading.ReadingTimestamp;

            await _environmentalReadingRepository.UpdateRecord(environmentalReading);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEnvironmentalReading(int id)
        {
            var environmentalReading = await _environmentalReadingRepository.ReadRecordByID(id);
            if (environmentalReading == null)
            {
                return NotFound();
            }

            await _environmentalReadingRepository.DeleteRecord(id);
            return NoContent();
        }
    }
}
