using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shekhovtsova_backend.Models;
using Shekhovtsova_backend.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Shekhovtsova_backend.Interfaces
{
    public interface IUser
    {
        public bool AddUser(Person person);
        public bool UpdateEnergyCard(int id, Person person);

        public bool UserAlreadyExists(string login);

        public object GetToken(LoginData ld);

    }
}
