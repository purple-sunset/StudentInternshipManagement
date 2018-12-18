using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Models.Entities
{
    public class Department:BaseEntity
    {
        [DisplayName("Mã Khoa/Viện")]
        public override int Id { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Tên Khoa/Viện")]
        public string DepartmentName { get; set; }
    }
}
