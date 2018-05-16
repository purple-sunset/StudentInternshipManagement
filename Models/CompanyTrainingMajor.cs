using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CompanyTrainingMajor
    {
        [Key]
        [Column(Order = 1)]
        [UIHint("CompanyTemplate")]
        [DisplayName("Mã công ty")]
        public int CompanyId { get; set; }

        [Key]
        [Column(Order = 2)]
        [UIHint("TrainingMajorTemplate")]
        [DisplayName("Mã định hướng")]
        public int TrainingMajorId { get; set; }

        [DisplayName("Công ty")]
        public virtual Company Company { get; set; }

        [DisplayName("Định hướng")]
        public virtual TrainingMajor TrainingMajor { get; set; }

        [Required]
        [Range(1,1000)]
        [DisplayName("Số lượng tối đa")]
        public int TotalTraineeCount { get; set; }

        [Range(0,1000)]
        [DisplayName("Số lượng còn lại")]
        public int AvailableTraineeCount { get; set; }
    }
}
