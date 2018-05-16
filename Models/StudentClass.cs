using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class StudentClass
    {
        [Key]
        [DisplayName("Mã lớp")]
        public int ClassId { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Tên lớp")]
        public string ClassName { get; set; }

        [DisplayName("Khoa/Viện")]
        [UIHint("DepartmentTemplate")]
        public int DepartmentId { get; set; }

        public virtual Department Department { get; set; }

        [DisplayName("Danh sách sinh viên")]
        public virtual ICollection<Student> Students { get; set; }
    }
}
