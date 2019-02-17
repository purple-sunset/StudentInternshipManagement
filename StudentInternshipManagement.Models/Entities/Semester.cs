using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentInternshipManagement.Models.Entities
{
    public class Semester : BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Mã học kỳ")]
        public override int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Ngày bắt đầu")]
        public DateTime StartDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayName("Ngày kết thúc")]
        public DateTime EndDate { get; set; }
    }
}