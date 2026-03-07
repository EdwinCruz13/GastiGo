using Application.DependencyInjection;
using GastiGo.API.Extensions;
using GastiGo.API.Middleware;

using Infrastructure.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// ================================
// Servicios
// ================================
builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddJwtAuthentication(builder.Configuration);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerDocumentation();


var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseAuthorization();
app.MapControllers();
app.Run();
