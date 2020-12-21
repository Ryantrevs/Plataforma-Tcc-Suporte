using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models.ViewModel
{
    public class CardViewModel
    {
        public String Titulo { get; set; }
        public DateTime DataLimite { get; set; }
        public String Id { get; set; }

        public CardViewModel(string titulo, DateTime dataLimite,String id)
        {
            Titulo = titulo;
            DataLimite = dataLimite;
        }

        public CardViewModel(string titulo,String id)
        {
            Titulo = titulo;
            Id = id;
        }
    }
}
