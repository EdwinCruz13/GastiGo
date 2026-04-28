using Application.DependencyInjection;
using GastiGo.API.Extensions;
using GastiGo.API.Middleware;

using Infrastructure.DependencyInjection;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);

// ================================
// Servicios
// ================================
builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddJwtAuthentication(builder.Configuration);


builder.Services
.AddControllers()
.AddJsonOptions(options =>
 {
     options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
 });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerDocumentation();


//cadenas para variables de conexion
builder.Configuration
       .AddJsonFile("appsettings.json", optional: false)
       .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
       .AddEnvironmentVariables();


//anadiendo cors para permitir peticiones desde el frontend de angular
builder.Services.AddCors(options =>
{
    options.AddPolicy("Angular",
        policy => policy
            .WithOrigins("http://localhost:54941")
            .WithOrigins("http://127.0.0.1:54941")
            .WithOrigins(
                "http://44.213.110.185",
                "http://44.213.110.185:80"
             )
            .AllowAnyHeader()
            .AllowAnyMethod());
});



// ================================
// Construir la aplicación
// ================================
var app = builder.Build();

// Configure the HTTP request pipeline.
    app.UseSwagger();
    app.UseSwaggerUI();


app.UseCors("Angular");
app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthorization();
app.MapControllers();

app.Run();
