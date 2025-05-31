using EHMSWebApp.Interface;
using EHMSWebApp.Services;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Data.SqlClient;
using Microsoft.Identity.Web;
using System.Data;
using Serilog;
using Microsoft.Extensions.Configuration;
using EHMSWebApp.DesignPattern;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();

builder.Services.AddTransient<IDbConnection>(sp => new SqlConnection(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IEmployeePhysicalFitnessService, EmployeePhysicalFitnessService>();
builder.Services.AddScoped<IEmployeeHealthInfoService, EmployeeHealthInfoService>();
builder.Services.AddScoped<IRequestForHelpService, RequestForHelpService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddScoped<IDepartmentServiceFactory, DepartmentServiceFactory>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});

builder.Services.AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("1"));
    options.AddPolicy("HR", policy => policy.RequireRole("2"));
    options.AddPolicy("Employee", policy => policy.RequireRole("3"));
});

builder.Services.AddServerSideBlazor().AddMicrosoftIdentityConsentHandler();


builder.Services.Configure(OpenIdConnectDefaults.AuthenticationScheme, (Action<OpenIdConnectOptions>)(options =>
{
    options.ClientId = builder.Configuration.GetSection("AzureAd").GetValue<string>("ClientId");
    options.Events.OnSignedOutCallbackRedirect = context =>
    {
        context.HttpContext.Response.Redirect(context.Properties?.RedirectUri ?? context.Options.SignedOutRedirectUri);
        context.HandleResponse();
        return Task.CompletedTask;
    };
}));

builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string? logDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "logs");
if (!Directory.Exists(logDirectory))
{
    Directory.CreateDirectory(logDirectory);
}
Log.Logger = new LoggerConfiguration()
    .WriteTo.Console() // Logs to console
    .WriteTo.File(Path.Combine(logDirectory, "log.txt"), rollingInterval: RollingInterval.Day) 
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog();
builder.Host.UseSerilog();


var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseCors("AllowAll");
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();



app.MapControllers();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Use(async (context, next) =>
{
    context.Response.Headers["X-Xss-Protection"] = "1; mode=block";
    context.Response.Headers["Content-Security-Policy"] = "frame-ancestors 'self';";
    context.Response.Headers["X-Frame-Options"] = "DENY";
    await next();
});
app.Run();
