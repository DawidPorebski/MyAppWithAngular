using DevOpsLearningApp.Database;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddCors();

builder.Host.ConfigureAppConfiguration((_, config) =>
{
    config.AddJsonFile("appsettings.json").AddEnvironmentVariables();
});

builder.Services.AddDbContext<MyAppContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("PostgreSQLConnectionString");
    options.UseNpgsql(connectionString);
});

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

app.UseCors(options =>
{
    var webUrl = builder.Configuration.GetValue<string>("WebHost");
    
    options
        .WithOrigins(webUrl)
        .AllowAnyMethod()
        .AllowAnyHeader();
});

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

using (var serviceScope = app.Services.CreateScope())
{
    var context = serviceScope.ServiceProvider.GetRequiredService<MyAppContext>();
    context.Database.Migrate();
}

app.Run();