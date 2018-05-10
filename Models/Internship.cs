using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Internship
    {
        public int InternshipId { get; set; }

        public DateTime RegistrationDate { get; set; }

        public InternshipStatus Status { get; set; }

        public int StudentId { get; set; }

        public Student Student { get; set; }

        public int ClassId { get; set; }

        public LearningClass Class { get; set; }

        public int CompanyId { get; set; }

        public int TrainingMajorId { get; set; }

        public CompanyTrainingMajor Major { get; set; }
    }
}
