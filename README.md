# CitasApp — Rama `hexagonal`
 
Aplicación web ASP.NET Core MVC para gestionar citas médicas, médicos y pacientes, construida sobre **Arquitectura Hexagonal (Puertos y Adaptadores)**. Esta rama migra de una arquitectura monolítica por capas a un diseño donde el núcleo de dominio está completamente aislado de la infraestructura y la presentación.
 
---
 
## Arquitectura Hexagonal
 
Este proyecto sigue el patrón introducido por Alistair Cockburn. La lógica de dominio vive en el centro del hexágono y se comunica con el exterior únicamente a través de **puertos** (interfaces). Los **adaptadores** son las implementaciones concretas que conectan el mundo exterior con esos puertos.
 
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
                    +--------+---------+
                    | CitasApp (Web)   |
                    | Adaptador MVC    |
                    | (puerto entrada) |
                    +------------------+
```
 
### Responsabilidades por capa
 
**`CitasApp.Domain`** — el hexagono interno. Contiene modelos de dominio (`Cita`, `Medico`, `Paciente`) e interfaces de puertos (`ICitaRepository`, `IMedicoRepository`, `IPacienteRepository`). Sin dependencias hacia infraestructura ni ASP.NET.
 
**`CitasApp.Application`** — servicios de aplicacion (`CitaService`, `MedicoService`, `PacienteService`). Orquestan la logica de negocio usando solo las interfaces del dominio.
 
**`CitasApp.Infrastructure`** — adaptadores de salida. Implementa las interfaces del dominio. Esta rama incluye tres adaptadores intercambiables sin tocar el dominio:
 
- `JsonPacienteRepository`, `JsonMedicoRepository`, `JsonCitaRepository`
- `CsvPacienteRepository`, `CsvMedicoRepository`, `CsvCitaRepository`
- `SqlitePacienteRepository`, `SqliteMedicoRepository`, `SqliteCitaRepository`

**`CitasApp` (Web)** — adaptador de entrada MVC. Expone la funcionalidad via controladores y vistas Razor. En `Program.cs` se elige que adaptador de persistencia se inyecta, sin modificar nada mas.
 
### Diagrama de dependencias
 
```
CitasApp.Web ---> CitasApp.Application ---> CitasApp.Domain
                         |                       ^
                         +---> CitasApp.Infrastructure ---+
```

---
 
## Intercambio de persistencia

Para este proyecto se integró diferentes formas para aplicar la persistencia que son:
- Formato JSON
- Formato csv
- Formato SQLite

El adaptador activo se controla con un solo comentario en 'Program.cs':

```csharp
// Bloque A - JSON
// builder.Services.AddSingleton<IPacienteRepository>(_ => new JsonPacienteRepository(...));
 
// Bloque B - CSV  <-- activo por defecto
builder.Services.AddSingleton<IPacienteRepository>(_ => new CsvPacienteRepository(...));
 
// Bloque C - SQLite
// builder.Services.AddSingleton<IPacienteRepository>(_ => new SqlitePacienteRepository(...));
```

El dominio y los servicios de CitasApp.Aplication no se tocan, sólo se realiza el cambio en Program.cs.

---

## Migración Arquitectónica
 
| Aspecto              | Anterior (Capas)                    | Actual (Hexagonal)                    |
|----------------------|-------------------------------------|---------------------------------------|
| Estructura           | Proyecto unico, carpetas por capa   | Cuatro proyectos separados            |
| Aislamiento dominio  | Dominio mezclado con infraestructura| Domain sin dependencias externas      |
| Ubicacion interfaces | Capa de infraestructura             | Capa de dominio (puertos)             |
| Cambio persistencia  | Requiere refactorizar controladores | Solo se comenta/descomenta un bloque  |
| Testabilidad         | Dificil de mockear                  | Se inyecta cualquier adaptador via DI |
 
---

## Stack Tecnológico
 
- .NET 10
- ASP.NET Core MVC
- `System.Text.Json`
- Persistencia en JSON, CSV y SQLite (intercambiable)

---
 

 
---
 
 
---
 

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
