using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using FairShareAPI.Data;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<FairShareContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("FairShareContext") ?? throw new InvalidOperationException("Connection string 'FairShareContext' not found.")));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
