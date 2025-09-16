# Farma App

Guía rápida para configurar el entorno y entender la estructura del repo.

## Requisitos previos
- Windows 10/11 con Visual Studio 2022 Community 17.11 (o superior).
  - Workloads recomendadas: **ASP.NET and web development**, **.NET Multi-platform App UI (MAUI)** y **.NET Desktop Development**.
- .NET SDK 9.0.x instalado y disponible en la variable de entorno PATH (dotnet --list-sdks debería listar 9.0).
- Herramientas adicionales según el target que quieras compilar en MAUI (Android/iOS/Windows). Visual Studio ofrece los instaladores desde el instalador de workloads.

## Puesta en marcha
1. Clonar el repositorio o sincronizarlo.
2. Abrir Farma.sln desde Visual Studio 2022. Esta solución referencia todos los proyectos del repositorio.
3. Esperar a que Visual Studio restaure los paquetes NuGet automáticamente.
4. Elegir la configuración deseada (Debug/Release) y la plataforma objetivo (Android en caso de Farma.ClientApp).
5. Compilar con Ctrl+Shift+B o desde la pestaña **Build**.

### Ejecutar los proyectos
- **Backend (Farma.Back)**: Ejecutá F5 desde Visual Studio o dotnet run --project Farma.Back/Farma.Back.csproj. Expone una API REST en https://localhost:7254 (según el perfil de lanzamiento) con swagger habilitado.
- **App de pacientes (Farma.ClientApp)**: Proyecto .NET MAUI. Seleccioná el dispositivo/emulador deseado desde la barra de herramientas de Visual Studio y levantalo con F5. Por CLI podés usar dotnet build Farma.ClientApp/Farma.PacienteApp.csproj -f net9.0-android (ajustá el -f para cada plataforma).
- **App web para farmacias (Farma.FarmaciaApp)**: Proyecto Blazor WebAssembly. Ejecutá F5 o dotnet run --project Farma.FarmaciaApp/Farma.FarmaciaApp.csproj y abrí el navegador en la URL indicada por la consola.

> Sugerencia: usá perfiles de inicio múltiples en Visual Studio para levantar backend y frontend en un único clic (Properties de la solución → Startup Project → Multiple startup projects).

## Soluciones y proyectos
- Farma.Back/ — API ASP.NET Core que expone endpoints para gestionar turnos, recetas y stock. Usa Entity Framework Core sobre SQLite y comparte DTOs con el resto de proyectos vía Farma.Shared.
- Farma.ClientApp/ — Aplicación .NET MAUI para pacientes. Contiene vistas, view models y servicios para consumir la API y gestionar turnos desde mobile (Android, iOS, Windows y Mac Catalyst).
- Farma.FarmaciaApp/ — Frontend web en Blazor WebAssembly orientado a farmacias, con componentes MudBlazor y helpers compartidos para consumir la API.
- Farma.Shared/ — Biblioteca de clases con DTOs, enums, constantes y utilidades compartidas entre backend y clientes. Compilarla primero evita warnings cuando cambian los contratos.

## Buenas prácticas
- Mantené sincronizados los contratos compartidos: cualquier cambio en Farma.Shared implica recompilar los demás proyectos.
- Probá los flujos completos corriendo el backend y, al menos, uno de los clientes para validar integraciones.

Con eso deberían poder levantar todo el entorno y empezar a iterar en las apps.
