namespace EscolaEcommerce.Models
{
    public class Curso
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        List<Curso>? ListaCurso { get; set; }
    }
}
