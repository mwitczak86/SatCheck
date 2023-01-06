using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SatCheck.Models;
using SatCheck.Services;

namespace SatCheck.ViewModels
{
    public class AddDbViewModel : INotifyPropertyChanged
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
        public ICommand cmdAddTask { get; private set; }
        public bool CanExectute
        {
            get { return !string.IsNullOrEmpty(Nazwa); }
        }
        public AddDbViewModel()
        {
            cmdAddTask = new RelayCommand(AddTask, () => CanExectute);
            getTask();
        }

        private async void AddTask()
        {
            var r = await App.Database.SaveTaskAsync(new Komponenty
            {
                Id = Id,
                Rola = Rola,
                Nazwa = Nazwa,
                AdresSat = AdresSat,
                AdresEth = AdresEth,

            });
            getTask();

        }
        public async void getTask()
        {
            Lista = await App.Database.GetTaskAsync();
        }

        
    }
}
