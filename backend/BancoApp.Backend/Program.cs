using BancoApp.Backend.Data;
using BancoApp.Backend.Services;
using BancoApp.Backend.Routes;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();

// Capa de datos
builder.Services.AddScoped<DatabaseHelper>();

// Capa de servicios
builder.Services.AddScoped<ICuentaService, CuentaService>();
builder.Services.AddScoped<IMovimientoService, MovimientoService>();

// CORS para permitir llamadas desde el frontend
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

// Middleware
app.UseCors();

// Configurar rutas
app.ConfigureRoutes();

app.MapControllers();

app.Run();
