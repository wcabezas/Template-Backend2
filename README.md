# Template-Backend

# Template para el desarrollo de una API REST como backend utilizando Azure Functions o ASP.NET Core:

## Notas
- Arquitectura en 3 capas (API, lógica de negocios, acceso a datos).
- Ejemplo de ASP.NET Core y Azure Functions compartiendo la misma arquitectura.
- Uso de interfaces e infección de dependencias.
- Configuración de CORS
- Swagger para la documentación de la API 
- Estandariza la respuesta de las solicitudes haciendo un "envelop" con una clase Response.
- Ejemplo de acceso a los valores del appsetting.json (por ejemplo, acceder a la cadena de conexión en la capa de datos).
- Seperación en capas (logica de negocios y acceso a datos)
- Soporte [HATEOAS](https://es.wikipedia.org/wiki/HATEOAS) (Hypermdia as Engine of Application State) 
- Las clases auxiliares para el manejo de Azure Storage y MSAL se movieron a Template.Helpers


## Azure Function V4 (.NET 6)
- El proyecto de Azure Function esta actualizado al runtime V4, utilizando .NET 6 (requiere Visual Studio 2022)
- Incopora un Middleware base que se puede utilizar para guardar información de la sesión. Por ejemplo, tomar un header como el username y pasarlo a las demas clases por inyección de dependencias (clase SessionProvider)
- A diferencia del template anterior, replaza el BaseFunction por una extención del HttpRequestData, simplificando el código

## Entity framework con SQLLite (para pruebas)
- El template esta configurado para utilizar SQLIte por defecto, lo que permite hacer pruebas locales sin requerir configurar un servidor de SQL (Este es soporte es solo para pruebas, en un proyecto se debe configurar Azure SQL o Cosmos DB)

## Para utilizar el entity framework con SQL
- Establecer el proyecto de SqlProvisiong como proyecto de inicio
- En VS, Tools, Nuget Package Manager abrir el Package Manager Console
- Seleccionar como ""Default project"" el de DataAccess (en la barra de la ventana)
- Escribir Add-Migration {nombre de la migración} para agregar las migraciones (se van a crear en el proyecto de DataAccess)
- Escribir Update-Database para ejecutar las migraciones contra la base de datos


## Soporte de Swagger en Azure Function
- El soporte de OpenAPI (swagger) es a través de esta [librería](https://www.nuget.org/packages/Microsoft.Azure.WebJobs.Extensions.OpenApi). 
- Es necesario agregar un archivo de configuración con los detalles de la API (ver en Configuration/OpenApiOptions.cs). 
- Luego ade agregar los atributos necesarios a cada método, al ejecutar la functión se genera un endpoint para ver la UI del swagger.


## Soporte de Swagger en ASP.NET Core
- Viene integrado como parte de Net 5, solo depende que se generé el archivo XML de documentación


## Próximamente
- Incoporar proyectos de TEST y pipelines de cada caso (GitHub, DevOps)
- Ejemplo de Durable Function )no está soportado en NET 5, lo agregaremos al template cuando migregrmos a NET 6).
