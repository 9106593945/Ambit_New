using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Ambit.AppCore.Common;
using Ambit.AppCore.Models;
using Ambit.Infrastructure.Persistence;
using Navrang.Services;
using System.Text;
using Ambit.API.Helpers;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration; // allows both to access and to set up the config
IWebHostEnvironment environment = builder.Environment;
//builder.Services.AddSession(options =>
//{
//	options.IdleTimeout = TimeSpan.FromMinutes(60);
//});

// Add services to the container.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie(options =>
				{
					options.Cookie.Name = "Authenticate";
					options.LoginPath = "/Login";
					options.LogoutPath = "/Logout";
					options.AccessDeniedPath = "/AccessDenied";
					options.SlidingExpiration = false;
					options.ReturnUrlParameter = "from";
					//options.Cookie.SameSite = SameSiteMode.None;
				})
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


builder.Services.AddTransient<IUserService,userService>();
builder.Services.AddTransient<IRepoSupervisor, RepoSupervisor>();
builder.Services.AddTransient<IitemService, itemService>();
builder.Services.AddTransient<IBannerService, BannerService>();
builder.Services.AddTransient<ICartService, CartService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Add User session
//app.UseSession();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
