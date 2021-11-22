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


## Para utilizar el entity framework
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
- Migración el template a NET 6.
- Incoporar proyectos de TEST y pipelines de cada caso (GitHub, DevOps)
- Ejemplo de Durable Function )no está soportado en NET 5, lo agregaremos al template cuando migregrmos a NET 6).
