using System.ComponentModel.DataAnnotations;

namespace Desafio.API.Models
{

    public class ProjetoViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public string Nome { get; set; }
   
    }
}
