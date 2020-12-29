using System;
using Tasks.Domain.Utils.Bases;

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
            this.Name = name;
            this.Login = login;
            this.CPF = cpf;
            this.Password = password;
        }
    }
}
