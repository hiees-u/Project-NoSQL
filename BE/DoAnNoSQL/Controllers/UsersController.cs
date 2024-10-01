using DoAnNoSQL.Data;
using DoAnNoSQL.Entities;
using DoAnNoSQL.ModelView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Driver;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DoAnNoSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMongoCollection<Users> _users;

        private readonly string _secretKey = "YourVeryLongSecretKeyOfAtLeast32Characters";

        public UsersController(MongoDbService mongoDbService)
        {
            _users = mongoDbService.Database.GetCollection<Users>("Users");
        }

        [HttpGet]
        [Authorize]
        public async Task<IEnumerable<Users>> Get()
        {
            return await _users.Find(FilterDefinition<Users>.Empty).ToListAsync();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel loginModel)
        {
            if (loginModel.IsValid())
            {
                return BadRequest("Invalid data");
            }

            var filter = Builders<Users>.Filter.Eq(u => u.email, loginModel.email);
            var user = await _users.Find(filter).FirstOrDefaultAsync();
            if (user == null)
            {
                return Unauthorized("Invalid username.");
            }

            if (user != null && user.password!.Equals(loginModel.password))
            {
                var tokenString = GenerateJwtToken(user.username!);
                return Ok(new
                {
                    Message = "Login successful",
                    userId = user._id,
                    Token = tokenString
                });
            }
            return BadRequest("Invalid username or password.");
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register([FromBody] RegisterModel registerModel)
        {
            if(registerModel.IsValid())
            {
                return BadRequest("Invalid data");
            }

            var filter = Builders<Users>.Filter.Eq(u => u.email, registerModel.email);
            var count = await _users.CountDocumentsAsync(filter);
            if (count > 0) {
                return BadRequest("Email has been registered!");
            }

            Users user = new Users()
            {
                _id = ObjectId.GenerateNewId().ToString(),
                username = registerModel.username,
                password = registerModel.password,
                email = registerModel.email,
                avatar = string.Empty,
                roles = ["user"]
            };

            await _users.InsertOneAsync(user);

            return Ok();
        }

        private string GenerateJwtToken(string userName) {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_secretKey);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, userName)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}