using System;
namespace Luby.Domain.Models
{
    public class Desenvolvedor : BaseEntity
    {
        public Desenvolvedor(string nome, string cpf,string cargo,string email, string login, string senha)
        {
            this.Cargo = cargo;
            this.Cpf = cpf;
            this.Login = login;
            this.Nome = nome;
            this.Senha = senha;
            this.Email=email;
            ValidaDesenvolvedor(cpf,cargo, login, nome, senha,email);
        }

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Cargo { get; set; }

        public string Email { get; set; }

        public string Login { get; set; }
        public string Senha { get; set; }

        public void Update(string nome, string cpf,string cargo, string email, string login, string senha)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new InvalidOperationException("O nome é inválido ou vazio");
            }
            if (string.IsNullOrEmpty(cargo))
            {
                throw new InvalidOperationException("O cargo é inválido ou vazio");
            }
            if (string.IsNullOrEmpty(cpf))
            {
                throw new InvalidOperationException("O cpf é inválido ou vazio");
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

        private void ValidaDesenvolvedor(string cpf,string cargo,string login,string nome, string senha,string email)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new InvalidOperationException("O nome é inválido ou vazio");
            }
            if (string.IsNullOrEmpty(cpf))
            {
                throw new InvalidOperationException("O cpf é inválido ou vazio");
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