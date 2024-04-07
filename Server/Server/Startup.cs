using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SalernoServer.Extensions;
using SalernoServer.Models;
using SalernoServer.Services;
using System.Text;
using Server.Triggers;
using Server.Triggers.Snapshot;
using Microsoft.AspNetCore.Authentication;
using System.Configuration;

namespace SalernoServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

		private readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

		public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration.GetConnectionString("SqlServerDatabase");

            services.AddDbContext<AppDbContext>(
	            dbContextOptions => dbContextOptions
	            .UseSqlServer(connectionString)
				.UseTriggers(triggerOptions =>
				{
					triggerOptions.AddTrigger<AssignCreatedAtDate>();
					triggerOptions.AddTrigger<AssignUpdatedAtDate>();
					triggerOptions.AddTrigger<SnapshotCategory>();
					triggerOptions.AddTrigger<SnapshotItem>();
					triggerOptions.AddTrigger<SnapshotAddon>();
					triggerOptions.AddTrigger<SnapshotNoOption>();
					triggerOptions.AddTrigger<SnapshotGroup>();
					triggerOptions.AddTrigger<SnapshotGroupOption>();
				})
	            .LogTo(Console.WriteLine, LogLevel.Information)
	            .EnableDetailedErrors()
            );

            services.AddControllers();

			//services.AddJWTTokenServices(Configuration);

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(options =>
				{
					options.TokenValidationParameters = new TokenValidationParameters
					{
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateLifetime = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = "https://localhost:7074",
						ValidAudience = "https://localhost:7074",
						IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("e7202338bf9a0384046b2753776c5fa9cc7e095dc0df1e63c8f95bbb0df31a7bc9a3732b13bfe0ecd2c4f9ce5abaaba7be85860f9dc2a25c453ec3aab6be157f"))
					};
				});

			services.AddCors(options =>
			{
				options.AddPolicy(name: MyAllowSpecificOrigins,
								  policy =>
								  {
									  policy.WithOrigins("http://localhost:3000",
														   "http://localhost:3001")
									  .AllowAnyHeader()
									  .AllowAnyMethod()
									  .AllowCredentials();
								  });
			});

			services.AddEndpointsApiExplorer();

			services.AddSwaggerGen(options => 
			{
				options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = Microsoft.OpenApi.Models.ParameterLocation.Header,
					Description = "JWT Authorization header using the Bearer scheme."
				});
				options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
				{
					new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
							Reference = new Microsoft.OpenApi.Models.OpenApiReference {
								Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
									Id = "Bearer"
							}
						},
						Array.Empty<string>()
				}
				});
			});
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
			if (env.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}
			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseCors(MyAllowSpecificOrigins);
			app.UseAuthentication();
			app.UseAuthorization();


			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
    }
}
