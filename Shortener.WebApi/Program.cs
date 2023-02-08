using System.Reflection;
using Shortener.Application;
using Shortener.Application.Common.Mappings;
using Shortener.Application.Interfaces;
using Shortener.Persistence;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

#region Service registrations
var connectionString = builder.Configuration.GetSection("DbConnection");

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

builder.Services.AddApplication();
builder.Services.AddPersistence(connectionString);

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

app.UseRouting();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
#endregion