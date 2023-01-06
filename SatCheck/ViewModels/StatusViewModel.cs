using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SatCheck.Models;
using SatCheck.Services;
using SatCheck.Views;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using ModernMessageBoxLib;
using System.Windows.Threading;
using SQLite;
using System.Timers;

namespace SatCheck.ViewModels
{
    public class StatusViewModel : INotifyPropertyChanged
    {
        
        public DispatcherTimer timer = new DispatcherTimer();


        


        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged([CallerMemberName] string name = "")

        {
            var propertyChanged = PropertyChanged;
            

            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
               
            }
        }

        

        private void timer_tick(object sender, EventArgs e)
        {
            if (StatVariab.TimerOFF)
            {
                JestOnline();
            }
            
        }

        public void Test()
        {
            if (timer != null && StatVariab.TimerOFF)
            {
               
                timer.Tick += timer_tick;
                timer.Interval = new TimeSpan(0, 0, 30);
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

        
        ~StatusViewModel()
        {
           StopTimer();
            
        }


        private int lp;
        public int Lp
        {
            get { return lp; }
            set
            {
                id = value;
                NotifyPropertyChanged("Lp");
            }
        }


        private int id;
        public int Id
        {
            get { return id; }
            set
            {
                id = value;
                NotifyPropertyChanged("Id");
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

        private string rola;
        public string Rola
        {
            get { return rola; }
            set
            {
                rola = value;
                NotifyPropertyChanged("Rola");
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

        private bool online;
        public bool Online
        {
            get { return online; }
            set
            {
                online = value;
                NotifyPropertyChanged("Online");

            }
        }

        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                NotifyPropertyChanged("IsSelected");



            }
        }






        //public ObservableCollection<Komponenty> kolekcja = new ObservableCollection<Komponenty>();
        
        //public void AddCollection()
        //{
        //    int count = 0;
        //    foreach (var item in lista)
        //    {
        //        if (item != null)
        //        {
        //            count++;
        //            kolekcja.Add(item);
        //        }
        //    }
                

        //    QModernMessageBox.Done("ile w kolekcji: " + count, "");
            
        //}

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
        

       
        
        
        public StatusViewModel()
        {

            AppUserDbConnect();
            
            Test();
            
         
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

        

        public void JestOnline()
        {

            

            if (timer != null)
            {
                string db = "satData.db";
                string fullPath;
                fullPath = System.IO.Path.GetFullPath(db);
                

                var dbconn = new SQLiteConnection(fullPath);

                dbconn.CreateTable<PingResult>();

                PingOptions opcje = new PingOptions();
                opcje.Ttl = 255;
                opcje.DontFragment = true;
                string dane = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";
                byte[] bufor = Encoding.ASCII.GetBytes(dane);
                string date = DateTime.Now.ToString("yyyyMMdd");

                foreach (Komponenty element in listaKomp)
                {


                    int rtt = 0;

                    Ping ping = new Ping();

                    try
                    {
                        
                        PingReply odpowiedz = ping.Send(element.AdresSat, opcje.Ttl, buffer: bufor, opcje);
                        if (odpowiedz.Status == IPStatus.Success && odpowiedz.RoundtripTime < 45)
                        {
                            element.Online = "Eth";
                            NotifyPropertyChanged("Online");
                            rtt = Convert.ToInt32(odpowiedz.RoundtripTime);
                            
                           

                        }

                        else if (odpowiedz.Status == IPStatus.Success)
                        {
                            element.Online = "Sat";
                            NotifyPropertyChanged("Online");
                            rtt = Convert.ToInt32(odpowiedz.RoundtripTime);

                        }
                        else
                        {
                            element.Online = "Off";
                            NotifyPropertyChanged("Online");
                            rtt = Convert.ToInt32(odpowiedz.RoundtripTime);


                        }

                        
                    }
                    catch (Exception ex)
                    {
                        QModernMessageBox.Warning("Błąd:" + element.AdresSat + " " + ex.Message, "BŁĄD");
                    }

                    dbconn.Query<PingResult>("Insert into PingResult (Id_Stacji, Date,  Nazwa, AdresSat, AdresEth, Rola, Online, RTT) values (?, ?, ?, ?, ?, ?, ?, ?)", element.Id, date, element.Nazwa, element.AdresSat, element.AdresEth, element.Rola, element.Online, rtt);



                }





            }
           

        }


    }
}
