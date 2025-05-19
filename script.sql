create database dbsys;
use dbsys;

create table Aluno
(
Rm int primary key auto_increment,
Nome varchar(50) not null,
Email varchar(100) not null,
Idade int not null
);

create table Curso
(
Id int primary key auto_increment,
Nome varchar(50) not null,
Descricao varchar(100) not null
);

delimiter $$
create procedure insertAluno (vNome varchar(50), vEmail varchar(100), vIdade int, out retorno int)
begin



if not exists (select Rm from Aluno where Nome = vNome) then
insert into Aluno(Nome, Email, Idade) values (vNome, vEmail, vIdade);

else
set retorno = 1;
end if;
end
$$

select * from Aluno;
select * from Curso;