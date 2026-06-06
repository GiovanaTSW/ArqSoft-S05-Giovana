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

```
CitasApp/
├── Controllers/
│   ├── CitaController.cs        # CRUD de citas + filtro por paciente
│   ├── MedicoController.cs      # CRUD de médicos
│   ├── PacienteController.cs    # CRUD de pacientes
│   └── HomeController.cs
├── Interfaces/
│   ├── ICitaRepository.cs
│   ├── IMedicoRepository.cs
│   └── IPacienteRepository.cs
├── Repositories/
│   ├── JsonCitaRepository.cs    # Persistencia en citas.json
│   ├── JsonMedicoRepository.cs  # Persistencia en medicos.json
│   └── JsonPacienteRepository.cs# Persistencia en pacientes.json
├── Models/
│   ├── Cita.cs
│   ├── CitaJson.cs              # DTO para serialización (Fecha/Hora como string)
│   ├── Medico.cs
│   └── Paciente.cs
├── Views/
│   ├── Cita/                    # Index, AgregarCita, Editar, Eliminar, PorPaciente
│   ├── Medico/                  # Index, Detalle, AgregarMedico, Editar, Eliminar
│   ├── Paciente/                # Index, Detalle, AgregarPaciente, Editar, Eliminar
│   └── Shared/                  # Layout, Error
├── data/
│   ├── citas.json
│   ├── medicos.json
│   └── pacientes.json
└── Program.cs
```

---

## Entidades
- **Paciente** — lista y detalle de pacientes registrados
- **Médico** — lista y detalle de médicos disponibles
- **Cita** — agenda completa y filtro por paciente

---

## Persistencia
Archivos JSON en `data/` — sin base de datos.
- `data/pacientes.json`
- `data/medicos.json`
- `data/citas.json`

Los datos se guardan como JSON en la carpeta `data/`. No se requiere ninguna base de datos ni configuración de conexión.
 
**`data/citas.json`** — ejemplo:
```json
[
  {
    "Id": 1,
    "PacienteId": 1,
    "MedicoId": 1,
    "Fecha": "2026-06-01",
    "Hora": "09:00",
    "Motivo": "Consulta general",
    "Estado": "Confirmada"
  }
]
```

---

## Arquitectura
Repositorios por interfaz con inyección de dependencias.
- `Interfaces/` — contratos (`IPacienteRepository`, `IMedicoRepository`, `ICitaRepository`)
- `Repositories/` — implementaciones JSON
- `Models/` — entidades + `CitaJson` como DTO de serialización

---

## Navegación
- `/Paciente` — lista de pacientes
- `/Medico` — lista de médicos
- `/Cita` — agenda completa
- `/Cita/PorPaciente?pacienteId=1` — citas de un paciente específico

---

## Tecnologías

- **ASP.NET Core MVC** (.NET 10)
- **Razor Views** (`.cshtml`)
- **Bootstrap 5** (incluido en `wwwroot/lib/`)
- **System.Text.Json** para serialización
- **jQuery Validation** para validación en cliente