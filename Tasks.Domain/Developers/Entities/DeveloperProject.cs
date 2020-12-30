using System;
using System.Collections.Generic;
using Tasks.Domain._Common.Entities;
using Tasks.Domain.Projects.Entities;
using Tasks.Domain.Works.Entities;

namespace Tasks.Domain.Developers.Entities
{
    public class DeveloperProject : EntityBase
    {
        public Guid DeveloperId { get; private set; }
        public Guid ProjectId { get; private set; }

        public virtual Developer Developer { get; private set; }
        public virtual Project Project { get; private set; }
        public virtual IEnumerable<WorkDeveloperProject> WorkDeveloperProjects { get; private set; }

        protected DeveloperProject() : base() { }

        public DeveloperProject(
            Guid id,
            Guid developerId,
            Guid projectId
        ) : base(id)
        {
            DeveloperId = developerId;
            ProjectId = projectId;
        }
    }
}
