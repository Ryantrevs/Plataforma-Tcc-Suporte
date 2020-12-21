using ConsultoriaApplication.Models;
using ConsultoriaApplication.Models.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientRepository ClientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            this.ClientRepository = clientRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var clients=ClientRepository.ListClients();
            return View(clients);
        }

        [HttpGet]
        public IActionResult Edit(string id)
        {
            var cli = ClientRepository.GetClient(id);
            return View(cli);
        }

        [HttpPost]
        public IActionResult Edit(Client cli)
        {
            ClientRepository.UpdateClient(cli);
            return RedirectToAction("Index");
        }
    }
}
