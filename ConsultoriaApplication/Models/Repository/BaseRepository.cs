using ConsultoriaApplication.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models.Repository
{
    public class BaseRepository<T> where T: class
    {
        protected readonly ConsultoriaContext context;
        protected readonly DbSet<T> dbSet;

        public BaseRepository(ConsultoriaContext context)
        {
            this.context = context;
            dbSet = context.Set<T>();
        }
    }
}
