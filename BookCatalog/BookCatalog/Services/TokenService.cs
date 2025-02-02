using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BookCatalog.Models;
using System.Security.Cryptography;

namespace BookCatalog.Services
{
    public class TokenService
    {
        // reference to application configuration settings
        private readonly IConfiguration _configuration;
        
        // constructor
        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        // generate and return JWT for user
        public string IssueToken(User user)
        {
            // creates symetric security key for signing in
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            // creates signing credentials using security key and sha256 hashing algorithm
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            // creates list of claims that is included in JWT
            var claims = new List<Claim>
            {
                // creates new claim that stores userId inside JWT
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                // creates new claim that stores user email inside JWT
                new Claim(ClaimTypes.Email, user.Email)
            };

            //  creates JWT token using jwt security token,
            // it defines issuer, audience, claims, expiration time and signing credentials
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"], // who creates token
                audience: _configuration["Jwt:Audience"], // define who is allowed to use this token
                claims: claims, // list of user claims-userId, email, role
                expires: DateTime.Now.AddHours(1), // token expiration time
                signingCredentials: credentials); // ensure token is signed using secure key
            
            // converts jwt security token into jwt string
            // it's actual token that sends back to client/user 
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        // generates refresh token -> long lived token for requesting new access token without requiring 
        // to log in again
        public RefreshToken GenerateRefreshToken()
        {
            // creates and returnes new refresh token 
            return new RefreshToken
            {
                // with random secure token value
                Value = GenerateRefreshTokenKey(),
                // and expiration date
                ExpiresAt = DateTime.UtcNow.AddDays(7)
            };
        }

        // generates random refresh token string
        public string GenerateRefreshTokenKey()
        {
            // storing 256-bit key of 32 byte in array
            var secretKey = new byte[32];

            // creates cryptografy random number generator
            using (var randNumb = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                // fills array with security key random bytes
                randNumb.GetBytes(secretKey);
                // converts byte array seckret key to string for storage
                return Convert.ToBase64String(secretKey);
            }
            //Console.WriteLine(secretKey);
            //string secretKey = Convert.ToBase64String(randomNumber);
                
        }

    }
}
