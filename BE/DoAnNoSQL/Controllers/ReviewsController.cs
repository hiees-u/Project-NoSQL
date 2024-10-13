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
    public class ReviewsController : ControllerBase
    {
        private Authentication authentication = new Authentication();
        private readonly IMongoCollection<Products> _product;
        private readonly IMongoCollection<Reviews> _reviews;
        private readonly IMongoCollection<Users> _users;

        public ReviewsController(MongoDbService mongoDbService)
        {

            authentication = new Authentication();
            _product = mongoDbService.Database.GetCollection<Products>("Products");
            _reviews = mongoDbService.Database.GetCollection<Reviews>("Reviews");
            _users = mongoDbService.Database.GetCollection<Users>("Users");
        }

        [HttpGet]
        public async Task<IEnumerable<Reviews>> Get()
        {
            string id = authentication.GetUserIdFromToken(
                HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "")
            );
            return await _reviews.Find(FilterDefinition<Reviews>.Empty).ToListAsync();
        }

        [HttpPost("Create Reviews")]
        [Authorize]
        [Authorize(Roles = "user")]
        public async Task<ActionResult<Reviews>> Post([FromBody] ReviewsCreateModel model)
        {
            if (!model.isValid())
            {
                return BadRequest("Is valid");
            }

            //check userId
            var filter = Builders<Products>.Filter.Eq(p => p._id, model.product_id);
            var product = await _product.Find(filter).FirstOrDefaultAsync();

            if (product != null)
            {
                Reviews reviews = new Reviews()
                {
                    _id = ObjectId.GenerateNewId().ToString(),
                    product_id = model.product_id,
                    user_id = authentication.GetUserIdFromToken( //get id user JWT
                        HttpContext.Request.Headers["Authorization"].ToString().Replace("Bearer ", "")
                    ),
                    rating = model.rating,
                    content = model.content,
                    created_at = DateTime.Now,
                    updated_at = DateTime.Now,
                    helpful_votes = 0,
                    unhelpful_votes = 0
                };

                await _reviews.InsertOneAsync(reviews);

                return Ok(reviews);
            }
            return BadRequest("Product does not exist!!");
        }

        [HttpGet("Get Reviews By ProductId/{ProductId}")]
        public async Task<ActionResult<List<Reviews>>> GetByProductId(string ProductId)
        {
            var filterP = Builders<Products>.Filter.Eq(p => p._id, ProductId);
            var product = await _product.Find(filterP).FirstOrDefaultAsync();

            if (product == null)
            {
                return BadRequest("Product does not exist!!");
            }

            var filterR = Builders<Reviews>.Filter.Eq(r => r.product_id, ProductId);
            List<Reviews> reviews = await _reviews.Find(filterR).ToListAsync();

            List<ReviewsResponseModel> response = new List<ReviewsResponseModel>();

            foreach (var review in reviews) {
                var userName = await GetUserNameById(review.user_id);
                response.Add(new ReviewsResponseModel
                {
                    _id = review._id,
                    content = review.content,
                    created_at = review.created_at,
                    updated_at = review.updated_at,
                    rating = review.rating,
                    user_name = userName,
                });
            }

            reviews = reviews.OrderBy(r => r.created_at).ToList();
            return Ok(response);
        }

        private async Task<string?> GetUserNameById(string? userId)
        {
            if (string.IsNullOrEmpty(userId)) return null;

            var filter = Builders<Users>.Filter.Eq(u => u._id, userId);
            var user = await _users.Find(filter).FirstOrDefaultAsync();

            return user?.username; // Trả về tên người dùng nếu tồn tại, ngược lại trả về null
        }

    }
}
