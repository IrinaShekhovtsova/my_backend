using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shekhovtsova_backend.Models;
using Shekhovtsova_backend.Dtos;
using Shekhovtsova_backend.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Shekhovtsova_backend.Services
{
    public class UserService : IUser
    {
        private readonly AuthContext _context;

        public UserService(AuthContext context)
        {
            _context = context;
        }

        public bool AddUser(Person person)
        {
            if(!UserAlreadyExists(person.Login)) _context.Persons.Add(person);
            return _context.SaveChanges() > 0;
        }
        

        public bool UserAlreadyExists(string login)
        {
            return _context.Persons.Any(u => u.Login == login);
        }


        public object GetToken(LoginData ld)
        {
            var user = _context.Persons.FirstOrDefault(u => u.Login == ld.login && u.Password == ld.Password);
            if (user == null)
            {
                return null;
            }
            return AuthOptions.GenerateToken(user);

        }

    }
}
