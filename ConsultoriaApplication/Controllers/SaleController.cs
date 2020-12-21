using ConsultoriaApplication.Models;
using ConsultoriaApplication.Models.Repository;
using ConsultoriaApplication.Models.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Controllers
{
    public class SaleController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IClientRepository ClientRepository;
        private readonly IUserRepository UserRepository;
        private readonly IJobRepository JobRepository;
        private readonly ISaleRepository SaleRepository;

        public SaleController(UserManager<User> userManager,IClientRepository ClientRepository,IUserRepository UserRepository,IJobRepository JobRepository,ISaleRepository SaleRepository)
        {
            this.userManager = userManager;
            this.ClientRepository = ClientRepository;
            this.UserRepository = UserRepository;
            this.JobRepository = JobRepository;
            this.SaleRepository = SaleRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(SaleViewModel svm)
        {
            if (ModelState.IsValid)
            {
                Client client = ClientRepository.GetClientByMail(svm.Client.Mail);
                if (client == null)// cliente não existe
                {//cadastra
                    svm.Client.Id = Guid.NewGuid().ToString();//gera id
                    ClientRepository.CreateClient(svm.Client);
                }
                else // cliente existe
                {
                    svm.Client.Id = client.Id;//pega o id para repassar ao banco a referencia
                }
                //info que precisa mudar, apenas de teste manual
                string idJob = Guid.NewGuid().ToString();//id de teste
                var userObj = await userManager.GetUserAsync(User);
                string user = userObj.Id;
                string saleId = Guid.NewGuid().ToString();
                //Job sempre vai ser novo, não tem porque checar
                Job j = new Job(svm.Client.Id,idJob,svm.Job.InstuicaoEnsino, svm.Job.NomeOrientador, svm.Job.Curso, svm.Job.Tema, svm.Job.NumPaginas, svm.Job.Previa_1, svm.Job.Previa_2, svm.Job.Previa_3, svm.Job.DataEntrega, svm.Job.Observacoes);
                JobRepository.CreateJob(j);

                Sale s = new Sale(svm.Client.Id,user,idJob,saleId,svm.Sale.Cartao, svm.Sale.Fechamento, svm.Sale.EstagioPagamento, svm.Sale.ValorTotal, svm.Sale.ValorPago);
                SaleRepository.CreateSale(s);
                ModelState.Clear();
                return View();
            }
            ModelState.Clear();
            return View();
        }
        
        [HttpGet]
        public Client GetDadosCliente(String mail)
        {
            var cli = ClientRepository.GetClientByMail(mail);
            return cli;
        }
        [HttpGet]
        public IActionResult Table()//retorana lista completa de vendas com clientes
        {
            List<Sale> sales=SaleRepository.GetSales();
            List<SaleViewModel> lista = new List<SaleViewModel>();
            foreach (Sale s in sales)
            {
                Job j = JobRepository.GetJob(s.JobId);
                Client c = ClientRepository.GetClient(s.ClientId);
                User u = UserRepository.GetUser(s.UserId);

                SaleViewModel svm = new SaleViewModel(c,j,s,u);
                lista.Add(svm);
            }
            return View(lista);
        }
        [HttpGet]
        public IActionResult Details(string id)
        {
            Sale sale=SaleRepository.GetSale(id);
            
            Job job = JobRepository.GetJob(sale.JobId);
            Client client =  ClientRepository.GetClient(sale.ClientId);
            User user = UserRepository.GetUser(sale.UserId);
            SaleViewModel svm = new SaleViewModel(client, job, sale,user);

            return View(svm);
        }
        [HttpGet]
        public IActionResult Edit(string id)
        {
            Sale sale = SaleRepository.GetSale(id);
            Job job = JobRepository.GetJob(sale.JobId) ;
            Client cli = ClientRepository.GetClient(sale.ClientId);
            SaleViewModel svm = new SaleViewModel();
            svm.Sale = sale;
            svm.Job = job;
            svm.Client = cli;
            return View(svm);
        }
        [HttpPost]
        public IActionResult Edit(SaleViewModel svm)
        {
            JobRepository.UpdateJob(svm.Job);
            SaleRepository.UpdateSale(svm.Sale);

            return RedirectToAction("Table");
        }
        [HttpGet]
        public IActionResult EditJob(string id)
        {
            Job job= JobRepository.GetJob(id);
            return View(job);
        }
        [HttpPost]
        public IActionResult EditJob(Job job)
        {
            JobRepository.UpdateJob(job);

            return RedirectToAction("Table");
        }
        [HttpPost]
        public IActionResult DeleteSale(string id)//id do sale
        {
            Sale s = SaleRepository.GetSale(id);
            Job j = JobRepository.GetJob(s.JobId);
            SaleRepository.DeleteSale(s);
            JobRepository.DeleteJob(j);
            return RedirectToAction("Table");
        }




    }



}
