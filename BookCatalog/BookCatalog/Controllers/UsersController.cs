using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookCatalog;
using BookCatalog.Models;
using BookCatalog.Mappers;
using BookCatalog.DTOs.DTOs_User;

namespace BookCatalog.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;

        public UsersController(DataContext context)
        {
            _context = context;
        }

        [HttpGet]
        // api action/endoint -> GET/all users from user dto table
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            List<User> users = await _context.Users.ToListAsync();
            
            return users.Select(user => user.ToUserDTO()).ToList();
        }

        // GET: api/Users/5
        // api action/endpoint -> GET 1 user by id from user detail dto table
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailDTO>> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            // if user doesn't exist
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // if id from URL request matches id from User object
            if (user.UserId != id)
            {
                return BadRequest("User doesn't exist.");
            }

            return user.ToUserDetailDTO();
        }

        // PUT: api/Users/5
        // api action/endpoint -> update user by id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, UserUpdateDTO userDTO)
        {
            // if id from URL request matches id from UserUpdateDTO
            if (id != userDTO.UserUpdateDTOId)
            {
                return BadRequest("User not found");
            }

            // first or default -> returns first matching user or null (if none is found)
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            // if user doesn't exist
            if (user == null)
            {
                return NotFound("User not found.");
            }

            _context.Entry(user).CurrentValues.SetValues(userDTO);

            // try to save changes async
            try
            {
                await _context.SaveChangesAsync();
            }
            // handling potencial errors/exceptions
            catch (DbUpdateConcurrencyException)
            {
                // if user doesn't exist
                if (!UserExists(id))
                {
                    return NotFound("User not found.");
                }
                // if user exists but other error occured throw exception
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // api action/endpoint -> create new user
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserCreateDTO userDTO)
        {
            User user = userDTO.ToUser();
            _context.Users.Add(user);
        
            await _context.SaveChangesAsync();

            // return http response with new user
            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            // returns first matching user or null (if none is found)
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            // if user doesn't exist
            if (user == null)
            {
                return NotFound("User doesn't exist.");
            }

            // if id from URL request matches id from User object
            if (user.UserId != id)
            {
                return BadRequest("User not found.");
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            // returns true if user exists
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
