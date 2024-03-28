using Microsoft.EntityFrameworkCore;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Context;
using SchoolProject.Infrustructure.Repositories;
using SchoolProject.Infrustructure;
using SchoolProject.Service;
using SchoolProject.Core;
using SchoolProject.Api.Middleware;
using Microsoft.AspNetCore.Localization;
using System.Globalization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity;
using SchoolProject.Data.Entites.Identity;
using SchoolProject.Infrustructure.Seeder;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc;
using SchoolProject.Core.ActionFilters;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
	option.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
});
#region Depenendcy Injection
//builder.Services.AddTransient<IStudentRepository, StudentRepository>();
builder.Services.AddInfrustructureDependendcies()
	.AddServiceDependendcies().AddCoreDependencies().AddServiceRegisteration(builder.Configuration);
// Can i use it from two differnt places ?
//ModuleCoreDependencies.AddCoreDependencies(builder.Services);
#endregion
#region Localization 
	builder.Services.AddControllersWithViews();
	builder.Services.AddLocalization(opt =>
	{
		opt.ResourcesPath = "";
	});

	builder.Services.Configure<RequestLocalizationOptions>(options =>
	{
		List<CultureInfo> supportedCultures = new List<CultureInfo>
		{
				new CultureInfo("en-US"),
				new CultureInfo("de-DE"),
				new CultureInfo("fr-FR"),
				new CultureInfo("ar-EG")
		};

		options.DefaultRequestCulture = new RequestCulture("ar-EG");
		options.SupportedCultures = supportedCultures;
		options.SupportedUICultures = supportedCultures;
	});

#endregion

#region Cors
var Cors = "_cors";
builder.Services.AddCors(options =>
  options.AddPolicy(name: Cors, builder =>
  {
	  builder.AllowAnyHeader();
	  builder.AllowAnyMethod();
	  builder.AllowAnyOrigin();
  }
	  )
);
#endregion
builder.Services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
builder.Services.AddTransient<IUrlHelper>(x =>
{
    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
    var factory = x.GetRequiredService<IUrlHelperFactory>();
    return factory.GetUrlHelper(actionContext);
});

builder.Services.AddTransient<OnlyUserFilter>();

//Serilog Configure
Log.Logger = new LoggerConfiguration()
              .ReadFrom.Configuration(builder.Configuration).CreateLogger();

builder.Services.AddSerilog();

var app = builder.Build();
using (var scope = app.Services.CreateScope())// to deal with it as scoped not singleton
{
	var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
	var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
	await RoleSeeder.SeedAsync(roleManager);
	await UserSeeder.SeedAsync(userManager);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}
#region Localization Middleware
	var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
	app.UseRequestLocalization(options.Value);
#endregion



app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseCors(Cors);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
