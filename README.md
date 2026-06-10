# CitasApp
 
Aplicación web ASP.NET Core MVC para gestionar citas médicas, médicos y pacientes. Esta versión migra de una **arquitectura monolítica por capas** a una **Arquitectura Hexagonal (Puertos y Adaptadores)**, separando el núcleo del dominio de la infraestructura y la presentación.
 
---
 
## Arquitectura
 
Este proyecto sigue el patrón de **Arquitectura Hexagonal** (también conocido como Puertos y Adaptadores), introducido por Alistair Cockburn. La idea central es que la lógica de dominio se ubica en el centro y se comunica con el exterior únicamente a través de interfaces bien definidas (puertos), con implementaciones concretas (adaptadores) provistas por la capa de infraestructura.
 
```
CitasApp (Solución)
├── CitasApp.Domain          # Núcleo — entidades e interfaces de puertos
├── CitasApp.Infrastructure  # Adaptadores — repositorios en archivos JSON
└── CitasApp (Web)           # Presentación — controladores, vistas, inyección de dependencias
```
 
### Responsabilidades por capa
 
**`CitasApp.Domain`** — el hexágono interno. Contiene:
- Modelos de dominio: `Cita`, `Medico`, `Paciente`
- Interfaces de puertos: `ICitaRepository`, `IMedicoRepository`, `IPacienteRepository`
Este proyecto **no tiene dependencias** hacia infraestructura ni ASP.NET. Define *qué* necesita la aplicación, no *cómo* se hace.
 
**`CitasApp.Infrastructure`** — la capa de adaptadores. Contiene:
- `JsonCitaRepository`, `JsonMedicoRepository`, `JsonPacienteRepository`
Estas clases implementan las interfaces del dominio usando persistencia en archivos JSON. Son el único lugar donde viven las preocupaciones de I/O. Cambiar a una base de datos solo requiere agregar un nuevo adaptador aquí — el dominio no se toca.
 
**`CitasApp` (Web)** — el punto de entrada y capa de presentación. Contiene:
- Controladores MVC y vistas Razor de ASP.NET Core
- Registro de dependencias en `Program.cs`
El proyecto Web depende de `Domain` (para las interfaces) e `Infrastructure` (para registrar las implementaciones concretas). Los controladores dependen únicamente de las interfaces del dominio, nunca directamente de infraestructura.
 
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
