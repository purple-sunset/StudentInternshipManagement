using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;

namespace StudentInternshipManagement.Models.Entities
{
    public class CompanyTrainingMajor : BaseEntity
    {
        //[Key]
        //[Column(Order = 1)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [UIHint("CompanyTemplate")]
        [DisplayName("Mã công ty")]
        public int CompanyId { get; set; }

        //[Key]
        //[Column(Order = 2)]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [UIHint("TrainingMajorTemplate")]
        [DisplayName("Mã định hướng")]
        public int TrainingMajorId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        [DisplayName("Công ty")]
        public virtual Company Company { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        [DisplayName("Định hướng")]
        public virtual TrainingMajor TrainingMajor { get; set; }

        [Required]
        [Range(1, 1000)]
        [DisplayName("Số lượng tối đa")]
        public int TotalTraineeCount { get; set; }

        [Range(0, 1000)]
        [DisplayName("Số lượng còn lại")]
        public int AvailableTraineeCount { get; set; }
    }
}