using Microsoft.EntityFrameworkCore;
using System.Text;
using BookCatalog.Services;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

// create builder object for registering services and setting up application
var builder = WebApplication.CreateBuilder(args);

// registering controller service -> enables API controllers in app
builder.Services.AddControllers();
// configure Swagger documentation for web api
builder.Services.AddSwaggerGen(c =>
{
    // defining api documentation metadata
    // create Swagger document named v1, titled BookCatalog, API version v1
    c.SwaggerDoc("v1", new() { Title = "BookCatalog", Version = "v1" });
});

// Add Swagger service
builder.Services.AddEndpointsApiExplorer();

// calling SQL database service
builder.Services.AddDbContext<BookCatalog.DataContext>(options =>
{
    // strings connection
    options.UseSqlServer("Data Source=localhost; Initial Catalog=bookcatalog; Integrated Security=true; TrustServerCertificate=true");
});


// building and starting application
var app = builder.Build();

// Configure the HTTP request pipeline
// Add Swagger middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// all HTTP requests are redirected to HTTPS
app.UseHttpsRedirection();

// enables authorization
app.UseAuthorization();

// enables authentication
app.UseAuthentication();

// enables routing controller-based APIs
app.MapControllers();

// run application
app.Run();

