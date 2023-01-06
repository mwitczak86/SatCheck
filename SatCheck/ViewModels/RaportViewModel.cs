using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using SQLitePCL;
using LiveCharts.Configurations;
using LiveCharts;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using SatCheck.Models;
using System.Windows.Input;
using SatCheck.Services;
using System.Collections.ObjectModel;
using LiveCharts.Wpf;
using Ookii.Dialogs.Wpf;
using System.Windows;
using System.IO;
using System.IO.Packaging;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Markup;

namespace SatCheck.ViewModels
{
    public class RaportViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        
        public Func<int, string> ValueFormatter2 { get; set; }

        public BtnRelayCommand BtnRelayCommand { get; set; }

        public SeriesCollection SeriesCollection { get; set; }
        public string[] Labels { get; set; }
        public Func<double, string> Formatter { get; set; }


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
            set { _zaznaczonyKomponent = value; NotifyPropertyChanged("ZaznaczonyKomponent"); }
        }




        private DateTime startDate = Convert.ToDateTime("2022-01-01");

        public DateTime StartDate
        {
            get { return startDate; }
            set
            {
                startDate = value;
                NotifyPropertyChanged("StartDate");
            }
        }


        private DateTime endDate = DateTime.Now;

        public DateTime EndDate
        {
            get { return endDate; }
            set
            {
                endDate = value;
                NotifyPropertyChanged("EndDate");
            }
        }

        private int maxStations;
        public int MaxStations
        {
            get { return maxStations; }
            set
            {
                maxStations = value;
                NotifyPropertyChanged("MaxStations");

            }
        }

        private int avgRTTactive;

        public int AvgRTTactive
        {
            get { return avgRTTactive; }
            set
            {
                avgRTTactive = value;
                NotifyPropertyChanged("AvgRTTactive");
            }
        }

        private int ethRTT;
        public int EthRTT
        {
            get { return ethRTT; }
            set
            {
                ethRTT = value;
                NotifyPropertyChanged("EthRTT");

            }
        }

        private int activeStations;

        public int ActiveStations
        {
            get { return activeStations; }
            set
            {
                activeStations = value;
                NotifyPropertyChanged("ActiveStations");
            }
        }


        private int maxRTT;
        public int MaxRTT
        {
            get { return maxRTT; }
            set
            {
                maxRTT = value;
                NotifyPropertyChanged("MaxRTT");

            }
        }


        private int allRTT;
        public int AllRTT
        {
            get { return allRTT; }
            set
            {
                allRTT = value;
                NotifyPropertyChanged("AllRTT");

            }
        }

        private int hmPings;
        public int HMPings
        {
            get { return hmPings; }
            set
            {
                hmPings = value;
                NotifyPropertyChanged("HMPings");

            }
        }


        private int offline;
        public int Offline
        {
            get { return offline; }
            set
            {
                offline = value;
                NotifyPropertyChanged("Offline");

            }
        }



        private int hmSat;
        public int HMSat
        {
            get { return hmSat; }
            set
            {
                hmSat = value;
                NotifyPropertyChanged("HMSat");

            }
        }

        private int hmEth;
        public int HMEth
        {
            get { return hmEth; }
            set
            {
                hmSat = value;
                NotifyPropertyChanged("HMEth");

            }
        }

        private int avgEth;
        public int AvgEth
        {
            get { return avgEth; }
            set
            {
                hmSat = value;
                NotifyPropertyChanged("AvgEth");

            }
        }


        public RaportViewModel()
        {
            IleStacji();
            this.BtnRelayCommand = new BtnRelayCommand(this);
            AppUserDbConnect();

            SeriesCollection = new SeriesCollection
            {
                
            };

           



            

            
        }

        



        public void  IleStacji()
        {
            string _startDate = startDate.ToString("yyyyMMdd");
            string _endDate = endDate.ToString("yyyyMMdd");
            string db = "satData.db";
            string fullPath = System.IO.Path.GetFullPath(db);
            var dbconn = new SQLiteConnection(fullPath);
            string howMuch = "SELECT COUNT(DISTINCT Id_Stacji) FROM PingResult Where(Date BETWEEN '" + _startDate + "' AND '" + _endDate + "')";
            string activeSql = "SELECT Count(Id_Stacji) From PingResult Where(Online = 'Sat' and (Date BETWEEN '" + _startDate + "' AND '" + _endDate + "')) GROUP BY Id_Stacji Having (Count(Online = 'Sat') > 20)";
            string activeRTTavg = "SELECT (AVG(RTT)) FROM PingResult Where(Online = 'Sat' and RTT>0 and (Date BETWEEN '" + _startDate + "' AND '" + _endDate + "'))";
            string allRTTavg = "SELECT (AVG(RTT)) FROM PingResult Where(Online != 'Off' and RTT>0 and (Date BETWEEN '" + _startDate + "' AND '" + _endDate + "'))";

            string activeRTTmax = "SELECT (MAX(RTT)) FROM PingResult Where(Online = 'Sat' and RTT>0 and (Date BETWEEN '" + _startDate + "' AND '" + _endDate + "'))";
            string ethRTTavg = "SELECT (AVG(RTT)) FROM PingResult Where(Online = 'Eth' and RTT>0 and (Date BETWEEN '"+ _startDate + "' AND '" +_endDate + "'))";
            SQLiteCommand cmd = new SQLiteCommand(dbconn);

            cmd.CommandText = howMuch;
            int resultHM = 0;
            resultHM = Convert.ToInt32(cmd.ExecuteScalar<int>());

            cmd.CommandText = activeSql;
           

            int resultAS = 0;
            resultAS = dbconn.Query<PingResult>(activeSql).Count;


            SQLiteCommand cmd2 = new SQLiteCommand(dbconn);
            cmd2.CommandText = activeRTTavg;
            int resultARTTavg = 0;
            resultARTTavg = Convert.ToInt32(cmd2.ExecuteScalar<int>());

            SQLiteCommand cmd3 = new SQLiteCommand(dbconn);
            cmd3.CommandText = activeRTTmax;
            int resultARTTmax = 0;
            resultARTTmax = Convert.ToInt32(cmd3.ExecuteScalar<int>());

            SQLiteCommand cmd4 = new SQLiteCommand(dbconn);
            cmd4.CommandText = ethRTTavg;
            int resultERTTavg = 0;
            resultERTTavg = Convert.ToInt32(cmd4.ExecuteScalar<int>());

            SQLiteCommand cmd5 = new SQLiteCommand(dbconn);
            cmd5.CommandText = allRTTavg;
            int resultAllRTT = 0;
            resultAllRTT= Convert.ToInt32(cmd5.ExecuteScalar<int>());

            allRTT = resultAllRTT;
            maxRTT = resultARTTmax;
            maxStations = resultHM;
            activeStations = resultAS;
            avgRTTactive = resultARTTavg;
            ethRTT = resultERTTavg;

            NotifyPropertyChanged("AllRTT");
            NotifyPropertyChanged("EthRTT");
            NotifyPropertyChanged("MaxRTT");
            NotifyPropertyChanged("AvgRTTactive");
            NotifyPropertyChanged("ActiveStations");
            NotifyPropertyChanged("MaxStations");
        }

        

        


        public void JednejStacji()
        {
            string _startDate = startDate.ToString("yyyyMMdd");
            string _endDate = endDate.ToString("yyyyMMdd");
            string idStacji = _zaznaczonyKomponent.Id.ToString();
            string db = "satData.db";
            string fullPath = System.IO.Path.GetFullPath(db);
            var dbconn = new SQLiteConnection(fullPath);
            string howMuch = "SELECT COUNT(Id_Stacji) FROM PingResult Where(Date BETWEEN '" + _startDate + "' AND '" + _endDate + "') and Id_Stacji = " + idStacji + "";
            string activeSql = "SELECT Count(Id_Stacji) From PingResult Where(Online = 'Sat'and (Date BETWEEN '" + _startDate + "' AND '" + _endDate + "')and Id_Stacji = " + idStacji + ")";
            string activeRTTavg = "SELECT (AVG(RTT)) FROM PingResult Where(Online = 'Sat' and RTT>0 and Id_Stacji = " + idStacji + " and (Date BETWEEN '" + _startDate + "' AND '" + _endDate + "'))";
            string _offline = "SELECT COUNT(Id_Stacji) FROM PingResult Where(Online = 'Off' and (Date BETWEEN '" + _startDate + "' AND '" + _endDate + "') and Id_Stacji = " + idStacji + ")";
            string _eth = "SELECT COUNT(Id_Stacji) FROM PingResult Where(Online = 'Eth' and (Date BETWEEN '" + _startDate + "' AND '" + _endDate + "') and Id_Stacji = " + idStacji + ")";

            string activeRTTmax = "SELECT (MAX(RTT)) FROM PingResult Where(Online = 'Sat' and RTT>0 and Id_Stacji = " + idStacji + " and (Date BETWEEN '" + _startDate + "' AND '" + _endDate + "'))";
            string ethRTTavg = "SELECT (AVG(RTT)) FROM PingResult Where(Online = 'Eth' and RTT>0 and Id_Stacji = " + idStacji + " and (Date BETWEEN '" + _startDate + "' AND '" + _endDate + "'))";
            SQLiteCommand cmd = new SQLiteCommand(dbconn);
            cmd.CommandText = howMuch;
            int resultHM = 0;
            resultHM = Convert.ToInt32(cmd.ExecuteScalar<int>());

            SQLiteCommand cmd6 = new SQLiteCommand(dbconn);
            cmd6.CommandText = activeSql;
            int resultAS = 0;
            resultAS = Convert.ToInt32(cmd6.ExecuteScalar<int>());


            SQLiteCommand cmd2 = new SQLiteCommand(dbconn);
            cmd2.CommandText = activeRTTavg;
            int resultARTTavg = 0;
            resultARTTavg = Convert.ToInt32(cmd2.ExecuteScalar<int>());

            SQLiteCommand cmd3 = new SQLiteCommand(dbconn);
            cmd3.CommandText = activeRTTmax;
            int resultARTTmax = 0;
            resultARTTmax = Convert.ToInt32(cmd3.ExecuteScalar<int>());

            SQLiteCommand cmd4 = new SQLiteCommand(dbconn);
            cmd4.CommandText = ethRTTavg;
            int resultERTTavg = 0;
            resultERTTavg = Convert.ToInt32(cmd4.ExecuteScalar<int>());

            SQLiteCommand cmd5 = new SQLiteCommand(dbconn);
            cmd5.CommandText = _offline;
            int resultOff = 0;
            resultOff = Convert.ToInt32(cmd5.ExecuteScalar<int>());

            SQLiteCommand cmd7 = new SQLiteCommand(dbconn);
            cmd7.CommandText = _eth;
            int resultEth= 0;
            resultEth = Convert.ToInt32(cmd7.ExecuteScalar<int>());

            


            hmPings = resultHM;
            hmSat = resultAS;
            offline = resultOff;
            hmEth = resultEth;
            avgRTTactive = resultARTTavg;
            maxRTT = resultARTTmax;
            avgEth = resultERTTavg;

            NotifyPropertyChanged("HMPings");
            NotifyPropertyChanged("HMSat");
            NotifyPropertyChanged("Offline");
            NotifyPropertyChanged("HMEth");
            NotifyPropertyChanged("AvgRTTactive");
            NotifyPropertyChanged("MaxRTT");
            NotifyPropertyChanged("AvgEth");



            SeriesCollection.Clear();

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Online - SAT",
                Values = new ChartValues<double> {hmSat }
            });

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Online - ETH",
                Values = new ChartValues<double> {hmEth}
            });

            SeriesCollection.Add(new ColumnSeries
            {
                Title = "Offline",
                Values = new ChartValues<double> { offline }
            });
        }

        private string folderPath;
        public string FolderPath
        {
            get { return folderPath; }
            set
            {
                folderPath = value;
                NotifyPropertyChanged("FolderPath");

            }
        }

        public void ShowFolderBrowserDialog()
        {
            var dialog = new VistaFolderBrowserDialog();
            dialog.Description = "Wybierz folder przeznaczony do zapisu plików.";
            dialog.UseDescriptionForTitle = true; // This applies to the Vista style dialog only, not the old dialog.

            if (!VistaFolderBrowserDialog.IsVistaFolderDialogSupported)
            {
                MessageBox.Show("Because you are not using Windows Vista or later, the regular folder browser dialog will be used. Please use Windows Vista to see the new dialog.", "Sample folder browser dialog");
            }

            if ((bool)dialog.ShowDialog())
            {
                MessageBox.Show($"Wybrano ścieżkę zapisu:{Environment.NewLine}{dialog.SelectedPath}", "Ścieżka zapisu plików");
                folderPath = dialog.SelectedPath;
                NotifyPropertyChanged("FolderPath");
                MessageBox.Show($"Wybrano ścieżkę zapisu:{Environment.NewLine}{folderPath}", "Ścieżka zapisu plików");

            }
        }

        public void PrintNetStatus()
        {
            ReportDbConnect();
            
        }

        private ObservableCollection<ReportObj> reportList;
        public ObservableCollection<ReportObj> ReportList

        {
            get { return reportList; }
            set
            {

                reportList = value;
                NotifyPropertyChanged("ReportList");


            }
        }
        public void ReportDbConnect()
        {
            string _startDate = startDate.ToString("yyyyMMdd");
            string _endDate = endDate.ToString("yyyyMMdd");
            string idStacji = _zaznaczonyKomponent.Id.ToString();
            string db = "satData.db";
            string fullPath = System.IO.Path.GetFullPath(db);
            var dbconn = new SQLiteConnection(fullPath);

            string allRep = "create table Part1 AS  SELECT Id_Stacji, Nazwa, AdresSat, AdresEth, Online, round(avg(RTT)) as 'RTT' FROM PingResult Where(Date BETWEEN "+ _startDate + " and  " + _endDate + ") and (Online = 'Off') GROUP by Id_Stacji; ";
            string satRep = "create table Part2 AS  SELECT Id_Stacji, Nazwa, AdresSat, AdresEth, Online, round(avg(RTT)) as 'RTT_SAT' FROM PingResult Where(Date BETWEEN " + _startDate + " and  " + _endDate + ") and (Online = 'Sat') GROUP by Id_Stacji; ";
            string ethRep = "create table Part3 AS  SELECT Id_Stacji, Nazwa, AdresSat, AdresEth, Online, round(avg(RTT)) as 'RTT_ETH' FROM PingResult Where(Date BETWEEN " + _startDate + " and  " + _endDate + ") and (Online = 'Eth') GROUP by Id_Stacji; ";

            string joinTab = "create table ReportObj as SELECT Part1.Id_Stacji, Part1.Nazwa, Part1.AdresSat, Part1.AdresEth,  ifnull(Part2.Online, 'Offline')'Interfejs_1', Part2.RTT_SAT as 'RTT_SAT', ifnull(Part3.Online, 'Offline')'Interfejs_2', Part3.RTT_ETH as 'RTT_ETH' FROM Part1 LEFT JOIN Part2 on Part2.Id_Stacji = Part1.Id_Stacji LEFT JOIN Part3 on Part3.Id_Stacji = Part1.Id_Stacji; ";

            string drop1 = "drop table ReportObj";
            string drop2 = "drop table Part1";
            string drop3 = "drop table Part2";
            string drop4 = "drop table Part3";

            //SQLiteCommand cmd1 = new SQLiteCommand(dbconn);
            //cmd1.CommandText = allRep;

            //SQLiteCommand cmd2 = new SQLiteCommand(dbconn);
            //cmd2.CommandText = satRep;

            //SQLiteCommand cmd3 = new SQLiteCommand(dbconn);
            //cmd3.CommandText = ethRep;

            //SQLiteCommand cmd4 = new SQLiteCommand(dbconn);
            //cmd4.CommandText = joinTab;

            //SQLiteCommand cmd5 = new SQLiteCommand(dbconn);
            //cmd5.CommandText = drop;

            //dbconn.Execute(drop);

            dbconn.Execute(allRep);
            dbconn.Execute(satRep);
            dbconn.Execute(ethRep);
            dbconn.Execute(joinTab);



            ReportList = new ObservableCollection<ReportObj>(dbconn.Table<ReportObj>().OrderBy(x => x.Id_Stacji).ToList());
            NotifyPropertyChanged("ReportList");

            dbconn.Execute(drop1);
            dbconn.Execute(drop2);
            dbconn.Execute(drop3);
            dbconn.Execute(drop4);

            
        }

    }
}
