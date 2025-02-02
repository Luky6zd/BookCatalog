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
    // validatons of api http requests
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        // readonly link/reference to database
        private readonly DataContext _context;

        // constructor
        public UsersController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Users/all
        [HttpGet]
        // api action/endoint -> get all users from user dto table
        // async method -> it can perform other operations while waiting for the operation to complete,
        // doesn't block execution thread while waiting for database query
        // returns IEnumerable<UserDTO> -> List of UserDTO objects
        public async Task<ActionResult<IEnumerable<UserDTO>>> GetUsers()
        {
            // async method fetch all users from database
            List<User> users = await _context.Users.ToListAsync();
            // mapping users to DTOs -> returns list of UserDTO as http response
            return users.Select(user => user.ToUserDTO()).ToList();
        }

        // GET: api/Users/5
        // api action/endpoint -> get 1 user by id from user detail dto table
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDetailDTO>> GetUser(int id)
        {
            // async retrieve 1 user by primary key/id from database
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

            // return user as user detail DTO
            return user.ToUserDetailDTO();
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // api action/endpoint -> update user by id
        // data annotation/atribute for HTTP PUT request
        [HttpPut("{id}")]
        // async method -> performs other operations while waiting for the operation to complete,
        // doesn't block execution thread while waiting for database query
        public async Task<IActionResult> PutUser(int id, UserUpdateDTO userDTO)
        {
            // if id from URL request matches id from UserUpdateDTO
            if (id != userDTO.UserUpdateDTOId)
            {
                return BadRequest("User not found");
            }

            // async query -> search user by id in database
            // await -> database query runs async -> doesn't block execution of other operations
            // first or default -> returns first matching user or null (if none is found)
            User? user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

            // if user doesn't exist
            if (user == null)
            {
                return NotFound("User not found.");
            }

            // update user object with new values from UserUpdateDTO object
            // maps all matching properties from UserUpdateDTO to User object-updates only existing values match by name
            _context.Entry(user).CurrentValues.SetValues(userDTO);

            // try to save changes to database async
            try
            {
                await _context.SaveChangesAsync();
            }
            // while handling potencial errors/exceptions
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

            // return ActionResult -> http response
            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // api action/endpoint -> create new user
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(UserCreateDTO userDTO)
        {
            // convert UserCreateDTO to User object
            User user = userDTO.ToUser();
            // add user to database
            _context.Users.Add(user);
            // save changes to database async
            await _context.SaveChangesAsync();

            // return ActionResult -> http response with new user
            return CreatedAtAction("GetUser", new { id = user.UserId }, user);
        }

        // DELETE: api/Users/5
        // api action/endpoint -> delete user by id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            // async query -> search user by id in database
            // await -> database query runs async -> doesn't block execution of other operations
            // first or default -> returns first matching user or null (if none is found)
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

            // remove user from database
            _context.Users.Remove(user);
            // save changes to database async
            await _context.SaveChangesAsync();

            // return ActionResult with http response
            return NoContent();
        }

        // check if user exists in database
        private bool UserExists(int id)
        {
            // returns true if user exists in database
            return _context.Users.Any(e => e.UserId == id);
        }
    }
}
