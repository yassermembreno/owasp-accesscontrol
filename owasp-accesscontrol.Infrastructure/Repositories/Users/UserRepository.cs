using System;
using owasp_accesscontrol.Domain.Entities;
using owasp_accesscontrol.Domain.Interfaces;

namespace owasp_accesscontrol.Infrastructure.Repositories.Users
{
    public class UserRepository : IUserRepository
    {
        private dbloanmanagerContext context;

        public UserRepository(dbloanmanagerContext context)
        {
            this.context = context;
        }

        public int Create(User t)
        {
            context.Add(t);
            return context.SaveChanges();
        }

        public bool Delete(User t)
        {
            context.Remove(t);
            return context.SaveChanges() > 0;
        }

        public IEnumerable<User> FindAll()
        {
            return context.Users.ToList();
        }

        public User? GetUser(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                return null;
            }

            return context.Users.Where(u => u.UserName.Equals(userName) &&
                                            u.Password.Equals(password)).FirstOrDefault();
        }

        public int Update(User t)
        {
            context.Users.Update(t);
            return context.SaveChanges();
        }
    }
}

