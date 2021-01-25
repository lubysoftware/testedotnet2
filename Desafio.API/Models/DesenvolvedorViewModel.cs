using System.ComponentModel.DataAnnotations;

namespace Desafio.API.Models
{
    public class DesenvolvedorAdicionarViewModel : DesenvolvedorViewModel
    {
        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Senha { get; set; }
    }

    public class DesenvolvedorViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string CPF { get; set; }


        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Usuario { get; set; }        
    }
}
