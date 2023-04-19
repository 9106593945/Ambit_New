using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Ambit.API.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	[Authorize]
	public class BaseAPIController : ControllerBase
	{
	}
}
