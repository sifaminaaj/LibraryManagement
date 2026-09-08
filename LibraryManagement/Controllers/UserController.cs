using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
namespace LibraryManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public IActionResult GetUsers()
        {
            return Ok(_context.Users.ToList());
        }

        // GET: api/User/1
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            var user = _context.Users.FirstOrDefault(x => x.UserId == id);

            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        // POST: api/User
        [HttpPost]
        public IActionResult CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(user);
        }

      
        // POST: api/User/signup
        [HttpPost("signup")]
        public IActionResult Signup(User user)
        {
            var existingUser = _context.Users
                .FirstOrDefault(x => x.Username == user.Username);

            if (existingUser != null)
                return BadRequest("Username already exists");

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new
            {
                message = "Signup successful",
                userId = user.UserId,
                name = user.Name,
                username = user.Username,
                role = user.Role
            });
        }
        // POST: api/User/login
        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(x =>
                x.Username == username &&
                x.Password == password);

            if (user == null)
                return Unauthorized("Invalid username or password");

            var claims = new[]
            {
        new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
        new Claim(ClaimTypes.Name, user.Username),
        new Claim(ClaimTypes.Role, user.Role)
    };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(
                    HttpContext.RequestServices
                        .GetRequiredService<IConfiguration>()["Jwt:Key"]!
                )
            );

            var credentials = new SigningCredentials(
                key,
                SecurityAlgorithms.HmacSha256
            );

            var token = new JwtSecurityToken(
                issuer: "LibraryManagementAPI",
                audience: "LibraryManagementUsers",
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: credentials
            );

            var jwtToken = new JwtSecurityTokenHandler()
                .WriteToken(token);

            return Ok(new
            {
                message = "Login successful",
                token = jwtToken,
                userId = user.UserId,
                name = user.Name,
                role = user.Role
            });
        }

        // POST: api/User/logout
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok("Logout successful");
        }

        // PUT: api/User/1
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, User user)
        {
            var existingUser = _context.Users
                .FirstOrDefault(x => x.UserId == id);

            if (existingUser == null)
                return NotFound("User not found");

            existingUser.Name = user.Name;
            existingUser.Department = user.Department;
            existingUser.Username = user.Username;
            existingUser.Password = user.Password;
            existingUser.Role = user.Role;

            _context.SaveChanges();

            return Ok(existingUser);
        }

        // DELETE: api/User/1
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var user = _context.Users
                .FirstOrDefault(x => x.UserId == id);

            if (user == null)
                return NotFound("User not found");

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok("User deleted successfully");
        }

        // GET: api/User/role/Student
        [HttpGet("role/{role}")]
        public IActionResult GetUsersByRole(string role)
        {
            var usersByRole = _context.Users
                .Where(x => x.Role.ToLower() == role.ToLower())
                .ToList();

            if (usersByRole.Count == 0)
                return NotFound("No users found with this role");

            return Ok(usersByRole);
        }
    }
}