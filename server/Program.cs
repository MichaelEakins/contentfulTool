using Contentful.Core;
using Contentful.Core.Configuration;
using DotNetEnv;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env
Env.Load();

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

// Configure Contentful Delivery client
builder.Services.AddSingleton<IContentfulClient>(sp =>
{
    var options = new ContentfulOptions
    {
        DeliveryApiKey = Environment.GetEnvironmentVariable("CONTENTFUL_DELIVERY_API_KEY"),
        SpaceId = Environment.GetEnvironmentVariable("CONTENTFUL_SPACE_ID")
    };

    return new ContentfulClient(new HttpClient(), options);
});

// Configure Contentful Management client
builder.Services.AddSingleton<IContentfulManagementClient>(sp =>
{
    var options = new ContentfulOptions
    {
        ManagementApiKey = Environment.GetEnvironmentVariable("CONTENTFUL_MANAGEMENT_API_KEY"),
        SpaceId = Environment.GetEnvironmentVariable("CONTENTFUL_SPACE_ID")
    };

    return new ContentfulManagementClient(new HttpClient(), options);
});

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
