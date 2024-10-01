using DoAnNoSQL.Data;
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
        private readonly IMongoCollection<Products> _product;
        private readonly IMongoCollection<Reviews> _reviews;

        public ReviewsController(MongoDbService mongoDbService)
        {
            _product = mongoDbService.Database.GetCollection<Products>("Products");
            _reviews = mongoDbService.Database.GetCollection<Reviews>("Reviews");
        }

        [HttpGet]
        public async Task<IEnumerable<Reviews>> Get()
        {
            return await _reviews.Find(FilterDefinition<Reviews>.Empty).ToListAsync();
        }

        [HttpPost("Create Reviews")]
        [Authorize]
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
                    user_id = model.user_id,
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
            reviews = reviews.OrderBy(r => r.created_at).ToList();
            return Ok(reviews);
        }
    }
}
