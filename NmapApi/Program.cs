using NmapApi.Business;
using NmapApi.Models;
using NmapApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<NmapDatabaseSettings>(
    builder.Configuration.GetSection("NmapDatabase"));

builder.Services.AddSingleton<INmapService, NmapService>();
builder.Services.AddScoped<INmapProcessingTasks, NmapProcessingTasks>();
builder.Services.AddControllers()
    .AddJsonOptions(
        options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUi(options =>
    {
        options.DocumentPath = "/openapi/v1.json";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
