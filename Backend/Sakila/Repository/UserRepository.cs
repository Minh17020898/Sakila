using Sakila.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sakila.Repository
{
    public interface IUserRepository
    {
        User Get(string username, string password);
        User Get(Guid userId);
        User Create(string username, string password);
        bool IsExisted(string username);
    }

    public class UserRepository : IUserRepository
    {
        private SakilaContext DbContext;

        public UserRepository(SakilaContext context)
        {
            DbContext = context;
        }

        public User Get(string username, string password)
        {
            return DbContext.User.Where(x => x.Username == username && x.Password == password).FirstOrDefault();
        }

        public User Get(Guid userId)
        {
            return DbContext.User.Where(x => x.Id == userId).FirstOrDefault();
        }

        public User Create(string username, string password)
        {
            try
            {
                User user = new User
                {
                    Id = Guid.NewGuid(),
                    Username = username,
                    Password = password,
                    DateCreated = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")
                };
                DbContext.User.Add(user);
                DbContext.SaveChanges();
                return user;
            } 
            catch (Exception)
            {
                return null;
            }
        }

        public bool IsExisted(string username)
        {
            return DbContext.User.Where(x => x.Username == username).Count() != 0;
        }
    }
}
