using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

namespace Malyshev_Project;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Add services to the container.
		builder.Services.AddControllersWithViews();

		builder.Services.AddDistributedMemoryCache();
		builder.Services.AddSession(options =>
		{
			options.Cookie.Name = ".MyApp.Session";
			options.IdleTimeout = TimeSpan.FromSeconds(3600);
		});

		//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
		//	.AddCookie(options => options.LoginPath = "/Auth/Login");
		//builder.Services.AddAuthorization();

		string? connectionString = builder.Configuration.GetConnectionString("DockerConnection");
		builder.Services.AddDbContext<Models.PostgresContext>(options => options.UseNpgsql(connectionString));

		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (!app.Environment.IsDevelopment())
		{
			app.UseExceptionHandler("/Home/Error");
			// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
			app.UseHsts();
		}

		app.UseHttpsRedirection();
		app.UseStaticFiles();

		app.UseRouting();

		app.UseSession();
		//app.UseAuthentication();
		//app.UseAuthorization();

		app.MapControllerRoute(
			name: "default",
			pattern: "{controller=Home}/{action=Index}");

		app.Run();
	}
}