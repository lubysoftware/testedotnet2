using System;
using System.Collections.Generic;
using Tasks.Domain.Commands.Developers;
using Tasks.Domain.Utils.Bases;

namespace Tasks.Domain.Commands.Projects
{
    public class Project : EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }

        public virtual IEnumerable<DeveloperProject> DeveloperProjects { get; private set; }

        protected Project() : base() { }

        public Project(
            Guid id,
            string name,
            string description
        ) : base(id)
        {
            this.SetData(
                name: name,
                description: description
            );
        }

        public void SetData(
            string name,
            string description
        ) {
            this.Name = name;
            this.Description = description;
        }
    }
}
