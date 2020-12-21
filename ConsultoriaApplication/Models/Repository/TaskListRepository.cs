using ConsultoriaApplication.Data;
using ConsultoriaApplication.Models.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models.Repository
{
    public interface ITaskListRepository
    {
        public List<TasksViewModel> getTasks(List<String> lista);
        public Task setTask(TasksViewModel taskViewModel);
        public TaskList InsertTaskList(String id, String Titulo);
        public TaskList GetTask(String Id);
        public void UpdateTaskList(String id, String titule);
        public void RemoveTaskList(String id);
    }
    public class TaskListRepository: BaseRepository<TaskList>,ITaskListRepository
    {

        public TaskListRepository(ConsultoriaContext context):base(context)
        {
        }

        public async Task setTask(TasksViewModel taskViewModel)
        {
            TaskList task = new TaskList(taskViewModel.Titulo);
            dbSet.Add(task);
            await context.SaveChangesAsync();
        }

        public List<TasksViewModel> getTasks(List<String> lista)
        {
            List<TasksViewModel> tasks = new List<TasksViewModel>();
            foreach (var item in lista)
            {
                tasks.Add(dbSet.Where(t=>t.Id==item).Select(t=>new TasksViewModel(t.Titulo,t.Id)).FirstOrDefault());
            }
            return tasks;
        }
        public TaskList InsertTaskList(String id, String Titulo)
        {
            var taskList = new TaskList(id, Titulo);
            dbSet.Add(taskList);
            context.SaveChanges();
            return taskList;
        }
        public void UpdateTaskList(String id,String titule)
        {
            var list = GetTask(id);
            list.Titulo = titule;
            dbSet.Update(list);
            context.SaveChanges();
        }
        public TaskList GetTask(String Id)
        {
            return dbSet.Where(t => t.Id == Id).FirstOrDefault();
        }
        public void RemoveTaskList(String id)
        {
            var list = GetTask(id);
            dbSet.Remove(list);
            context.SaveChanges();
        }
        

    }
}
