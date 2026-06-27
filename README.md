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
 
## Arquitectura
 
```
CitasApp (Solución)
├── CitasApp.Domain          # Núcleo — entidades e interfaces de puertos
├── CitasApp.Application     # Servicios de aplicación (CitaService, MedicoService, PacienteService)
├── CitasApp.Infrastructure  # Adaptadores — repositorios JSON / CSV / SQLite
├── CitasApp (Web)           # Presentación MVC
└── CitasApp.Api             # Presentación REST
```

### Diagrama de dependencias
 
```
CitasApp.Web  ──┐
                ├──► CitasApp.Application ──► CitasApp.Domain
CitasApp.Api  ──┘         │                        ▲
                           └──► CitasApp.Infrastructure ┘
```

---
 
## Endpoints de la API
 
### Calculadora — `GET /api/calculadora`
 
| Endpoint                              | Descripción            | Ejemplo de respuesta                                      |
|---------------------------------------|------------------------|-----------------------------------------------------------|
| `GET /api/calculadora/sumar?a=5&b=3`  | Suma dos números       | `{ "operacion": "suma", "a": 5, "b": 3, "resultado": 8 }` |
| `GET /api/calculadora/restar?a=5&b=3` | Resta dos números      | `{ "operacion": "resta", "a": 5, "b": 3, "resultado": 2 }` |
| `GET /api/calculadora/multiplicar?a=4&b=2` | Multiplica       | `{ "operacion": "multiplicacion", "a": 4, "b": 2, "resultado": 8 }` |
| `GET /api/calculadora/dividir?a=10&b=2` | Divide (valida ÷0) | `{ "operacion": "division", "a": 10, "b": 2, "resultado": 5 }` |

### Pacientes — `GET /api/pacientes`
 
| Endpoint                    | Descripción                   |
|-----------------------------|-------------------------------|
| `GET /api/pacientes`        | Lista todos los pacientes     |
| `GET /api/pacientes/{id}`   | Obtiene un paciente por ID    |
 
### Médicos — `GET /api/medicos`
 
| Endpoint                  | Descripción                 |
|---------------------------|-----------------------------|
| `GET /api/medicos`        | Lista todos los médicos     |
| `GET /api/medicos/{id}`   | Obtiene un médico por ID    |

### Citas — `GET /api/citas`
 
| Endpoint                              | Descripción                          |
|---------------------------------------|--------------------------------------|
| `GET /api/citas`                      | Lista todas las citas                |
| `GET /api/citas/porpaciente/{id}`     | Filtra citas por ID de paciente      |
 

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
