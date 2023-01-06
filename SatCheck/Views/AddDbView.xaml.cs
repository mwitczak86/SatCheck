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
using SatCheck.ViewModels;
using SQLite;

namespace SatCheck.Views
{
    /// <summary>
    /// Logika interakcji dla klasy AddDbView.xaml
    /// </summary>
    public partial class AddDbView : UserControl
    {
       


        public AddDbView()
        {   
            

            InitializeComponent();

            
        }

       

        private void Saved_clicked(object sender, RoutedEventArgs e)
        {
            saved.Visibility = Visibility.Visible;
           

        }
        
       
    }
}
