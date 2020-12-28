using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Data.Database;
using Data.Models;

namespace Services
{
	public class AuthenticationService : IAuthenticationService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly MyDbContext _dbContext;
		public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, MyDbContext dbContext)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_dbContext = dbContext;
		}

		public async Task<bool> SignInAsync(SignIn signIn)
		{
			var user = await _userManager.FindByNameAsync(signIn.UserName);
			var result = await _signInManager.PasswordSignInAsync(user, signIn.Password, false, false);
			return result.Succeeded;
		}

		public async Task SignOutAsync()
		{
			await _signInManager.SignOutAsync();
		}
		public async Task<IdentityResult> CreateUserAsync(User user)
		{
			var result = await _userManager.CreateAsync(new ApplicationUser { UserName = user.UserName }, user.Password);
			System.Console.WriteLine(result);
			await _dbContext.SaveChangesAsync();
			return result;
		}
	}
}