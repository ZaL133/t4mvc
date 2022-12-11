using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using t4mvc.core;

namespace t4mvc.data.services
{
    public partial interface IUserService
    {
        t4mvcUser Find(params object[] keyValues);
        IQueryable<t4mvcUser> GetAllUsers();
        void CreateUser(t4mvcUser user);
        void UpdateUser(t4mvcUser user, IEnumerable<string> ignore);
        void DeleteUser(t4mvcUser user);
    }

    public partial class UserService : IUserService
    {
        private readonly t4DbContext context;

        public UserService(t4DbContext context)
        {
            this.context = context;
        }
        public void CreateUser(t4mvcUser user)
        {
            this.context.Users.Add(user);
        }

        public t4mvcUser Find(params object[] keyValues)
        {
            return this.context.Users.Find(keyValues);
        }

        public IQueryable<t4mvcUser> GetAllUsers()
        {
            return this.context.Users.AsQueryable();
        }

        public void UpdateUser(t4mvcUser user, IEnumerable<string> ignore)
        {
            this.context.Users.Attach(user);

            var entry = this.context.Entry(user);
            entry.State = EntityState.Modified;

            foreach (var prop in ignore)
            {
                entry.Property(prop).IsModified = false;
            }
        }

        public void DeleteUser(t4mvcUser user)
        {
            this.context.Users.Remove(user);
        }
    }
}
