using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Models
{
    public class Message
    {
        public int MessageId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Content { get; set; }

        [Required]
        [MaxLength(50)]
        public string File { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Time { get; set; }

        public string StudentId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual Student Student { get; set; }

        public string TeacherId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual Teacher Teacher { get; set; }

    }
}
