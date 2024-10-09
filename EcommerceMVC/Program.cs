using EcommerceMVC.Data;
using EcommerceMVC.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using EcommerceMVC.Data.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApplicationModels;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<EcommerceDBContext>(options =>
{
	options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultDbConnection"]);
});

builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
	options.IdleTimeout = TimeSpan.FromMinutes(10);
	options.Cookie.HttpOnly = true;
	options.Cookie.IsEssential = true;
});
builder.Services.AddIdentity<User, IdentityRole>()
	.AddEntityFrameworkStores<EcommerceDBContext>().AddDefaultTokenProviders();
builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("RequireAdministratorRole", policy => policy.RequireRole("Administrator"));
	options.AddPolicy("RequireManagerRole", policy => policy.RequireRole("Manager"));
	options.AddPolicy("RequireUserRole", policy => policy.RequireRole("User"));
});

builder.Services.Configure<IdentityOptions>(options =>
{
	options.Password.RequireDigit = true;
	options.Password.RequireLowercase = true;
	options.Password.RequireNonAlphanumeric = false;
	options.Password.RequireUppercase = false;
	options.Password.RequiredLength = 8;
	// options.Password.RequiredUniqueChars = 1;

	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
	options.Lockout.MaxFailedAccessAttempts = 5;
	options.Lockout.AllowedForNewUsers = true;

	options.User.AllowedUserNameCharacters =
		"abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
	options.User.RequireUniqueEmail = false;
});

builder.Services.ConfigureApplicationCookie(options =>
{
	options.Cookie.HttpOnly = true;
	options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

	options.LoginPath = "/Account/Login";
	options.AccessDeniedPath = "/Account/AccessDenied";
	options.SlidingExpiration = true;
});

builder.Services.AddScoped<IBrandService, BrandService>();
builder.Services.AddScoped<ICategoryService, CategoryService>();
builder.Services.AddScoped<EcommerceMVC.Areas.Admin.Services.IProductService, EcommerceMVC.Areas.Admin.Services.ProductService>();
builder.Services.AddScoped<EcommerceMVC.Areas.Admin.Services.ICategoryService, EcommerceMVC.Areas.Admin.Services.CategoryService>();
builder.Services.AddScoped<EcommerceMVC.Areas.Admin.Services.IBrandService, EcommerceMVC.Areas.Admin.Services.BrandService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	await RoleSeeder.SeedRolesAsync(scope.ServiceProvider);
}

app.UseSession();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
	name: "Areas",
	pattern: "{area:exists}/{controller=Product}/{action=Index}/{id?}");

// app.MapControllerRoute(
// 	name: "category",
// 	pattern: "category/{Slug}",
// 	defaults: new {controller="Category", action="Index"});

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");


var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<EcommerceDBContext>();
DatabaseSeeder.SeedingData(context);

app.Run();
