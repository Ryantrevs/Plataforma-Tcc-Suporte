using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models.ViewModel
{
    public class UserClaimsViewModel
    {
        public string UserId { get; set; }
        public List<UserClaim> Claims { get; set; }//lista de claims de um ususario
        public UserClaimsViewModel()
        {
            Claims = new List<UserClaim>();
        }
    }
}
