using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MyRepos.Domain.Dictionary;
using MyRepos.Domain.Interfaces;
using MyRepos.Infrastructure.Base;
using MyRepos.Infrastructure.Repositories;
using MyRepos.Infrastructure.Service;
using MyRepos.Presentation.Data;
using MyRepos.Presentation.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<IGitHubRepository, GitHubRepository>();

Dictionary.GitHubAPIBase = builder.Configuration["ServiceUrls:Github"];
Dictionary.ClientId = builder.Configuration["OAuth:client_id"];
Dictionary.ClientSecret = builder.Configuration["OAuth:client_secret"];
Dictionary.OAuthURL = builder.Configuration["OAuth:OAuthUrl"];

builder.Services.AddScoped<IGitHubRepository, GitHubRepository>();
builder.Services.AddScoped<IBaseService, BaseService>();
builder.Services.AddScoped<ITokenProvider, TokenProvider>();
builder.Services.AddScoped<IOAuthRepository, OAuthRepository>();

builder.Services.AddDefaultIdentity<User>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapRazorPages();

app.Run();
