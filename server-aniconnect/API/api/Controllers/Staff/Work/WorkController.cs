using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Staff.Work;

[ApiController]
[Route("api/staff")]
[Authorize(Roles = "Staff")]
public class WorkController : ControllerBase
{
    
}