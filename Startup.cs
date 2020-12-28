using System;
using Data.Database;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication;
using Data.Models;


namespace rtchat_backend
{
	public class Startup
	{
		public IConfiguration Configuration { get; }
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			var connectionString = Configuration["DbConnectionString"];

			services.AddEntityFrameworkMySql()
				.AddDbContext<MyDbContext>(options => options.UseMySql(connectionString, new MySqlServerVersion(new Version(10, 4, 13))));
			services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<MyDbContext>().AddDefaultTokenProviders();
			services.AddIdentityServer()
				.AddApiAuthorization<ApplicationUser, MyDbContext>();
			services.AddAuthentication()
				.AddIdentityServerJwt();

			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder =>
					{
						builder.SetIsOriginAllowed(_ => true)
						.AllowAnyMethod()
						.AllowAnyHeader()
						.AllowCredentials();
					});
			});

			services.AddTransient<Services.IAuthenticationService, Services.AuthenticationService>();

			services.AddMvc(options => options.EnableEndpointRouting = false);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseRouting();

			app.UseAuthentication();
			//not sure if I need the one below
			app.UseIdentityServer();

			app.UseCors("CorsPolicy");

			app.UseMvc();
		}
	}
}
