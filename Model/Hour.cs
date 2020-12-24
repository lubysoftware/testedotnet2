using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model
{
    [Table("tb_hour")]
    public class Hour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime Begin { get; set; }
        [DataType(DataType.Date)]
        public DateTime End { get; set; }
        public int DeveloperId { get; set; }
        [ForeignKey("DeveloperId")]
        public virtual Developer Developer { get; set; }
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        public virtual Project Project { get; set; }
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            Hour p2 = obj as Hour;
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
            hash = hash * 23 + typeof(Hour).GetHashCode();
            hash = hash * 23 + Id.GetHashCode();
            return hash;
        }
        public override string ToString()
        {
            return $"{DeveloperId} {Begin.ToShortDateString()}-{End.ToShortDateString()}";
        }
    }
}
