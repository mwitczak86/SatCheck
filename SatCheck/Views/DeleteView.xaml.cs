using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SatCheck.Models;
using SQLite;
using System.IO;
using System.Collections.ObjectModel;

namespace SatCheck.Views
{
    /// <summary>
    /// Logika interakcji dla klasy DeleteView.xaml
    /// </summary>
    public partial class DeleteView : UserControl
    {
        public ObservableCollection<Usuwane> usuwane { get; set; }
        public class Usuwane
        {
            
            public bool IsSelected { get; set; }
           
            [PrimaryKey, AutoIncrement]
            public int Id { get; set; }
            public string Nazwa { get; set; }
            public string AdresSat { get; set; }
            public string AdresEth { get; set; }
            public string Rola { get; set; }

        }

        public DeleteView()
        {
            PobierzDane();
            InitializeComponent();
            
        }
        private void PobierzDane()
        {
            usuwane = new ObservableCollection<Usuwane>();
            string db = "satData.db";
            string fullPath;
            fullPath = System.IO.Path.GetFullPath(db);

            var dbconn = new SQLite.SQLiteConnection(fullPath);

            dbconn.Table<Komponenty>().OrderBy(x => x.Id).ToList();



            for (int i = 0; i < 40; i++)
            {

                usuwane.Add(new Usuwane() {Nazwa = i.ToString(), AdresSat = i.ToString(), AdresEth = i.ToString(), Rola = i.ToString(), IsSelected = false });
            }

            listView4.ItemsSource = usuwane;

        }

        //    public List<Usuwane> usuwane;
        //    public List<Usuwane> Usuwane
        //    {
        //        get { return usuwane; }
        //        set
        //        {
        //            usuwane = value;


        //        }
        //    }
        //    public DeleteView()
        //    {

        //    Dodaj();
        //    //InitializeComponent();
        //    }

        //    public void Dodaj()
        //    {
        //        string db = "satData.db";
        //        string fullPath;
        //        fullPath = System.IO.Path.GetFullPath(db);

        //        var dbconn = new SQLite.SQLiteConnection(fullPath);

        //        var istniejacyKomponent = dbconn.Query<Usuwane>("select * from Komponenty");



        //    }






        public void Checked_Delete_Clicked(object sender, RoutedEventArgs e)

        {
            MessageBox.Show("Ilosc elementow listy: " + usuwane.Count().ToString());

            foreach (var Checkboxitem in usuwane)
        {
                if (Checkboxitem.IsSelected == true)
                {
                    MessageBox.Show(Checkboxitem.Nazwa.ToString() + Checkboxitem.AdresSat.ToString() + Checkboxitem.AdresEth.ToString() + Checkboxitem.Rola.ToString());
                }

}
    }
        
    }
}
