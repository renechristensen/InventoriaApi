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

namespace InventoriaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : Controller
    {
        private IUserRepository _userRepository;
        private IUserRoleRepository _userRoleRepository;
        private IConfiguration _config;
        public LoginController(IConfiguration config, IUserRepository userRepository, IUserRoleRepository userRoleRepository)
        {
            _config = config;
            _userRepository = userRepository;
            _userRoleRepository = userRoleRepository;
        }

        [HttpPost]
        public async Task<ActionResult<string>> Login(AuthenticateUserDTO authenticateUserDTO){
            
            // check if login was succesful
            if (!await _userRepository.VerifyLogin(authenticateUserDTO.StudieEmail, authenticateUserDTO.PasswordHash)) 
                return StatusCode(404, "Username or password is wrong");

            // note that User will never be null here
            User? user = _userRepository.ReadUserRecordByEmail(authenticateUserDTO.StudieEmail);

            var userRoles = await _userRoleRepository.ReadAllUserRolesByUserID(user.UserID);
            //setting up claims list
            List<Claim> claims = new()
            {
                new Claim(JwtRegisteredClaimNames.Sub, authenticateUserDTO.StudieEmail)
            };

            // Add role claims
            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole.Role.RoleName));
            }

            // This line is used for getting the key from the appsettings configuration file. It is defunct.
            //var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY")));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var Sectoken = new JwtSecurityToken(
              issuer: _config["Jwt:Issuer"],
              audience: _config["Jwt:Audience"],
              claims: claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(Sectoken);

            AuthenticatedUserSuccessDTO login = new AuthenticatedUserSuccessDTO()
            {
                UserID = user.UserID,
                Displayname = user.Displayname,
                StudieEmail = user.StudieEmail,
                CompanyID = user.CompanyID

            };
            var obj = new {login, token};
            return Ok(obj);
        }
    }
}