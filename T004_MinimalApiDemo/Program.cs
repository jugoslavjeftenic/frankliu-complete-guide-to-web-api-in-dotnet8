using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
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

app.MapGet("/shirts", () =>
{
	return "Reading all the shirts.";
});

app.MapGet("/shirts/{id}", (int id) =>
{
	return $"Reading shirt with id: {id}.";
});

app.MapPost("/shirts", () =>
{
	return "Creating a shirt.";
});

app.MapPut("/shirts/{id}", (int id) =>
{
	return $"Updating shirt with id: {id}.";
});

app.MapDelete("/shirts/{id}", (int id) =>
{
	return $"Deleting shirt with id: {id}.";
});

app.Run();
