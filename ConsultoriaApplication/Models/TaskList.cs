using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models
{
    public class TaskList
    {
        public String Id { get; set; }
        public String Titulo { get; set; }
        public List<Scope> Scopes { get; set; }
        public virtual List<UserTasklist> UserTasklist { get; set; }

        public TaskList(string id, string titulo)
        {
            Id = id;
            Titulo = titulo;
        }

        public TaskList(String Titulo, Scope scope, User user)
        {
            this.Titulo = Titulo;
            Scopes.Add(scope);
            
        }
        public TaskList()
        {

        }

        public TaskList(string titulo)
        {
            Titulo = titulo;
        }
    }
}
