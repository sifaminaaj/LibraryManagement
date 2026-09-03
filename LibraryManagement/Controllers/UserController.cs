using LibraryManagement.Models;
using Microsoft.AspNetCore.Mvc;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private static List<User> users = new List<User>();

    // GET: api/User
    [HttpGet]
    public IActionResult GetUsers()
    {
        return Ok(users);
    }

    // GET: api/User/1
    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        var user = users.FirstOrDefault(x => x.UserId == id);

        if (user == null)
            return NotFound();

        return Ok(user);
    }

    // POST: api/User
    [HttpPost]
    public IActionResult CreateUser(User user)
    {
        users.Add(user);

        return Ok(user);
    }

    // PUT: api/User/1
    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, User user)
    {
        var existingUser = users.FirstOrDefault(x => x.UserId == id);

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
        var user = users.FirstOrDefault(x => x.UserId == id);

        if (user == null)
            return NotFound();

        users.Remove(user);

        return Ok("User deleted");
    }
}