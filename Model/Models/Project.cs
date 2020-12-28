using Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Model
{
    [Table("tb_project")]
    public class Project: IEntidadeGenerica
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }
        public virtual ICollection<Hour> Hours { get; set; }
        public ICollection<DeveloperProject> DeveloperProjects { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Project p2 = obj as Project;
            if (p2 == null)
            {
                return false;
            }
            if (base.Equals(obj))
            {
                return true;
            }
            return this.Id == p2.Id;
        }
        public override int GetHashCode()
        {
            int hash = 37;
            hash = hash * 23 + typeof(Project).GetHashCode();
            hash = hash * 23 + Id.GetHashCode();
            return hash;
        }
        public override string ToString()
        {
            return Name;
        }
    }
    public class DeveloperProject
    {
        public int DeveloperId { get; set; }
        [JsonIgnore]
        [IgnoreDataMember]
        public Developer Developer { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public Project Project { get; set; }

        public int ProjectId { get; set; }
    }
}
