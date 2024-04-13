using Microsoft.AspNetCore.Mvc;
using InventoriaApi.Services.RepositoryInterfaces;
using InventoriaApi.DTOs.ReceivedDTOs;
using InventoriaApi.DTOs.ResponseDTO;
using System.Threading.Tasks;

namespace InventoriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserChangeLogController : ControllerBase
    {
        private readonly IUserChangeLogRepository _userChangeLogRepository;

        public UserChangeLogController(IUserChangeLogRepository userChangeLogRepository)
        {
            _userChangeLogRepository = userChangeLogRepository;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserChangeLogDTO>> GetUserChangeLog(int id)
        {
            var changeLog = await _userChangeLogRepository.ReadRecordByID(id);
            if (changeLog == null)
            {
                return NotFound();
            }

            return new UserChangeLogDTO
            {
                UserChangeLogID = changeLog.UserChangeLogID,
                UserID = changeLog.UserID,
                ChangedByUserID = changeLog.ChangedByUserID,
                ChangeType = changeLog.ChangeType,
                ChangeTimestamp = changeLog.ChangeTimestamp,
                ChangeDescription = changeLog.ChangeDescription
            };
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserChangeLogDTO>>> GetAllUserChangeLogs()
        {
            var changeLogs = await _userChangeLogRepository.ReadAllRecords();
            var changeLogDTOs = changeLogs.Select(cl => new UserChangeLogDTO
            {
                UserChangeLogID = cl.UserChangeLogID,
                UserID = cl.UserID,
                ChangedByUserID = cl.ChangedByUserID,
                ChangeType = cl.ChangeType,
                ChangeTimestamp = cl.ChangeTimestamp,
                ChangeDescription = cl.ChangeDescription
            }).ToList();

            return Ok(changeLogDTOs);
        }
    }
}
