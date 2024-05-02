
using Microsoft.EntityFrameworkCore;
using task_tracker_group.Services;
using task_tracker_group.Services.Context;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<PasswordService>();
builder.Services.AddScoped<TaskService>();
builder.Services.AddScoped<BoardService>();


var connectionString = builder.Configuration.GetConnectionString("MarcosRTaskTracker");

builder.Services.AddDbContext<DataContext>(Options => Options.UseSqlServer(connectionString));
builder.Services.AddCors(options => options.AddPolicy("TaskTrackerPolicy",
builder =>
{
    builder.WithOrigins("http://localhost:5053", "http://localhost:3000", "https://tasktrackergroup.azurewebsites.net")
    .AllowAnyHeader()
    .AllowAnyMethod();
}));

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

// app.UseHttpsRedirection();

app.UseCors("TaskTrackerPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
