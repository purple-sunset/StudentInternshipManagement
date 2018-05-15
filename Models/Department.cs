using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Department
    {
        [DisplayName("Mã Khoa/Viện")]
        public int DepartmentId { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Tên Khoa/Viện")]
        public string DepartmentName { get; set; }
    }
}
