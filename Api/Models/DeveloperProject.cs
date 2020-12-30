namespace Api.Models
{
    public class DeveloperProject
    {
        public DeveloperProject() { }

        public DeveloperProject(int developerId, int projectId)
        {
            DeveloperId = developerId;
            ProjectId = projectId;
        }

        public int DeveloperId { get; set; }
        public Developer Developer { get; set; }

        public int ProjectId { get; set; }

        public Project Project { get; set; }
    }
}
