﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models.ViewModel
{
    public class EditUserViewModel
    {
        public string  Id{ get; set; }
        public string  UserName { get; set; }
        public string Email { get; set; }
        public List<string> Claims { get; set; }
        public IList<string> Roles { get; set; }

        public EditUserViewModel()
        {
            Claims = new List<string>();
            Roles = new List<string>();

        }
    }
}
