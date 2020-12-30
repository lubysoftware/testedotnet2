using System;
using System.Collections;
using System.Collections.Generic;
using Tasks.Domain._Utils.Bases;
using Tasks.Domain._Utils.Hash;

namespace Tasks.Domain.Developers
{
    public class Developer : EntityBase
    {
        public string Name { get; private set; }
        public string Login { get; private set; }
        public string CPF { get; private set; }
        public string Password { get; private set; }

        public virtual IEnumerable<DeveloperProject> DeveloperProjects { get; private set; }

        protected Developer() : base() { }

        public Developer(
            Guid id,
            string name,
            string login,
            string cpf,
            string password
        ) : base(id)
        {
            this.Password = MD5Crypto.Encode(TasksStartup.Secret + password);
            SetData(
                name: name,
                login: login,
                cpf: cpf
            );
        }

        public void SetData(
            string name,
            string login,
            string cpf
        )
        {
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
