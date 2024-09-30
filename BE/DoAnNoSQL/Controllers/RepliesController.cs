using DoAnNoSQL.Data;
using DoAnNoSQL.Entities;
using DoAnNoSQL.ModelView;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DoAnNoSQL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RepliesController : ControllerBase
    {
        private readonly IMongoCollection<Reviews> _reviews;
        private readonly IMongoCollection<Replies> _replies;

        public RepliesController(MongoDbService mongoDbService)
        {
            _reviews = mongoDbService.Database.GetCollection<Reviews>("Reviews");
            _replies = mongoDbService.Database.GetCollection<Replies>("Replies");
        }

        [HttpGet]
        public async Task<IEnumerable<Replies>> Get()
        {
            return await _replies.Find(FilterDefinition<Replies>.Empty).ToListAsync();
        }

        [HttpPost] 
        public async Task<ActionResult<Replies>> Post([FromBody] RepliesCreateModel model)
        {
            if(!model.IsValid())
            {
                return BadRequest("Is Valid");
            }

            var replies = new Replies()
            {
                _id = ObjectId.GenerateNewId().ToString(),
                review_id = model.review_id,
                user_id = model.user_id,
                content = model.content,
                created_at = DateTime.Now,
                updated_at = DateTime.Now
            };

            await _replies.InsertOneAsync(replies);

            return Ok(replies);
        }

        [HttpGet("Get by ReviewsId/{reviewId}")]
        public async Task<ActionResult<List<Replies>>> GetByReviewId(string reviewId)
        {
            var filterR = Builders<Reviews>.Filter.Eq(p => p._id, reviewId);
            var review = await _reviews.Find(filterR).FirstOrDefaultAsync();

            if (review == null)
            {
                return BadRequest("Review does not exist!!");
            }

            var filterRp = Builders<Replies>.Filter.Eq(rp => rp.review_id, reviewId);
            List<Replies> replies = await _replies.Find(filterRp).ToListAsync();
            replies = replies.OrderBy(r => r.created_at).ToList();
            return Ok(replies);
        }
    }
}
