using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models.ViewModel
{
    public class EnvioViewModel
    {
        public String id { get; set; }
        public SendType tipoEntrega { get; set; }
        public List<IFormFile> arquivos { get; set; }
    }
}
