using CitasApp.Application.Services;
using CitasApp.Domain.Interfaces;
using CitasApp.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Rutas de archivos JSON
var dataFolder = Path.Combine(Directory.GetCurrentDirectory(), "data");
Directory.CreateDirectory(dataFolder);

var jsonPacientes = Path.Combine(dataFolder, "pacientes.json");
var jsonMedicos = Path.Combine(dataFolder, "medicos.json");
var jsonCitas = Path.Combine(dataFolder, "citas.json");

// Repositorios
builder.Services.AddScoped<IPacienteRepository>(_ => new JsonPacienteRepository(jsonPacientes));
builder.Services.AddScoped<IMedicoRepository>(_ => new JsonMedicoRepository(jsonMedicos));
builder.Services.AddScoped<ICitaRepository>(_ => new JsonCitaRepository(jsonCitas));

// Servicios de aplicación
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<MedicoService>();
builder.Services.AddScoped<CitaService>();

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();