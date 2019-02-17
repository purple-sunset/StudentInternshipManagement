using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Script.Serialization;

namespace StudentInternshipManagement.Models.Entities
{
    public class Teacher : BaseEntity
    {
        [DisplayName("Khoa/Viện")]
        [UIHint("DepartmentTemplate")]
        public int DepartmentId { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual Department Department { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Message> Messages { get; set; }

        [ScriptIgnore(ApplyToOverrides = true)]
        public virtual ICollection<Group> Groups { get; set; }

        [DisplayName("Mã giảng viên")]
        [ForeignKey("User")]
        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}