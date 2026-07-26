using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using TrainReservationSystem.Data;
using TrainReservationSystem.Services;
using TrainReservationSystem.Services.Api;

var builder = WebApplication.CreateBuilder(args);

// Database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

// MVC
builder.Services.AddControllersWithViews();

// Session (used by Chatbot)
builder.Services.AddSession();

// Cookie Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/Login";
        options.ExpireTimeSpan = TimeSpan.FromHours(8);
        options.SlidingExpiration = true;
    });

builder.Services.AddAuthorization();

// Schedule Microservice
builder.Services.AddHttpClient<IScheduleApiService, ScheduleApiService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5242/");
});

// Booking Microservice
builder.Services.AddHttpClient<IBookingApiService, BookingApiService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5141/");
});

// Special Request Microservice
builder.Services.AddHttpClient<ISpecialRequestApiService, SpecialRequestApiService>(client =>
{
    client.BaseAddress = new Uri("http://localhost:5293/");
});

// Application Services
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<ChatbotService>();
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    DbInitializer.Seed(context);
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseSession();

app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.Run();