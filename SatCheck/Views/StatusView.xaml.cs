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
using System.ComponentModel;
using SatCheck.ViewModels;
using ModernMessageBoxLib;
using SatCheck.Services;
using System.Timers;

namespace SatCheck.Views
{
    /// <summary>
    /// Logika interakcji dla klasy StatusView.xaml
    /// </summary>
    public partial class StatusView : UserControl
    {
        
        System.Windows.Threading.DispatcherTimer timer = new System.Windows.Threading.DispatcherTimer();

        
        public void Timer()
        {
            if (timer != null && StatVariab.TimerOFF)
            {
               
                timer.Tick += timer_tick;
                timer.Interval = new TimeSpan(0, 0, 2);
                timer.Start();
                
            }
         }


        public void StopTimer()
        {
            if (timer != null)
            {
                timer.Tick -= timer_tick;
                timer.Stop();
            }
        }
        
        
        ~StatusView()
        {

            // Stop the timer when the viewmodel gets destroyed
            StopTimer();
        }

        private void timer_tick(object sender, EventArgs e)
        {
            if (StatVariab.TimerOFF)
            {
                MB();
            }
            
           
           
        }

        public StatusView()
        {
            
            InitializeComponent();
            Timer();
           
        }

        public void MB()
        {
            
            var ids = listView4.Items.SourceCollection;
            int count = 0;


            foreach (Komponenty element in ids)
            {
                var item = listView4.ItemContainerGenerator.ContainerFromItem(element) as ListViewItem;
                if (item != null)
                {
                    
                    if (element.Online == "Eth")
                    {


                        count++;
                        item.Background = Brushes.Green;
                        item.Opacity = 1;


                    }
                    else if (element.Online == "Sat")
                    {
                        
                        item.Background = Brushes.Navy;
                        item.Opacity = 1;
                    }

                    else
                    {
                        item.Background = Brushes.Red;
                        item.Opacity = 0.7;
                    }


                }
                


            }
            









        }
    }
}
