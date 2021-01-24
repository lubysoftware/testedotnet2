using ControleHoras.API.BaseModels;
using ControleHoras.API.Repository;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControleHoras.API.EntityModels
{
    public class Projeto : ProjetoValidable
    {
        public Projeto() { }
        public Projeto(ProjetoValidable projeto)
        {
            Nome = projeto.Nome;
        }

        public IList<DesenvolvedorProjeto> DesenvolvedorProjeto { get; set; }
    }
    public class ProjetoValidable : EntityModelBase, IValidatableObject
    {
        [Required(ErrorMessage = "Required")]
        [MinLength(3, ErrorMessage = "Must have at leats 3 characters")]
        public string Nome { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var obj = validationContext.ObjectInstance as ProjetoValidable;

            if (ProjetoRepository.Instance.IsNameAlreadyRegistered(obj.Nome))
                yield return new ValidationResult(
                    errorMessage: "Name already registered",
                    memberNames: new[] { nameof(obj.Nome) }
               );
        }
    }
}
