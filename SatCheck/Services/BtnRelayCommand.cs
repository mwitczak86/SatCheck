using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using SatCheck.ViewModels;
using SatCheck.Views;

namespace SatCheck.Services
{
    public class BtnRelayCommand : ICommand
    {
        public RaportView View { get; set; }
        public RaportViewModel ViewModel { get; set; }
        public BtnRelayCommand(RaportViewModel viewModel)
        {
            this.ViewModel = viewModel;
           
        }

        public BtnRelayCommand(RaportView view)
        {
            this.View = view;

        }


        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            switch (int.Parse(parameter.ToString()))
            {
                case 1:
                    this.ViewModel.IleStacji();
                    break;
                case 2:
                    this.ViewModel.JednejStacji();
                    break;
                case 3:
                    this.ViewModel.ShowFolderBrowserDialog();
                    break;
                case 4:
                    this.ViewModel.PrintNetStatus();
                    break;
            }
            
        }
    }
}
