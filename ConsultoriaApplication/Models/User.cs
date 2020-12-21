using ConsultoriaApplication.Models.Repository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models
{
    public class User : IdentityUser
    {
        
        public String Nome { get; set; }        
        public List<Sale> Sales { get; set; }
        public virtual List<UserTasklist> UserTasklist { get; set; }
        public virtual List<UserScope> UserScope { get; set; }
        public User()
        {
            
        }

    }
}
