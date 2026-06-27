# CitasApp — Rama `api`
 
Aplicación ASP.NET Core MVC para gestionar citas médicas, médicos y pacientes, construida sobre una **Arquitectura Hexagonal (Puertos y Adaptadores)**. Esta rama extiende la rama anterior agregando *Citas.Api* como un segundo adaptador de entrada REST, todo eso sin modificaciones tanto en el dominio, ni la infraestructura.

Este proyecto permite la demostración en la práctica del principio central de la arquitectura hexagonal: **El núcleo: Domain no tiene ninguna referencia a otros proyectos es por eso que no sabe a quién lo llama**

---
 
## Arquitectura Hexagonal
 
Este proyecto sigue el patrón de **Arquitectura Hexagonal**, pues la lógica de dominio vive en el centro del hexágono y se comunica con el exterior únicamente a través de interfaces que actúan como puertos. Los adaptadores son las implementaciones concretas que conectan el mundo exterior con esos puertos.
 
```
                    +------------------+
                    |  CitasApp.Domain |
                    |  (el hexagono)   |
                    |                  |
                    | - Cita           |
                    | - Medico         |
                    | - Paciente       |
                    | - IRepository    |
                    +--------+---------+
                             |
              implementan las interfaces
                             |
                    +--------+---------+
                    | CitasApp.         |
                    | Infrastructure   |
                    | JSON/CSV/SQLite  |
                    +--------+---------+
                             |
              dos adaptadores de ENTRADA lo consumen
                    /                  \
    +--------------+--+          +-----+-------------+
    | CitasApp (Web)  |          | CitasApp.Api      |
    | Adaptador MVC   |          | Adaptador REST    |
    | (puerto web)    |          | (puerto HTTP/JSON)|
    +-----------------+          +-------------------+
```

Lo importante: el agregar *CitasApp-Api* no se tocó ni una línea de *CitasApp.Domain* ni de *CitasApp.Infrastructure*. El nuevo adaptador simplemente implementó el mismo puerto de entrada que ya existía.

### Responsabilidades por capa
 
**`CitasApp.Domain`** — el núcleo del proyecto. Contiene modelos de dominicio (*Cita, Médico, Paciente*) e interfaces de puertos (*ICitaRepository, IMedicoRepository, IPacienteRepository*). Sin dependencias hacia infraestructura ni ASP.NET.

**`CitasApp.Application`** — servicios de aplicacion (`CitaService`, `MedicoService`, `PacienteService`). Orquestan la logica de negocio usando solo las interfaces del dominio.
 
**`CitasApp.Infrastructure`** — adaptadores de salida. Implementa las interfaces del dominio con persistencia en JSON, CSV o SQLite. Reemplazar la base de datos no requiere tocar el dominio.
 
**`CitasApp` (Web)** — adaptador de entrada MVC. Expone la funcionalidad via controladores Razor y vistas Bootstrap.
 
**`CitasApp.Api`** — adaptador de entrada REST. Expone la misma funcionalidad via endpoints HTTP/JSON, usando los mismos servicios de aplicacion.


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
