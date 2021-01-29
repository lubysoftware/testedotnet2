using System;
using System.Collections.Generic;

namespace TesteDotNet2.ProjectControlSystem.Domain.Entities
{
    public class Project
    {
        public Project()
        {
            ProjectId = Guid.NewGuid();
        }

        public Guid ProjectId { get; set; }
        public string Name { get; set; }
        public virtual List<TimeSheet> TimeSheets { get; set; }
    }
}
