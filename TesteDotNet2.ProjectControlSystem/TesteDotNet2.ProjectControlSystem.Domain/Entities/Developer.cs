using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteDotNet2.ProjectControlSystem.Domain.Entities
{
    public class Developer
    {
        public Developer()
        {
            DeveloperId = Guid.NewGuid();
        }

        public Guid DeveloperId { get; set; }
        public string Name { get; set; }
        public string CPF { get; set; }
        public virtual List<TimeSheet> TimeSheets { get; set; }
       
        [NotMapped]
        public List<string> Messages { get; set; }
    }
}
