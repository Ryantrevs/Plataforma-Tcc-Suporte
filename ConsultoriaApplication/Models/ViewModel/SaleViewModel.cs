using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models.ViewModel
{
    public class SaleViewModel
    {
        public Client Client { get; set; }
        public Job Job { get; set; }
        public Sale Sale { get; set; }
        public User User { get; set; }
        public SaleViewModel()
        {

        }
        public SaleViewModel(Client client, Job job, Sale sale)
        {
            Client = client;
            Job = job;
            Sale = sale;
        }
        public SaleViewModel(Client client, Job job, Sale sale,User user)
        {
            Client = client;
            Job = job;
            Sale = sale;
            User = user;
        }
    }
}
