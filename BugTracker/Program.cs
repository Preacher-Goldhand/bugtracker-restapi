using BugTracker;
using BugTracker.Entities;
using BugTracker.Middleware;
using BugTracker.Models.AuthenticationDtos;
using BugTracker.Models.Pagination;
using BugTracker.Models.Validators;
using BugTracker.Services;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NLog.Web;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;

/*
    CONFIGURATION OF SERVICES IN THE CONTAINER
*/

var builder = WebApplication.CreateBuilder(args);

// NLog: Setup NLog for Dependency injection
builder.Logging.ClearProviders();
builder.Host.UseNLog();

builder.Services.AddControllers().AddJsonOptions(options =>
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddDbContext<BugTrackerDbContext>();
builder.Services.AddScoped<DataSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<IQuestService, QuestService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IPasswordHasher<Employee>, PasswordHasher<Employee>>();
builder.Services.AddScoped<IValidator<RegisterEmployeeDto>, RegisterEmployeeDtoValidator>();
builder.Services.AddScoped<IValidator<PaginationQuery>, BoardPaginationQueryValidator>();
builder.Services.AddScoped<IValidator<PaginationQuery>, EmployeePaginationQueryValidator>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontEndClient", policyBuilder =>
        policyBuilder.AllowAnyMethod()
        .AllowAnyHeader()
        .WithOrigins(builder.Configuration["AllowedOrigins"]) // example domain for front-end
        );
});

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

app.UseResponseCaching();
app.UseStaticFiles();
app.UseCors("FrontEndClient");

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