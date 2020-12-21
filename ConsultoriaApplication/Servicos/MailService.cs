using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace ConsultoriaApplication.Servicos
{

    public interface IServicoEmail
    {
        public Task<SendGrid.Response> EnviarEmail(String nome, String destinatario, List<String> arquivos);
    }
    public class MailService : IServicoEmail
    {
        public async Task<SendGrid.Response> EnviarEmail(String nome, String destinatario, List<String> arquivos)
        {
            
            var client = new SendGridClient("xxx");
            var from = new EmailAddress("mailmkt@suportetcc.com", "TccSuporte");
            var subject = "Temos Boas Novas";
            var to = new EmailAddress(destinatario, nome);
            var plainTextContent = "Seu trabalho acaba de ser finalizado, estamos muito feliz em poder estar te ajudando nessa etapa ´da vida, aproveite e se puder, recomende para seus amigos o nosso serviço";
            var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent,"teste");
            foreach (var arq in arquivos)
            {
                String[] nomeArquivo = arq.Split(@"\");
                byte[] byteArray = Encoding.UTF8.GetBytes(arq);

                Stream theStream = new MemoryStream(byteArray);

                await msg.AddAttachmentAsync(nomeArquivo[nomeArquivo.Length-1], theStream);
            }
            var response = await client.SendEmailAsync(msg);
            return response;
        }
    }
}
