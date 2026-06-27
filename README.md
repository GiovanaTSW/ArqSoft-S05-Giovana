# CitasApp — Rama `api-calculadora`
 
Aplicación ASP.NET Core con dos proyectos de entrada: una app web MVC para gestionar citas médicas y una **API REST** (`CitasApp.Api`) que expone los mismos datos vía endpoints HTTP/JSON. Esta rama además incorpora un `CalculadoraController` que demuestra el uso básico de una Web API con parámetros de query.
 
---
 
## Descripción del proyecto
 
CitasApp permite registrar y consultar pacientes, médicos y citas médicas. La lógica de negocio vive en el núcleo de dominio (`CitasApp.Domain`) y se accede desde dos adaptadores de presentación independientes:
 
- **CitasApp** (MVC): interfaz web con vistas Razor y Bootstrap
- **CitasApp.Api** (REST): endpoints JSON consumibles por cualquier cliente HTTP
Ambos proyectos comparten los mismos servicios de aplicación e infraestructura gracias a la **arquitectura hexagonal (Puertos y Adaptadores)**.
 
---
 
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
CitasApp (Solucion)
  CitasApp.Domain          - Nucleo: entidades e interfaces de puertos
  CitasApp.Application     - Servicios de aplicacion
  CitasApp.Infrastructure  - Adaptadores: repositorios JSON / CSV / SQLite
  CitasApp (Web)           - Presentacion MVC
  CitasApp.Api             - Presentacion REST
```
 
### Diagrama de dependencias
 
```
CitasApp.Web  ---+
                 +---> CitasApp.Application ---> CitasApp.Domain
CitasApp.Api  ---+              |                      ^
                                +---> CitasApp.Infrastructure ---+
```
 
---
 
## Endpoints de la API
 
### Calculadora — `GET /api/calculadora`
 
| Endpoint | Descripción | Ejemplo de respuesta |
|---|---|---|
| `GET /api/calculadora/sumar?a=5&b=3` | Suma dos números | `{ "operacion": "suma", "resultado": 8 }` |
| `GET /api/calculadora/restar?a=5&b=3` | Resta dos números | `{ "operacion": "resta", "resultado": 2 }` |
| `GET /api/calculadora/multiplicar?a=4&b=2` | Multiplica | `{ "operacion": "multiplicacion", "resultado": 8 }` |
| `GET /api/calculadora/dividir?a=10&b=2` | Divide (valida div/0) | `{ "operacion": "division", "resultado": 5 }` |
 
### Pacientes — `/api/pacientes`
 
| Endpoint | Descripción |
|---|---|
| `GET /api/pacientes` | Lista todos los pacientes |
| `GET /api/pacientes/{id}` | Obtiene un paciente por ID |
 
### Médicos — `/api/medicos`
 
| Endpoint | Descripción |
|---|---|
| `GET /api/medicos` | Lista todos los médicos |
| `GET /api/medicos/{id}` | Obtiene un médico por ID |
 
### Citas — `/api/citas`
 
| Endpoint | Descripción |
|---|---|
| `GET /api/citas` | Lista todas las citas |
| `GET /api/citas/porpaciente/{id}` | Filtra citas por ID de paciente |
 
---
 
## Estructura del proyecto
 
```
ArqSoft-S05-Giovana-Api-Calculadora/
  CitasApp.Domain/
    Interfaces/
      ICitaRepository.cs
      IMedicoRepository.cs
      IPacienteRepository.cs
    Models/
      Cita.cs
      Medico.cs
      Paciente.cs
  CitasApp.Application/
    Service/
      CitaService.cs
      MedicoService.cs
      PacienteService.cs
  CitasApp.Infrastructure/
    Repositories/
      JsonCitaRepository.cs
      JsonMedicoRepository.cs
      JsonPacienteRepository.cs
      CsvCitaRepository.cs
      CsvMedicoRepository.cs
      CsvPacienteRepository.cs
      SqliteCitaRepository.cs
      SqliteMedicoRepository.cs
      SqlitePacienteRepository.cs
  CitasApp/ (Web)
    Controllers/
    Views/
    Program.cs
  CitasApp.Api/
    Controllers/
      CalculadoraController.cs
      CitasController.cs
      MedicosController.cs
      PacientesController.cs
    Program.cs
```
 
---
 
## Cómo ejecutar
 
**Requisito:** .NET 10 SDK
 
```bash
# Clonar el repositorio y cambiar a la rama
git clone https://github.com/GiovanaTSW/CitasApp.git
cd CitasApp
git checkout api-calculadora
 
# Ejecutar la app MVC
dotnet run --project CitasApp
 
# En otra terminal, ejecutar la API REST
dotnet run --project CitasApp.Api
```
 
La app MVC estará disponible en `https://localhost:5001`.  
La API REST estará disponible en `http://localhost:5071`.
 
**Probar la calculadora desde el navegador:**
 
```
http://localhost:5071/api/calculadora/sumar?a=10&b=5
http://localhost:5071/api/calculadora/dividir?a=20&b=4
```
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


### API REST — Calculadora API funciona
<img width="2076" height="1306" alt="Captura de pantalla 2026-06-27 001105" src="https://github.com/user-attachments/assets/da048093-5eeb-4b3a-bac2-72f8ce2a0e8e" />


### API REST - Calculadora no conecta a API
<img width="2090" height="1306" alt="Captura de pantalla 2026-06-27 001128" src="https://github.com/user-attachments/assets/98490b23-db3c-4c0e-af68-abf8a4e48f30" />


---

## Uso de Inteligencia Artificial

Durante el desarrollo de este proyecto se utilizaron herramientas de inteligencia artificial (Claude de Anthropic) como apoyo en la generación de código, documentación y revisión de estructura. Todo el contenido fue revisado, validado e integrado por la autora del proyecto, Giovana Díaz.
