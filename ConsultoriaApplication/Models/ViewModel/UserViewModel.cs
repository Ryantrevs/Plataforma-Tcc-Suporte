using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models.ViewModel
{
    public class UserViewModel
    {
        public String id { get; set; }
        public String Nome { get; set; }

        public UserViewModel(string id, string nome)
        {
            this.id = id;
            Nome = nome;
        }
        public UserViewModel()
        {
        }
    }
}
