using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TrainingMajor
    {
        [DisplayName("Mã định hướng")]
        public int TrainingMajorId { get; set; }

        [Required]
        [MaxLength(50)]
        [DisplayName("Tên định hướng")]
        public string TrainingMajorName { get; set; }

        [DisplayName("Môn học")]
        [UIHint("SubjectTemplate")]
        public string SubjectId { get; set; }

        public virtual Subject Subject { get; set; }

        [DisplayName("Danh sách công ty")]
        public virtual ICollection<CompanyTrainingMajor> CompanyTrainingMajors { get; set; }
    }
}
