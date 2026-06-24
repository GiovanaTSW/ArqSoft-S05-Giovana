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
builder.Services.AddScoped<IMedicoRepository>(_ => new JsonMedicoRepository(jsonMedicos));
builder.Services.AddScoped<ICitaRepository>(_ => new JsonCitaRepository(jsonCitas));

// Servicios de aplicación
builder.Services.AddScoped<PacienteService>();
builder.Services.AddScoped<MedicoService>();

builder.Services.AddScoped<CitaService>(sp =>
{
    var repo = sp.GetRequiredService<ICitaRepository>();
    var service = new CitaService(repo);
    service.AgregarObserver(new CitasApp.Infrastructure.Observers.SmsObserver());
    service.AgregarObserver(new CitasApp.Infrastructure.Observers.EmailObserver());
    return service;
});

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

builder.Services.AddScoped<IPacienteRepository>(sp =>
{
    var env = sp.GetRequiredService<IWebHostEnvironment>();
    var repo = RepositoryFactory.CrearPacienteRepository(
    builder.Environment.EnvironmentName, env);
    return new LoggingPacienteRepository(repo);
});

var app = builder.Build();

app.UseCors();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();