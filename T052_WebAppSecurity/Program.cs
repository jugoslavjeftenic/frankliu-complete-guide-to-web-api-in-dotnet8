using T052_WebAppSecurity.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddHttpClient("ShirtsApi", client =>
{
	//client.BaseAddress = new Uri("https://localhost:7294/api/v1/");
	client.BaseAddress = new Uri("https://localhost:7229/api/v1/");
	client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddHttpClient("AuthorityApi", client =>
{
	client.BaseAddress = new Uri("https://localhost:7229/");
	client.DefaultRequestHeaders.Add("Accept", "application/json");
});

builder.Services.AddSession(options =>
{
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
	options.IdleTimeout = TimeSpan.FromHours(5);
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IWebApiExecutor, WebApiExecutor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
