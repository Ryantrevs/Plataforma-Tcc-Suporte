using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models
{
    public class Sale
    {
        public String Id { get; set; }
        public string ClientId { get; set; }
        public string UserId { get; set; }
        public string JobId { get; set; }
        public Client Client { get; set; }
        public User User { get; set; }
        public Job Job { get; set; }        
        public bool Cartao { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Fechamento { get; set; }
        public String EstagioPagamento { get; set; }
        public double ValorTotal { get; set; }
        public double ValorPago { get; set; }
        public Sale()
        {

        }

        public Sale(string clientId, string userId, string jobId, string id, bool cartao, DateTime fechamento, string estagioPagamento, double valorTotal, double valorPago)
        {
            ClientId = clientId;
            UserId = userId;
            JobId = jobId;
            Id = id;
            Cartao = cartao;
            Fechamento = fechamento;
            EstagioPagamento = estagioPagamento;
            ValorTotal = valorTotal;
            ValorPago = valorPago;
        }
    }
}
