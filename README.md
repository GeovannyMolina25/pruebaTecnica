![image](https://github.com/user-attachments/assets/d073329a-d3f7-412d-b5ea-1128b861dfa7)# pruebaTecnica
Este proyecto fue realizado con las tecnologias de ASP .NET 8, Angular 19 y Microsoft SQL Server 2019

**Requisitos**
Para ejecutar el proyecto en un entorno local necesitas:

- .NET 6 SDK o superior instalado (para el backend), puede descargarlo en https://dotnet.microsoft.com/download

- Node.js (versión 14 o superior) y Angular CLI (para el frontend).
- Descargar Node.js: https://nodejs.org/
- Instalar Angular CLI: npm install -g @angular/cli
- SQL Server o cualquier motor compatible para la base de datos.
- Visual Studio 2022 (recomendado) o Visual Studio Code para edición.
- Git para control de versiones.

Ejecución del Backend
Primero lo debemos de descargar del repositorio del Git hub https://github.com/GeovannyMolina25/pruebaTecnica.git
directamente puedes abrir la terminal en donde desees el proyecto : git clone https://github.com/GeovannyMolina25/pruebaTecnica.git
- y entramos a la carpeta :
cd pruebaTecnica/backend/appInventario
Una vez adentro podemos poner en la terminal para poder ejecutar el proyecto
-dotnet restore
-dotnet build
- dotnet run
o podemos entrar ala carpeta del backent pruebaTecnica/backend/appInventario desde Visual estudio 2022 y presionar el boton inicial :
![image](https://github.com/user-attachments/assets/ed588a87-129f-4aed-b57d-805eb552c887)

Despues debemos conectar nuestra base de datos en nuestra sulucion podemos ver nuestros proyectos los cuales usan los dos asincronicamente la base de datos, en el explorador de soluciones estara asi :
![image](https://github.com/user-attachments/assets/62b334fc-9f31-4e78-b820-f5d27fad1f25)
- nosotros debemos ir al documneto llamado **appsettings.json** abrimos el documento y debemos poner nuestras credenciales :
![image](https://github.com/user-attachments/assets/d578392f-c25c-4c4a-913c-aef1939f5c42)

Encontraremos algo parecido, y nosotros debemos de cambiar la infomacion que tiene nuestra cadena :
Server=DESKTOP-SGA0PI5;Database=appInventarioDB;User Id=Nelson;Password=123456;TrustServerCertificate=True
como el **Server**, **Database**, **User**, **Id** y **Password** esto lo podemos encontrar en nuestro Microsotf SQL server al momento de inicial seccion:
![image](https://github.com/user-attachments/assets/ac9d692c-9221-412e-bdb0-b8f3570556d0)

**Nota** recuerda que debes hacer esto con los dos proyectos 
por ultimo para correr el proyecto, debemos hacer que si inician los dos proyectos en nuestra solucion, para eso nos vamos a la parte de arriba de nuestro visual estudio 2022, abrimos la pestaña que esta alado del boton inicial:

![image](https://github.com/user-attachments/assets/5f71f9b2-c896-43be-aeb6-12e9d5dc8a0d)

Le damos en **Configurar proyecto de inicio** y nos saldra la siguiente pantalla:
![image](https://github.com/user-attachments/assets/e40dccba-95f6-44a1-9fbc-20d143d1d16c)
aqui debemos poner la siguiente configuracion para que se pueda ejecutar los dos proyectos :
![image](https://github.com/user-attachments/assets/d6cb706a-0c2d-4288-95ef-bf7c780468cf)

y cerramos una vez cerrado debemos inicial el proyecto dandole en el boton **Iniciar** "La fecha verde"
![image](https://github.com/user-attachments/assets/22a176d1-cf91-4746-a4aa-ee3c3f89dc40)
Comenzara a correr nuestro proyecto 
**Importante**: No olvidarse que nosotros utilizamos los paquetes entity Framework Core, ntity Framework Core SqlServer, ntity Framework Core Sqlite diseng que lo podemos intalar con estos comandos desde el terminal:
Paquetes :
- dotnet add package Microsoft.EntityFrameworkCore
- dotnet add package Microsoft.EntityFrameworkCore.SqlServer
- dotnet add package Microsoft.EntityFrameworkCore.Sqlite

EJECUCION DEL FRONTEND

cd pruebaTecnica/frontend/appInventario
debemos de intalar 
- npm install
y angular 19 y angular material:
Podemos poner en la tarminal de visual estudio code:
**Paquetes**
- npm install
- ng update @angular/core@19 @angular/cli@19
- ng add @angular/material

despues en el teminal escribimos ng serve 
y se nos abrira el proyecto
**Evidencias**
A continuación se muestran capturas de pantalla que demuestran la funcionalidad del sistema:

- Listado dinámico de productos y transacciones con paginación
- - Productos
![image](https://github.com/user-attachments/assets/33c92b6f-f5e4-492b-b5a6-64b54dde48e3)
- - Transacciones
 ![image](https://github.com/user-attachments/assets/ad894c30-4019-47aa-abc8-918e1944e9ca)
- Pantalla para la creación de productos.
![image](https://github.com/user-attachments/assets/0b5e2b79-3e3f-4058-8a44-4ba71f3f2c2f)
- Pantalla para la edición de productos.
![image](https://github.com/user-attachments/assets/bf1c746a-b817-4de7-8af6-7341efb02204)
- Pantalla para la creación de transacciones.
![image](https://github.com/user-attachments/assets/78d3bcfc-60c4-4814-a068-19891ad0be76)
- Pantalla para la edición de transacciones.

![image](https://github.com/user-attachments/assets/b5cbbeae-2294-4040-9f27-7aca55e14fd3)

- Pantalla de filtros dinámicos.

![image](https://github.com/user-attachments/assets/daf8be41-e3a8-4d68-af58-664b8c6642bd)
- Pantalla para la consulta de información de un formulario (extra).
![image](https://github.com/user-attachments/assets/319ae991-6ab4-4679-a619-09e81708725c)

