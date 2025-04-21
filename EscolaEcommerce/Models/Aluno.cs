namespace EscolaEcommerce.Models
{
    public class Aluno
    {
        public int Rm { get; set; }
        public string? Nome { get; set; }
        public string? Email { get; set; }
        public int Idade { get; set; }
        List<Aluno>? ListaAluno { get; set; }
    }
}