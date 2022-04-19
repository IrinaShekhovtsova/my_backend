using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Shekhovtsova_backend.Models;
using Shekhovtsova_backend.Dtos;
using Shekhovtsova_backend.Services;
using Shekhovtsova_backend.Interfaces;
using System.Text;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;

namespace Shekhovtsova_backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthContext _context;
        private readonly IUser userService;

        public AuthController(AuthContext context, IUser service)
        {
            _context = context;
            userService = service;
        }


        // POST: api/Auth/AddUser
        [HttpPost("AddUser")]
        public IActionResult AddUser([FromForm] Person person)
        {
            if (!userService.AddUser(person))
                return NotFound();
            else return Ok(person);
        }

        // POST: api/Auth
        [HttpPost]

        public IActionResult GetToken([FromForm] LoginData ld)
        {
            if (userService.GetToken(ld) is null) return Unauthorized();
            else return Ok(userService.GetToken(ld));
        }

        


    }
}
