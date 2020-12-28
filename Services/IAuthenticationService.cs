using System.Threading.Tasks;
using Data.Models;
using Microsoft.AspNetCore.Identity;

namespace Services
{
	public interface IAuthenticationService
	{
		Task<bool> SignInAsync(SignIn signIn);
		Task SignOutAsync();
		Task<IdentityResult> CreateUserAsync(User user);
	}
}