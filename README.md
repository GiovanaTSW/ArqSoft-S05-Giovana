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
CitasApp.Web  --+
                +---> CitasApp.Application ---> CitasApp.Domain
CitasApp.Api  --+              |                      ^
                               +---> CitasApp.Infrastructure ---+
```
 
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
- ASP.NET Core Web API (*ControllerBase*)
- Bootstrap 5
- Persistencia en archivos JSON, CSV y SQLite

---
 
## Estructura del Proyecto
 
```
ArqSoft-S05-Giovana-Api/
+-- CitasApp.Domain/
|   +-- Interfaces/
|   |   +-- ICitaRepository.cs
|   |   +-- IMedicoRepository.cs
|   |   +-- IPacienteRepository.cs
|   +-- Models/
|       +-- Cita.cs
|       +-- Medico.cs
|       +-- Paciente.cs
+-- CitasApp.Application/
|   +-- Service/
|       +-- CitaService.cs
|       +-- MedicoService.cs
|       +-- PacienteService.cs
+-- CitasApp.Infrastructure/
|   +-- Repositories/
|       +-- JsonCitaRepository.cs / CsvCitaRepository.cs / SqliteCitaRepository.cs
|       +-- JsonMedicoRepository.cs / CsvMedicoRepository.cs / SqliteMedicoRepository.cs
|       +-- JsonPacienteRepository.cs / CsvPacienteRepository.cs / SqlitePacienteRepository.cs
+-- CitasApp/ (Web MVC)
|   +-- Controllers/
|   +-- Views/
|   +-- Program.cs
+-- CitasApp.Api/ (REST)
    +-- Controllers/
    |   +-- CitasController.cs
    |   +-- MedicosController.cs
    |   +-- PacientesController.cs
    +-- Program.cs
```
 
---
 
## Endpoints del adaptador REST
 
### Pacientes
 
| Metodo | Endpoint               | Descripcion                 |
|--------|------------------------|-----------------------------|
| GET    | `/api/pacientes`       | Lista todos los pacientes   |
| GET    | `/api/pacientes/{id}`  | Obtiene un paciente por ID  |

 
### Medicos
 
| Metodo | Endpoint             | Descripcion                |
|--------|----------------------|----------------------------|
| GET    | `/api/medicos`       | Lista todos los medicos    |
| GET    | `/api/medicos/{id}`  | Obtiene un medico por ID   |

 
### Citas
 
| Metodo | Endpoint                        | Descripcion                     |
|--------|---------------------------------|---------------------------------|
| GET    | `/api/citas`                    | Lista todas las citas           |
| GET    | `/api/citas/porpaciente/{id}`   | Filtra citas por ID de paciente |

---
 
 
## Como ejecutar
 
**Requisito:** .NET 10 SDK
 
```bash
git clone https://github.com/GiovanaTSW/CitasApp.git
cd CitasApp
git checkout api
 
# Adaptador MVC
dotnet run --project CitasApp
 
# Adaptador REST (en otra terminal)
dotnet run --project CitasApp.Api
```
 
La app MVC estara disponible en `https://localhost:5001`.  
La API REST estara disponible en `http://localhost:5071`.
 

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

### API REST- Pacientes JSON
<img width="2542" height="1336" alt="Captura de pantalla 2026-06-27 110954" src="https://github.com/user-attachments/assets/32d44c17-2e4c-45d6-80ce-7f90dadd7a93" />


### API REST- Citas JSON
<img width="2558" height="1344" alt="Captura de pantalla 2026-06-27 110909" src="https://github.com/user-attachments/assets/b5bb8df0-ee1a-4f83-9c86-68aac5187611" />


---

## Uso de Inteligencia Artificial

Durante el desarrollo de este proyecto se utilizaron herramientas de inteligencia artificial (Claude de Anthropic) como apoyo en la generacion de codigo, documentacion y revision de estructura. Todo el contenido fue revisado, validado e integrado por la autora del proyecto, Giovana Diaz.
