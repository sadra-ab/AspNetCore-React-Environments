using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AspNetCoreReactEnvironments
{
	public class Startup
	{
		private readonly IWebHostEnvironment _environment;

		public Startup(IConfiguration configuration, IWebHostEnvironment env)
		{
			this.Configuration = configuration;
			this._environment = env;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{

			services.AddControllersWithViews();

			// In production, the React files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				if (this._environment.EnvironmentName.Equals("staging", StringComparison.InvariantCultureIgnoreCase))
					configuration.RootPath = "ClientApp/build/staging";

				// else if (this._environment.EnvironmentName.Equals("ANOTHER_ENVIRONMENT", StringComparison.InvariantCultureIgnoreCase))
				// 	configuration.RootPath = "ClientApp/build/ANOTHER_ENVIRONMENT";

				configuration.RootPath = "ClientApp/build/production";
			});		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseSpaStaticFiles();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller}/{action=Index}/{id?}");
			});

			app.UseSpa(spa =>
			{
				spa.Options.SourcePath = "ClientApp";

				if (env.IsDevelopment())
				{
					spa.UseReactDevelopmentServer(npmScript: "start");
				}
			});
		}
	}
}
