using Azure.Identity;
using Microsoft.AspNetCore.Mvc;
using PrimeiraAPI.Application.Services;
using PrimeiraAPI.Domain.Models;
using PrimeiraAPI.Domain.Models.EmployeeAggregate;

namespace PrimeiraAPI.Controllers
{
    [ApiController]
	[Route("api/v1/auth")]
	public class AuthController : Controller
	{
		[HttpPost]
		public IActionResult Auth(string username, string password)
		{
			if (username == "matheus" && password == "matheusbueno123456")
			{
				var token = TokenService.GenerateToken(new Employee());
				return Ok(token);
			}

			return BadRequest("username or password invalid");
		}
	}
}
