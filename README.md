# Prueba.Api

Gestor de base de datos que se utilizo postgres sql version 16

Correr el proyecto en Visual studio 2022

Primero en el appsettings.development ajustar el DefaultConnection

Server=localhost;Database=PruebaIDTG;User Id=Usuario;Password =Password;

Luego correr el comando para la migracion en primero abrir la pestaña view y abrrir el package manager console
En la consola colocan: Update-DataBase

Conectarse a la base de datos creada en pgadmin o dbeaver e ingresar el siguiente script para cargar los estados
insert into "States" ("Name")
values ('to do'), ('in progress'), ('done');

y realizar las pruebas de los endpoints
