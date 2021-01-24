# Desafio Back-end Luby Software
Primeiramente, obrigado pelo seu interesse em trabalhar na Luby. Somos uma fábrica de software com mais de 110 desenvolvedores e 15 anos de mercado. Temos atuação em mais de 5 países e estamos em busca de talentos para integrar o nosso time no desenvolvimento .NET de forma 100% remota.

#### Premissas:
- Criar uma API usando .NET CORE.
- O banco de dados pode ser  MySql ou SQL Server.

#### Teste:
Desenvolver um serviço que seja capaz de gerar um lançamento de horas.
- Um lançamento de horas é composta por pelo menos **id**, **data inicio**, **data fim**, **desenvolvedor**.

#### Sua tarefa é desenvolver os serviços REST abaixo:
- CRUD para desenvolvedor (Será considerado um diferencial paginação na listagem)
- CRUD de projeto (Será considerado um diferencial paginação na listagem)
- Criar um lançamento de hora
- Retornar ranking dos 5 desenvolvedores da semana com maior média de horas trabalhadas.

#### Algumas regras à serem consideradas
- Um desenvolvedor só pode lançar horas em projetos que ele esteja vinculado
- Um desenvolvedor só pode lançar horas se estiver autenticado (Autenticação JWT com expiração de 5 minutos)
- Validações de integridade e duplicidade
- Antes de cadastrar um desenvolvedor, devemos validar se seu CPF é válido, para essa validação, pode ser usado o endpoint (https://run.mocky.io/v3/067108b3-77a4-400b-af07-2db3141e95c9)
- Na confirmação do lançamento de horas, uma notificação é enviada, e o serviço pode estar indisponível/instável. Para enviar a notificação, use o endpoint abaixo (https://run.mocky.io/v3/a1b59b8e-577d-4996-a4c5-56215907d9dd)

#### Instruções:
1. Realizar `fork` deste projeto.
2. Desenvolver em cima do seu `fork`.
3. Após finalizar, realizar o `pull request`.
4. Atualize esse README.md com sua identificação no fim do arquivo
5. Fique à vontade para perguntar qualquer dúvida aos recrutadores.

#### E por fim:
- Gostaríamos de ver o uso do controle de versão.
- Entendimento de OO, conceitos de SOLID, e outros relacionados
- Reuso do código
- Vamos avaliar a maneira que você escreveu seu código, a solução apresentada.
- Caso encontre algum impedimento no decorrer do desenvolvimento, entregue da maneira que preferir e faça uma explicação sobre o impedimento.
- Avaliaremos também sua postura, honestidade e a maneira que resolve problemas.

#### Desejável (Será considerado um diferencial)
- Automação de testes - unitários e integração. 
- Configurar o Swagger para termos acesso a documentação da API.
- É de suma importância se utilizar das melhores práticas para um projeto seguro e organizado, como a utilização de controllers, services, factory, middlewares, controle de exceções, utilização de um ORM ou MicroORM (Object Relational Mapper) para operações de banco de dados.
- Criar um client WEB para consumir essa API 

#### Identificação:
Nome: THAYSON FERNANDES DO NASCIMENTO <br/>
E-mail: thayson_f@hotmail.com


#### Orientações:

#### Pré-requisito:
  - [Node.js LTS](https://nodejs.org/en/) e [.NET 5 SDK](https://dotnet.microsoft.com/download/dotnet/5.0);
  
#### Banco de Dados:
  - Bando de dados utilizado: SQL Server;
  - Está utilizando a ferramenta de migração automática. 
  - Existe uma flag no WebUI/appsettings.json "UseInMemoryDatabase", para utilizar o banco em memória. Por padrão está como true. Caso precise criar o banco é só configurar a connectionString e mudar essa mesma flag para false;

#### Executar Projeto:
  - Entre na pasta `src/WebUI/ClientApp` e execute o comando `npm start` para subir o front end (Angular);
  - Entre na pasta `src/WebUI` e execute o comando `dotnet run` para subir o back end (ASP.NET Core Web API);
  - Console exibirá o endereço do projeto. Navegue até https://localhost:port/ (por padrão a porta é 5001);

#### Tecnologias

  - CQRS com MediatR;
  - Validações com FluentValidation;
  - Mapeamento com AutoMapper;
  - Acesso a dados Entity Framework Core;
  - Web API ASP.NET Core;
  - Front-end Angular;
  - Open API NSwag;
  - Autenticação e segurança ASP.NET Core Identity + IdentityServer;







