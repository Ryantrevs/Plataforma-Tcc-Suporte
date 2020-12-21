using ConsultoriaApplication.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;
using Twilio;
using Twilio.Http;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace ConsultoriaApplication.Servicos
{
    public interface ITwilioService
    {
        public Task EnviarTrabalhoAsync(List<String> links, String id);
    }
    public class TwilioService : ITwilioService
    {
        string accountSid = "xxx";
        string authToken = "xxx";


        public async Task EnviarTrabalhoAsync(List<String> links,String id)
        {
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
            new PhoneNumber("whatsapp:+5521970240251"));
            messageOptions.From = new PhoneNumber("whatsapp:+14155238886");
            messageOptions.Body = "Temos boas novas, seu trabalho acaba de ficar pronto\n\n esperamos que você aproveite seu tempo poupado!\n\n\n TCC SUPORTE";
            List<Uri> media = new List<Uri>();
            foreach (var link in links)
            {
                media.Append(new Uri(link));
            }
            messageOptions.MediaUrl = media; //inserir link aqui

            var message = await MessageResource.CreateAsync(messageOptions);
            Console.WriteLine(message.Status);
        }

    }
}
