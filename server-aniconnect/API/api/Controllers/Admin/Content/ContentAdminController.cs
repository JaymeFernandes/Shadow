using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Admin.Content;

[ApiController]
[Tags("Admin")]
[Route("api/admin/content")]
[Authorize(Roles = "Admin,Moderator")]
public class ContentAdminController : ControllerBase
{
    
}