using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models
{
    public class Scope
    {
        public String id { get; set; }
        public String Titule { get; set; }
        public virtual TaskList taskList { get; set; }
        public String TaskId { get; set; }
        public List<Card> Cards { get; set; }
        public virtual IEnumerable<UserScope> UserScope { get; set; }

        public Scope()
        {

        }

        public Scope(string id, string titule, string taskId)
        {
            this.id = id;
            Titule = titule;
            TaskId = taskId;
        }
    }
}
