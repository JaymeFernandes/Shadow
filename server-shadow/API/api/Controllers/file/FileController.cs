using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.file
{
    [Route("file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IAwsS3 _awsS3;

        public FileController(IAwsS3 awsS3)
        {
            _awsS3 = awsS3;
        }

        [HttpGet("{**key}")]
        public async Task<IActionResult> GetImageAsync([FromRoute] string key)
        {
            var file = await _awsS3.GetObjectAsync("shadow", key);
            
            if(file == null)
                return NotFound(new ProblemDetails { Status = StatusCodes.Status404NotFound,  Detail = "File not found" });

            return File(file.ResponseStream, file.Headers.ContentType ?? "application/octet-stream", key,
                enableRangeProcessing: true);
        }
        
        [HttpGet("user/{**key}")]
        public async Task<IActionResult> GetAvatarAsync([FromRoute] string key)
        {
            var file = await _awsS3.GetObjectAsync("users", key);
            
            if(file == null)
                return NotFound(new ProblemDetails { Status = StatusCodes.Status404NotFound,  Detail = "File not found" });

            return File(file.ResponseStream, file.Headers.ContentType ?? "image/png");
        }
       
    }
}
