using System;
using System.Collections.Generic;
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
        public int CompanyId { get; set; }

        [Key]
        [Column(Order = 2)]
        public int TrainingMajorId { get; set; }

        public virtual Company Company { get; set; }

        public virtual TrainingMajor TrainingMajor { get; set; }

        [Range(1,1000)]
        public int TotalTraineeCount { get; set; }

        [Range(0,1000)]
        public int AvailableTraineeCount { get; set; }
    }
}
