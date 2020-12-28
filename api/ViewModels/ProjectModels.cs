using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.ViewModels
{
    public class NewProjectModel
    {
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
    }
    public class ProjectResponse
    {
        public Project Project { get; set; }
        public string Message { get; set; }

        public ProjectResponse(Project project, string message)
        {
            Project = project;
            Message = message;
        }
    }
    public class ProjectList
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public int Items { get; set; }
        public Project[] List { get; set; }
    }
}
