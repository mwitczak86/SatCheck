using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Xps.Packaging;
using SatCheck.Models;
using SatCheck.ViewModels;

namespace SatCheck.Views
{
    /// <summary>
    /// Logika interakcji dla klasy RaportView.xaml
    /// </summary>
    public partial class RaportView : UserControl, INotifyPropertyChanged


    {

        public event PropertyChangedEventHandler PropertyChanged;
        private void NotifyPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public RaportView()
        {

            InitializeComponent();

        }



        private void CalendarDb_Clicked(object sender, RoutedEventArgs e)
        {

        }

        private void RaportALL_Click(object sender, RoutedEventArgs e)
        {
            singleReport.Visibility = Visibility.Collapsed;
            raportSieci.Visibility = Visibility.Visible;

        }

        private void RaporOne_Click(object sender, RoutedEventArgs e)
        {

            raportSieci.Visibility = Visibility.Collapsed;
            singleReport.Visibility = Visibility.Visible;

        }

        private void PrintBtn(object sender, RoutedEventArgs e)
        {
            ReportInfo.Visibility = Visibility.Visible;

            PrintDialog dlg = new PrintDialog();
            if ((bool)dlg.ShowDialog().GetValueOrDefault())
            {
                dlg.PrintVisual(ReportInfo, "RaportSieci");
            }
            ReportInfo.Visibility = Visibility.Collapsed;
        }

        //private static void CreateFixedDocument()
        //{



        //    FixedPage page = new FixedPage();
        //    page.Background = Brushes.White;
        //    page.Width = 96 * 8.5;
        //    page.Height = 96 * 11;

        //    TextBlock tbTitle = new TextBlock();
        //    tbTitle.Text = "Raport statusu sieci.";
        //    tbTitle.FontSize = 24;
        //    tbTitle.FontFamily = new FontFamily("Arial");
        //    FixedPage.SetLeft(tbTitle, 96 * 0.75); // left margin
        //    FixedPage.SetTop(tbTitle, 96 * 0.75); // top margin
        //    page.Children.Add((UIElement)tbTitle);

            

        //    page.Children.Add((UIElement));

        //    Size sz = new Size(96 * 8.5, 96 * 11);
        //    page.Measure(sz);
        //    page.Arrange(new Rect(new Point(), sz));
        //    page.UpdateLayout();


        //}
    }

}
