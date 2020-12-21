using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models.ViewModel
{
    public class UserTaskViewModel
    {
        public User User { get; set; }
        public List<TasksViewModel> Tasks { get; set; }

        public UserTaskViewModel(User user, List<TasksViewModel> tasks)
        {
            User = user;
            Tasks = tasks;
        }
    }
}
