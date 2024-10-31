using Contentful.Core;
using Contentful.Core.Configuration;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env
Env.Load();

// Register controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations(); // Enables SwaggerOperation annotations
    options.DocInclusionPredicate((_, api) => !string.IsNullOrWhiteSpace(api.GroupName)); // Includes only grouped endpoints
    options.TagActionsBy(api => new[] { api.GroupName ?? "Default" }); // Groups endpoints based on GroupName
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

// Configure Contentful Delivery client with options from environment variables
builder.Services.AddSingleton<IContentfulClient>(sp =>
{
    var options = new ContentfulOptions
    {
        DeliveryApiKey = Environment.GetEnvironmentVariable("CONTENTFUL_DELIVERY_API_KEY"),
        SpaceId = Environment.GetEnvironmentVariable("CONTENTFUL_SPACE_ID")
    };

    return new ContentfulClient(new HttpClient(), options);
});

// Configure Contentful Management client with options from environment variables
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

// Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enable CORS policy
app.UseCors("AllowLocalhost3000");

app.UseAuthorization();
app.MapControllers();

app.Run();
