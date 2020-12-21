using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsultoriaApplication.Models;
using ConsultoriaApplication.Models.Repository;
using ConsultoriaApplication.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace ConsultoriaApplication.Controllers
{
    public class AcademicoController : Controller
    {
        private readonly IUserRepository userRepository;
        private readonly ITaskListRepository taskListRepository;
        private readonly ICardRepository cardRepository;
        private readonly IScopeRepository scopeRepository;
        private readonly IUserTasklistRepository userTasklistRepository;
        private readonly UserManager<User> userManager;
        
        public AcademicoController(IUserRepository userRepository, ITaskListRepository taskListRepository, ICardRepository cardRepository, IScopeRepository scopeRepository, IUserTasklistRepository userTasklistRepository,UserManager<User> userManager)
        {
            this.userRepository = userRepository;
            this.taskListRepository = taskListRepository;
            this.cardRepository = cardRepository;
            this.scopeRepository = scopeRepository;
            this.userTasklistRepository = userTasklistRepository;
            this.userManager = userManager;
            
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Organizacao()
        {
            User user = await userManager.GetUserAsync(User); 
            var lista = userTasklistRepository.GetListId(user.Id);
            List<TasksViewModel> tasks = taskListRepository.getTasks(lista);
            return View(tasks);
        }

        [HttpPost]
        public Card GetCard(String Id)
        {
            var task = cardRepository.GetCard(Id);
            return task;
            //return "teste controller";
        }

        [HttpPost]
        public List<ScopeViewModel> GetTasks(String Id)
        {
            
            var list = scopeRepository.getScope(Id);
            var cards = cardRepository.getCards(list);
            return cards;
        }
        [HttpPost]
        public String InsertScope(String taskId)
        {
            var id = Guid.NewGuid().ToString();
            var resultado = scopeRepository.InsertRepository(id, taskId);
            if(resultado == true)
            {
                return id;
            }
            else
            {
                return resultado.ToString();
            }
        }
        [HttpPost]
        public async  Task<string> InsertTaskList(string Titulo)
        {
            var id = Guid.NewGuid().ToString();
            var task = taskListRepository.InsertTaskList(id, Titulo);
            var user = await userManager.GetUserAsync(User);
            userTasklistRepository.InsertUserTasklist(task, user);

            return id;
        }
        [HttpPost]
        public String changeScopeTitule(String titule, String id)
        {
            scopeRepository.ChangeTitule(id, titule);
            return id;
        }
        [HttpPost]
        public string ChangeCardScope(String IdCard, String ScopeId)
        {
            Card c = cardRepository.GetCard(IdCard);
            c.ScopeId = ScopeId;
            cardRepository.UpdateCard(c);
            return "tipo";
        }
        [HttpPost]
        public String InsertCard (String ScopeId,String Titule, String Describe)
        {
            var id = Guid.NewGuid().ToString();
            cardRepository.InsertCard(Titule, ScopeId, Describe,id,DateTime.Now.AddDays(7));
            return id;
        }
        public String DeleteCard(String Id)
        {
            cardRepository.DeleteCard(Id);
            return "funcionou";
        }
        [HttpPost]
        public String ChangeCard(String Id,String Titule,String Describe,int Porc)
        {
            var card = GetCard(Id);
            card.Titulo = Titule;
            card.Descricao = Describe;
            card.Porcentagem = Porc;
            cardRepository.UpdateCard(card);
            return "funcionou";
        }
        [HttpPost]
        public String ExcludeList(String Id)
        {
            userTasklistRepository.RemoveUserTaskList(Id);
            taskListRepository.RemoveTaskList(Id);
            return "teste";
        }
        [HttpPost]
        public async Task<String> AddUserOnList(String Email, String Id)
        { 
            var task = taskListRepository.GetTask(Id);
            var user = await userManager.GetUserAsync(User);
            userTasklistRepository.InsertUserTasklist(task, user);
            return "teste";
        }
        [HttpPost]
        public List<UserViewModel> UsersOnList(String Id)
        {
            var list = userTasklistRepository.GetUserTaskList(Id);
            List<UserViewModel> users = new List<UserViewModel>();
            foreach(var item in list)
            {
                var user = userRepository.User(item.UserId);
                users.Add(user);
            }
            return users;
        }
        [HttpPost]
        public String ExcludeUserOnList(String userId, String Id)
        {
            var user = userRepository.GetUser(userId);
            userTasklistRepository.RemoveUser(user, Id);
            if (userTasklistRepository.GetListId(Id)==null){
                taskListRepository.RemoveTaskList(Id);
            }
            var teste = userTasklistRepository.GetTask(user.Id, Id);
            return "teste";
        }
        [HttpPost]
        public String UpdateTitule(String Id,String Titule)
        {
            taskListRepository.UpdateTaskList(Id, Titule);
            return "teste";
        }
        [HttpPost]
        public String RemoveScope(String Id)
        {
            cardRepository.DeleteOnCascade(Id);
            scopeRepository.RemoveScope(Id);
            return "teste";
        }

    }
}
