using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class WebContext : DbContext
    {
        public WebContext():base("name=StudentInternshipManagement")
        {
            Database.SetInitializer<WebContext>(new DataInitializer());
        }
    }
}
