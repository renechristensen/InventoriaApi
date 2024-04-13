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
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyController(ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<IEnumerable<CompanyResponseDTO>>> GetAllCompanies()
        {
            var companies = await _companyRepository.ReadAllRecordsWithDetailsAsync();
            var companyDTOs = companies.Select(c => new CompanyResponseDTO
            {
                CompanyID = c.CompanyID,
                Name = c.Name,
                Description = c.Description
            }).ToList();

            return Ok(companyDTOs);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> CreateCompany(CreateCompanyDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var company = new Company
            {
                Name = dto.Name,
                Description = dto.Description
            };

            await _companyRepository.CreateRecord(company);
            return Ok(new { message = "Company created successfully" });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> UpdateCompany(int id, UpdateCompanyDTO dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var company = await _companyRepository.ReadRecordByID(id);
            if (company == null)
            {
                return NotFound($"Company with ID {id} not found.");
            }

            company.Name = dto.Name ?? company.Name;
            company.Description = dto.Description ?? company.Description;

            await _companyRepository.UpdateRecord(company);
            return Ok(new { message = "Company updated successfully" });
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                await _companyRepository.DeleteRecord(id);
                return Ok("Company deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An error occurred while deleting the company: " + ex.Message);
            }
        }
    }
}

