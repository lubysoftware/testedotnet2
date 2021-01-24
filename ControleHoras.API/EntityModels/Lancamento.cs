using ControleHoras.API.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ControleHoras.API.EntityModels
{
    public class Lancamento : LancamentoValidable
    {
        public Lancamento() { }
        public Lancamento(LancamentoValidable lancamento, int idDesenvolvedor)
        {
            DataInicio = lancamento.DataInicio;
            DataFim = lancamento.DataFim;
            IdDesenvolvedor = idDesenvolvedor;
            IdProjeto = lancamento.IdProjeto;
        }

        public int IdDesenvolvedor { get; set; }

        public Desenvolvedor Desenvolvedor { get; set; }
    }
    public class LancamentoValidable : EntityModelBase, IValidatableObject
    {
        [Required(ErrorMessage = "Required")]
        public int? IdProjeto { get; set; }

        [Required(ErrorMessage = "Required")]
        public DateTime? DataInicio { get; set; }

        [Required(ErrorMessage = "Required")]
        public DateTime? DataFim { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var obj = validationContext.ObjectInstance as LancamentoValidable;

            if (obj.DataInicio >= obj.DataFim)
                yield return new ValidationResult(
                    errorMessage: $"Must be greater than {nameof(obj.DataInicio)}",
                    memberNames: new[] { nameof(obj.DataFim) }
               );
        }
    }
}