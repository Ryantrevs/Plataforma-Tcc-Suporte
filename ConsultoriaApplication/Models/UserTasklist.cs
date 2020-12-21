using ConsultoriaApplication.Models.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models
{
    public class UserTasklist
    {
        public User Users { get; set; }
        public String UserId { get; set; }
        public UserFunction Function { get; set; }
        public TaskList TaskLists { get; set; }
        public String TaskId { get; set; }

        public UserTasklist()
        {

        }

        public UserTasklist(string userId, string taskId)
        {
            UserId = userId;
            TaskId = taskId;
        }

        public UserTasklist(User users, TaskList taskLists)
        {
            Users = users;
            TaskLists = taskLists;
        }
    }
}
