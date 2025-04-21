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

select * from Aluno;
select * from Curso;