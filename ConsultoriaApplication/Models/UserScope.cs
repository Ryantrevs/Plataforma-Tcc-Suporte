using ConsultoriaApplication.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models
{
    public class UserScope
    {
        public virtual User user { get; set; }
        public String UserId { get; set; }
        public String ScopeId { get; set; }
        public UserFunction Function { get; set; }
        public virtual Scope scope { get; set; }
    }
}
