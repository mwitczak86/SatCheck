using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using SatCheck.Models;
using SatCheck.Services;
using SatCheck.ViewModels;
using SatCheck.Views;
using System.Collections.ObjectModel;
using SQLite;
using System.Net.NetworkInformation;
using System.Windows.Threading;
using LiveCharts;
using LiveCharts.Defaults;
using LiveCharts.Wpf;
using System.Threading;
using LiveCharts.Configurations;

namespace SatCheck.ViewModels
{
    public class PingViewModel
    {
        



        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public ObservableCollection<PingRep> ReplyLista = new ObservableCollection<PingRep>();

        
        
        public DispatcherTimer timer = new DispatcherTimer();
       
        public PingViewModel()
        {
            
            
            AppUserDbConnect();
            ListaRep = new ObservableCollection<PingRep>();
            
            TimerON();
            
            
            
            



        }
        
        



        public void TimerON()
        {
            if (StatVariab.PingON)
            {
                Test();
                
                
            }
            
        }


        private void timer_tick(object sender, EventArgs e)
        {
            if (StatVariab.PingON && StatVariab.BtnStart)
            {
                AddZaznKomp();
               
            }

        }

        public void Test()
        {
            
            if (timer != null && StatVariab.PingON)
            {
                timer.Tick += timer_tick;
                timer.Interval = new TimeSpan(0, 0, 2);
                timer.Start();


            }
            else
                timer.Stop();



        }

        public void StopTimer()
        {

            if (timer != null)
            {
                timer.Tick -= timer_tick;
                timer.Stop();
                timer = null;

            }
        }





        private string address;
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
                NotifyPropertyChanged("Address");
            }
        }

        private string rtt;
        public string Rtt
        {
            get { return rtt; }
            set
            {
                rtt = value;
                NotifyPropertyChanged("Address");
            }
        }


        private string ttl;
        public string TTL
        {
            get { return ttl; }
            set
            {
                ttl = value;
                NotifyPropertyChanged("TTL");
            }
        }


        private string buffer;
        public string Buffer
        {
            get { return buffer; }
            set
            {
                buffer = value;
                NotifyPropertyChanged("Buffer");
            }
        }

        private string status;
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                NotifyPropertyChanged("Status");
            }
        }

        private string nazwa;
        public string Nazwa
        {
            get { return nazwa; }
            set
            {
                nazwa = value;
                NotifyPropertyChanged("Nazwa");
            }
        }


        private string adresSat;
        public string AdresSat
        {
            get { return adresSat; }
            set
            {
                adresSat = value;
                NotifyPropertyChanged("AdresSat");
            }
        }

        private string adresEth;
        public string AdresEth
        {
            get { return adresEth; }
            set
            {
                adresEth = value;
                NotifyPropertyChanged("AdresEth");
            }
        }


        private ObservableCollection<Komponenty> listaKomp;
        public ObservableCollection<Komponenty> ListaKomp

        {
            get { return listaKomp; }
            set
            {

                listaKomp = value;
                NotifyPropertyChanged("ListaKomp");


            }
        }


        private ObservableCollection<PingRep> listaRep;
        public ObservableCollection<PingRep> ListaRep

        {
            get { return listaRep; }
            set
            {

                listaRep = value;
                NotifyPropertyChanged("ListaRep");


            }
        }








        public void AppUserDbConnect()
        {
            string db = "satData.db";
            string fullPath;
            fullPath = System.IO.Path.GetFullPath(db);

            var _database = new SQLiteAsyncConnection(fullPath);

            var syncDatabase = _database.GetConnection();
            syncDatabase.CreateTable<Komponenty>();

            ListaKomp = new ObservableCollection<Komponenty>(syncDatabase.Table<Komponenty>().OrderBy(x => x.Id).ToList());
            
        }

        private Komponenty _zaznaczonyKomponent;
        public Komponenty ZaznaczonyKomponent
        {
            get { return _zaznaczonyKomponent; }
            set { _zaznaczonyKomponent = value; NotifyPropertyChanged("ZaznaczonyKomponent");  }
        }

        


        public ObservableCollection<Komponenty> ZaznKomp = new ObservableCollection<Komponenty>();



        public void AddZaznKomp()
        {
            if (_zaznaczonyKomponent != null)
            {

                NotifyPropertyChanged("ZaznKomp");
                LocalPing(_zaznaczonyKomponent.AdresSat, _zaznaczonyKomponent.AdresEth);
            }
        }






        public void LocalPing(string AdresSat, string AdresEth)
        {
            PingOptions options = new PingOptions();
            Ping pingSender = new Ping();
            options.Ttl = 128;
            options.DontFragment = true;
            int timeout = 1000;
            byte[] buffer = new byte[32];


            if (!StatVariab.TimerOFF)
            {

                PingReply reply = pingSender.Send(AdresSat, timeout, buffer, options);


                if (reply.Status == IPStatus.Success)
                {


                    string IPadd = reply.Address.ToString();
                    string Rtt = reply.RoundtripTime.ToString();
                    string TTL = reply.Options.ToString();
                    string buffRep = reply.Buffer.ToString();
                    

                    ListaRep.Add(new PingRep() { Address = IPadd, TTL = TTL, Rtt = Rtt, Buffer = buffRep, Status = "SAT_ONLINE" });
                    NotifyPropertyChanged("ListaRep");
                                  
                   



                }
                else
                {
                    string IPadd = AdresSat;
                    string Rtt = "-";
                    string TTL = "-";
                    string buffRep = "-";
                    
                    ListaRep.Add(new PingRep() { Address = IPadd, TTL = TTL, Rtt = Rtt, Buffer = buffRep, Status = "NIEOSIĄGALNY" });
                    NotifyPropertyChanged("ListaRep");
                    
                    

                }


                PingReply reply2 = pingSender.Send(AdresEth, timeout, buffer, options);

                if (reply2.Status == IPStatus.Success)
                {


                    string IPadd = reply2.Address.ToString();
                    string Rtt = reply2.RoundtripTime.ToString();
                    string TTL = reply2.Options.ToString();
                    string buffRep = reply2.Buffer.ToString();
                    

                    ListaRep.Add(new PingRep() { Address = IPadd, TTL = TTL, Rtt = Rtt, Buffer = buffRep, Status = "ETH_ONLINE" });
                    NotifyPropertyChanged("ListaRep");
                   
                }
                else
                {
                    string IPadd = AdresEth;
                    string Rtt = "-";
                    string TTL = "-";
                    string buffRep = "-";
                    
                    ListaRep.Add(new PingRep() { Address = IPadd, TTL = TTL, Rtt = Rtt, Buffer = buffRep, Status = "NIEOSIĄGALNY" });
                    NotifyPropertyChanged("ListaRep");
                    

                }


            }

        }
        public void AddRL(string adr, string ttl, string rtt, string buff, string Stat)
        {

            ListaRep = new ObservableCollection<PingRep>();
            ListaRep.Add(new PingRep() { Address = adr, TTL = ttl, Rtt = rtt, Buffer = buff, Status = Stat });
            NotifyPropertyChanged("ListaRep");
        }

        }
    }




