using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.Extensions.Options;
using IdentityServer4.EntityFramework.Options;
using Data.Models;


namespace Data.Database
{
	public class MyDbContext : ApiAuthorizationDbContext<ApplicationUser>
	{
		public MyDbContext(DbContextOptions<MyDbContext> dbOptions, IOptions<OperationalStoreOptions> operationalStoreOptions) : base(dbOptions, operationalStoreOptions)
		{
		}

		public DbSet<ApplicationUser> ApplicationUsers { get; set; }
	}
}
