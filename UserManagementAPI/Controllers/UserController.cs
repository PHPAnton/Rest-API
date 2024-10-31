using Microsoft.AspNetCore.Mvc;
using UserManagementAPI.Data;
using UserManagementAPI.Models;
using UserManagementAPI.Services;

namespace UserManagementAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet("users")]
        public IActionResult GetUsers()
        {
            var users = _context.Users.ToList();
            return Ok(users);
        }

        [HttpGet("users/{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPost("users")]
        public IActionResult CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("users/{id}")]
        public IActionResult UpdateUser(int id, User updatedUser)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            user.Username = updatedUser.Username;
            user.Password = updatedUser.Password;
            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("users/{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users.Find(id);
            if (user == null) return NotFound();
            _context.Users.Remove(user);
            _context.SaveChanges();
            return NoContent();
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] int userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null) return NotFound("User not found");

            AuthService.Login(userId);
            user.IsOnline = true;
            _context.SaveChanges();
            return Ok("Logged in");
        }

        [HttpPost("logout")]
        public IActionResult Logout([FromBody] int userId)
        {
            var user = _context.Users.Find(userId);
            if (user == null) return NotFound("User not found");

            AuthService.Logout(userId);
            user.IsOnline = false;
            _context.SaveChanges();
            return Ok("Logged out");
        }

    }
}
