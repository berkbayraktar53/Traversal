using MediatR;
using WebUILayer.Models;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor;
using AspNetCoreHero.ToastNotification;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using WebUILayer.CQRS.Handlers.DestinationHandlers;
using DataAccessLayer.Concrete.EntityFramework.Contexts;

namespace WebUILayer
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddScoped<GetAllDestinationQueryHandler>();
			services.AddScoped<GetDestinationByIdQueryHandler>();
			services.AddScoped<CreateDestinationCommandHandler>();
			services.AddScoped<DeleteDestinationCommandHandler>();
			services.AddScoped<UpdateDestinationCommandHandler>();

			services.AddMediatR(typeof(Startup));

			services.AddDbContext<DatabaseContext>();
			services.AddIdentity<User, Role>()
				.AddEntityFrameworkStores<DatabaseContext>()
				.AddErrorDescriber<CustomIdentityValidator>()
				.AddTokenProvider<DataProtectorTokenProvider<User>>(TokenOptions.DefaultProvider)
				.AddEntityFrameworkStores<DatabaseContext>();
			services.AddHttpClient();
			services.AddHttpContextAccessor();
			services.AddControllersWithViews().AddRazorRuntimeCompilation();
			services.AddMvc(config =>
			{
				var policy = new AuthorizationPolicyBuilder()
				.RequireAuthenticatedUser()
				.Build();
				config.Filters.Add(new AuthorizeFilter(policy));
			});
			services.AddLocalization(opt =>
			{
				opt.ResourcesPath = "Resources";
			});

			services.AddRazorPages()
				.AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
				.AddDataAnnotationsLocalization();
			services.AddNotyf(cfg =>
			{
				cfg.DurationInSeconds = 5;
				cfg.IsDismissable = true;
				cfg.Position = NotyfPosition.TopRight;
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}
			app.UseStatusCodePagesWithReExecute("/ErrorPage/Error404", "?code={0}");
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();
			var suppertedCultures = new[] { "tr", "en", "fr", "es", "it", "de" };
			var localizationOptions = new RequestLocalizationOptions().SetDefaultCulture(suppertedCultures[1])
				.AddSupportedCultures(suppertedCultures)
				.AddSupportedUICultures(suppertedCultures);
			app.UseRequestLocalization(localizationOptions);
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
				  name: "admin",
				  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
				);
			});
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
				  name: "member",
				  pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
				);
			});
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}