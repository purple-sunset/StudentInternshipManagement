using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class CompanyTrainingMajor
    {
        public int CompanyId { get; set; }

        public int TrainingMajorId { get; set; }

        public Company Company { get; set; }

        public TrainingMajor TrainingMajor { get; set; }

        [Range(1,1000)]
        public int TotalTraineeCount { get; set; }

        [Range(0,1000)]
        public int AvailableTraineeCount { get; set; }
    }
}
