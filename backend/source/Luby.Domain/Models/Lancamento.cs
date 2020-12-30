using System;
using System.Collections.Generic;

namespace Luby.Domain.Models
{
    public class Lancamento : BaseEntity
    {
        public Lancamento(string nome, string cpf, string cargo,
        string login, string senha,
        List<Desenvolvedor> lst_Desenvolvedores, List<Projeto> lst_Projetos)
        {
            this.Nome = nome;
            this.Cpf = cpf;
            this.Cargo = cargo;
            this.Login = login;
            this.Senha = senha;
            this.Lst_Desenvolvedor = lst_Desenvolvedores;
            this.Lst_Projeto = lst_Projetos;
        }

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Cargo { get; set; }
        public string Login { get; set; }
        public string Senha { get; set; }

        public List<Desenvolvedor> Lst_Desenvolvedor { get; set; }
        public List<Projeto> Lst_Projeto { get; set; }


        public void Update(string nome, string cpf, string cargo, string login, string senha,List<Desenvolvedor> Lst_Desenvolvedor,List<Projeto> Lst_Projeto )
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new InvalidOperationException("O nome é inválido ou vazio");
            }
            if (string.IsNullOrEmpty(cpf) || cpf.Length < 11)
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
        }
        private void ValidaLancamento(string nome, string cpf, string cargo, string login, string senha)
        {
            if (string.IsNullOrEmpty(nome))
            {
                throw new InvalidOperationException("O nome é inválido ou vazio");
            }
            if (string.IsNullOrEmpty(cpf) || cpf.Length < 11)
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
        }
    }
}