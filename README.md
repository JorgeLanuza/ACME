# ACME Visit Management API

## Descripción del Proyecto

El ACME Visit Management es una aplicación backend desarrollada en .NET que proporciona funcionalidades para gestionar visitas de empleados a clientes. Se basa en principios de arquitectura limpia (Clean architecture) utilizando Entity Framework Core, AutoMapper y un repositorio basado en archivos JSON.

## Tecnologías Utilizadas

- **.NET 8**: Framework principal para la API.
- **Entity Framework Core**: ORM para gestionar la base de datos.
- **AutoMapper**: Librería para mapear entidades y DTOs.
- **Autofac**: Inyección de dependencias.
- **ASP.NET Core Web API**: Exposición de endpoints RESTful.

## Estructura del Proyecto

- **ACME.BL**: Contiene los servicios y la lógica de negocio.
- **ACME.DataAccess**: Contiene la implementación de repositorios, el contexto de base de datos y las entidades.
- **ACME.DependencyInjection**: Registro de instancias y clases.
- **ACME.Dtos**: Define los objetos de transferencia de datos (DTOs).
- **ACME.IoC**: Configura la inyección de dependencias con Autofac.
- **ACME.WebApi**: Contiene los controladores y la configuración de la API.

## Configuración e Instalación

### 1. Clonar el Repositorio
```sh
 git clone https://github.com/JorgeLanuza/ACME.git
 cd ACME
```

### 2. Configurar la Base de Datos

El sistema permite gestionar datos desde archivos JSON. La carpeta **Assets** dentro de `ACME.DataAccess` almacena los archivos de persistencia.

Si deseas usar Entity Framework con una base de datos real, configura la cadena de conexión en `appsettings.json` y ejecuta las migraciones:

```sh
 dotnet ef migrations add InitialCreate
 dotnet ef database update
```

### 3. Ejecutar la Aplicación
Para ejecutar la API localmente:
```sh
 dotnet run --project ACME.WebApi
```

La API estará disponible en `https://localhost:7194/api/VisitController`.

## Endpoints Disponibles

### **VisitController**

- `GET /api/VisitController/GetAllAsync` **Acciones sobre el BBDD**: - Obtiene todas las visitas registradas.
- `GET /api/VisitController/GetFromJsonAsync` **Acciones sobre el Json**: - Obtiene todas las visitas registradas.
- `GET /api/VisitController/GetAllNotDeletedAsync` **Acciones sobre el BBDD**: - Obtiene visitas que no han sido eliminadas.
- `GET /api/VisitController/GetByIdAsync/{id}` **Acciones sobre el BBDD**: - Obtiene una visita por ID.
- `GET /api/VisitController/GetByIdFromJsonAsync/{id}` **Acciones sobre el Json**: - Obtiene una visita por ID.
- `POST /api/VisitController/AddAsync` **Acciones sobre el BBDD**: - Agrega una nueva visita.
- `POST /api/VisitController/AddToJsonAsync` **Acciones sobre el Json**: - Agrega una nueva visita.
- `PUT /api/VisitController/UpdateAsync/{id}` **Acciones sobre el BBDD**: - Actualiza una visita existente.
- `PUT /api/VisitController/UpdateInJsonAsync/{id}` **Acciones sobre el Json**: - Actualiza una visita existente.
- `DELETE /api/VisitController/DeleteAsync/{id}` **Acciones sobre el BBDD**: - Elimina una visita.
- `DELETE /api/VisitController/DeleteFromJson/{id}` **Acciones sobre el Json**: - Elimina una visita.

## Implementaciones Claves

### 1. **Repositorio y Persistencia en JSON**
Los datos de visitas se almacenan en `Assets/visits.json` mediante `VisitJsonRepository`, que permite lectura y escritura de datos de manera persistente.

### 2. **Inyección de Dependencias con Autofac**
El archivo `DependencyInjection.cs` registra los servicios y repositorios en Autofac para su uso en la aplicación.

### 3. **Manejo de Excepciones**
Se implementa un sistema de manejo de errores en los controladores para capturar y loguear excepciones.

### 4. **Buenas Prácticas**
- Uso de **DTOs** para desacoplar la lógica de negocio del modelo de datos.
- **Principio SOLID**, especialmente Inyección de Dependencias.
- **Logging estructurado** para seguimiento de eventos y errores.

Desarrollado por Jorge Pérez de Lanuza para la prueba técnica solicitada sobre el asunto [JOB] [netDeveloper] - 2025.


