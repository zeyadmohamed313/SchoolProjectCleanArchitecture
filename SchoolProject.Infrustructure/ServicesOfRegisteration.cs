using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SchoolProject.Data.Entites.Identity;
using SchoolProject.Data.Helper;
using SchoolProject.Infrustructure.Abstracts;
using SchoolProject.Infrustructure.Context;
using SchoolProject.Infrustructure.InfrustructureBases;
using SchoolProject.Infrustructure.Repositories;
using SchoolProject.Infrustructure.UnitOfwork;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolProject.Infrustructure
{
	public static class ServicesOfRegisteration
	{
		public static IServiceCollection AddServiceRegisteration(this IServiceCollection services,
			IConfiguration configuration)
		{
			services.AddIdentity<User,Role>(option =>
			{
				// Password settings.
				option.Password.RequireDigit = true;
				option.Password.RequireLowercase = true;
				option.Password.RequireNonAlphanumeric = true;
				option.Password.RequireUppercase = true;
				option.Password.RequiredLength = 6;
				option.Password.RequiredUniqueChars = 1;

				// Lockout settings.
				option.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
				option.Lockout.MaxFailedAccessAttempts = 5;
				option.Lockout.AllowedForNewUsers = true;

				// User settings.
				option.User.AllowedUserNameCharacters =
				"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
				option.User.RequireUniqueEmail = true;
				option.SignIn.RequireConfirmedEmail = true;

			}).AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();
			//JWT Authentication
			var jwtSettings = new JwtSettings();
			configuration.GetSection(nameof(jwtSettings)).Bind(jwtSettings);
			var emailSettings = new EmailSettings();
            configuration.GetSection(nameof(emailSettings)).Bind(emailSettings);

            services.AddSingleton(jwtSettings);
            services.AddSingleton(emailSettings); // 


            services.AddAuthentication(x =>
			{
				x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
				x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
			})
		  .AddJwtBearer(x =>
		  {
			  x.RequireHttpsMetadata = false;
			  x.SaveToken = true;
			  x.TokenValidationParameters = new TokenValidationParameters
			  {
				  ValidateIssuer = jwtSettings.ValidateIssuer,
				  ValidIssuers = new[] { jwtSettings.Issuer },
				  ValidateIssuerSigningKey = jwtSettings.ValidateIssuerSigningKey,
				  IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtSettings.Secret)),
				  ValidAudience = jwtSettings.Audience,
				  ValidateAudience = jwtSettings.ValidateAudience,
				  ValidateLifetime = jwtSettings.ValidateLifeTime,
			  };
		  });

			//Swagger Gn
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "School Project", Version = "v1" });
				c.EnableAnnotations();

				c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
				{
					Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
					Name = "Authorization",
					In = ParameterLocation.Header,
					Type = SecuritySchemeType.ApiKey,
					Scheme = JwtBearerDefaults.AuthenticationScheme
				});

				c.AddSecurityRequirement(new OpenApiSecurityRequirement
			{
			{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type = ReferenceType.SecurityScheme,
					Id = JwtBearerDefaults.AuthenticationScheme
				}
			},
			Array.Empty<string>()
			}
		   });
			});

            services.AddAuthorization(option =>
            {
                option.AddPolicy("Create", policy =>
                {
                    policy.RequireClaim("Create", "true");
                });
                option.AddPolicy("Delete", policy =>
                {
                    policy.RequireClaim("Delete", "true");
                });
                option.AddPolicy("Edit", policy =>
                {
                    policy.RequireClaim("Edit", "true");
                });
            });

            return services;

			
		}
		

	}
}
