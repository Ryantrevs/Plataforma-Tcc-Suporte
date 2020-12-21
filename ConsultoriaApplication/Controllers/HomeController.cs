using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ConsultoriaApplication.Models;
using ConsultoriaApplication.Models.ViewModel;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Drawing.Printing;
using Microsoft.AspNetCore.Builder;
using System.Security.Principal;
using Grpc.Core;
using ConsultoriaApplication.Servicos;
using Microsoft.Extensions.WebEncoders.Testing;
using Microsoft.AspNetCore.Identity;

namespace ConsultoriaApplication.Controllers
{   
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        public readonly DirectoryInfo dir = new DirectoryInfo(@"C:\Users\Patrick\Desktop\ConsultoriaApplication\ConsultoriaApplication\wwwroot\ArquivosEnviados");
        private readonly IServicoEmail mailService;
        private readonly ITwilioService twilioService;
        private readonly UserManager<User> userManager;
        public HomeController(UserManager<User> userManager,ILogger<HomeController> logger, IServicoEmail mailService, ITwilioService twilioService)
        {
            _logger = logger;
            this.mailService = mailService;
            this.twilioService = twilioService;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await userManager.GetUserAsync(User);
            ViewBag.UserInfo = user;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Index(EnvioViewModel model)
        {
            //String[] extensao = { ".jpg", ".gif",".png",".pdf","docx",".tmp",".zip",".7zip"};
            if (ModelState.IsValid)
            {
                List<String> lista = new List<String>();
                List<String> linkDownload = new List<String>();
                var pasta = dir.GetDirectories(model.id).FirstOrDefault();
                if (pasta != null)
                {
                    foreach(var item in model.arquivos)
                    {
                        String[] nomeArquivo = item.FileName.Split(".");
                        String nome = model.id + "_" + model.tipoEntrega + "." + nomeArquivo[nomeArquivo.Length - 1];
                        var archive = pasta.GetFiles(nome);
                        if (archive.Length>0)
                        {
                            ModelState.AddModelError("", "Arquivo já existe, requisição invalida");
                            return View();
                        }
                        using (var stream = new FileStream(Path.Combine(pasta.ToString(), nome), FileMode.Create, FileAccess.Read))
                        {
                            await item.CopyToAsync(stream);
                        }
                        lista.Add(Path.Combine(pasta.ToString(), nome));

                    }
                    await mailService.EnviarEmail("patrick", "patrick.campos55@gmail.com", lista);
                    return View();
                }
                var diretorio = dir.CreateSubdirectory(model.id);
                foreach (var item in model.arquivos)
                {
                    String[] nomeArquivo = item.FileName.Split(".");
                   
                    String nome = model.id + "_" + model.tipoEntrega + "." + nomeArquivo[nomeArquivo.Length - 1];
                    using (var stream = new FileStream(Path.Combine(diretorio.ToString(), nome), FileMode.Create))
                    {
                        await item.CopyToAsync(stream);
                    }
                    lista.Add(Path.Combine(diretorio.ToString(), nome));
                    linkDownload.Add(Url.Action("download", "Home", new { model.id, nome }, Request.Scheme));
                }
                await twilioService.EnviarTrabalhoAsync(linkDownload, model.id);
                await mailService.EnviarEmail("patrick", "patrick.campos55@gmail.com", lista);
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> download(String id, String arquivo)
        {
            var pasta = dir.GetDirectories(id).FirstOrDefault();
            var archive = pasta.GetFiles(arquivo).FirstOrDefault();
            var fs = new FileStream(Path.Combine(dir.ToString(), pasta.ToString(), archive.ToString()),FileMode.Open);
            return File(fs, "application/octet-stream", arquivo);
        }
        public async Task<String[]> getId(String Nome)
        {
            String[] lista = { "teste", "marcell", "Matheus" };
            return lista;
        }

       
    }
}
