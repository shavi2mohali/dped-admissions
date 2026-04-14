using DPEDAdmissionSystem.Data;
using DPEDAdmissionSystem.Models;
using DPEDAdmissionSystem.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddScoped<AuthService>();
builder.Services.AddHttpClient();
builder.Services
    .AddIdentity<ApplicationUser, IdentityRole>(options =>
    {
        options.SignIn.RequireConfirmedAccount = false;
        options.Password.RequireDigit = true;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = true;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequiredLength = 6;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/login";
    options.AccessDeniedPath = "/access-denied";
});

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddCascadingAuthenticationState();
builder.Services.AddMudServices();

builder.Services.AddScoped<IApplicationService, ApplicationService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ICsvExportService, CsvExportService>();
builder.Services.AddScoped<INotificationService, NotificationService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();

app.UseAuthentication();
app.UseAuthorization();

app.MapPost("/account/register", async (
    [FromForm] string name,
    [FromForm] string email,
    [FromForm] string mobileNumber,
    [FromForm] string password,
    [FromForm] string confirmPassword,
    UserManager<ApplicationUser> userManager,
    SignInManager<ApplicationUser> signInManager,
    INotificationService notificationService) =>
{
    if (password != confirmPassword)
    {
        return Results.LocalRedirect("/register?error=Passwords%20do%20not%20match");
    }

    var user = new ApplicationUser
    {
        FullName = name,
        UserName = email,
        Email = email,
        PhoneNumber = mobileNumber,
        EmailConfirmed = true,
        PhoneNumberConfirmed = true
    };

    var result = await userManager.CreateAsync(user, password);
    if (!result.Succeeded)
    {
        var error = Uri.EscapeDataString(string.Join(" ", result.Errors.Select(x => x.Description)));
        return Results.LocalRedirect($"/register?error={error}");
    }

    await userManager.AddToRoleAsync(user, "Student");
    await notificationService.SendAsync(mobileNumber, $"Welcome {name}, your DPED registration account has been created.");
    await signInManager.SignInAsync(user, isPersistent: false);
    return Results.LocalRedirect("/");
});

app.MapPost("/account/login", async (
    [FromForm] string email,
    [FromForm] string password,
    SignInManager<ApplicationUser> signInManager) =>
{
    var result = await signInManager.PasswordSignInAsync(email, password, false, lockoutOnFailure: false);
    return result.Succeeded
        ? Results.LocalRedirect("/")
        : Results.LocalRedirect("/login?error=Invalid%20email%20or%20password");
});

app.MapGet("/account/logout", async (SignInManager<ApplicationUser> signInManager) =>
{
    await signInManager.SignOutAsync();
    return Results.LocalRedirect("/login");
});

app.MapRazorComponents<DPEDAdmissionSystem.Components.App>()
    .AddInteractiveServerRenderMode();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.InitializeAsync(services);
}

app.Run();
