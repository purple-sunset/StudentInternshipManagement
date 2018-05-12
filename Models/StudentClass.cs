using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StudentClass
    {
        [Key]
        public int ClassId { get; set; }

        [Required]
        [MaxLength(50)]
        public string ClassName { get; set; }

        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }
    }
}
