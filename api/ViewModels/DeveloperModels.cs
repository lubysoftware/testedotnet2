using api.ValidationAttributes;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace api.ViewModels
{
    public class DeveloperList
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public int Items { get; set; }
        public DeveloperView[] List { get; set; }
    }
    public class LoginModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public virtual string Password { get; set; }

    }

    public class EditDeveloperModel
    {
        [Required]

        public int Id { get; set; }
        [MaxLength(100)]
        public string Name { get; set; }
        public int[] NewProjects { get; set; }
        public int[] RemovedProjects { get; set; }
    }
    public class NewDeveloperModel : Developer
    {
        [Required]
        [EmailAddress]
        [UniqueEmail(ErrorMessage = "Email already taken")]
        public virtual string Email { get; set; }
        [Required]
        [MaxLength(11)]
        [CPFValidation(ErrorMessage = "CPF Invalid")]
        [UniqueCPF(ErrorMessage = "CPF already taken")]
        public virtual string CPF { get; set; }
    }
    public class DeveloperView
    {
        public int Id { get; set; }
        public string CPF { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public DeveloperView() { }
        public DeveloperView(Developer dev) {
            Id = dev.Id;
            CPF = dev.CPF;
            Name = dev.Name;
            Email = dev.Email;
        }
    }
}
