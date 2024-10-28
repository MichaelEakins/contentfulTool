using Contentful.Core;
using Contentful.Core.Configuration;
using DotNetEnv;

var builder = WebApplication.CreateBuilder(args);

// Load environment variables from .env
Env.Load();

// Register controllers and Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Contentful client with options from environment variables
builder.Services.AddSingleton<IContentfulClient>(sp =>
{
    var options = new ContentfulOptions
    {
        DeliveryApiKey = Environment.GetEnvironmentVariable("CONTENTFUL_DELIVERY_API_KEY"),
        SpaceId = Environment.GetEnvironmentVariable("CONTENTFUL_SPACE_ID")
    };

    return new ContentfulClient(new HttpClient(), options);
});

var app = builder.Build();

// Enable Swagger in development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
