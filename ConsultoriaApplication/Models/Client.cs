using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models
{
    public class Client
    {
        public String Id { get; set; }
        public String Name { get; set; }
        public String Sex { get; set; }
        public String Mail { get; set; }
        public String Tel { get; set; }
        public List<Sale> Sales { get; set; }
        public List<Job> Jobs { get; set; }

    }
}
