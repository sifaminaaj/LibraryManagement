using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

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
            return NotFound();

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
    [HttpPost("signup")]
    public IActionResult Signup(User user)
    {
        var existingUser = _context.Users.FirstOrDefault(x => x.Username == user.Username);
        if (existingUser != null)
            return BadRequest("Username already exists");
        _context.Users.Add(user);
        return Ok(user);
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

        return Ok(new
        {
            message = "Login successful",
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
        var existingUser = _context.Users.FirstOrDefault(x => x.UserId == id);

        if (existingUser == null)
            return NotFound();

        existingUser.Name = user.Name;
        existingUser.Department = user.Department;
        existingUser.Username = user.Username;
        existingUser.Password = user.Password;
        existingUser.Role = user.Role;

        return Ok(existingUser);
    }

    // DELETE: api/User/1
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        var user = _context.Users.FirstOrDefault(x => x.UserId == id);

        if (user == null)
            return NotFound();

        _context.Users.Remove(user);

        return Ok("User deleted");
    }
    [HttpGet("role/{role}")]
    public IActionResult GetUsersByRole(string role)
    {
        var usersByRole = _context.Users.Where(x => x.Role.ToLower() == role.ToLower()).ToList();
        if (usersByRole.Count == 0)
            return NotFound("No users found with this role");
        return Ok(usersByRole);
    }
}