using Application.DTOs;
using Application.DTOs.Admin.Work;
using Domain.Models.Works;
using Infrastructure.Interfaces.Work;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Admin.Work;

[ApiController]
[Route("api/admin")]
[Authorize(Roles = "Admin,Moderator")]
public class AdminWorkController : ControllerBase
{
    private readonly IWorkRepository  _repository;

    public AdminWorkController(IWorkRepository repository)
    {
        _repository = repository;
    }


    [HttpPost("work")]
    public async Task<IActionResult> CreateWorkAsync([FromBody] RequestNewWork work)
    {
        if(work == null)
            throw new ArgumentNullException(nameof(work));
        
        var entity = new Domain.Models.Works.Work
        {
            Code = Guid.CreateVersion7(),
            Status = work.Status,
            Names = work.Names.Select(x => new Names()
            {
                Name = x.Name,
                Lang = x.Lang
            }).ToList(),
            Categories = work.Categories
                .Select(x => new Category()
                    {
                        Code = x
                    }).ToList(),
            Tags = work.Categories
                .Select(x => new Tag()
                {
                    Code = x
                }).ToList(),
            Description = work.Description ?? string.Empty,
            Authors = work.Authors.Select(x => new Author()
            {
                Name = x
            }).ToList(),
            UsersAvaliable = 0,
            TotalScore = 0,
            UpdatedDate = DateTime.UtcNow,
        };

        await _repository.PostWorkAsync(entity);

        return this.ApiResponseCreated(data:entity, meta:new
        {
            CreatedAt = DateTime.Now
        });
    }
}