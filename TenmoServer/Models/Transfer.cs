using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TenmoServer.Models
{
    public class Transfer
    {
        public int ID { get; set; }
        public string FromUser { get; set; }
        public string ToUser { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public decimal Amount { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
    }
}
