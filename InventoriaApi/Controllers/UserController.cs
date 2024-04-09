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
using System.ComponentModel.Design;

namespace InventoriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private IUserRepository _userRepository;
        private IConfiguration _config;
        public UserController(IConfiguration config, IUserRepository userRepository)
        {
            _config = config;
            _userRepository = userRepository;
        }
        [HttpPost("CreateUser")]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserDTO createUserDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Check if user exists
            var existingUser = _userRepository.ReadUserRecordByEmail(createUserDTO.StudieEmail);
            //Check if company exists

            if (existingUser != null)
            {
                return StatusCode(409, new { message = "User already exists." });
            }

            // Generate password hash and salt
            using (var hmac = new HMACSHA512())
            {
                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(createUserDTO.Password));
                var passwordSalt = hmac.Key; 

                var user = new User
                {
                    Displayname = createUserDTO.Displayname,
                    StudieEmail = createUserDTO.StudieEmail,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                    CreationDate = DateTime.UtcNow,
                    LastLoginDate = DateTime.UtcNow,
                    CompanyID = createUserDTO.CompanyID
                };

                try
                {
                    await _userRepository.CreateRecord(user);
                }
                catch (Exception ex)
                {
                    // Generic error for handling server exceptions
                    return StatusCode(500, "An internal server error has occurred: " + ex.Message);
                }
            }

            return Ok(new { message = "User created successfully" });
        }
        [HttpPut("UpdateUser/{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserDTO updateUserDTO)
        {

            var user = await _userRepository.ReadRecordByID(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            // Update user properties
            user.Displayname = updateUserDTO.Displayname ?? user.Displayname;
            user.StudieEmail = updateUserDTO.StudieEmail ?? user.StudieEmail;

            // If the password needs to be updated
            if (!string.IsNullOrEmpty(updateUserDTO.Password))
            {

                using (var hmac = new HMACSHA512())
                {
                    user.PasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(updateUserDTO.Password));
                    user.PasswordSalt = hmac.Key;
                }
            }

            try
            {
                await _userRepository.UpdateRecord(user);
                return Ok(new { message = "User updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, "An internal server error has occurred: " + ex.Message);
            }
        }
        [HttpGet("GetUser/{id}")]
        public async Task<ActionResult<UserDTO>> GetUser(int id)
        {
            var user = await _userRepository.ReadUserRecordByUserID(id);
            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }
            List<string> roleNames = new();

            if (user.UserRoles == null)
            {
                roleNames = new List<string>();
            }
            else {
                roleNames = user.UserRoles.Select(ur => ur.Role.RoleName).ToList();
            }

            UserDTO userDto = new UserDTO
            {
                UserID = user.UserID,
                Displayname = user.Displayname,
                StudieEmail = user.StudieEmail,
                CreationDate = user.CreationDate,
                LastLoginDate = user.LastLoginDate,
                CompanyID = user.CompanyID,
                Roles = roleNames

            };

            return Ok(userDto);
        }
    }
}
