using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BookCatalog;
using BookCatalog.Models;
using BookCatalog.Services;
using System.Security.Claims;
using BookCatalog.Auth_JWT.AuthDTOs;

namespace BookCatalog.Controllers
{
    // data annotations
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        // static list of users
        public static List<User> Users = new List<User>
        {
            // creates admin
            new User { UserId = 1, Username = "admin", Password = "admin", Email = "admin@example.com" },
            // creates regular user
            new User { UserId = 2, Username = "user", Password = "user", Email = "user@example.com" }
        };

        // readonly link/reference to token service
        private readonly TokenService _tokenService;

        // constructor
        public AuthController(TokenService tokenService)
        {
            _tokenService = tokenService;
        }

        // POST: api/Auth
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        // attribute -> method only accepts HTTP POST requests 
        // endpoint -> for user login at route: POST /api/Auth/login
        [HttpPost("login")]
        public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginDTO loginDTO)
        {
            // searching matching user from list of Users -> matching username and password from loginDTO
            // u => filter for searching
            User? user = Users.Find(u => u.Username == loginDTO.Username && u.Password == loginDTO.Password);

            // if user doesn't exist
            if (user == null)
            {
                return Unauthorized("Invalid username or password.");
            }

            // generates JWT token for authenticated user
            // IssueToken method creates JWT token based on user details
            string token = _tokenService.IssueToken(user);
            // generates new refresh token when old token expires
            RefreshToken refreshToken = _tokenService.GenerateRefreshToken();

            // add new refresh token to list of stored refresh tokens for user
            user.RefreshTokens.Add(refreshToken);

            // return response after generating JWT access token and refresh token
            return Ok(new TokenResponse { 
                AccessToken = token, 
                RefreshToken = refreshToken.Value
            });

        }

        // atribute -> POST endpoint allow client request new access token
        // refresh token url route -> endpoint will handle refreshing access tokens
        [HttpPost("refreshToken")]
        // async method for handling refresh JWT access token based on refresh token provided by client 
        // FromBody -> request will be passed in the body od http post request
        public async Task<ActionResult<TokenResponse>> RefreshToken([FromBody] RefreshTokenRequest refreshTokenRequest)
        {
            // search user in Users List based on refresh token passed in request
            // checks if refresh token exists in user's refresh token list and that refresh token is not expired,
            // if both conditions are true, it will generate and return new access token
            User? user = Users.Find(u => 
                u.RefreshTokens.Exists(r => 
                r.Value == refreshTokenRequest.RefreshToken &&
                r.ExpiresAt > DateTime.Now));

            // if user doesn't exist
            if (user == null)
            {
                // return to client
                return Unauthorized("Invalid refresh token.");
            }

            // remove refresh token from user's refresh token List
            // it searches refresh tokens by value and removes all matching tokens from list
            user.RefreshTokens.RemoveAll(r => r.Value == refreshTokenRequest.RefreshToken);

            // generates JWT for user by using token service
            string token = _tokenService.IssueToken(user);
            // generates refresh token
            RefreshToken refreshToken = _tokenService.GenerateRefreshToken();
            // adds newly generated refresh token to user's List of refresh tokens
            user.RefreshTokens.Add(refreshToken);

            // returns ActionResult -> TokenResponse -> new access token
            return Ok(new TokenResponse
            {
                AccessToken = token,
                RefreshToken = refreshToken.Value
            });

        }

        // validation atribute 
        // endpoint for logout user -> post request
        [HttpPost("logout")]
        public async Task<ActionResult<TokenResponse>> Logout()
        {
            // retrieve user's unique identifier from claims in current authenticated user's JWT
            string? UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // if user id doesn't exist in user's claims
            if (UserId == null)
            {
                return Unauthorized("Invalid user name");
            }

            // converting string UserId retrived from JWT claims into int
            int authUserId = int.Parse(UserId);
            // finding user in list od users based on authUserId
            User? user = Users.Find(u => u.UserId == authUserId);

            // if user doesn't exist
            if (user == null)
            {
                return Unauthorized("User not found");
            }

            // clear refresh tokens list
            user.RefreshTokens.Clear();
            // return request was successfull
            return Ok("Deleted successfully");

            //user.RefreshTokens = [];
            //return NoContent();

        }

    }
}
