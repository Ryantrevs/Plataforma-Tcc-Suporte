using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models.ViewModel
{
    public class ScopeViewModel
    {
        public String Titulo { get; set; }
        public String Id { get; set; }
        public List<CardViewModel> Cards{ get; set; }

        public ScopeViewModel(string titulo,String id)
        {
            Titulo = titulo;
            Id = id;
        }
        public ScopeViewModel()
        {

        }
        public void AddCards(List<CardViewModel> cards)
        {
            Cards = cards;
        }
    }
}
