using Microsoft.Build.ObjectModelRemoting;
using SIGD.Enums;
using System.ComponentModel.DataAnnotations;

namespace SIGD.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public string Cpf { get; set; }=string.Empty; 
        public string Cargo { get; set; } = string.Empty;
    
        public PerfilEnum Perfil {get; set; }

        public string Senha { get; set;}

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAtualizacao { get; set;}

        public bool SenhaValida(string senha)
        {
            return Senha == senha;
        }


    }
}
