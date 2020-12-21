using ConsultoriaApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models.Repository
{
    public interface IJobRepository
    {
        public void CreateJob(Job Job);
        public Job GetJob(string id);
        public void UpdateJob(Job job);
        public List<Job> GetJobs();
        public void DeleteJob(Job job);
    }
    public class JobRepository : BaseRepository<Job>,IJobRepository
    {

        public JobRepository(ConsultoriaContext context): base(context)
        {
        }

        public void CreateJob(Job Job)
        {
            dbSet.Add(Job);
            context.SaveChanges();

        }
        public Job GetJob(string id)
        {
            Job j = dbSet.Where(t => t.Id == id).SingleOrDefault();
            return j;
        }
        public List<Job> GetJobs()
        {
            return dbSet.Where(t => t.Id != null).ToList();
        }
        public void UpdateJob(Job job)
        {
            dbSet.Update(job);
            context.SaveChanges();
        }
        public void DeleteJob(Job job)
        {
            dbSet.Remove(job);
            context.SaveChanges();
        }

    }
}
