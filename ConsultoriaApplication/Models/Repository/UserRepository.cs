using ConsultoriaApplication.Data;
using ConsultoriaApplication.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConsultoriaApplication.Models.Repository
{
    public interface IUserRepository
    {
        public Task CreateUser(User newUser);
        public User GetUser(String id);
        public UserViewModel User(String userId);

    }
    public class UserRepository : BaseRepository<User>, IUserRepository
    {

        public UserRepository(ConsultoriaContext context):base(context)
        {
        }

        public User GetUser(String id)
        {
            User user = dbSet.Where(t => t.Id == id).SingleOrDefault();
            return user;
        }
        public async Task CreateUser(User newUser)
        {
            dbSet.Add(newUser);
            await context.SaveChangesAsync();
        }
        public UserViewModel User(String userId)
        {
            UserViewModel user = dbSet.Where(t => t.Id == userId).Select(x => new UserViewModel(x.Id, x.Nome)).FirstOrDefault();
            return user;
        }

    }
}
