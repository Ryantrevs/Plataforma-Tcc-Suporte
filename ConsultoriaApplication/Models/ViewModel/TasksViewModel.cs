using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models.ViewModel
{
    public class TasksViewModel
    {
        public String Titulo { get; set; }
        public String id { get; set; }

        public TasksViewModel(String Titulo, String id)
        {
            this.Titulo = Titulo;
            this.id = id;
            
        }
        public TasksViewModel(String Titulo)
        {
            this.Titulo = Titulo;
        }
    }
}
