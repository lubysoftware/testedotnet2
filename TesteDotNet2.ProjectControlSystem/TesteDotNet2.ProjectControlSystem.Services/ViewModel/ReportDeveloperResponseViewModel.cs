using System.Runtime.Serialization;

namespace TesteDotNet2.ProjectControlSystem.Services.ViewModel
{
    [DataContract(Name = "reportDeveloper")]
    public class ReportDeveloperResponseViewModel
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "hours")]
        public int Hours { get; set; }
    }
}
