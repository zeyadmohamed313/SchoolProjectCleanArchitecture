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
	.AddServiceDependendcies().AddCoreDependencies();
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


var app = builder.Build();

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

app.UseAuthorization();

app.MapControllers();

app.Run();
