using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoApp.Repository.Interfaces;
using ToDoApp.Repository.Models;

namespace ToDoApp.Repository
{
    public class UserRepository : IUserRepositoryInterface
    {
        private readonly ToDoAppContext _appContext;
        public UserRepository(ToDoAppContext appContext)
        {
           _appContext = appContext;
        }

        public bool ValidateUser(string username, string password)
        {
            var users = _appContext.Users
                                .Where(user=>user.UserName.Equals(username) && user.Password.Equals(password));
            if(users.Any())
            {
                return true;
            }
            return false;
        }

        public Boolean SignUp(string username, string password)
        {
            var user = _appContext.Users.Where(user => user.UserName.Equals(username));
            if (user.Any())
            {
                return true;
            }
            User userCredentials = new User
            {
                UserName = username,
                Password = password
            };
            _appContext.Users.Add(userCredentials);
            _appContext.SaveChanges();
            return false;
        }

        public int GetUserId(string username, string password)
        {
            var user = _appContext.Users
                .Where(user => user.UserName.Equals(username) && user.Password.Equals(password));
            return user.Select(user => user.UserId).FirstOrDefault();
        }


    }
}
