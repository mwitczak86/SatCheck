using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatCheck.Models
{
    public class PingRep
    {
       
        public string Address { get; set; }
        public string Rtt { get; set; }
        public string TTL { get; set; }
        public string Buffer { get; set; }

        public string Status { get; set; }

    }
}
