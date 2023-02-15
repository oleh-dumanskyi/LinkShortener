using System.Reflection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Shortener.Application;
using Shortener.Application.Common.Mappings;
using Shortener.Application.Interfaces;
using Shortener.Persistence;

var builder = WebApplication.CreateBuilder(args);

#region Service registrations
ConfigurationManager configuration = builder.Configuration;
builder.Environment.ContentRootPath = Assembly.GetEntryAssembly().Location;
builder.Services.AddApplication();
builder.Services.AddPersistence(configuration);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/api/User/Login";
        opt.LogoutPath = "/api/User/Logout";
    });
    
builder.Services.AddAuthorization();

using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    try
    {
        var context = serviceProvider.GetRequiredService<UrlDbContext>();
        DbInitializer.Initialize(context);
    }
    catch (Exception e)
    {
        
    }
}

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile(new AssemblyMappingProfile(Assembly.GetExecutingAssembly()));
    config.AddProfile(new AssemblyMappingProfile(typeof(IUrlDbContext).Assembly));
});

builder.Services.AddControllersWithViews();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin();
        policy.AllowAnyMethod();
        policy.AllowAnyHeader();
    });
});
#endregion

#region Pipeline
var app = builder.Build();

app.UseRouting();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors("AllowAll");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    //endpoints.MapControllerRoute(name: "default", pattern: "{controller=Home}/{action=Index}/");
});

app.Run();
#endregion