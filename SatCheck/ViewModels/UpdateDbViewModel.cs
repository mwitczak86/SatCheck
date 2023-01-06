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

namespace SatCheck.ViewModels
{
    public class UpdateDbViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
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


        private List<Komponenty> lista;
        public List<Komponenty> Lista
        {
            get { return lista; }
            set
            {
                lista = value;
                NotifyPropertyChanged("Lista");

            }
        }
        

        public ICommand cmdUpdate { get; private set; }
        public bool CanExectute
        {
            get { return !string.IsNullOrEmpty(Nazwa); }
        }
       
        public UpdateDbViewModel()
        {
            cmdUpdate = new RelayCommand(UpdateTask, () => CanExectute);
            getTask();

        }



        public void UpdateTask()
        {
            string db = "satData.db";
            string fullPath;
            fullPath = System.IO.Path.GetFullPath(db);

            var dbconn = new SQLite.SQLiteConnection(fullPath);

            var istniejacyKomponent = dbconn.Query<Komponenty>("select * from Komponenty where Id = ?", ZaznaczonyKomponent.Id).FirstOrDefault();
            if (istniejacyKomponent != null) {


                istniejacyKomponent.Id = Id;
                istniejacyKomponent.Rola = Rola;
                istniejacyKomponent.Nazwa = Nazwa;
                istniejacyKomponent.AdresSat = AdresSat;
                istniejacyKomponent.AdresEth = AdresEth;

            }

            dbconn.Update(istniejacyKomponent);
            

        }



        //public Task<int> UpdateTaskAsync(Komponenty taskModel)
        //{
        //    return App.Database.UpdateTaskAsync(taskModel);

        //}


        public async void getTask()
        {
            Lista = await App.Database.GetTaskAsync();
        }

        

        private Komponenty _zaznaczonyKomponent;
        public Komponenty ZaznaczonyKomponent
        {
            get { return _zaznaczonyKomponent; }
            set { _zaznaczonyKomponent = value; NotifyPropertyChanged("ZaznaczonyKomponent"); }
        }

    }
}
