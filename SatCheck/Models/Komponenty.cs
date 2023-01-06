using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLitePCL;

namespace SatCheck.Models
{
    public class Komponenty
    {
        
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public string Nazwa { get; set; }
            public string AdresSat { get; set; }
            public string AdresEth { get; set; }
            public string Rola { get; set; }
            public bool IsSelected { get; set; }
            
            public string Online { get; set; }
            
            
            




    }

    
}
