using Contentful.Core;
using Contentful.Core.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Register controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Configure Swagger with grouping and annotations
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Contentful API",
        Version = "v1",
        Description = "API for interacting with Contentful content types and assets"
    });
});

// Configure CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalhost3000", policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

// Bind Contentful options from appsettings.json
builder.Services.Configure<ContentfulOptions>(builder.Configuration.GetSection("Contentful"));

// Configure Contentful Delivery client using options from appsettings.json
builder.Services.AddSingleton<IContentfulClient>(sp =>
{
    var options = sp.GetRequiredService<IOptions<ContentfulOptions>>().Value;
    return new ContentfulClient(new HttpClient(), options);
});

// Configure Contentful Management client using options from appsettings.json
builder.Services.AddSingleton<IContentfulManagementClient>(sp =>
{
    var options = sp.GetRequiredService<IOptions<ContentfulOptions>>().Value;
    return new ContentfulManagementClient(new HttpClient(), options);
});

builder.Configuration
       .SetBasePath(Directory.GetCurrentDirectory())
       .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
       .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
       .AddEnvironmentVariables();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Contentful API V1");
        c.DefaultModelsExpandDepth(-1);
    });
}

app.UseCors("AllowLocalhost3000");
app.UseAuthorization();
Console.WriteLine("Mapping controllers...");
app.MapControllers();

app.Run();
