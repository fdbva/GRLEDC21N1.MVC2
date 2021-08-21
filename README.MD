
#dotnet ef tool
```powershell
dotnet tool install --global dotnet-ef --version 5.0.8
```

#Migrations para BibliotecaContext
Executados na pasta do projeto Data:
```powershell
dotnet ef migrations add Initial --context BibliotecaContext --startup-project ..\Presentation\Presentation.csproj
dotnet ef database update --context BibliotecaContext --startup-project ..\Presentation\Presentation.csproj
```

#Migrations para o LoginContext
Executados na pasta do projeto Presentation:
```powershell
dotnet ef migrations add Initial --context LoginContext --output-dir Areas\Identity\Data\Migrations
dotnet ef database update --context LoginContext
```