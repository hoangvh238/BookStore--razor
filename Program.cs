using BookStore.Models;
using BookStore.Untils;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<IdentityOptions>(options =>
{
	// Default Password settings.
	options.Password.RequireDigit = true;
	options.Password.RequiredLength = 8;
	options.Password.RequireNonAlphanumeric = true;
	options.Password.RequireUppercase = true;
	options.Password.RequireLowercase = true;
	options.Password.RequiredUniqueChars = 6;

	// Lockout settings
	options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
	options.Lockout.MaxFailedAccessAttempts = 5;
	options.Lockout.AllowedForNewUsers = true;

	// User settings
	options.User.RequireUniqueEmail = true;


	//Sign-in
	options.SignIn.RequireConfirmedEmail = true;
});

builder.Services.AddAuthorization(options =>
{
	options.AddPolicy("RequireAdminRole", policy => policy.RequireRole("Staff"));
});

builder.Services.AddAuthentication(options =>
{
	options.DefaultScheme = "Cookies"; // Change based on your authentication scheme
	options.DefaultChallengeScheme = "Cookies";
}).AddCookie("Cookies", options =>
{
	options.AccessDeniedPath = "/Account/AccessDenited"; // Custom access denied path if needed
	options.LoginPath = "/Account/AccessDenited"; // Path to your login page
});

builder.Services.AddRazorPages(options =>
{
	options.Conventions.AuthorizeFolder("/Admin", "RequireAdminRole");
	// Add more folders as needed
});

builder.Services.Configure<PasswordHasherOptions>(options =>
	options.CompatibilityMode = PasswordHasherCompatibilityMode.IdentityV2
);


// Add services to the container.
builder.Services.AddRazorPages();

// Configure the database context
builder.Services.AddDbContext<BookStoreContext>(options =>
	options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Configure Identity to use ApplicationUser
builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
	.AddRoles<IdentityRole>()    // Enable Identity Role
	.AddEntityFrameworkStores<BookStoreContext>();


builder.Services.AddSession(options =>
	  {
		  options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
		  options.Cookie.HttpOnly = true;
		  options.Cookie.IsEssential = true; // Make the session cookie essential
	  });

builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios.
	app.UseHsts();
}



using (var scope = app.Services.CreateScope())
{
	var services = scope.ServiceProvider;
	var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
	var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
	await RoleSeed.InitializeAsync(services, userManager, roleManager);
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
