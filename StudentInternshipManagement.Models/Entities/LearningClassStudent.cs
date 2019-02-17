using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentInternshipManagement.Models.Entities
{
    public class LearningClassStudent : BaseEntity
    {
        //[Key]
        //[Column(Order = 1)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Lớp")]
        [UIHint("LearningClassTemplate")]
        public int ClassId { get; set; }

        //[Key]
        //[Column(Order = 2)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [DisplayName("Sinh viên")]
        [UIHint("StudentTemplate")]
        public int StudentId { get; set; }

        [Range(0, 10)]
        [DisplayName("Điểm giữa kỳ")]
        public float? MidTermPoint { get; set; }

        [Range(0, 10)]
        [DisplayName("Điểm cuối kỳ")]
        public float? EndTermPoint { get; set; }

        [Range(0, 10)]
        [DisplayName("Điểm tổng kết")]
        public float? TotalPoint { get; set; }

        public virtual LearningClass Class { get; set; }

        public virtual Student Student { get; set; }
    }
}