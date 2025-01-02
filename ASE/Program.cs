using ASE.Data; // Import your data namespace
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Enable reference handling to prevent object cycles during serialization
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve;
        options.JsonSerializerOptions.WriteIndented = true; // Optional: Pretty print JSON
    });

// Configure Swagger/OpenAPI for API documentation and testing
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure Entity Framework with SQL Server connection
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    // Enable Swagger UI for development mode
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection(); // Ensure HTTPS is used
app.UseAuthorization(); // Enable authorization middleware

// Map controllers to endpoints
app.MapControllers();

// Run the application
app.Run();
