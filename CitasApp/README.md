# CitasApp

Aplicación web de gestión de citas médicas construida con ASP.NET Core MVC (.NET 10). Permite administrar pacientes, médicos y citas a través de una interfaz web, con persistencia en archivos JSON (sin base de datos).

---
## Requisitos
- .NET 10.0
- Visual Strudio Community 2022

---
## Instalación y ejecución
```bash
# Clonar el repositorio
git clone <url-del-repo>
cd ArqSoft-S05-Giovana/CitasApp
 
# Restaurar dependencias y ejecutar
dotnet run
```

La aplicación estará disponible en `https://localhost:7108`

---

## Estructura del proyecto



## Entidades
- **Paciente** — lista y detalle de pacientes registrados
- **Médico** — lista y detalle de médicos disponibles
- **Cita** — agenda completa y filtro por paciente

## Persistencia
Archivos JSON en `data/` — sin base de datos.
- `data/pacientes.json`
- `data/medicos.json`
- `data/citas.json`

## Arquitectura
Repositorios por interfaz con inyección de dependencias.
- `Interfaces/` — contratos (`IPacienteRepository`, `IMedicoRepository`, `ICitaRepository`)
- `Repositories/` — implementaciones JSON
- `Models/` — entidades + `CitaJson` como DTO de serialización

## Navegación
- `/Paciente` — lista de pacientes
- `/Medico` — lista de médicos
- `/Cita` — agenda completa
- `/Cita/PorPaciente?pacienteId=1` — citas de un paciente específico

## Requisitos
- .NET 10.0
- Visual Studio 2022