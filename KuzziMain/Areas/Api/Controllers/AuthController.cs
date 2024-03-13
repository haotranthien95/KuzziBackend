using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Kuzzi.DataAccess.Repository.IRepository;
using Kuzzi.Models.Auth;
using Kuzzi.Utility;
using KuzziMain.Areas.Api.Models.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

namespace KuzziMain.Areas.Api.Controllers
{
    [Area("Api")]
    [Route("[area]/v1/[controller]")]
    public class AuthController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IConfiguration _configuration;

        private readonly IUnitOfWork _unitOfWork;

        public AuthController(UserManager<ApplicationUser> userManager, IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _configuration = configuration;
            _unitOfWork = unitOfWork;

        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User already exists!" });

            ApplicationUser user = new ApplicationUser()
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.UserName,
                Name = model.UserName,
                PhoneNumber = "000000000",
                // Các trường khác từ model
            };
            //
            // _userManager.CreateAsync(new ApplicationUser
            // {
            //     UserName = "admin@gmail.com",
            //     Email = "admin@gmail.com",
            //     Name = "Admin",
            //     PhoneNumber = "000000000",
            // }, "Gaumoe@2").GetAwaiter().GetResult();
            // ApplicationUser user = _db.ApplicationUser.FirstOrDefault(u=>u.Email =="admin@gmail.com");
            // _userManager.AddToRoleAsync(user, SD.Role_Admin).GetAwaiter().GetResult();
            // }
            //




            var result = await _userManager.CreateAsync(user, model.Password);

            _userManager.AddToRoleAsync(user, SD.Role_User).GetAwaiter().GetResult();
            if (!result.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new { Status = "Error", Message = "User creation failed! Please check user details and try again." });

            return Ok(new { Status = "Success", Message = "User created successfully!" });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var role = await _userManager.GetRolesAsync(user);

                var token = GenerateJwtToken(user, role.FirstOrDefault(SD.Role_User));

                return Ok(new { Token = token, Message = "Success" });
            }
            return Unauthorized(new { Status = "Error", Message = "Invalid Authentication" });
        }

        // Phương thức tạo JWT token
        private string GenerateJwtToken(ApplicationUser user, string role)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.Id),
        new Claim(JwtRegisteredClaimNames.Sub, user.Email!),
        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        new Claim(ClaimTypes.Role, role)
    };

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}