using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Models
{
    public class Internship
    {
        public int InternshipId { get; set; }

        public DateTime RegistrationDate { get; set; }

        public InternshipStatus Status { get; set; }

        public string StudentId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual Student Student { get; set; }

        public int ClassId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual LearningClass Class { get; set; }

        public int CompanyId { get; set; }

        public int TrainingMajorId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual CompanyTrainingMajor Major { get; set; }
    }
}
