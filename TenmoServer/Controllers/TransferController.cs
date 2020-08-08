using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TenmoServer.DAO;
using TenmoServer.Models;

namespace TenmoServer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class TransferController : Controller
    {
        private static IUserDAO userDAO;
        private static IAccountDAO aDAO;
        private static ITransferDAO tDAO;

        public TransferController(IUserDAO _userDAO, IAccountDAO _aDAO, ITransferDAO _tDAO)
        {
            userDAO = _userDAO;
            aDAO = _aDAO;
            tDAO = _tDAO;
        }

        [HttpPost]
        public ActionResult<Transfer> AddTransaction()
        {

            return null; 
        }


    }
}