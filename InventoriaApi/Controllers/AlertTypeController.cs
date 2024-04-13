using Microsoft.AspNetCore.Mvc;
using InventoriaApi.Services.RepositoryInterfaces;
using InventoriaApi.DTOs.ReceivedDTOs;
using InventoriaApi.DTOs.ResponseDTO;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using InventoriaApi.Models;
using Microsoft.AspNetCore.Authorization;

namespace InventoriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AlertTypeController : ControllerBase
    {
        private readonly IAlertTypeRepository _alertTypeRepository;

        public AlertTypeController(IAlertTypeRepository alertTypeRepository)
        {
            _alertTypeRepository = alertTypeRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<AlertTypeDTO>>> GetAllAlertTypes()
        {
            var alertTypes = await _alertTypeRepository.ReadAllRecords();
            return Ok(alertTypes.Select(at => new AlertTypeDTO
            {
                AlertTypeID = at.AlertTypeID,
                TypeName = at.TypeName,
                Description = at.Description
            }));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateAlertType([FromBody] CreateAlertTypeDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var alertType = new AlertType
            {
                TypeName = dto.TypeName,
                Description = dto.Description
            };

            await _alertTypeRepository.CreateRecord(alertType);
            return CreatedAtAction(nameof(GetAllAlertTypes), new { id = alertType.AlertTypeID }, alertType);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateAlertType(int id, [FromBody] UpdateAlertTypeDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var alertType = await _alertTypeRepository.ReadRecordByID(id);
            if (alertType == null)
            {
                return NotFound();
            }

            alertType.TypeName = dto.TypeName ?? alertType.TypeName;
            alertType.Description = dto.Description ?? alertType.Description;

            await _alertTypeRepository.UpdateRecord(alertType);
            return Ok();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteAlertType(int id)
        {
            var alertType = await _alertTypeRepository.ReadRecordByID(id);
            if (alertType == null)
            {
                return NotFound();
            }

            await _alertTypeRepository.DeleteRecord(id);
            return NoContent();
        }
    }
}
