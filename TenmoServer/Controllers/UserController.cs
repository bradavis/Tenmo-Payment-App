using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : Controller
    {
        private readonly IUserDAO userDAO;

        public UserController(IUserDAO _userDAO) //constructor
        {
            userDAO = _userDAO;
        }



        [HttpGet]
        [AllowAnonymous]
        public List<User> ListUsers()
        {
            List<User> users;  
            users = userDAO.GetUsers();
            return users;
                
        }


    }
}
