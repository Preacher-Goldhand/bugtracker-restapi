using BugTracker;
using BugTracker.Entities;
using BugTracker.Middleware;
using BugTracker.Models.AuthenticationDtos;
using BugTracker.Models.Validators;
using BugTracker.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Reflection;
using System.Text;

/*
    CONFIGURATION OF SERVICES IN THE CONTAINER
*/

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers().AddFluentValidation();
builder.Services.AddDbContext<BugTrackerDbContext>();
builder.Services.AddScoped<DataSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<IQuestService, QuestService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAcountService, AccountService>();
builder.Services.AddScoped<IPasswordHasher<Employee>, PasswordHasher<Employee>>();
builder.Services.AddScoped<IValidator<RegisterEmployeeDto>, RegisterEmployeeDtoValidator>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var authenticationSettings = new AuthenticationSettings();
builder.Configuration.GetSection("Authentication").Bind(authenticationSettings);
builder.Services.AddSingleton(authenticationSettings);
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = "Bearer";
    option.DefaultScheme = "Bearer";
    option.DefaultChallengeScheme = "Bearer";
}).AddJwtBearer(cfg =>
{
    cfg.RequireHttpsMetadata = false;
    cfg.SaveToken = true;
    cfg.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = authenticationSettings.JwtIssuer,
        ValidAudience = authenticationSettings.JwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authenticationSettings.JwtKey)),
    };
});

/*
   GENERAL CONFIGURATION
*/
var app = builder.Build();

// Data seeder setup
var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<DataSeeder>();

seeder.Seed();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseMiddleware<ErrorHandlingMiddleware>();

app.UseAuthentication();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();