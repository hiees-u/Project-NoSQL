using DoAnNoSQL.Data;
using DoAnNoSQL.DTO;
using DoAnNoSQL.Entities;
using DoAnNoSQL.ModelView;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DoAnNoSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IMongoCollection<Users> _users;
        private Authentication authentication = new Authentication();

        public UsersController(MongoDbService mongoDbService)
        {
            authentication = new Authentication();
            _users = mongoDbService.Database.GetCollection<Users>("Users");
        }

        [HttpGet]
        [Authorize(Roles = "user")]
        public async Task<IEnumerable<Users>> Get()
        {
            return await _users.Find(FilterDefinition<Users>.Empty).ToListAsync();
        }

        [HttpGet("Get by Id")]
        [Authorize(Roles = "user")]
        public async Task<IActionResult> getuserByIdJWT()
        {
            string userId = authentication.GetUserIdFromToken( //get id user JWT
                        HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "")
                    );
            var filter = Builders<Users>.Filter.Eq(u => u._id, userId);

            // Retrieve the user from the database
            var user = await _users.Find(filter).FirstOrDefaultAsync();

            if (user == null)
            {
                return NotFound(); // Return a 404 Not Found response if the user doesn't exist
            }

            return Ok(user);
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
                var tokenString = authentication.GenerateJwtToken(user);
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
    }
}