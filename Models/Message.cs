using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Student Student { get; set; }

        public string TeacherId { get; set; }

        public Teacher Teacher { get; set; }

    }
}
