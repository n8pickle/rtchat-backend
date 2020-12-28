using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Services;
using Data.Models;

namespace server.Controllers
{
	[Route("api/[controller]")]
	public class AuthenticationController : Controller
	{
		private readonly IAuthenticationService _authenticationService;

		public AuthenticationController(IAuthenticationService authenticationService)
		{
			_authenticationService = authenticationService;
		}

		[HttpPost]
		[Route("signin")]
		public async Task<IActionResult> SignIn([FromBody] SignIn signIn)
		{
			try
			{
				var testResult = await _authenticationService.SignInAsync(signIn);
				if (!testResult)
				{
					return Unauthorized();
				}
			}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex);
				return Unauthorized();
			}
			return Ok();
		}

		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> Create([FromBody] User user)
		{
			await _authenticationService.CreateUserAsync(user);
			return Ok();
		}

		[HttpPost]
		// [ValidateAntiForgeryToken]
		[Route("signout")]
		public async Task<IActionResult> SignOut()
		{
			await _authenticationService.SignOutAsync();
			return Ok();
		}
	}
}