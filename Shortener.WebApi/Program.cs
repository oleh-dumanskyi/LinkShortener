using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Shortener.Application;
using Shortener.Application.Common.Mappings;
using Shortener.Application.Interfaces;
using Shortener.Persistence;
using System.Reflection;
using Shortener.Application.Urls.Commands.CreateUrl;

#region Service registrations
var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;
builder.Environment.ContentRootPath = Assembly.GetEntryAssembly().Location;
builder.Services.AddApplication();
builder.Services.AddPersistence(configuration);
builder.Services.AddAuthorization();

builder.Services.AddHttpClient<CreateUrlCommandHandler>();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.LoginPath = "/User/Login";
        opt.LogoutPath = "/User/Logout";
        opt.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

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

app.UseStaticFiles();
app.UseCors("AllowAll");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
#endregion