using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.IdentityModel.Tokens;
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
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// calling SQL database service
builder.Services.AddDbContext<BookCatalog.DataContext>(options =>
{
    // strings connection
    options.UseSqlServer("Data Source=localhost; Initial Catalog=bookcatalog; Integrated Security=true; TrustServerCertificate=true");
});

// adding JWT token authentication in swagger
builder.Services.AddSwaggerGen(options =>
{
    // configuring JWT Bearer authentication using Swagger
    options.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Name = "Authorization", // header name
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http, // http authentication scheme
        Scheme = "Bearer", // token follows bearer scheme
        BearerFormat = "JWT", // token format
        In = Microsoft.OpenApi.Models.ParameterLocation.Header, // token should be sent in request header
        Description = "'Bearer Auth'" // token example
    });

    // configure Swagger 
    // Require JWT Authentication for all endpoints
    options.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {   // security scheme for JWT authentication in SwaggerUI (Swashbuckle)
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    // reference type is security scheme linked to "Bearer" security definition
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    // security scheme name Bearer
                    Id = "Bearer"
                }
            },
            //  creates empty string array
            Array.Empty<string>()
        }

    });
});

// register a TokenService
builder.Services.AddScoped<TokenService>();

// configuring JWT authentication system 
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    // sets up how the application validates incoming JWT tokens
    .AddJwtBearer(options =>
    {
        // defines token validation rules for JWT (JSON Web Token) authentication
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true, // ensures token issuer is valid
            ValidateAudience = true, // ensures token audience is valid
            ValidateLifetime = true,  // ensures token is not expired
            ValidateIssuerSigningKey = true, // ensures token signature is valid
            ValidIssuer = builder.Configuration["Jwt:Issuer"], // expected issuer from configuration
            ValidAudience = builder.Configuration["Jwt:Audience"], // expected audience from configuration
            // secret key for signing in
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
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

