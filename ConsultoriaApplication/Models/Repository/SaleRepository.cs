using ConsultoriaApplication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models.Repository
{
    public interface ISaleRepository
    {
        public void CreateSale(Sale s);
        public Sale GetSale(string id);
        public List<Sale> GetSales();
        public void UpdateSale(Sale sale);
        public void DeleteSale(Sale sale);
    }
    public class SaleRepository : BaseRepository<Sale>, ISaleRepository
    {
        public SaleRepository(ConsultoriaContext context) : base(context)
        {
        }
        public void CreateSale(Sale s)
        {

            dbSet.Add(s);
            context.SaveChanges();
        }
        public Sale GetSale(string id)
        {
            Sale s = dbSet.Where(t => t.Id == id).SingleOrDefault();
            return s;
        }
        public List<Sale> GetSales()
        {
            return dbSet.Where(t => t.Id != null).ToList();
        }
        public void UpdateSale(Sale sale)
        {
            dbSet.Update(sale);
            context.SaveChanges();
        }
        public void DeleteSale(Sale sale)
        {
            dbSet.Remove(sale);
            context.SaveChanges();
        }

    }
}
