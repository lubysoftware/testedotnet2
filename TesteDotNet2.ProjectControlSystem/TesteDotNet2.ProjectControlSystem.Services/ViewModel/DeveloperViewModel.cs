using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TesteDotNet2.ProjectControlSystem.Services.ViewModel
{
    [DataContract(Name = "developer")]
    public class DeveloperViewModel
    {
        public DeveloperViewModel()
        {
            DeveloperId = Guid.NewGuid();
        }
        public Guid DeveloperId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório: nome")]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Campo obrigatório: CPF")]
        [DataMember(Name = "cpf")]
        public string CPF { get; set; }

        [DataMember(Name = "messages")]
        public List<string> Messages { get; set; }
    }
}
