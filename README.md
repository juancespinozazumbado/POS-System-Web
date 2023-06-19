# Projecto programado - Comercio
## Universidad de Costa Rica sede Guanacaste
### IF4101  Lenguajes para aplicaciones comerciales



## Projecto programado 

##Modulos de aplicacion:
### Usuarios

- [ ] Registrar usuarios
- [ ] Login
- [ ] Logout
- [ ] Cambio de clave

### Catalogo de Inventario
 Modulo para administrar los inventarios
 - [ ] Lista 
 - [ ] agregar
 - [ ] editar
 - [ ] detalles

 ### Ajustes de inventario
 Catalogo de ajustes a los inventarios. Permite administrar las existencias.

 - [ ]Ver ajustes
 - [ ] Agregar ajuses ( adicion y disminucion)
 - [ ] Detalle de un ajuste (usuario que lo crreo, fecha)
 
 ### Aperturas de Caja
 Permite manejar las cajas abiertas, y el total de ventas por cada apertura de caja
 - [ ]Crear una apertura de caja
 - [ ]Ver total de ventas de cada caja
 - [ ] Ver cajas por fecha y por usuario.

 ### Ventas 
 Permite administrar las ventas, crear ventas y cerrar la caja.
 - [ ] Crear una venta
 - [ ] agrgar item de inventarios a una venta
 - [ ]eliminar items de inventario a una venta
 - [ ] Litar las ventas de la caja actual.
 - [ ]Terminar una venta





## Tecnologias 

- [ ] [Micosoft dotNet 7.] [https://learn.microsoft.com/en-us/aspnet/core/getting-started/?view=aspnetcore-7.0&tabs=windows]
- [ ] [asp.net MVC.] [https://learn.microsoft.com/en-us/aspnet/core/tutorials/first-mvc-app/start-mvc?view=aspnetcore-7.0&tabs=visual-studio]
- [ ][Entity Framework core v.7][https://learn.microsoft.com/en-us/ef/core/get-started/overview/first-app?tabs=netcore-cli]
- [ ][ASP.NET Core Identity][https://learn.microsoft.com/en-us/aspnet/core/security/authentication/identity?view=aspnetcore-7.0&tabs=visual-studio]
- [ ][Microsoft SQLServer] [https://learn.microsoft.com/en-us/sql/sql-server/what-s-new-in-sql-server-2022?view=sql-server-ver16]

### Architectura
- [][N-layered architecture][https://learn.microsoft.com/en-us/dotnet/architecture/modern-web-apps-azure/common-web-application-architectures]
Capa de modelos (Dominio)
Capa de Bussines logic(aplicacion)
capa de Acceso a datos (EF core)
capa Web o UI (asp.Net MVC)

## Ejecucion

```
cd Invenatrio.UI
dotnet build
dotnet run

```
