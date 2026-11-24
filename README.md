# TesisApi (.NET 10 + EF Core 10 + SQL Server)

Sistema de Tesis y Tutores según la pizarra (ejercicio B).

## Modelo
- **Tesis**(id, titulo, estudiante, estado, imagen, idTutor)
- **Tutor**(id, nombre, especialidad, correo)

## Requisitos de la pizarra
| # | Requisito | Dónde |
|---|-----------|-------|
| 1 | POST Tutor | `POST /api/tutores` |
| 2 | POST Tesis | `POST /api/tesis` |
| 3 | Buscar tesis y mostrar tutor | `GET /api/tesis?q=texto` (y `/api/tesis/{id}`) |
| 4 | Subir imágenes | `POST /api/tesis/{id}/imagen` (multipart, campo `archivo`) |
| 5 | Frontend color magenta | `wwwroot/index.html` |
| – | No duplicar tutor | índice único en Correo y Nombre + validación en `TutorService` |
| – | No duplicar tesis | índice único en Titulo + validación en `TesisService` |
| – | Tutor máx. 3 tesis | `Tutor.MaxTesisPorTutor` validado en `TesisService` |

Errores: 400 validación, 404 no encontrado, 409 regla de negocio (duplicado / límite 3).

## Arquitectura
Controllers → Services → Repositories → DbContext, con Factories (creación y mapeo a DTO),
Contratos (carpeta `Contracts/`: repositorios, servicios, factories y almacenamiento), DTOs de entrada/salida y un middleware de excepciones.

## Semillas (10)
4 tutores y 10 tesis (3 + 3 + 3 + 1) que respetan el máximo de 3 por tutor.

## Ejecutar
1. Ajusta `ConnectionStrings:DefaultConnection` en `appsettings.json`
   (ej. `Server=PCB-03;Database=DBexamen;Trusted_Connection=True;TrustServerCertificate=True;`).
2. `dotnet restore && dotnet run`
3. Frontend: `http://localhost:5000/` · Swagger: `/swagger`

La base y las tablas se crean solas al iniciar (`EnsureCreated`) y se cargan las semillas.

## Paquetes (versión 10.0.x)
`Microsoft.EntityFrameworkCore.SqlServer`, `.Design` y `.Tools` en `10.0.*`; `TargetFramework` = `net10.0`.
Si quieres fijar un parche exacto, cambia `10.0.*` en `TesisApi.csproj` (ver con `dotnet ef --version`).
