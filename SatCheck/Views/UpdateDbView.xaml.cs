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
using SatCheck.Views;
using SatCheck.Services;
using SatCheck.ViewModels;
using SatCheck.Models;
using SQLite;

namespace SatCheck.Views
{
    /// <summary>
    /// Logika interakcji dla klasy UpdateDbView.xaml
    /// </summary>
    public partial class UpdateDbView : UserControl
    {
        public UpdateDbView()
        {
            
           
            InitializeComponent();

        }

        private void Update_Clicked(object sender, RoutedEventArgs e)
        {



            //UpdateID();
            UpdateParams();
            update.Visibility = Visibility.Visible;




        }


        public void UpdateParams()
        {
            int cid = Convert.ToInt16(id.Text);
            int tempId = cid;
            string rola = maslave.Text;
            string cname = nazwa.Text;
            string csat = adresSat.Text;
            string ceth = adresEth.Text;

            update.Visibility = Visibility.Visible;

            string db = "satData.db";
            string fullPath;
            fullPath = System.IO.Path.GetFullPath(db);

            var dbconn = new SQLite.SQLiteConnection(fullPath);

            var istniejacyKomponent = dbconn.Query<Komponenty>("select * from Komponenty where Id = ?", tempId).FirstOrDefault();
            if (istniejacyKomponent != null)
            {


                istniejacyKomponent.Id = Convert.ToInt32(id.Text);
                istniejacyKomponent.Rola = rola;
                istniejacyKomponent.Nazwa = cname;
                istniejacyKomponent.AdresSat = csat;
                istniejacyKomponent.AdresEth = ceth;

            }

            dbconn.Update(istniejacyKomponent);


        }

        


    }
}
