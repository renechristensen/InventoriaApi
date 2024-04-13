using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using InventoriaApi.Services.RepositoryInterfaces;
using InventoriaApi.Services.Repositories;
using InventoriaApi.Models;
using InventoriaApi.DTOs.ReceivedDTOs;
using InventoriaApi.DTOs.ResponseDTO;
using System.Security.Cryptography;

namespace InventoriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataRackController : Controller
    {
        private IDataRackRepository _dataRackRepository;
        private IConfiguration _config;
        public DataRackController(IConfiguration config, IDataRackRepository dataRackRepository)
        {
            _config = config;
            _dataRackRepository = dataRackRepository;
        }

        /*
        public async Task<ActionResult<IEnumerable<DataRackTableRecordsDTO>>> GetAllDataRackTableRecords()
        {
            var dataRackDTOs = new List<DataRackTableRecordsDTO>();

            return Ok(dataRackDTOs);
        }*/
    }
}

