using System;
namespace Luby.Domain.Models
{
    public class Desenvolvedor : BaseEntity
    {
        public Desenvolvedor(string nome, string cargo,string email, string login, string senha)
        {
            this.cargo = cargo;
            this.login = login;
            this.nome = nome;
            this.senha = senha;
            this.email=email;
            ValidaDesenvolvedor(cargo, login, nome, senha,email);
        }

        public string nome { get; set; }
        public string cargo { get; set; }

        public string email { get; set; }

        public string login { get; set; }
        public string senha { get; set; }

        public void Update(string nome, string cargo, string email, string login, string senha)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new InvalidOperationException("O nome é inválido ou vazio");
            }
            if (string.IsNullOrEmpty(cargo))
            {
                throw new InvalidOperationException("O cargo é inválido ou vazio");
            }
            if (string.IsNullOrEmpty(login))
            {
                throw new InvalidOperationException("O login é inválido ou vazio");
            }
            if (string.IsNullOrEmpty(senha))
            {
                throw new InvalidOperationException("O senha é inválida ou vazia");
            }
            if (string.IsNullOrEmpty(email))
            {
                throw new InvalidOperationException("O email é inválida ou vazia");
            }
        }

        private void ValidaDesenvolvedor(string cargo,string login,string nome, string senha,string email)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new InvalidOperationException("O nome é inválido ou vazio");
            }
            if (string.IsNullOrEmpty(cargo))
            {
                throw new InvalidOperationException("O cargo é inválido ou vazio");
            }
            if (string.IsNullOrEmpty(login))
            {
                throw new InvalidOperationException("O login é inválido ou vazio");
            }
            if (string.IsNullOrEmpty(senha))
            {
                throw new InvalidOperationException("O senha é inválida ou vazia");
            }
            if (string.IsNullOrEmpty(email))
            {
                throw new InvalidOperationException("O email é inválida ou vazia");
            }
        }
    }
}