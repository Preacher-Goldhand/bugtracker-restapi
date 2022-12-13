using BugTracker.Entities;
using BugTracker.Middleware;
using BugTracker.Services;
using Microsoft.AspNetCore.Identity;
using System.Reflection;

/*
    CONFIGURATION OF SERVICES IN THE CONTAINER
*/
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddDbContext<BugTrackerDbContext>();
builder.Services.AddScoped<DataSeeder>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddScoped<IBoardService, BoardService>();
builder.Services.AddScoped<IQuestService, QuestService>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IAcountService, AccountService>();
builder.Services.AddScoped<IPasswordHasher<Employee>, PasswordHasher<Employee>>();
builder.Services.AddScoped<ErrorHandlingMiddleware>();
builder.Services.AddScoped<RequestTimeMiddleware>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
//app.UseMiddleware<RequestTimeMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();