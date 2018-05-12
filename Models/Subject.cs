using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Subject
    {
        public int SubjectId { get; set; }

        [Required]
        [MaxLength(50)]
        public string SubjectName { get; set; }

        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
    }
}
