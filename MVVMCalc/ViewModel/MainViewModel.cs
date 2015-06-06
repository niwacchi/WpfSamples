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
        private string lhs;
        private string rhs;
        private double answer;
        private CalculateTypeViewModel selectedCalculateType;
        private DelegateCommand calculateCommand;

        public MainViewModel()
        {
            this.CalculateTypes = CalculateTypeViewModel.Create();
            this.SelectedCalculateType = this.CalculateTypes.First();

            this.Lhs = string.Empty;
            this.Rhs = string.Empty;
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

        public string Lhs
        {
            get
            {
                return this.lhs;
            }

            set
            {
                this.lhs = value;
                if (!this.IsDouble(value))
                {
                    this.SetError("Lhs", "数字を入力してください");
                }
                else
                {
                    this.ClearError("Lhs");
                }
                
                this.RaisePropertyChanged("Lhs");
            }            
        }

        public string Rhs
        {
            get
            {
                return this.rhs;
            }

            set
            {
                this.rhs = value;
                if(!this.IsDouble(value))
                {
                    this.SetError("Rhs", "数字を入力してください");
                }
                else
                {
                    this.ClearError("Rhs");
                }

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
            this.Answer = calc.Execute(double.Parse(this.Lhs), double.Parse(this.Rhs), this.SelectedCalculateType.CalculateType);
        }

        private bool CanCalculateExecute()
        {
            return this.SelectedCalculateType.CalculateType != CalculateType.None && !this.HasError;
        }

        private bool IsDouble(string value)
        {
            var temp = default(double);
            return double.TryParse(value, out temp);
        }
    }
}
