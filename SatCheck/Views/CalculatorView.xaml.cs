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

namespace SatCheck.Views
{
    /// <summary>
    /// Logika interakcji dla klasy CalculatorView.xaml
    /// </summary>
    public partial class CalculatorView : UserControl
    {
        public CalculatorView()
        {
            InitializeComponent();
        }

        

        private void srBtn_Click(object sender, RoutedEventArgs e)
        {
            srView.Visibility = Visibility.Visible;
            frView.Visibility = Visibility.Collapsed;
            unitsView.Visibility = Visibility.Collapsed;
            srBtn.Background = Brushes.YellowGreen;
            dbmBtn.Background = Brushes.Transparent;
            
            frBtn.Background = Brushes.Transparent;
            calculate.CommandParameter = 1;
        }

        private void frBtn_Click(object sender, RoutedEventArgs e)
        {
            srView.Visibility = Visibility.Collapsed;
            frView.Visibility = Visibility.Visible;
            unitsView.Visibility = Visibility.Collapsed;
            srBtn.Background = Brushes.Transparent;
            dbmBtn.Background = Brushes.Transparent;
           
            frBtn.Background = Brushes.YellowGreen;
            calculate.CommandParameter = 2;
        }

        private void dbmBtn_Click(object sender, RoutedEventArgs e)
        {
            
            srView.Visibility = Visibility.Collapsed;
            frView.Visibility = Visibility.Collapsed;
            unitsView.Visibility = Visibility.Visible;
            srBtn.Background = Brushes.Transparent;
           
            frBtn.Background = Brushes.Transparent;
            dbmBtn.Background = Brushes.YellowGreen;
            calculate.CommandParameter = 3;
        }
    }
}
