using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using API.Data;
using Microsoft.AspNetCore.Authorization;
using API.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;

        public AuthController(DataContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterModel register)
        {
            if (await _context.Users.AnyAsync(u => u.Username == register.Username))
            {
                return BadRequest("Username already exists");
            }

            CreatePasswordHash(register.Password, out byte[] passwordHash, out byte[] passwordSalt);

            var user = new User
            {
                Username = register.Username,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Role = register.Role
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return Ok(user);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel login)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == login.Username);

            if (user == null || !VerifyPasswordHash(login.Password, user.PasswordHash, user.PasswordSalt))
            {
                return Unauthorized("Invalid username or password");
            }

            return Ok(new
            {
                message = "Login successful",
                user = new
                {
                    user.Username,
                    user.Role // Include any other user info you want to return
                }
            });
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512();
            passwordSalt = hmac.Key;
            passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using var hmac = new HMACSHA512(passwordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return computedHash.SequenceEqual(passwordHash);
        }
    }
}
