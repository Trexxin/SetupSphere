using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SetupSphere.Models;

namespace SetupSphere.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : Controller
{
    // Declares a private read-only field for the database context. 
    private readonly ApplicationDbContext _context;
    
    // ASP.NET Core's dependency injection system automatically passes the ApplicationDbContext object to the controller.
    // This process is called "constructor injection" 
    public UsersController(ApplicationDbContext context)
    {
        _context = context; 
    }
    
    // Endpoint to GET all users from the database
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        try
        {
            var users = await _context.Users.ToListAsync();
            return await _context.Users.ToListAsync();
        }
        catch (Exception ex)
        {
            // Logs the exception details
            Console.WriteLine(ex.ToString());
            // Returns a 500 Internal Server Error status.
            return StatusCode(500, "Internal server error");
        }
    }
    
    // Endpoint to GET an individual user by their id
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        return user == null ? NotFound() : user;
    }
    
    // Endpoint to ADD a user to the database
    [HttpPost]
    public async Task<ActionResult<User>> AddUser(User userData)
    {
        _context.Users.Add(userData);
        try
        {
            // Save the changes and if successful, it adds the new User to the database
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest("An error occurred while creating the user.");
        }
        // 'CreatedAtAction' returns a 201 status code, indicating the resource was created.
        return CreatedAtAction(nameof(GetUsers), new { id = userData.UserID}, userData);
    }
    
    // Endpoint to Update a user in the database
    [HttpPut]
    public async Task<ActionResult<User>> UpdateUser(int id, User userUpdateData)
    {
        if (id != userUpdateData.UserID)
        {
            return BadRequest("The url does not match the ID.");
        }

        var user = await _context.Users.FindAsync(id);

        if (user != null)
        {
            user.Username = userUpdateData.Username;
            user.Email = userUpdateData.Email;
            user.ProfilePicture = userUpdateData.ProfilePicture;
        }
        else
        {
            return NotFound();
        }

        try
        {
            // Save the changes and if successful, it adds the new User to the database
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return BadRequest("An error occurred while updating the user.");
        }

        return Ok(user);
    }
}