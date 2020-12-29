using System;
using Tasks.Domain.Utils.Bases;
using Tasks.Domain.Utils.Hash;

namespace Tasks.Domain.Commands.Developers
{
    public class Developer : EntityBase
    {
        public string Name { get; private set; }
        public string Login { get; private set; }
        public string CPF { get; private set; }
        public string Password { get; private set; }

        protected Developer() : base() { }

        public Developer(
            Guid id,
            string name, 
            string login, 
            string cpf, 
            string password
        ) : base(id) {
            this.Password = MD5Crypto.Encode(TasksStartup.Secret + password);
            this.SetData(
                name: name,
                login: login,
                cpf: cpf
            );
        }

        public void SetData(
            string name,
            string login,
            string cpf
        ) {
            this.Name = name;
            this.Login = login;
            this.CPF = cpf;
        }

        public bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;
            var hash = MD5Crypto.Encode(TasksStartup.Secret + password);
            return hash.Equals(Password);
        }
    }
}
