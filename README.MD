
#dotnet ef tool
```powershell
dotnet tool install --global dotnet-ef --version 5.0.8
```

#Migrations para BibliotecaContext
Executados na pasta do projeto Data:
```powershell
dotnet ef migrations add Initial --context BibliotecaContext --startup-project ..\Application.WebApi\Application.WebApi.csproj
dotnet ef database update --context BibliotecaContext --startup-project ..\Application.WebApi\Application.WebApi.csproj
```

#Migrations para o LoginContext
Executados na pasta do projeto Presentation:
```powershell
dotnet ef migrations add Initial --context LoginContext --output-dir Areas\Identity\Data\Migrations
dotnet ef database update --context LoginContext
```

#google external auth
https://docs.microsoft.com/en-us/aspnet/core/security/authentication/social/google-logins?view=aspnetcore-5.0
Substitua em client-id e client-secret as respectivas informações criadas no painel da google
```powershell
dotnet user-secrets set "Authentication:Google:ClientId" "<client-id>"
dotnet user-secrets set "Authentication:Google:ClientSecret" "<client-secret>"
```