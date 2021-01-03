using System;
//classe de projetos
namespace Luby.Domain.Models
{
    public class Projeto : BaseEntity
    {
        public Projeto(string nome)
        {
            this.Nome = nome;
            ValidaNome(nome);
        }
        public string Nome { get; private set; }

        public void Update(string nome)
        {
            ValidaNome( nome);
        }
        private void ValidaNome(string nome)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new InvalidOperationException("O nome é inválido ou vazio");
            }
        }
    }
}
