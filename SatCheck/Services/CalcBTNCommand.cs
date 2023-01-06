using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using SatCheck.ViewModels;

namespace SatCheck.Services
{
    public class CalcBTNCommand : ICommand
    {
         
        
        public CalculatorViewModel ViewModel { get; set; }
        public CalcBTNCommand(CalculatorViewModel viewModel)
        {
            this.ViewModel = viewModel;

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
                    this.ViewModel.SymbolRateCalculator();
                    break;
                case 2:
                    this.ViewModel.FrCalculator();
                    break;
                case 3:
                    this.ViewModel.UnitConverter();
                    this.ViewModel.dbmConverter();
                    break;
            }

        }
    }
}

