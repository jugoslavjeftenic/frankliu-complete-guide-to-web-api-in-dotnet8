var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseRouting();
//app.UseAuthentication();
//app.UseAuthorization();

// Routing
// Create "/shirts"
app.MapPost("/shirts", () =>
{
	return $"Create a shirt.";
});

// Read "/shirts"
app.MapGet("/shirts", () =>
{
	return $"Read all shirts.";
});

// Read "/shirts/{id}"
app.MapGet("/shirts/{id}", (int id) =>
{
	return $"Read a shirt with Id:{id}.";
});

// Update "/shirts/{id}"
app.MapPut("/shirts/{id}", (int id) =>
{
	return $"Update a shirt with Id:{id}.";
});

// Delete "/shirts/{id}"
app.MapDelete("/shirts/{id}", (int id) =>
{
	return $"Delete a shirt with Id:{id}.";
});

app.Run();
