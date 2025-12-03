using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp.Web.DependencyInjection;
using WageWizard.Services;
using WageWizard.Services.Interfaces;
using WageWizard.Data;
using WageWizard.Data.Repositories;
using WageWizard.Repositories;
using WageWizard.Middleware;
using WageWizard.Config;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddImageSharp(options =>
{
    options.BrowserMaxAge = TimeSpan.FromDays(7);
    options.CacheMaxAge = TimeSpan.FromDays(365);
});

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<PayrollContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("WageWizard")));

var allowedOrigin = builder.Configuration["AllowedOrigin"] ?? "";

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy
            .WithOrigins(allowedOrigin)
            .AllowAnyHeader()
            .AllowAnyMethod());
});

builder.Services.Configure<JwtSettings>(
    builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IPayrollsRepository, PayrollsRepository>();
builder.Services.AddScoped<IPayrollsService, PayrollsService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthentication();
app.UseAuthorization();
app.UseImageSharp();
app.UseStaticFiles();
app.MapControllers();

await app.RunAsync();
