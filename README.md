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

---

## Capturas de pantalla

### Home
<img width="2538" height="1336" alt="Captura de pantalla 2026-06-05 225053" src="https://github.com/user-attachments/assets/5da775e7-3db0-4c59-9247-fd66d5c0f2e0" />

### Pacientes
<img width="2508" height="1334" alt="Captura de pantalla 2026-06-05 225118" src="https://github.com/user-attachments/assets/5a6af793-779f-4b44-81c7-3137d1aa6fd2" />

### Médicos
<img width="2544" height="1340" alt="Captura de pantalla 2026-06-05 225137" src="https://github.com/user-attachments/assets/194a88e2-370f-47a3-ae0d-09734a789704" />

### Citas
<img width="2546" height="1330" alt="Captura de pantalla 2026-06-05 225149" src="https://github.com/user-attachments/assets/80dda4ba-281b-4174-b150-5457d439cc67" />

### Privacy
<img width="2504" height="1334" alt="Captura de pantalla 2026-06-05 225209" src="https://github.com/user-attachments/assets/fc54ba7d-5a9c-4f62-9dc8-00e3a024cd4e" />

---

## Uso de Inteligencia Artificial

Durante el desarrollo de este proyecto se utilizaron herramientas de inteligencia artificial
(Claude de Anthropic) como apoyo en la generación de código, documentación y revisión de
estructura. Todo el contenido fue revisado, validado e integrado por la autora del proyecto, Giovana Díaz.
