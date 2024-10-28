using DoAnNoSQL.Data;
using DoAnNoSQL.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace DoAnNoSQL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMongoCollection<Products> _product;

        public ProductsController(MongoDbService mongoDbService)
        {
            _product = mongoDbService.Database.GetCollection<Products>("Products");
        }

        [HttpGet]
        public async Task<IEnumerable<Products>> Get()
        {
            return await _product.Find(FilterDefinition<Products>.Empty).ToListAsync();
        }

        [HttpGet("Get By Id {id}")]
        public async Task<ActionResult<Products>> GetById(string id)
        {
            var filter = Builders<Products>.Filter.Eq(p => p._id, id);
            var product = await _product.Find(filter).FirstOrDefaultAsync();
            return product is not null ? Ok(product) : NotFound();
        }
    }
}
