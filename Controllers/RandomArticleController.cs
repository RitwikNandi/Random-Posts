using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Random_Posts.Models;
using Random_Posts.Services;

namespace Random_Posts.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RandomArticleController : ControllerBase
    {
        private readonly IArticleService _service;

        public RandomArticleController(IArticleService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] DateTime? afterTimestamp, [FromQuery] int pageSize = 10)
        {
            var article = await _service.GetAllAsync(afterTimestamp, pageSize);

            return Ok(article);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(Guid id)
        {
            var article = await _service.GetByIdAsync(id);

            if (article == null) return NotFound();

            return Ok(article);
        }
    }
}
