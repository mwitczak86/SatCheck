using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SatCheck.Models
{
    public class ReportObj
    {
        public int Id_Stacji { get; set; }
        public string Nazwa { get; set; }
        public string AdresSat { get; set; }
        public string AdresEth { get; set; }
        public string Interfejs_1 { get; set; }
        public string Interfejs_2 { get; set; }

        public string RTT_SAT { get; set; }

        public string RTT_ETH {get; set;}

    }


}
