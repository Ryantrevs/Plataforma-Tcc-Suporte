using ConsultoriaApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models.Repository
{
    public interface IUserTasklistRepository
    {
        public List<String> GetListId(String id);
        public void InsertUserTasklist(TaskList task, User user);
        public List<UserTasklist> GetUserTaskList(String Id);
        public void RemoveUserTaskList(String Id);
        public void RemoveUser(User user, String id);
        public UserTasklist GetTask(String userId, String id);
    }
    public class UserTasklistRepository : BaseRepository<UserTasklist>,IUserTasklistRepository
    {
        public UserTasklistRepository(ConsultoriaContext context):base(context)
        {

        }
        public List<String> GetListId(String id)
        {
            return dbSet.Where(t => t.UserId == id).Select(t => t.TaskId).ToList();
        }
        public void InsertUserTasklist(TaskList task, User user)
        {
            var userTask = new UserTasklist(user,task);
            dbSet.Add(userTask);
            context.SaveChanges();
        }
        public void RemoveUserTaskList(String Id)
        {
            var list = GetUserTaskList(Id);
            foreach(var item in list)
            {
                dbSet.Remove(item);
            }
            context.SaveChanges();
        }
        public List<UserTasklist> GetUserTaskList(String Id)
        {
            return dbSet.Where(t => t.TaskId == Id).ToList();
        }
        public UserTasklist GetTask(String userId, String id)
        {
            return dbSet.Where(t => t.TaskId == id && t.UserId == userId).FirstOrDefault();
        }
        public void RemoveUser(User user,String id)
        {
            var userTaskList = GetTask(user.Id,id);
            dbSet.Remove(userTaskList);
            context.SaveChanges();
        }
    }
}
