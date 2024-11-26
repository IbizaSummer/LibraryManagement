using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LibraryManagement.Data;
using LibraryManagement.Services;

var builder = WebApplication.CreateBuilder(args);

// Add Razor Pages services
builder.Services.AddRazorPages();
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

// Register custom services
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<AuthorService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<LibraryBranchService>();

// Configure database context
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add Identity services for user authentication
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = true;
})
.AddEntityFrameworkStores<AppDbContext>();

// Add third-party authentication (Google & Facebook)
builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
    })
    .AddFacebook(options =>
    {
        options.AppId = builder.Configuration["Authentication:Facebook:ClientId"]!;
        options.AppSecret = builder.Configuration["Authentication:Facebook:ClientSecret"]!;
    });

// Configure Identity options for password, lockout, and user settings
builder.Services.Configure<IdentityOptions>(options =>
{
    // Password settings
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireNonAlphanumeric = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequiredUniqueChars = 1;

    // Lockout settings
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;

    // User settings
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
    options.User.RequireUniqueEmail = false;
});

// Configure application cookie settings
builder.Services.ConfigureApplicationCookie(options =>
{
    // Cookie settings
    options.Cookie.HttpOnly = true;
    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
    options.LoginPath = "/Identity/Account/Login";
    options.AccessDeniedPath = "/Identity/Account/AccessDenied";
    options.SlidingExpiration = true;
});

// Add Swagger for API documentation
builder.Services.AddSwaggerGen();

// Add support for MVC controllers
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Add global exception handling and status code pages
app.UseExceptionHandler("/Home/Error");
app.UseStatusCodePages("text/plain", "Status code page: {0}");

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    // Configure middleware for production environment
    app.UseHsts();
}
else
{
    // Configure middleware for development environment
    app.UseDeveloperExceptionPage();
    app.UseSwagger(); // Enable Swagger middleware
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Library API v1"); // Define the Swagger endpoint
        c.RoutePrefix = "swagger"; // Set Swagger UI to /swagger instead of root
    });
}

// Middleware configuration
app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
app.UseStaticFiles(); // Serve static files (e.g., CSS, JS, images)

app.Use(async (context, next) =>
{
    if (context.Request.Path.Value.Equals("/index.html", StringComparison.OrdinalIgnoreCase))
    {
        context.Response.Redirect("/Home/Index");
        return;
    }
    await next();
});

app.UseRouting(); // Enable routing
app.UseAuthentication(); // Enable user authentication
app.UseAuthorization(); // Enable user authorization

// Configure default route mapping for MVC controllers
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// 配置 API 控制器路由
app.MapControllerRoute(
    name: "api",
    pattern: "api/{controller}/{action=Index}/{id?}");

// Map Razor Pages routes
app.MapRazorPages();

app.Run(); // Run the application