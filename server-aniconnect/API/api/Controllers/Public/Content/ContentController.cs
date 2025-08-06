using Application.DTOs;
using Infrastructure.Interfaces.Content;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Public.Content
{
    [Route("api/content")]
    [ApiController]
    public class ContentController : ControllerBase
    {
        private readonly IContentRepository _repository;

        public ContentController(IContentRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync([FromRoute] string id)
        {
            var result = await _repository.GetContentAsync(id);
            
            if(result == null)
                return NotFound(new ProblemDetails { Status = StatusCodes.Status404NotFound, Detail = "Content not found" });

            return this.ApiResponseOk(data: result);
        }
    }
}
