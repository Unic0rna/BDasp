using MySql.Data.MySqlClient;
using EscolaEcommerce.Models;
using System.Data;

namespace EscolaEcommerce.Repositorio
{
    public class AlunoRepositorio (IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("ConexaoMySQL");

        public void CadastrarAluno (Aluno aluno)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into Aluno (Nome, Email, Idade) values (@nome, @email, @idade)", conexao);
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = aluno.Nome;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = aluno.Email;
                cmd.Parameters.Add("@idade", MySqlDbType.Int32).Value = aluno.Idade;

                cmd.ExecuteNonQuery();

                conexao.Close();
            }
        }
    }
}
