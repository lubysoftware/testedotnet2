using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Api.Models
{
    public class Developer
    {
        public Developer()
        { }
        public Developer(int id, string name, string cpf, string email, string password)
        {
            Id = id;
            Name = name;
            CPF = cpf;
            Email = email;
            Password = password;
        }

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Key]
        [Required]
        [MaxLength(11)]
        public string CPF { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public IEnumerable<DeveloperProject> DeveloperProject {get; set;}

        public IEnumerable<Entry> Entries { get; set; }
    }
}