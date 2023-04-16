using Ambit.API.Helpers;
using Ambit.AppCore.Common;
using Ambit.AppCore.Models;
using Ambit.Infrastructure.Persistence;
using Ambit.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

string MyAllowSpecificOrigins = "MyPolicy";
var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration; // allows both to access and to set up the config
IWebHostEnvironment environment = builder.Environment;
//builder.Services.AddSession(options =>
//{
//	options.IdleTimeout = TimeSpan.FromMinutes(60);
//});
builder.Services.AddCors(options =>
{
	options.AddPolicy(name: MyAllowSpecificOrigins,
				   builder =>
				   {
					   builder.AllowAnyOrigin()
						.AllowAnyMethod()
						.AllowAnyHeader();
				   });
});
// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
			   .AddJwtBearer(options =>
			   {
				   options.TokenValidationParameters = new TokenValidationParameters
				   {
					   ValidateIssuer = false,
					   ValidateAudience = false,
					   ValidateIssuerSigningKey = false,
					   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration.GetValue<string>("AppSettings:SecretKey")))
				   };
				   //options.Audience = "http://localhost";
				   //options.Authority = "http://localhost";
				   options.RequireHttpsMetadata = false;
			   });
builder.Services.AddDbContext<AppDbContext>(o => o.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
var appSettingsSection = configuration.GetSection("appSettings");
builder.Services.Configure<AppSettings>(appSettingsSection);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
	// Include 'SecurityScheme' to use JWT Authentication
	var jwtSecurityScheme = new OpenApiSecurityScheme
	{
		BearerFormat = "JWT",
		Name = "JWT Authentication",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.Http,
		Scheme = JwtBearerDefaults.AuthenticationScheme,
		Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

		Reference = new OpenApiReference
		{
			Id = JwtBearerDefaults.AuthenticationScheme,
			Type = ReferenceType.SecurityScheme
		}
	};

	setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);
	setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
	   { jwtSecurityScheme, Array.Empty<string>() }
    });

});
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IDapper, Dapperr>();
builder.Services.AddScoped<IRepoSupervisor, RepoSupervisor>();


builder.Services.AddTransient<IUserService, userService>();
builder.Services.AddTransient<IRepoSupervisor, RepoSupervisor>();
builder.Services.AddTransient<IitemService, itemService>();
builder.Services.AddTransient<IBannerService, BannerService>();
builder.Services.AddTransient<ICartService, CartService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (true || app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);
//Add User session
//app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
