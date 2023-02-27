
using Microsoft.EntityFrameworkCore;
using SalernoServer.Models;
using SalernoServer.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SalernoServer;
using Microsoft.AspNetCore.Identity;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = Microsoft.AspNetCore.Builder.WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddDbContext<ItemContext>(
//    options => options.UseMySqlServer(@"Server=localhost;Port=3306;Database=Salerno;User ID=root;Password="));
var connectionString = "server=localhost;user=root;password=;database=salerno;Convert Zero Datetime=True";
var serverVersion = ServerVersion.AutoDetect(connectionString);

builder.Services.AddDbContext<AppDbContext>(
    dbContextOptions => dbContextOptions
    .UseMySql(connectionString, serverVersion)
    .LogTo(Console.WriteLine, LogLevel.Information)
    .EnableDetailedErrors()
);
builder.Services.AddJWTTokenServices(builder.Configuration);

/*//builder.Services.AddJWTTokenServices(builder.Configuration);
//builder.Services.AddNewJWTTokenServices(builder.Configuration);
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,

        ValidAudience = "https://localhost:7074",
        ValidIssuer = "https://localhost:7074",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("e7202338bf9a0384046b2753776c5fa9cc7e095dc0df1e63c8f95bbb0df31a7bc9a3732b13bfe0ecd2c4f9ce5abaaba7be85860f9dc2a25c453ec3aab6be157f"))
    };
});*/


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("http://example.com",
                                              "http://localhost:3000",
                                               "http://localhost:3001");
                      });
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options => {
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

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
