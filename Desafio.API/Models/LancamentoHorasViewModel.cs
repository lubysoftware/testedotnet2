using System;
using System.ComponentModel.DataAnnotations;

namespace Desafio.API.Models
{


    public class LancamentoHorasViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int DesenvolvedorId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public int ProjetoId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataInicio { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime DataFim { get; set; }
    }
}
