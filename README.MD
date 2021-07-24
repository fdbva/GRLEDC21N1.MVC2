
#dotnet ef tool
```ps2
dotnet tool install --global dotnet-ef --version 5.0.8
```

#Migrations
Executados na pasta do projeto Data:
```ps2
dotnet ef migrations add Initial --startup-project ..\Presentation\Presentation.csproj
dotnet ef database update --startup-project ..\Presentation\Presentation.csproj
```