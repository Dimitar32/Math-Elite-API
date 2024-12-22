using MathEliteAPI.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure DbContext with SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5174", "http://localhost:5173") // Replace with your React frontend's URL
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Add Controllers
builder.Services.AddControllers();

// Configure Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(); // Add Swagger generation

// Configure Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
})
.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
.AddGoogle(GoogleDefaults.AuthenticationScheme, options =>
{
    options.ClientId = "1057640799920-ao2d1u399ms60vt30mjido36p8tjt7gh.apps.googleusercontent.com"; // Replace with your Client ID
    options.ClientSecret = "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"; // Replace with your Client Secret
    options.SaveTokens = true; // Save access and refresh tokens for later use
});

// Add Authorization
builder.Services.AddAuthorization();

var app = builder.Build();

// Enable Swagger in Development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MathElite API v1"));
}


app.UseCors("AllowFrontend");

// Use Middleware
app.UseRouting();
app.UseAuthentication(); // Enable Authentication
app.UseAuthorization(); // Enable Authorization
app.MapControllers();

app.Run();
