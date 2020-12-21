using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models.ViewModel
{
    public class ProfileViewModel
    {
        public User user { get; set; }
        public ProfileViewModel(User User)
        {
            user = User;
        }
        public ProfileViewModel()
        {
                
        }

    }
}
