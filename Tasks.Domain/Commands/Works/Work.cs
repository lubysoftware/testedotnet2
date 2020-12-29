using System;
using Tasks.Domain.Utils.Bases;

namespace Tasks.Domain.Commands.Works
{
    public class Work : EntityBase
    {
        public DateTime StartTime { get; private set; }
        public DateTime EndTime { get; private set; }
        public string Comment { get; private set; }

        protected Work() : base() { }

        public Work(
            Guid id, 
            DateTime startTime,
            DateTime endTime,
            string comment
        ) : base(id)
        {
            this.SetData(
                startTime: startTime,
                endTime: endTime,
                comment: comment
            );
        }

        public void SetData(
            DateTime startTime,
            DateTime endTime,
            string comment
        ) {
            StartTime = startTime;
            EndTime = endTime;
            Comment = comment;
        }
    }
}
