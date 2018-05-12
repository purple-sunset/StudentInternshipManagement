using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Group
    {
        public int GroupId { get; set; }

        public string GroupName { get; set; }

        public int CompanyId { get; set; }

        public int TrainingMajorId { get; set; }

        public CompanyTrainingMajor Major { get; set; }

        public int ClassId { get; set; }

        public LearningClass Class { get; set; }

        public ICollection<Student> Menbers { get; set; }

        [ForeignKey("Leader")]
        public string LeaderId { get; set; }

        public Student Leader { get; set; }

        public string TeacherId { get; set; }

        public Teacher Teacher { get; set; }
    }
}
