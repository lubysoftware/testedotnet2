using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteDotNet2.ProjectControlSystem.Domain.Entities
{
    public class TimeSheet
    {
        public TimeSheet()
        {
            TimeSheetId = Guid.NewGuid();
        }
        public Guid TimeSheetId { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AmountOfHours { get; set; }
        public Guid? DeveloperId { get; set; }
        public virtual Developer Developer { get; set; }
        public Guid? ProjectId { get; set; }
        public virtual Project Project { get; set; }

        [NotMapped]
        public List<string> Messages { get; set; }
    }
}
