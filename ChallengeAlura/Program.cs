using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ChallengeAlura.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ChallengeAluraContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ChallengeAluraContext") ?? throw new InvalidOperationException("Connection string 'ChallengeAluraContext' not found.")));

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
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

app.MapControllers();

app.Run();
