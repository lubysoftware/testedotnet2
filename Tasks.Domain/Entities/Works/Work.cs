using System;
using System.Collections.Generic;
using Tasks.Domain._Common.Entities;

namespace Tasks.Domain.Entities.Works
{
    public class Work : EntityBase
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public string Comment { get; private set; }

        public virtual IEnumerable<WorkDeveloperProject> WorkDeveloperProjects { get; private set; }

        protected Work() : base() { }

        public Work(
            Guid id,
            DateTime startTime,
            DateTime endTime,
            string comment
        ) : base(id)
        {
            SetData(
                startTime: startTime,
                endTime: endTime,
                comment: comment
            );
        }

        public void SetData(
            DateTime startTime,
            DateTime endTime,
            string comment
        )
        {
            StartTime = startTime;
            EndTime = endTime;
            Comment = comment;
        }
    }
}
