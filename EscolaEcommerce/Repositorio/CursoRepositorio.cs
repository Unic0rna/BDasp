using EscolaEcommerce.Models;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

namespace EscolaEcommerce.Repositorio
{
    public class CursoRepositorio (IConfiguration configuration)
    {
        private readonly string _conexaoMySQL = configuration.GetConnectionString("conexaoMySQL");

        public void CadastrarCurso(Curso curso)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("insert into Curso (Nome, Descricao) values (@nome, @descricao)", conexao);
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = curso.Nome;
                cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = curso.Descricao;

                cmd.ExecuteNonQuery();

                conexao.Close();
            }
        }

        public bool Atualizar(Curso curso)
        {
            try
            {
                using (var conexao = new MySqlConnection(_conexaoMySQL))
                {
                    conexao.Open();

                    MySqlCommand cmd = new MySqlCommand("Update Curso set Nome=@nome, Descricao= @descricao where Id=@codigo", conexao);
                    cmd.Parameters.Add("@codigo", MySqlDbType.Int32).Value = curso.Id;
                    cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = curso.Nome;
                    cmd.Parameters.Add("@descricao", MySqlDbType.VarChar).Value = curso.Descricao;

                    int linhasAfetadas = cmd.ExecuteNonQuery();
                    return linhasAfetadas > 0;
                }
            }

            catch (MySqlException ex)
            {
                Console.WriteLine($"Erro ao atualizar curso: {ex.Message}");
                return false;
            }
        }

        public IEnumerable<Curso> TodosCursos()
        {
            List<Curso> CursoLista = new List<Curso>();

            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from Curso", conexao);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                conexao.Close();

                foreach (DataRow dr in dt.Rows)
                {
                    CursoLista.Add(
                        new Curso
                        {
                            Id = Convert.ToInt32(dr["Id"]),
                            Nome = ((string)dr["Nome"]),
                            Descricao = ((string)dr["Descricao"]),
                        });
                }
                return CursoLista;
            }
        }

        public Curso ObterCurso(int codigo)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("select * from Curso where Id = @codigo", conexao);
                cmd.Parameters.AddWithValue("@codigo", codigo);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                MySqlDataReader dr;
                Curso curso = new Curso();

                dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (dr.Read())
                {
                    curso.Id = Convert.ToInt32(dr["Id"]);
                    curso.Nome = (string)(dr["Nome"]);
                    curso.Descricao = (string)(dr["Descricao"]);
                }
                return curso;
            }
        }

        public void Excluir(int Id)
        {
            using (var conexao = new MySqlConnection(_conexaoMySQL))
            {
                conexao.Open();

                MySqlCommand cmd = new MySqlCommand("delete from Curso where Id = @codigo", conexao);
                cmd.Parameters.AddWithValue("@codigo", Id);
                int linhasAfetadas = cmd.ExecuteNonQuery();

                conexao.Close();
            }
        }

    }
}
