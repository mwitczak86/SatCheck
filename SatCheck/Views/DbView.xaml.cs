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
using SatCheck.Models;
using SQLite;
using System.IO;
using System.Collections.ObjectModel;
using SatCheck.Services;
using System.ComponentModel;
using ModernMessageBoxLib;

namespace SatCheck.Views

   
{
    /// <summary>
    /// Logika interakcji dla klasy DbView.xaml
    /// </summary>
    public partial class DbView : UserControl
    {

       
        

       
        public DbView()
        {
            
            InitializeComponent();
           

        }

       

        public void Ok_Delete_Clicked(object sender, RoutedEventArgs e)
        {
           
            var ids = listView4.Items.SourceCollection;

            anuluj.Visibility = Visibility.Collapsed;
            warning.Visibility = Visibility.Collapsed;
           

            


            foreach (Komponenty element in ids)
            {
                if (element.IsSelected)
                {
                    App.Database.DeleteTaskAsync(element);
                    

                }




            }
            potwierdzenie.Visibility = Visibility.Visible;

            ok2.Visibility = Visibility.Visible;













        }


        public void Ok_Delete_Clicked2(object sender, RoutedEventArgs e)
        {
            DeleteMB.Visibility = Visibility.Collapsed;
            DataContext = new DbViewModel();
            ok2.Visibility = Visibility.Collapsed;
            potwierdzenie.Visibility = Visibility.Collapsed;

            
        }

            public void Cancel_Delete_Clicked(object sender, RoutedEventArgs e)
        {
            DeleteMB.Visibility = Visibility.Collapsed;
            

        }

        
        


        public void Checked_Delete_Clicked(object sender, RoutedEventArgs e)
        {
            int counter = 0;
            var ids = listView4.Items.SourceCollection;
            

        foreach (Komponenty element in ids)
        {
            if (element.IsSelected)
            {
                counter++;




            }

        }

        if (counter != 0)
        {

            DeleteMB.Visibility = Visibility.Visible;
            warning.Visibility = Visibility.Visible;
            ok.Visibility = Visibility.Visible;
            anuluj.Visibility = Visibility.Visible;
            ok2.Visibility = Visibility.Collapsed;
            potwierdzenie.Visibility = Visibility.Collapsed;



        }

    }








}






    }

