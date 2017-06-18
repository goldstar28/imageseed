using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ImageSeeds.Codes.Entities;

namespace ImageSeeds.Codes.Repositories
{
    public class UserRepository : RepositoryBase
    {
        public List<User> Listuser()
        {
            using (var context = GetDbContext())
            {
                return context.Users.ToList();
            }
        }

        public User GetUserById(long id)
        {
            using (var context = GetDbContext())
            {
                return context.Users.FirstOrDefault(x => x.Id == id);
            }
        }

        public long InsertUser(User obj)
        {
            using (var context = GetDbContext())
            {
                context.Users.Add(obj);
                context.SaveChanges();
                return obj.Id;
            }
        }

        public long UpdateUser(User obj)
        {
            using (var context = GetDbContext())
            {
                context.Users.Attach(obj);
                context.Entry(obj).State = EntityState.Modified;
                context.SaveChanges();
                return obj.Id;
            }
        }

        public void DeleteUser(int id)
        {
            using (var context = GetDbContext())
            {
                var objToDelete = context.Users.Find(id);
                context.Users.Remove(objToDelete);
                context.SaveChanges();
            }
        }
    }
}