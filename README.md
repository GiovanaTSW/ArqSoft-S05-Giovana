# CitasApp — Rama `api-calculadora`
 
Aplicación ASP.NET Core con dos proyectos de entrada: una app web MVC para gestionar citas médicas y una **API REST** (`CitasApp.Api`) que expone los mismos datos vía endpoints HTTP/JSON. Esta rama además incorpora un `CalculadoraController` que demuestra el uso básico de una Web API con parámetros de query.
 
---
 
## Descripción del proyecto
 
CitasApp permite registrar y consultar pacientes, médicos y citas médicas. La lógica de negocio vive en el núcleo de dominio (`CitasApp.Domain`) y se accede desde dos adaptadores de presentación independientes:
 
- **CitasApp** (MVC): interfaz web con vistas Razor y Bootstrap
- **CitasApp.Api** (REST): endpoints JSON consumibles por cualquier cliente HTTP
Ambos proyectos comparten los mismos servicios de aplicación e infraestructura gracias a la **arquitectura hexagonal (Puertos y Adaptadores)**.


```
 
## Tecnologías usadas
 
- .NET 10
- ASP.NET Core MVC
- ASP.NET Core Web API (`ControllerBase`)
- Bootstrap 5
- `System.Text.Json`
- Persistencia en archivos JSON, CSV y SQLite

--- 
 
### Diagrama de dependencias
 
```
CitasApp.Web ──────────────► CitasApp.Domain
      │                            ▲
      └──► CitasApp.Infrastructure ┘
```
 
Infrastructure implementa las interfaces de Domain. Web depende de ambos, pero los controladores solo interactúan con los puertos del Domain.
 
---
 
## Migración Arquitectónica
 
| Aspecto | Anterior (Capas) | Actual (Hexagonal) |
|---|---|---|
| Estructura | Proyecto único, carpetas por capa | Tres proyectos separados |
| Aislamiento del dominio | Dominio mezclado con infraestructura | Domain no tiene dependencias externas |
| Ubicación de interfaces | Capa de infraestructura | Capa de dominio (puertos) |
| Cambio de persistencia | Requiere refactorizar controladores | Solo se reemplaza el adaptador |
| Testabilidad | Difícil de mockear | Se inyecta cualquier adaptador vía DI |
 
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
