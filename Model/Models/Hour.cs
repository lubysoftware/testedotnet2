using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Model
{
    [Table("tb_hour")]
    public class Hour
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DataType(DataType.Date)]
        public DateTime DtBegin { get; set; }
        [DataType(DataType.Date)]
        public DateTime DtEnd { get; set; }
        public int DeveloperId { get; set; }
        [ForeignKey("DeveloperId")]
        [JsonIgnore]
        public virtual Developer Developer { get; set; }
        public int ProjectId { get; set; }
        [ForeignKey("ProjectId")]
        [JsonIgnore]
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
            return $"{DeveloperId} {DtBegin.ToShortDateString()} - {DtEnd.ToShortDateString()}";
        }
    }
}
