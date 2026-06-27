# CitasApp — Rama `gof`

Aplicación web ASP.NET Core MVC para gestionar citas médicas, médicos y pacientes.  
Esta rama extiende la arquitectura hexagonal de la rama anterior integrando tres **patrones de diseño GoF**: Factory Method, Observer y Decorator. También se agrega una API REST (`CitasApp.Api`) y soporte para múltiples adaptadores de persistencia (JSON, CSV, SQLite).
---
 
## Patrones GoF implementados
 
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
 
---
## Stack Tecnológico
 
- .NET 10
- ASP.NET Core MVC
- Bootstrap 5
- Persistencia en archivos JSON (`System.Text.Json`)
---
 
## Estructura del Proyecto
 
```
ArqSoft-S05-Giovana-hexagonal/
├── CitasApp.Domain/
│   ├── Interfaces/
│   │   ├── ICitaRepository.cs
│   │   ├── IMedicoRepository.cs
│   │   └── IPacienteRepository.cs
│   └── Models/
│       ├── Cita.cs
│       ├── Medico.cs
│       └── Paciente.cs
├── CitasApp.Infrastructure/
│   └── Repositories/
│       ├── JsonCitaRepository.cs
│       ├── JsonMedicoRepository.cs
│       └── JsonPacienteRepository.cs
└── CitasApp/ (Web)
    ├── Controllers/
    │   ├── CitaController.cs
    │   ├── MedicoController.cs
    │   ├── PacienteController.cs
    │   └── HomeController.cs
    ├── Views/
    ├── data/
    │   ├── citas.json
    │   ├── medicos.json
    │   └── pacientes.json
    └── Program.cs
```
 
---
 
## Cómo ejecutar
 
**Requisito:** .NET 10 SDK
 
```bash
# Clonar el repositorio
git clone https://github.com/GiovanaTSW/CitasApp.git
cd ArqSoft-S05-Giovana-hexagonal
 
# Ejecutar la aplicación
dotnet run --project CitasApp
```
 
La app estará disponible en `https://localhost:5001` (o el puerto que indique la consola).
 
---
 
## Funcionalidades
 
- CRUD completo de Pacientes, Médicos y Citas
- Filtrar citas por paciente
- Persistencia en archivos JSON (sin base de datos)
- Separación limpia de la lógica de dominio e infraestructura
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
