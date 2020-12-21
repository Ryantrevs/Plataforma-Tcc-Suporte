using ConsultoriaApplication.Data;
using ConsultoriaApplication.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models.Repository
{
    public interface IScopeRepository
    {
        public List<ScopeViewModel> getScope(String id);
        public bool InsertRepository(String id, String taskId);
        public void ChangeTitule(String id, String titule);
        public Scope GetScope(String id);
        public void RemoveScope(String id);
    }
    public class ScopeRepository:BaseRepository<Scope>,IScopeRepository
    {
        public ScopeRepository(ConsultoriaContext context):base(context)
        {
        }

        public List<ScopeViewModel> getScope(String id)
        {
            return dbSet.Where(t=>t.TaskId==id).OrderBy(a=>a.Titule).Select(x=>new ScopeViewModel(x.Titule, x.id)).ToList();
        }
        public bool InsertRepository(String id, String taskId)
        {
            var resultado = dbSet.Add(new Scope(id, "Insira seu titulo aqui", taskId)).State;
            context.SaveChanges();
            if (resultado.ToString() == "Added")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ChangeTitule(String id, String titule)
        {
            var scope = dbSet.Where(t => t.id == id).FirstOrDefault();
            scope.Titule = titule;
            dbSet.Update(scope);
            context.SaveChanges();
        }
        public void RemoveScope(String id)
        {
            var scope = GetScope(id);
            dbSet.Remove(scope);
            context.SaveChanges();
        }
        public Scope GetScope(String id)
        {
            return dbSet.Where(t => t.id == id).FirstOrDefault();
        }
    }
}
