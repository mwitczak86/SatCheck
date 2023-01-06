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
using MaterialDesignThemes;
using MaterialDesignColors;
using SatCheck.ViewModels;
using SatCheck.Views;
using ModernMessageBoxLib;

using System.IO;
using SatCheck.Models;
using System.Windows.Threading;
using SatCheck.Services;
using System.Timers;


namespace SatCheck
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window 
    {


        



        public MainWindow()
        {
            InitializeComponent();
            
            DataContext = new StartViewModel();
            
            

        }

        private void StartView_Clicked(object sender, RoutedEventArgs e)
        {
            StatVariab.TimerOFF = false;
            StatusViewModel status = DataContext as StatusViewModel;
            if (status != null)
            {
                status.StopTimer();
            }
            DataContext = new StartViewModel();
            add.Visibility = Visibility.Hidden;
            update.Visibility = Visibility.Hidden;
            


        }

        private void DbView_Clicked(object sender, RoutedEventArgs e)
        {
            StatVariab.TimerOFF = false;
            StatusViewModel status = DataContext as StatusViewModel;
            if (status != null)
            {
                status.StopTimer();
            }
            
            
            DataContext = new DbViewModel();
            add.Visibility = Visibility.Visible;
            update.Visibility = Visibility.Visible;
            
            //ReadDatabase();

        }

        private void StatusView_Clicked(object sender, RoutedEventArgs e)
        {
            StatVariab.TimerOFF = true;
            DataContext = new StatusViewModel();
            add.Visibility = Visibility.Hidden;
            update.Visibility = Visibility.Hidden;
            
            
        }

        private void PingView_Clicked(object sender, RoutedEventArgs e)
        {
            StatVariab.TimerOFF = false;
            StatusViewModel status = DataContext as StatusViewModel;
            if (status != null)
            {
                status.StopTimer();
            }

            StatVariab.PingON = true;
            StatVariab.ReadON = true;
            DataContext = new PingViewModel();
            add.Visibility = Visibility.Hidden;
            update.Visibility = Visibility.Hidden;
            
            
        }

        private void RaportView_Clicked(object sender, RoutedEventArgs e)
        {
            StatVariab.TimerOFF = false;
            StatusViewModel status = DataContext as StatusViewModel;
            if (status != null)
            {
                status.StopTimer();
            }
            DataContext = new RaportViewModel();
            add.Visibility = Visibility.Hidden;
            update.Visibility = Visibility.Hidden;
            

        }

        private void CalculatorView_Clicked(object sender, RoutedEventArgs e)
        {
            StatVariab.TimerOFF = false;
            StatusViewModel status = DataContext as StatusViewModel;
            if (status != null)
            {
                status.StopTimer();
            }
            DataContext = new CalculatorViewModel();
            add.Visibility = Visibility.Hidden;
            update.Visibility = Visibility.Hidden;
            

        }

        private void AddDb_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new AddDbViewModel();
            add.Visibility = Visibility.Hidden;
            update.Visibility = Visibility.Hidden;
            back.Visibility = Visibility.Visible;
            

        }

        private void UpdateDb_Clicked(object sender, RoutedEventArgs e)
        {
           
            DataContext = new UpdateDbViewModel();
            add.Visibility = Visibility.Hidden;
            update.Visibility = Visibility.Hidden;
            back.Visibility = Visibility.Visible;
            

        }

        private void Back_Clicked(object sender, RoutedEventArgs e)
        {
            DataContext = new DbViewModel();
            add.Visibility = Visibility.Visible;
            update.Visibility = Visibility.Visible;
            back.Visibility = Visibility.Collapsed;
            

        }

        private void HelpBTN(object sender, RoutedEventArgs e)
        {
            QModernMessageBox.Info("\tMichał Witczak \n\tSatCheck v1.0.0\n\n\tm.witczak@tutanota.com \n\t2022", "SatCheck - Info");
            
        }

        private void ExitBTN(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
