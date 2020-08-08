using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")] // account
    [ApiController]
    [Authorize]
    public class AccountController : Controller
    {
        private static IUserDAO userDAO;
        private static IAccountDAO aDAO;


        public AccountController(IUserDAO _userDAO, IAccountDAO _aDAO)
        {
            userDAO = _userDAO;
            aDAO = _aDAO;
        }

        [HttpGet("balance")]
        public decimal GetBalance()
        {
            var userId = User.FindFirst("sub")?.Value;
            int id = Convert.ToInt32(userId);
            return aDAO.GetBalance(id);

            //jwt c# web api get user
        }
    }
}