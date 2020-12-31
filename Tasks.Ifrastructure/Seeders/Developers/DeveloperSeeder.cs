using System;
using System.Collections.Generic;
using System.Text;
using Tasks.Domain.Developers.Entities;
using Tasks.Ifrastructure._Common.Interfaces;

namespace Tasks.Ifrastructure.Seeders.Developers
{
    public class DeveloperSeeder : ISeeder<Developer>
    {
        public IEnumerable<Developer> GetList()
        {
            return new List<Developer> { 
                new Developer(
                    id: Guid.NewGuid(),
                    name: "Pleno",
                    login: "pleno",
                    cpf: "13467669085",
                    password: "321654"
                )    
            };
        }
    }
}
