using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TrainingMajor
    {
        public int TrainingMajorId { get; set; }

        [Required]
        [MaxLength(50)]
        public string TrainingMajorName { get; set; }

        public int SubjectId { get; set; }

        public Subject Subject { get; set; }
    }
}
