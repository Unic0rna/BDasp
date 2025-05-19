using MySql.Data.MySqlClient;
using EscolaEcommerce.Models;
using System.Data;

namespace EscolaEcommerce.Repositorio
{
    public class AlunoRepositorio (IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("conexaoMySQL");

        public int CadastrarAluno (Aluno aluno)
        {
            
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("call insertAluno (@nome, @email, @idade);", conexao);
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = aluno.Nome;
                cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = aluno.Email;
                cmd.Parameters.Add("@idade", MySqlDbType.Int32).Value = aluno.Idade;

                //MySqlParameter retorno = new MySqlParameter("@retorno", MySqlDbType.Int32);
                //retorno.Direction = ParameterDirection.Output;
                //cmd.Parameters.Add(retorno);

                int linhasAfetadas = cmd.ExecuteNonQuery();

                conexao.Close();

                return linhasAfetadas;
            }
        }

        public bool Atualizar(Aluno aluno)
        {
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();

                    MySqlCommand cmd = new MySqlCommand("Update Aluno set Nome=@nome, Email = @email, Idade = @idade where Rm=@codigo", conexao);
                    cmd.Parameters.Add("@codigo", MySqlDbType.Int32).Value = aluno.Rm;
                    cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = aluno.Nome;
                    cmd.Parameters.Add("@email", MySqlDbType.VarChar).Value = aluno.Email;
                    cmd.Parameters.Add("@idade", MySqlDbType.VarChar).Value = aluno.Idade;

                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
            }

            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao atualizar aluno: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<Aluno> TodosAlunos()
        {
            List<Aluno> ALunoLista = new List<Aluno>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from Aluno", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    ALunoLista.Add(
                        new Aluno
                        {
                            Rm = Convert.ToInt32(dr["Rm"]),
                            Nome = ((string)dr["Nome"]),
                            Email = ((string)dr["Email"]),
                            Idade = Convert.ToInt32(dr["Idade"]),
                        });
                }
                return ALunoLista;
            }
        }

        public Aluno ObterAluno(int codigo)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from Aluno where Rm = @codigo", conexao);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;
                Aluno aluno = new Aluno();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    aluno.Rm = Convert.ToInt32(dr["Rm"]);
                    aluno.Nome = (string)(dr["Nome"]);
                    aluno.Email = (string)(dr["Email"]);
                    aluno.Idade = Convert.ToInt32(dr["Idade"]);
                }
                return aluno;
            }
        }

        public void Excluir(int Rm)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("delete from Aluno where Rm = @codigo", conexao);
                cmd.Parameters.AddWithValue("@codigo", Rm);
                int linhasAfetadas = cmd.ExecuteNonQuery();

                conexao.Close();
            }
        }
    }
}
