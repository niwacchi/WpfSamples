using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMCalc.Common;
using MVVMCalc.Model;

namespace MVVMCalc.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private double lhs;
        private double rhs;
        private double answer;
        private CalculateTypeViewModel selectedCalculateType;
        private DelegateCommand calculateCommand;

        public MainViewModel()
        {
            this.CalculateTypes = CalculateTypeViewModel.Create();
            this.SelectedCalculateType = this.CalculateTypes.First();
        }

        public IEnumerable<CalculateTypeViewModel> CalculateTypes { get; private set; }

        public CalculateTypeViewModel SelectedCalculateType
        {
            get
            {
                return this.selectedCalculateType;
            }

            set
            {
                this.selectedCalculateType = value;
                this.RaisePropertyChanged("SelectedCalculateType");
            }
        }

        public double Lhs
        {
            get
            {
                return this.lhs;
            }

            set
            {
                this.lhs = value;
                this.RaisePropertyChanged("Lhs");
            }            
        }

        public double Rhs
        {
            get
            {
                return this.rhs;
            }

            set
            {
                this.rhs = value;
                this.RaisePropertyChanged("Rhs");
            }
        }

        public double Answer
        {
            get
            {
                return this.answer;
            }

            set
            {
                this.answer = value;
                this.RaisePropertyChanged("Answer");
            }
        }

        public DelegateCommand CalculateCommand
        {
            get
            {
                if(this.calculateCommand == null)
                {
                    this.calculateCommand = new DelegateCommand(CalculateExecute, CanCalculateExecute);
                }

                return this.calculateCommand;
            }
        }

        private void CalculateExecute()
        {
            var calc = new Calculator();
            this.Answer = calc.Execute(this.Lhs, this.Rhs, this.SelectedCalculateType.CalculateType);
        }

        private bool CanCalculateExecute()
        {
            return this.SelectedCalculateType.CalculateType != CalculateType.None;
        }
    }
}
