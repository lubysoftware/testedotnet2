using System;
using System.Collections.Generic;
using Tasks.Domain._Common.Entities;
using Tasks.Domain.Entities.Projects;
using Tasks.Domain.Entities.Works;

namespace Tasks.Domain.Entities.Developers
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
