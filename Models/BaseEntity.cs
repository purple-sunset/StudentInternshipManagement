using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BaseEntity
    {
        public virtual int Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreatedAt { get; set; }
        public string CreatedBy { get; set; }

        public DateTime UpdatedAt { get; set; }
        public string UpdatedBy { get; set; }
    }
}
