# CitasApp — Rama `deuda-tecnica`

Aplicación web ASP.NET Core MVC para gestionar citas médicas, médicos y pacientes.  
Esta rama extiende la arquitectura hexagonal de la rama anterior integrando tres un diagrama de los niveles C4.

 
## Deuda técnica dentro del proyecto
 
### Factory Method — `RepositoryFactory`
Centraliza la creación de repositorios según el entorno de ejecución. El código que consume los repositorios no necesita conocer qué implementación concreta se instancia.
 
```
CitasApp.Infrastructure/
└── Repositories/
    └── RepositoryFactory.cs   ← fábrica de IPacienteRepository, IMedicoRepository, ICitaRepository
```
 
### Observer — notificaciones de citas
Al crear o modificar una cita, se notifica automáticamente a todos los observadores registrados (SMS y Email). El servicio de dominio no depende de los observadores concretos, solo de la interfaz `ICitaObserver`.

```
CitasApp.Domain/Interfaces/
└── ICitaObserver.cs
 
CitasApp.Infrastructure/Observers/
├── EmailObserver.cs
└── SmsObserver.cs
```

### Decorator — `LoggingPacienteRepository`
Envuelve cualquier implementación de `IPacienteRepository` y agrega logging con timestamp sin modificar la implementación base. Se puede apilar sobre JSON, CSV o SQLite sin cambiar el dominio.
 
```
CitasApp.Infrastructure/Repositories/
└── LoggingPacienteRepository.cs
```
 
## Arquitectura

📐 Diagrama de componentes (C4 Nivel 3) del contenedor `CitasApp.Api`: [arquitectura.md](./arquitectura.md)
 
```
CitasApp (Solución)
├── CitasApp.Domain          # Núcleo — entidades, interfaces de puertos y ICitaObserver
├── CitasApp.Infrastructure  # Adaptadores — JSON / CSV / SQLite + Factory + Observers + Decorator
├── CitasApp (Web)           # Presentación MVC — controladores, vistas, DI
└── CitasApp.Api             # API REST — controladores ControllerBase, endpoints HTTP
```

### Diagrama de dependencias
 
```
CitasApp.Web ──────────────► CitasApp.Domain
      │                            ▲
      └──► CitasApp.Infrastructure ┘
 
CitasApp.Api ──────────────► CitasApp.Domain
      │                            ▲
      └──► CitasApp.Infrastructure ┘
```

---
 
## Stack tecnológico
 
- .NET 10
- ASP.NET Core MVC + Web API
- Bootstrap 5
- Persistencia: JSON (`System.Text.Json`), CSV, SQLite
- Patrones: Factory Method, Observer, Decorator (GoF)
---

## Estructura del proyecto
 
```
ArqSoft-S05-Giovana-GOF/
├── CitasApp.Domain/
│   ├── Interfaces/
│   │   ├── ICitaObserver.cs
│   │   ├── ICitaRepository.cs
│   │   ├── IMedicoRepository.cs
│   │   └── IPacienteRepository.cs
│   └── Models/
│       ├── Cita.cs
│       ├── Medico.cs
│       └── Paciente.cs
├── CitasApp.Infrastructure/
│   ├── Observers/
│   │   ├── EmailObserver.cs
│   │   └── SmsObserver.cs
│   └── Repositories/
│       ├── RepositoryFactory.cs         ← Factory Method
│       ├── LoggingPacienteRepository.cs ← Decorator
│       ├── JsonCitaRepository.cs
│       ├── JsonMedicoRepository.cs
│       ├── JsonPacienteRepository.cs
│       ├── CsvCitaRepository.cs
│       ├── CsvMedicoRepository.cs
│       ├── CsvPacienteRepository.cs
│       ├── SqliteCitaRepository.cs
│       ├── SqliteMedicoRepository.cs
│       └── SqlitePacienteRepository.cs
├── CitasApp.Application/
│   └── Service/
│       ├── CitaService.cs
│       ├── MedicoService.cs
│       └── PacienteService.cs
├── CitasApp/ (Web)
│   ├── Controllers/
│   ├── Views/
│   └── Program.cs
└── CitasApp.Api/
    ├── Controllers/
    │   ├── CitasController.cs
    │   ├── MedicosController.cs
    │   └── PacientesController.cs
    └── Program.cs
```

## Cómo ejecutar
 
**Requisito:** .NET 10 SDK
 
```bash
# Clonar el repositorio y cambiar a la rama gof
git clone https://github.com/GiovanaTSW/CitasApp.git
cd CitasApp
git checkout gof
 
# Ejecutar la app MVC
dotnet run --project CitasApp
 
# O ejecutar la API REST
dotnet run --project CitasApp.Api
```
 
La app MVC estará disponible en `https://localhost:5001`.  
La API REST estará disponible en `https://localhost:7030`.


---
 
## Funcionalidades
 
- CRUD completo de Pacientes, Médicos y Citas (MVC y API REST)
- Notificaciones por Email y SMS al crear/modificar citas (Observer)
- Logging automático de operaciones sobre pacientes (Decorator)
- Selección de repositorio por entorno (Factory Method)
- Soporte para persistencia en JSON, CSV y SQLite
- Arquitectura hexagonal con puertos y adaptadores

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

### ACITIVIDAD GOF
<img width="2560" height="1600" alt="GET en navegador_Giovana Díaz" src="https://github.com/user-attachments/assets/af304a27-f8f3-4459-b364-05828f2d079b" />

<img width="2560" height="1600" alt="GET en navegador paciente 6_Giovana Díaz" src="https://github.com/user-attachments/assets/f189b55a-9eb9-49dc-a7a3-0a28c95ffacf" />

<img width="2560" height="1600" alt="EndpointPOST en Powershell_Giovana Díaz" src="https://github.com/user-attachments/assets/e3fa123e-d813-43ff-970d-53fbb3209175" />

<img width="2560" height="1600" alt="Demostración de registros GET y POST_Giovana Díaz" src="https://github.com/user-attachments/assets/fa0341ed-f7ff-4688-a141-57bac4920c23" />


---

## Uso de Inteligencia Artificial

Durante el desarrollo de este proyecto se utilizaron herramientas de inteligencia artificial (Claude de Anthropic) como apoyo en la generación de código, documentación y revisión de estructura. Todo el contenido fue revisado, validado e integrado por la autora del proyecto, Giovana Díaz.
