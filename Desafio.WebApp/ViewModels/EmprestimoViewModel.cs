using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Desafio.WebApp.ViewModels
{
    public class EmprestimoViewModel
    {

        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [RegularExpression(@"^\d*[1-9]\d*$", ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Amigo")]
        public int PessoaId { get; set; }



        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [RegularExpression(@"^\d*[1-9]\d*$", ErrorMessage = "O campo {0} é obrigatório")]
        [DisplayName("Jogo")]
        public int JogoId { get; set; }

        [DisplayName("Data Emprestimo")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime DataEmprestimo { get; set; }

        [DisplayName("Data Devolução")]
        [DisplayFormat(DataFormatString = "{0:d}")]
        public DateTime? DataDevolucao { get; set; }

        [DisplayName("Amigo")]
        public PessoaViewModel Pessoa { get; set; }

        public JogoViewModel Jogo { get; set; }

        public IEnumerable<PessoaViewModel> Pessoas { get; set; }

        public IEnumerable<JogoViewModel> Jogos { get; set; }


    }
}
