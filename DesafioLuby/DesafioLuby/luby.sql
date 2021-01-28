
create table HorasLancamento(
IdHorasLancamento int identity,
DataInicio DateTime not null,  
DataFim DateTime not null,
IdDesenvolvedor int not null,
IdProjeto int not null,
DataCriacao dateTime not null
)

create table Desenvolvedor(
IdDesenvolvedor  int identity,
NomeDesenvolvedor varchar(100) not null,
Cpf varchar(14) not null,
DataCriacao dateTime not null
)

create table Projeto(
IdProjeto  int identity,
NomeProjeto varchar(100) not null,
DescricaoProjeto varchar(500) not null,
DataCriacao dateTime not null
)