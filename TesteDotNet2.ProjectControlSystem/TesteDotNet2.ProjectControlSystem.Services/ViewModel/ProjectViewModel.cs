using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace TesteDotNet2.ProjectControlSystem.Services.ViewModel
{
    [DataContract(Name = "project")]
    public class ProjectViewModel
    {
        public ProjectViewModel()
        {
            ProjectId = Guid.NewGuid();
        }

        public Guid ProjectId { get; set; }

        [Required(ErrorMessage = "Campo obrigatório: nome")]
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "messages")]
        public List<string> Messages { get; set; }
    }
}
