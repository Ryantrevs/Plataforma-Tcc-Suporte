using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models
{
    public class Job

    {
        public string ClientId { get; set; }
        public String Id { get; set; }
        public Client Client { get; set; }
        public String InstuicaoEnsino { get; set; }
        public String NomeOrientador { get; set; }
        public String Curso { get; set; }
        public String Tema { get; set; }
        public int NumPaginas { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Previa_1 { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Previa_2 { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Previa_3 { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DataEntrega { get; set; }
        public String Observacoes { get; set; }
        public List<Sale> Sales { get; set; }

        public Job()
        {

        }

        public Job(string clientId, string id, string instuicaoEnsino, string nomeOrientador, string curso, string tema, int numPaginas, DateTime previa_1, DateTime previa_2, DateTime previa_3, DateTime dataEntrega, string observacoes)
        {
            ClientId = clientId;
            Id = id;
            InstuicaoEnsino = instuicaoEnsino;
            NomeOrientador = nomeOrientador;
            Curso = curso;
            Tema = tema;
            NumPaginas = numPaginas;
            Previa_1 = previa_1;
            Previa_2 = previa_2;
            Previa_3 = previa_3;
            DataEntrega = dataEntrega;
            Observacoes = observacoes;            
        }
    }
}
