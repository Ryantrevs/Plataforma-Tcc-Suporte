using ConsultoriaApplication.Data;
using ConsultoriaApplication.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models.Repository
{
    public interface ICardRepository
    {
        public Card GetCard(String id);
        public List<ScopeViewModel> getCards(List<ScopeViewModel> lista);
        public void UpdateCard(Card card);
        public void InsertCard(String titule, String ScopeId, String Descricao, String id,DateTime dataFinal);
        public void DeleteCard(String id);
        public void DeleteOnCascade(String ScopeId);

    }
    public class CardRepository : BaseRepository<Card>,ICardRepository
    {
        public CardRepository(ConsultoriaContext context):base(context)
        {
        }

        public Card GetCard(String id)
        {
            return dbSet.Where(t => t.id == id).FirstOrDefault();
        }
        public List<ScopeViewModel> getCards(List<ScopeViewModel> lista)
        {
            List<CardViewModel> cards = new List<CardViewModel>();
            foreach(var item in lista)
            {
                var teste = dbSet.Where(x => x.ScopeId == item.Id).Select(x => new CardViewModel(x.Titulo,x.id)).ToList();
                item.AddCards(teste);
            }
            return lista;
        }
        public void UpdateCard(Card card)
        {
            dbSet.Update(card);
            context.SaveChanges();
        }
        public void InsertCard(String titule, String ScopeId, String Descricao,String id, DateTime FinalDate)
        {
            bool conclude = false;
            Card card = new Card(id, titule, Descricao, 0, FinalDate, conclude, ScopeId);
            dbSet.Add(card);
            context.SaveChanges();
        }
        public void DeleteCard(String id)
        {
            dbSet.Remove(GetCard(id));
            context.SaveChanges();
        }
        public void DeleteOnCascade(String ScopeId)
        {
            var list = dbSet.Where(t => t.ScopeId == ScopeId).ToList();
            foreach(var item in list)
            {
                dbSet.Remove(item);
            }
            context.SaveChanges();
        }
    }
}
