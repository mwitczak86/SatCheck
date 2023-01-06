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
using SatCheck.Services;
using SatCheck.Models;
using LiveCharts;
using LiveCharts.Configurations;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;

namespace SatCheck.Views
{
    /// <summary>
    /// Logika interakcji dla klasy PingView.xaml
    /// </summary>
    public partial class PingView : UserControl, INotifyPropertyChanged
    {
        private double _axisMax;
        private double _axisMin;
        private int _trend;
        private int _trend2;

        public PingView()
        {
            InitializeComponent();
            var mapper = Mappers.Xy<MeasureModel>()
                .X(model => model.DateTime.Ticks)   //use DateTime.Ticks as X
                .Y(model => model.Value);           //use the value property as Y
            Charting.For<MeasureModel>(mapper);
            ChartValues = new ChartValues<MeasureModel>();
            ChartValues2 = new ChartValues<MeasureModel>();
            DateTimeFormatter = value => new DateTime((long)value).ToString("mm:ss");
            ValueFormatter2 = value => value.ToString("N");
            AxisStep = TimeSpan.FromSeconds(1).Ticks;
            AxisUnit = TimeSpan.TicksPerSecond;
            
            SetAxisLimits(DateTime.Now);
            IsReading = false;
            ChartGrid.DataContext = this;
            
        }
        //CHART CONTROL

        public ChartValues<MeasureModel> ChartValues { get; set; }
        public ChartValues<MeasureModel> ChartValues2 { get; set; }
        public Func<double, string> DateTimeFormatter { get; set; }
        public Func<int, string> ValueFormatter2 { get; set; }
        public double AxisStep { get; set; }
        public double AxisUnit { get; set; }

        public double AxisMax
        {
            get { return _axisMax; }
            set
            {
                _axisMax = value;
                OnPropertyChanged("AxisMax");
            }
        }
        public double AxisMin
        {
            get { return _axisMin; }
            set
            {
                _axisMin = value;
                OnPropertyChanged("AxisMin");
            }
        }

        public bool IsReading { get; set; }

        private void Read()
        {
            

            while (IsReading)
            {
                Thread.Sleep(450);
                var now = DateTime.Now;

                ListForegroundColor();
                
                ChartValues.Add(new MeasureModel
                {
                    DateTime = now,
                    Value = _trend
                });

                SetAxisLimits(now);

                //lets only use the last 150 values
                if (ChartValues.Count > 150) ChartValues.RemoveAt(0);
            }
        }

        private void Read2()
        {


            while (IsReading)
            {
                Thread.Sleep(450);
                var now = DateTime.Now;

                ListForegroundColor();

                ChartValues2.Add(new MeasureModel
                {
                    DateTime = now,
                    Value = _trend2
                });

                SetAxisLimits(now);

                //lets only use the last 150 values
                if (ChartValues2.Count > 150) ChartValues2.RemoveAt(0);
            }
        }

        private void SetAxisLimits(DateTime now)
        {
            AxisMax = now.Ticks + TimeSpan.FromSeconds(1).Ticks; // lets force the axis to be 1 second ahead
            AxisMin = now.Ticks - TimeSpan.FromSeconds(8).Ticks; // and 8 seconds behind
        }

        private void InjectStopOnClick(object sender, RoutedEventArgs e)
        {
            IsReading = !IsReading;
            if (IsReading) Task.Factory.StartNew(Read);
            if (IsReading) Task.Factory.StartNew(Read2);
        }

        #region INotifyPropertyChanged implementation

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            if (PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion




        //END OF CHART CONTROL

        private void StartClicked(object sender, RoutedEventArgs e)
        {
            IsReading = !IsReading;
            if (IsReading) Task.Factory.StartNew(Read);
            if (IsReading) Task.Factory.StartNew(Read2);


            StatVariab.PingON = true;
            StatVariab.BtnStart = true;
            StatVariab.ReadON = true;
            
            wybor.Visibility = Visibility.Hidden;
            wybor_label.Visibility = Visibility.Hidden;
            //ListForegroundColor();


        }

        private void StopClicked(object sender, RoutedEventArgs e)
        {
            IsReading = !IsReading;
            if (IsReading) Task.Factory.StartNew(Read);

            StatVariab.PingON = false;
            wybor.Visibility = Visibility.Visible;
            wybor_label.Visibility = Visibility.Visible;
            StatVariab.BtnStart = false;
            StatVariab.ReadON = false;
            StatVariab.LoadChartBTN = false;
        }

        private void ListForegroundColor()
        {
            var ids = lstRep.Items.SourceCollection;

            foreach (PingRep element in ids)
            {
                var item = lstRep.ItemContainerGenerator.ContainerFromItem(element) as ListViewItem;
                if (element.Status == "SAT_ONLINE")
                {
                    
                    _trend = Convert.ToInt32(element.Rtt);
                }
                else if (element.Status == "ETH_ONLINE")
                {
                    _trend2 = Convert.ToInt32(element.Rtt);
                }
                else
                {

                   _trend = 0;
                   _trend2 = 0;
                }
            }


        }
    }
}
