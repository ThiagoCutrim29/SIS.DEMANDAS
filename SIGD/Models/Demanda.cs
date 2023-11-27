namespace SIGD.Models
{
    public class Demanda
    {
        public int Id { get; set; }
        public string? titulo { get; set; }
        public string? descricao { get; set; }
        public string? status { get; set; }
        public DateTime DtCadastro { get; set; }
        public DateTime? Prazo { get; set; }

        public string? Demandante{ get; set;}
        public  string? Programador { get; set;}
        public  string? Observacao { get; set;}
      
    }
}
