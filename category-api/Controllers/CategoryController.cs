using category_api.Entities;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace category_api.Controllers
{
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        public CategoriesController(MongoDbContext context)
        {
            _categoryCollection = context.GetCollection<Category>("categories");
        }
        [HttpGet(Name = "get_all")]
        public async Task<ActionResult> GetAll()
        {
            List<Category> value = await _categoryCollection.Find(c => true).ToListAsync();
            var returningCategories = value.Select(value =>
            new
            {
                Id = value.Id.ToString().ToUpper(),
                value.CategoryName,
                value.CategoryId
            });
            return Ok(returningCategories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetOne(string id) =>
            Ok(await _categoryCollection.Find(c => c.Id.ToString().ToUpper().Equals(id)).FirstAsync());

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Category cat)
        {
            await _categoryCollection.InsertOneAsync(cat);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _categoryCollection.DeleteOneAsync(cat => cat.Id.ToString().ToUpper().Equals(id));
            return NoContent();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, [FromBody] Category cat)
        {
            var catInDb = await _categoryCollection.Find(cat => cat.Id.ToString().ToUpper().Equals(id)).FirstOrDefaultAsync();

            cat.Id = catInDb.Id;
            await _categoryCollection.ReplaceOneAsync(cat => cat.Id.ToString().ToUpper().Equals(id), cat);
            return Ok();
        }

    }
}

