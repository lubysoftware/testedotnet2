using ControleHoras.API.BaseModels;
using ControleHoras.API.Repository;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ControleHoras.API.EntityModels
{
    public class Desenvolvedor : DesenvolvedorValidable
    {
        public Desenvolvedor() { }
        public Desenvolvedor(DesenvolvedorValidable desenvolvedor)
        {
            Nome = desenvolvedor.Nome;
            CPF = desenvolvedor.CPF;
        }
        public IList<DesenvolvedorProjeto> DesenvolvedorProjeto { get; set; }
        [NotMapped]
        public IList<Lancamento> Lancamentos { get; set; }
    }

    public class DesenvolvedorValidable : EntityModelBase, IValidatableObject
    {
        [Required(ErrorMessage = "Required")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Required")]
        [StringLength(11, ErrorMessage = "Must have 11 digits, only numbers")]
        public string CPF { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var obj = validationContext.ObjectInstance as DesenvolvedorValidable;

            if (!Maoli.Cpf.Validate(obj.CPF))
                yield return new ValidationResult(
                    errorMessage: "Invalid",
                    memberNames: new[] { nameof(obj.CPF) }
               );
            else if (DesenvolvedorRepository.Instance.IsCPFAlreadyRegistered(obj.CPF))
                yield return new ValidationResult(
                  errorMessage: "CPF already registered",
                  memberNames: new[] { nameof(obj.CPF) }
               );
        }
    }
}
