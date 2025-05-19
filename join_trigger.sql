select * from Curso;

select * from Aluno;

select * from Aluno_curso;

-- INNER JOIN
select Aluno.nome as nome_aluno, Aluno_curso.IdAluno, Aluno_curso.IdCurso
from Aluno
inner join Aluno_curso on Aluno.Rm = Aluno_curso.IdAluno;

select Aluno.nome as nome_aluno, Aluno_curso.IdAluno, Aluno_curso.IdCurso 
from Aluno, Aluno_curso 
where Aluno.Rm = Aluno_curso.IdAluno;


select Aluno.nome as nome_aluno, Curso.nome as nome_curso, Aluno_curso.IdAluno, Aluno_curso.IdCurso 
from Aluno
inner join Aluno_curso on Aluno.Rm = Aluno_curso.IdAluno
inner join Curso on Curso.Id = Aluno_curso.IdCurso;

select Aluno.nome as nome_aluno, Aluno_curso.IdAluno, Aluno_curso.IdCurso
from Aluno
left join Aluno_curso on Aluno.Rm = Aluno_curso.IdAluno;

select Aluno.nome as nome_aluno, Aluno.Rm, Aluno_curso.IdCurso
from Aluno
cross join Aluno_curso;

delimiter $$
create procedure Idade (vNome varchar(50), vEmail varchar(100), vIdade int)
begin

insert into Aluno (Nome, Email, Idade) values (vNome, vEmail, vIdade);
update Aluno set Idade = Idade + 10;


end
$$

call Idade('Olaf','ol@hotmail.com',11);

select * from Aluno
inner join Curso on Curso.Id = Aluno.Rm;

select * from Curso
inner join Aluno_curso on Curso.Id = Aluno_curso.IdAluno;

Delimiter $$
create trigger somarIdade before insert on Aluno
for each row
begin

set new.Idade = new.Idade + 10;

end;
$$

insert into Curso (Nome, descricao) values ('teste','dfsfdf');
delete from Curso where Nome = 'teste';

SET FOREIGN_KEY_CHECKS = 0;
delete from Aluno where Rm = 2;
SET FOREIGN_KEY_CHECKS = 1;
drop trigger somarIdade;
