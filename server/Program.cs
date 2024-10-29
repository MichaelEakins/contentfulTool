using Contentful.Core;
using Contentful.Core.Configuration;
using DotNetEnv;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env file
Env.Load();

// Register IContentfulClient for content delivery API
builder.Services.AddSingleton<IContentfulClient>(sp =>
{
    var deliveryApiKey = Environment.GetEnvironmentVariable("CONTENTFUL_DELIVERY_API_KEY") ?? throw new InvalidOperationException("CONTENTFUL_DELIVERY_API_KEY not set");
    var spaceId = Environment.GetEnvironmentVariable("CONTENTFUL_SPACE_ID") ?? throw new InvalidOperationException("CONTENTFUL_SPACE_ID not set");
    var options = new ContentfulOptions
    {
        DeliveryApiKey = deliveryApiKey,
        SpaceId = spaceId
    };

    return new ContentfulClient(new HttpClient(), options);
});

// Register IContentfulManagementClient for content management API
builder.Services.AddSingleton<IContentfulManagementClient>(sp =>
{
    var managementApiKey = Environment.GetEnvironmentVariable("CONTENTFUL_MANAGEMENT_API_KEY") ?? throw new InvalidOperationException("CONTENTFUL_MANAGEMENT_API_KEY not set");
    var spaceId = Environment.GetEnvironmentVariable("CONTENTFUL_SPACE_ID") ?? throw new InvalidOperationException("CONTENTFUL_SPACE_ID not set");
    var options = new ContentfulOptions
    {
        ManagementApiKey = managementApiKey,
        SpaceId = spaceId
    };

    return new ContentfulManagementClient(new HttpClient(), options);
});

// Register Controllers, CORS, and Swagger
builder.Services.AddControllers();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Apply CORS
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
