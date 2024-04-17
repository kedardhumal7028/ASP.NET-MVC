using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TokenAuthAPI.Data;
using TokenAuthAPI.Models;

namespace TokenAuthAPI.UserRepository
{
    public class UserRepo : IDisposable
    {
        ApplicationDbContext dbContext = new ApplicationDbContext();

      

        public User ValidateUser(string username, string password)
        {
           return dbContext.Users.FirstOrDefault(user => user.Username.Equals(username, StringComparison.OrdinalIgnoreCase)
            && user.Password == password);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}