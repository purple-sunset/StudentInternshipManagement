using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Models
{
    public class Message:BaseEntity
    {
        public override int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        [MaxLength(50)]
        public string File { get; set; }

        [Range(0,3)]
        public int Type { get; set; }

        public bool IsRead { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }

        public int StudentId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual Student Student { get; set; }

        public int TeacherId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual Teacher Teacher { get; set; }

    }
}
