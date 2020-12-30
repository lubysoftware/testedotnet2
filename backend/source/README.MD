### COMANDOS
dotnet sln add source/Luby.Domain/Luby.Domain.csproj

dotnet new classlib --name Luby.Application
dotnet new classlib --name Luby.Infra
dotnet new webapi --name Luby.Web
em backend
dotnet sln add source/Luby.Application/Luby.Application.csproj

dotnet add .\Luby.infra\Luby.infra.csproj reference .\Luby.domain\Luby.domain.csproj

### FONTES
- https://github.com/rafael-act/testedotnet2
- http://www.macoratti.net/20/07/aspnc_ucddd1.htm
- https://www.youtube.com/watch?v=N10QW_AIOnI
- https://medium.com/beelabacademy/implementando-na-prática-rest-api-com-conceitos-de-ddd-net-2160291a44b7
- https://ajlopez.wordpress.com/2008/09/12/layered-architecture-in-domain-driven-design/
- https://imasters.com.br/dotnet/criando-uma-webapi-utilizando-net-core-e-vs-code
- https://www.youtube.com/watch?v=lrYElKHrGdY

### nuget
- https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/5.0.0
- https://www.nuget.org/packages/Pomelo.EntityFrameworkCore.MySql/5.0.0-alpha.2
- https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Tools/5.0.0

### ef comandos
dotnet ef => aparece o unicornio
dotnet ef -- help => ajuda
dotnet ef dbcontext --help=> gerencia os tipos de dbcontext
dotnet ef dbcontext scaffold --help=> Organiza um DbContext e tipos de entidade para um banco de dados
comando completo é
dotnet ef dbcontext scaffold "server=localhost;database=luby;user=root;pwd=;" "Pomelo.EntityFrameworkCore.MySql" -o .\Context