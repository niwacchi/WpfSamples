using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using MVVMCalcPrism.Common;
using MVVMCalcPrism.Model;
using System.Collections.Generic;
using System.Linq;

namespace MVVMCalcPrism.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private string lhs;
        private string rhs;
        private double answer;
        private CalculateTypeViewModel selectedCalculateType;
        private DelegateCommand calculateCommand;
        private InteractionRequest<Confirmation> errorRequest = new InteractionRequest<Confirmation>();

        public MainViewModel()
        {
            this.CalculateTypes = CalculateTypeViewModel.Create().ToArray();
            this.InitializeProperties();
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
                this.RaisePropertyChanged(() => SelectedCalculateType);
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
                    this.Errors.SetErrors(() => Lhs, new[]{"数字を入力してください"});
                }
                else
                {
                    this.Errors.ClearErrors(() => Lhs);
                }
                
                this.RaisePropertyChanged(() => Lhs);
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
                    this.Errors.SetErrors(() => Rhs, new[]{"数字を入力してください"});
                }
                else
                {
                    this.Errors.ClearErrors(() => Rhs);
                }

                this.RaisePropertyChanged(() => Rhs);
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
                this.RaisePropertyChanged(() => Answer);
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

        public InteractionRequest<Confirmation> ErrorRequest
        {
            get
            {
                return this.errorRequest;
            }
        }

        private void CalculateExecute()
        {
            var calc = new Calculator();
            this.Answer = calc.Execute(
                double.Parse(this.Lhs),
                double.Parse(this.Rhs),
                this.SelectedCalculateType.CalculateType);

            if(IsInvalidAnswer())
            {
                this.ErrorRequest.Raise(
                    new Confirmation
                    {
                        Title = "確認",
                        Content = "計算結果が実数の範囲を超えました。入力値を初期化しますか？"
                    },
                    r =>
                    {
                        if (!r.Confirmed)
                        {
                            return;
                        }

                        InitializeProperties();
                    });
            }
        }

        private bool CanCalculateExecute()
        {
            return this.SelectedCalculateType.CalculateType != CalculateType.None
                && !this.HasError;
        }

        private bool IsInvalidAnswer()
        {
            return double.IsInfinity(this.Answer) || double.IsNaN(this.Answer);
        }

        private void InitializeProperties()
        {
            this.Lhs = string.Empty;
            this.Rhs = string.Empty;
            this.Answer = default(double);
            this.SelectedCalculateType = this.CalculateTypes.First();
        }

        private bool IsDouble(string value)
        {
            var temp = default(double);
            return double.TryParse(value, out temp);
        }
    }
}
