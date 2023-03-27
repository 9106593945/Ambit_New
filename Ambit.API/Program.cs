using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
