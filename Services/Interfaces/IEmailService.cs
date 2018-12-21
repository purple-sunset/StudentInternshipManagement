using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace Services.Interfaces
{
    public interface IEmailService
    {
        void SendCreateEmail();
        void SendProcessEmail();
    }
}
