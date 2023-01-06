using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using SatCheck.Models;
using SatCheck.Services;

namespace SatCheck.ViewModels
{
    public class CalculatorViewModel : INotifyPropertyChanged 
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        private double fec;
        public double Fec
        {
            get { return fec; }
            set
            {
                fec = value;
                NotifyPropertyChanged("Fec");
            }
        }

        private double dataRate;
        public double DataRate
        {
            get { return dataRate; }
            set
            {
                dataRate = value;
                NotifyPropertyChanged("DataRate");
            }
        }

        private double symbolRate;
        public double SymbolRate
        {
            get { return symbolRate; }
            set
            {
                symbolRate = value;
                NotifyPropertyChanged("SymbolRate");
            }
        }

        private double occBand;
        public double OccBand
        {
            get { return occBand; }
            set
            {
                occBand = value;
                NotifyPropertyChanged("OccBand");
            }
        }

        private double rolloff;
        public double RollOff
        {
            get { return rolloff; }
            set
            {
                rolloff = value;
                NotifyPropertyChanged("RollOff");
            }
        }

        private double modulation;
        public double Modulation
        {
            get { return modulation; }
            set
            {
                modulation = value;
                NotifyPropertyChanged("Modulation");
            }
        }

        private static readonly KeyValuePair<double, string>[] fecList =
        {
            new KeyValuePair<double, string>(0.5,"1/2"),
            new KeyValuePair<double, string>(0.67,"2/3"),
            new KeyValuePair<double, string>(0.75,"3/4"),
            new KeyValuePair<double, string>(0.83,"5/6"),
            new KeyValuePair<double, string>(0.875,"7/8"),
        };

        public KeyValuePair<double, string>[] FecList
        {
            get
            {
                return fecList;
            }
           
        }

        private static readonly KeyValuePair<double, string>[] modList =
        {
            new KeyValuePair<double, string>(1,"BPSK"),
            new KeyValuePair<double, string>(2,"QPSK"),
            new KeyValuePair<double, string>(3,"8-PSK"),
            new KeyValuePair<double, string>(4,"16QAM"),
            new KeyValuePair<double, string>(6,"64QAM"),
        };

        public KeyValuePair<double, string>[] ModList
        {
            get
            {
                return modList;
            }

        }

        private static readonly KeyValuePair<double, string>[] rollList =
        {
            new KeyValuePair<double, string>(0.2,"20%"),
            new KeyValuePair<double, string>(0.3,"30%"),
            new KeyValuePair<double, string>(0.4,"40%"),
            new KeyValuePair<double, string>(0.5,"50%"),
            
        };

        public KeyValuePair<double, string>[] RollList
        {
            get
            {
                return rollList;
            }

        }

        public CalcBTNCommand CalcBTNCommand { get; set; }

        public void SymbolRateCalculator()
        {
            symbolRate = Math.Round(dataRate / (fec * modulation));
           

            occBand = (symbolRate + (symbolRate * rolloff)) / 1000;
            NotifyPropertyChanged("OccBand");
            NotifyPropertyChanged("SymbolRate");

        }

        // Tracking Frequency calculations

        // LO values 

        private bool isEnabled;
        public bool IsEnabled
        {
            get { return isEnabled; }
            set
            {
                isEnabled = value;
                NotifyPropertyChanged("isEnabled");
            }
        }

        private bool warning;
        public bool Warning
        {
            get { return warning; }
            set
            {
                warning = value;
                NotifyPropertyChanged("Warning");
            }
        }

        private string loSelector;
        public string LoSelector
        {
            get { return loSelector; }
            set
            {
                loSelector = value;
                if(loSelector == "Active")
                {
                    isEnabled = true;
                    NotifyPropertyChanged("IsEnabled");
                }
                else
                {
                    isEnabled = false;
                    NotifyPropertyChanged("IsEnabled");
                }
                
                ;
            }
        }


        private double loValue;
        public double LoValue
        {
            get { return loValue; }
            set
            {
                loValue = value;
                NotifyPropertyChanged("LoValue");
            }
        }


        private double beacon;
        public double Beacon
        {
            get { return beacon; }
            set
            {
                beacon = value;
                NotifyPropertyChanged("Beacon");
            }
        }

        private double trackingFrequency;
        public double TrackingFrequency
        {
            get { return trackingFrequency; }
            set
            {
                trackingFrequency = value;
                NotifyPropertyChanged("TrackingFrequency");
            }
        }



        private static readonly KeyValuePair<string, string>[] loSelectorList =
              {
            new KeyValuePair<string, string>("Active","Aktywny"),
            new KeyValuePair<string, string>("Passive","Pasywny"),
            

        };

        public KeyValuePair<string, string>[] LoSelectorList
        {
            get
            {
                return loSelectorList;


            }

        }


        private static readonly KeyValuePair<double, string>[] loListActive =
               {
            new KeyValuePair<double, string>(9.75,"9.75 GHz"),
            new KeyValuePair<double, string>(10.00,"10.00 GHz"),
            new KeyValuePair<double, string>(10.75,"10.75 GHz"),
            new KeyValuePair<double, string>(11.25,"11.25 GHz"),
            new KeyValuePair<double, string>(11.30,"11.30 GHz"),
            new KeyValuePair<double, string>(11.80,"11.80 GHz"),
            new KeyValuePair<double, string>(12.80,"12.80 GHz"),
            new KeyValuePair<double, string>(13.05,"13.05 GHz"),

        };

        private static readonly KeyValuePair<double, string>[] loListPassive =
        {
            new KeyValuePair<double, string>(9.75,"9.75 GHz"),
            new KeyValuePair<double, string>(10.00,"10.00 GHz"),
            new KeyValuePair<double, string>(10.75,"10.75 GHz"),
            new KeyValuePair<double, string>(11.30,"11.30 GHz"),
            new KeyValuePair<double, string>(12.80,"12.80 GHz"),
            new KeyValuePair<double, string>(13.05,"13.05 GHz"),

        };

       
        public KeyValuePair<double, string>[] LoListPassive
        {
            get
            {
                return loListPassive;
            }

        }

        public KeyValuePair<double, string>[] LoListActive
        {
            get
            {
                return loListActive;
            }

        }


        public void FrCalculator()
        {
            double lo = (loValue * 1000);

            trackingFrequency = beacon - lo;
            NotifyPropertyChanged("TrackingFrequency");
            if ((trackingFrequency < 950) || (trackingFrequency > 1950))
            {
                warning = true;
                NotifyPropertyChanged("Warning");
            }
            else
                warning = false;
                NotifyPropertyChanged("Warning");

        }


        //conversion calculator to convert between commonly used units of measure

        private static readonly KeyValuePair<string, string>[] comboDRSelectorList =
             {
            new KeyValuePair<string, string>("b/s","bit - b/s"),
            new KeyValuePair<string, string>("B/s","bajt - B/s"),
            new KeyValuePair<string, string>("kb/s","kilobit - b/s"),
            new KeyValuePair<string, string>("KB/s","kilobajt - B/s"),
            new KeyValuePair<string, string>("Mb/s","megabit - b/s"),
            new KeyValuePair<string, string>("MB/s","megabajt - B/s"),
            new KeyValuePair<string, string>("Gb/s","gigabit - b/s"),
            new KeyValuePair<string, string>("GB/s","gigabajt - B/s"),
            new KeyValuePair<string, string>("Tb/s","terabit - b/s"),
            new KeyValuePair<string, string>("TB/s","terabajt - B/s"),

        };

        public KeyValuePair<string, string>[] ComboDRSelectorList
        {
            get
            {
                return comboDRSelectorList;
            }

        }

        private static readonly KeyValuePair<string, string>[] comboDbmSelectorList =
             {
            new KeyValuePair<string, string>("dBm","dBm"),
            new KeyValuePair<string, string>("W","W"),
           

        };

        public KeyValuePair<string, string>[] ComboDbmSelectorList
        {
            get
            {
                return comboDbmSelectorList;
            }

        }


        private string unit;
        
        public string Unit
        {
            get { return unit; }
            set
            {
                unit = value;
                NotifyPropertyChanged("Unit");
            }
        }

        private double unitInput;
        public double UnitInput
        {
            get { return unitInput; }
            set
            {
                unitInput = value;
                NotifyPropertyChanged("UnitInput");
            }
        }
        private string valueDR;

        public string ValueDR
        {
            get { return valueDR; }
            set
            {
                valueDR = value;
                NotifyPropertyChanged("ValueDR");
            }
        }

        private string drSelector;
        public string DrSelector
        {
            get { return drSelector; }
            set
            {
                drSelector = value;
                NotifyPropertyChanged("DrSelector");
            }
        }


        private string dbmSelector;
        public string DbmSelector
        {
            get { return dbmSelector; }
            set
            {
                dbmSelector = value;
                NotifyPropertyChanged("DbmSelector");
            }
        }

        private double dbmResult;
        public double DbmResult
        {
            get { return dbmResult; }
            set
            {
                dbmResult = value;
                NotifyPropertyChanged("DbmResult");
            }
        }

        private double dbmInput;
        public double DbmInput
        {
            get { return dbmInput; }
            set
            {
                dbmInput = value;
                NotifyPropertyChanged("DbmInput");
            }
        }

        private string dbmUnit;
        public string DbmUnit
        {
            get { return dbmUnit; }
            set
            {
                dbmUnit = value;
                NotifyPropertyChanged("DbmUnit");
            }
        }



        private ObservableCollection<Units> unitList;
        public ObservableCollection<Units> UnitList

        {
            get { return unitList; }
            set
            {

                unitList = value;
                NotifyPropertyChanged("UnitList");


            }
        }
        public double b;
        public double B;
        public double kb;
        public double kB;
        public double Mb;
        public double MB;
        public double Gb;
        public double GB;
        public double Tb;
        public double TB;

        public void UnitConverter()
        {
            double _bit;
            double _byte;
            
            double _kilobit;
            double _kilobyte;
            double _megabit;
            double _megabyte;
            double _gigabit;
            double _gigabyte;
            double _terabit;
            double _terabyte;

            string b = "b/s";
            string B = "B/s";
            string kb = "kb/s";
            string KB = "KB/s";
            string Mb = "Mb/s";
            string MB = "MB/s";
            string Gb = "Gb/s";
            string GB = "GB/s";
            string Tb = "Tb/s";
            string TB = "TB/s";

            if(UnitList.Count != 0)
            {
                UnitList.Clear();
            }


            if (drSelector == "b/s" && unitInput != 0)
            {
                _bit = unitInput * 1;
                _byte = unitInput * 8;
                _kilobit = unitInput * 1024;
                _kilobyte = _byte*1024;
                
                _megabit = _kilobit * 1024;
                _megabyte = _megabit*8;
                _gigabit = _megabit *1024;
                _gigabyte = _gigabit*8;
                _terabit = _gigabit *1024;
                _terabyte = _terabit*8;

               
                UnitList.Add(new Units() {Unit = b, ValueDR = _bit });
                UnitList.Add(new Units() { Unit = B, ValueDR = _byte });
                UnitList.Add(new Units() { Unit = kb, ValueDR = _kilobit });
                UnitList.Add(new Units() { Unit = KB, ValueDR = _kilobyte });
                UnitList.Add(new Units() { Unit = Mb, ValueDR = _megabit });
                UnitList.Add(new Units() { Unit = MB, ValueDR = _megabyte });
                UnitList.Add(new Units() { Unit = Gb, ValueDR = _gigabit });
                UnitList.Add(new Units() { Unit = GB, ValueDR = _gigabyte });
                UnitList.Add(new Units() { Unit = Tb, ValueDR = _terabit });
                UnitList.Add(new Units() { Unit = TB, ValueDR = _terabyte });

                NotifyPropertyChanged("UnitList");
                
            }
            if (drSelector == "B/s" && unitInput != 0)
            {
                
                _byte = unitInput * 1;
                _bit = _byte*8;
                double bit = unitInput / 8;

                _kilobit = bit * 1024;
                _kilobyte = _kilobit * 8;

                _megabit = _kilobit * 1024;
                _megabyte = _megabit*8;
                _gigabit = _megabit * 1024;
                _gigabyte = _gigabit*8;
                _terabit = _gigabit * 1024;
                _terabyte = _terabit*8;

                UnitList.Clear();
                UnitList.Add(new Units() { Unit = b, ValueDR = _bit });
                UnitList.Add(new Units() { Unit = B, ValueDR = _byte });
                UnitList.Add(new Units() { Unit = kb, ValueDR = _kilobit });
                UnitList.Add(new Units() { Unit = KB, ValueDR = _kilobyte });
                UnitList.Add(new Units() { Unit = Mb, ValueDR = _megabit });
                UnitList.Add(new Units() { Unit = MB, ValueDR = _megabyte });
                UnitList.Add(new Units() { Unit = Gb, ValueDR = _gigabit });
                UnitList.Add(new Units() { Unit = GB, ValueDR = _gigabyte });
                UnitList.Add(new Units() { Unit = Tb, ValueDR = _terabit });
                UnitList.Add(new Units() { Unit = TB, ValueDR = _terabyte });
                NotifyPropertyChanged("UnitList");
            }
            else if (drSelector == "kb/s" && unitInput != 0)
            {
                _kilobit = unitInput * 1;
                

                _bit = _kilobit*1024;
               
                
                _byte = _bit/8;
                _kilobyte = _kilobit*8;
                _megabit = _kilobit * 1024;
                _megabyte = _megabit*8;
                _gigabit = _megabit * 1024;
                _gigabyte = _gigabit*8;
                _terabit = _gigabit * 1024;
                _terabyte = _terabit*8;

                UnitList.Clear();
                UnitList.Add(new Units() { Unit = b, ValueDR = _bit });
                UnitList.Add(new Units() { Unit = B, ValueDR = _byte });
                UnitList.Add(new Units() { Unit = kb, ValueDR = _kilobit });
                UnitList.Add(new Units() { Unit = KB, ValueDR = _kilobyte });
                UnitList.Add(new Units() { Unit = Mb, ValueDR = _megabit });
                UnitList.Add(new Units() { Unit = MB, ValueDR = _megabyte });
                UnitList.Add(new Units() { Unit = Gb, ValueDR = _gigabit });
                UnitList.Add(new Units() { Unit = GB, ValueDR = _gigabyte });
                UnitList.Add(new Units() { Unit = Tb, ValueDR = _terabit });
                UnitList.Add(new Units() { Unit = TB, ValueDR = _terabyte });
                NotifyPropertyChanged("UnitList");
            }
            else if (drSelector == "KB/s" && unitInput != 0)
            {
                _kilobyte = unitInput * 1;
                _kilobit = _kilobyte * 8;

               
                _bit = _kilobit * 1024;
                _byte = _bit/8;



                _megabit = _kilobit * 1024;
                _megabyte = _megabit / 8;
                _gigabit = _megabit * 1024;
                _gigabyte = _gigabit / 8;
                _terabit = _gigabit * 1024;
                _terabyte = _terabit / 8;

                UnitList.Clear();
                UnitList.Add(new Units() { Unit = b, ValueDR = _bit });
                UnitList.Add(new Units() { Unit = B, ValueDR = _byte });
                UnitList.Add(new Units() { Unit = kb, ValueDR = _kilobit });
                UnitList.Add(new Units() { Unit = KB, ValueDR = _kilobyte });
                UnitList.Add(new Units() { Unit = Mb, ValueDR = _megabit });
                UnitList.Add(new Units() { Unit = MB, ValueDR = _megabyte });
                UnitList.Add(new Units() { Unit = Gb, ValueDR = _gigabit });
                UnitList.Add(new Units() { Unit = GB, ValueDR = _gigabyte });
                UnitList.Add(new Units() { Unit = Tb, ValueDR = _terabit });
                UnitList.Add(new Units() { Unit = TB, ValueDR = _terabyte });
                NotifyPropertyChanged("UnitList");
            }
            else if (drSelector == "Mb/s" && unitInput != 0)
            {
                _megabit = unitInput * 1;
                _megabyte = unitInput * 8;

                _kilobit = _megabit * 1024;
                _kilobyte = _kilobit/8;
               
                _bit = _kilobit * 1024;
                _byte = _bit/8;
               
                _gigabit = _megabit * 1024;
                _gigabyte = _gigabit*8;

                _terabit = _gigabit * 1024;
                _terabyte = _terabit*8;

                UnitList.Clear();
                UnitList.Add(new Units() { Unit = b, ValueDR = _bit });
                UnitList.Add(new Units() { Unit = B, ValueDR = _byte });
                UnitList.Add(new Units() { Unit = kb, ValueDR = _kilobit });
                UnitList.Add(new Units() { Unit = KB, ValueDR = _kilobyte });
                UnitList.Add(new Units() { Unit = Mb, ValueDR = _megabit });
                UnitList.Add(new Units() { Unit = MB, ValueDR = _megabyte });
                UnitList.Add(new Units() { Unit = Gb, ValueDR = _gigabit });
                UnitList.Add(new Units() { Unit = GB, ValueDR = _gigabyte });
                UnitList.Add(new Units() { Unit = Tb, ValueDR = _terabit });
                UnitList.Add(new Units() { Unit = TB, ValueDR = _terabyte });
                NotifyPropertyChanged("UnitList");

            }
            else if (drSelector == "MB/s" && unitInput != 0)
            {
                _megabyte = unitInput * 1;
                _megabit = _megabyte * 8;

                _kilobit = _megabit * 1024;
                _kilobyte = _kilobit/8;
                _bit = _kilobit * 1024;
                _byte = _bit/8;

                
                _gigabyte = _megabyte * 1024;
                _gigabit = _gigabyte/8;

                _terabit = _gigabit * 1024;
                _terabyte = _terabit * 8;

                UnitList.Clear();
                UnitList.Add(new Units() { Unit = b, ValueDR = _bit });
                UnitList.Add(new Units() { Unit = B, ValueDR = _byte });
                UnitList.Add(new Units() { Unit = kb, ValueDR = _kilobit });
                UnitList.Add(new Units() { Unit = KB, ValueDR = _kilobyte });
                UnitList.Add(new Units() { Unit = Mb, ValueDR = _megabit });
                UnitList.Add(new Units() { Unit = MB, ValueDR = _megabyte });
                UnitList.Add(new Units() { Unit = Gb, ValueDR = _gigabit });
                UnitList.Add(new Units() { Unit = GB, ValueDR = _gigabyte });
                UnitList.Add(new Units() { Unit = Tb, ValueDR = _terabit });
                UnitList.Add(new Units() { Unit = TB, ValueDR = _terabyte });
                NotifyPropertyChanged("UnitList");
            }
            else if (drSelector == "Gb/s" && unitInput != 0)
            {
                _gigabit = unitInput * 1;
                _gigabyte = _gigabit * 8;


                _megabit = _gigabit * 1024;
                _megabyte = _megabit / 8;

                _kilobit = _megabit * 1024;
                _kilobyte = _kilobit / 8;

                _bit = _kilobit * 1024;
                _byte = _bit / 8;

                

                _terabit = _gigabit * 1024;
                _terabyte = _terabit*8;


                UnitList.Clear();
                UnitList.Add(new Units() { Unit = b, ValueDR = _bit });
                UnitList.Add(new Units() { Unit = B, ValueDR = _byte });
                UnitList.Add(new Units() { Unit = kb, ValueDR = _kilobit });
                UnitList.Add(new Units() { Unit = KB, ValueDR = _kilobyte });
                UnitList.Add(new Units() { Unit = Mb, ValueDR = _megabit });
                UnitList.Add(new Units() { Unit = MB, ValueDR = _megabyte });
                UnitList.Add(new Units() { Unit = Gb, ValueDR = _gigabit });
                UnitList.Add(new Units() { Unit = GB, ValueDR = _gigabyte });
                UnitList.Add(new Units() { Unit = Tb, ValueDR = _terabit });
                UnitList.Add(new Units() { Unit = TB, ValueDR = _terabyte });
                NotifyPropertyChanged("UnitList");
            }
            else if (drSelector == "GB/s" && unitInput != 0)
            {
                _gigabyte = unitInput * 1;
                _gigabit = _gigabyte * 8;

                _megabit = _gigabit * 1024;
                _megabyte = _megabit / 8;

                _kilobit = _megabit * 1024;
                _kilobyte = _kilobit / 8;
                _bit = _kilobit * 1024;
                _byte = _bit / 8;


                

                
                _terabyte = _gigabyte * 1024;
                _terabit = _terabyte / 8;


                UnitList.Clear();
                UnitList.Add(new Units() { Unit = b, ValueDR = _bit });
                UnitList.Add(new Units() { Unit = B, ValueDR = _byte });
                UnitList.Add(new Units() { Unit = kb, ValueDR = _kilobit });
                UnitList.Add(new Units() { Unit = KB, ValueDR = _kilobyte });
                UnitList.Add(new Units() { Unit = Mb, ValueDR = _megabit });
                UnitList.Add(new Units() { Unit = MB, ValueDR = _megabyte });
                UnitList.Add(new Units() { Unit = Gb, ValueDR = _gigabit });
                UnitList.Add(new Units() { Unit = GB, ValueDR = _gigabyte });
                UnitList.Add(new Units() { Unit = Tb, ValueDR = _terabit });
                UnitList.Add(new Units() { Unit = TB, ValueDR = _terabyte });
                NotifyPropertyChanged("UnitList");

            }
            
            else if (drSelector == "Tb/s" && unitInput != 0)
            {
                _terabit = unitInput * 1;
                _terabyte = _terabit * 8;

                _gigabit = _terabit * 1024;
                _gigabyte = _gigabit / 8;
                
                _megabit = _gigabit *1024;
                _megabyte = _megabit / 8;
                _kilobit = _megabit * 1024;
                _kilobyte = _kilobit / 8;
                _bit = _kilobit * 1024;
                _byte = _bit /8;
                
               

                UnitList.Clear();
                UnitList.Add(new Units() { Unit = b, ValueDR = _bit });
                UnitList.Add(new Units() { Unit = B, ValueDR = _byte });
                UnitList.Add(new Units() { Unit = kb, ValueDR = _kilobit });
                UnitList.Add(new Units() { Unit = KB, ValueDR = _kilobyte });
                UnitList.Add(new Units() { Unit = Mb, ValueDR = _megabit });
                UnitList.Add(new Units() { Unit = MB, ValueDR = _megabyte });
                UnitList.Add(new Units() { Unit = Gb, ValueDR = _gigabit });
                UnitList.Add(new Units() { Unit = GB, ValueDR = _gigabyte });
                UnitList.Add(new Units() { Unit = Tb, ValueDR = _terabit });
                UnitList.Add(new Units() { Unit = TB, ValueDR = _terabyte });
                NotifyPropertyChanged("UnitList");
            }
            else if (drSelector == "TB/s" && unitInput != 0)
            {
                _terabyte = unitInput * 1;
                _terabit = _terabyte*8;
                _gigabit = _terabit * 1024;
                _gigabyte = _gigabit / 8;
                
                _megabit = _gigabit *1024;
                _megabyte = _megabit / 8;
                _kilobit = _megabit * 1024;
                _kilobyte = _kilobit / 8;
                _bit = _kilobit * 1024;
                _byte = _bit / 8;
                
                

                UnitList.Clear();
                UnitList.Add(new Units() { Unit = b, ValueDR = _bit });
                UnitList.Add(new Units() { Unit = B, ValueDR = _byte });
                UnitList.Add(new Units() { Unit = kb, ValueDR = _kilobit });
                UnitList.Add(new Units() { Unit = KB, ValueDR = _kilobyte });
                UnitList.Add(new Units() { Unit = Mb, ValueDR = _megabit });
                UnitList.Add(new Units() { Unit = MB, ValueDR = _megabyte });
                UnitList.Add(new Units() { Unit = Gb, ValueDR = _gigabit });
                UnitList.Add(new Units() { Unit = GB, ValueDR = _gigabyte });
                UnitList.Add(new Units() { Unit = Tb, ValueDR = _terabit });
                UnitList.Add(new Units() { Unit = TB, ValueDR = _terabyte });
                NotifyPropertyChanged("UnitList");
            }



        }

        public void dbmConverter()
        {
            if(dbmSelector == "dBm")
            {
                var dbm = dbmInput / 10;
                dbmUnit = "W";
                dbmResult = Math.Round((Math.Pow(10, dbm)/1000),8);
                NotifyPropertyChanged("DbmResult");
                NotifyPropertyChanged("DbmUnit");
                
            }
            else
            {
                dbmUnit = "dBm";
                dbmResult = Math.Round((10*Math.Log10(dbmInput)+30),4);
                NotifyPropertyChanged("DbmResult");
                NotifyPropertyChanged("DbmUnit");
            }
        }




        public CalculatorViewModel()
        {
            SymbolRateCalculator();
            this.CalcBTNCommand = new CalcBTNCommand(this);
            UnitList = new ObservableCollection<Units>();
        }
    }
}
