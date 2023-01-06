using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace SatCheck.Models
{
    public class PingResult
    {

        [PrimaryKey, AutoIncrement]
        public int Lp { get; set; }

        public string Date { get; set; }
           
        public int Id_Stacji { get; set; }
                       
        public string Nazwa { get; set; }
        public string AdresSat { get; set; }
        public string AdresEth { get; set; }
        public string Rola { get; set; }
        public string Online { get; set; }

        public int RTT { get; set; }
    }
}
