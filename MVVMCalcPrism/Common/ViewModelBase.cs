using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Practices.Prism.ViewModel;

namespace MVVMCalcPrism.Common
{
    public class ViewModelBase : NotificationObject, IDataErrorInfo
    {
        private ErrorsContainer<string> errors;

        protected ErrorsContainer<string> Errors
        {
            get
            {
                if(this.errors == null)
                {
                    this.errors = new ErrorsContainer<string>(
                        s => this.RaisePropertyChanged(() => HasError));
                }

                return errors;
            }
        }

        string IDataErrorInfo.Error
        {
            get { throw new NotSupportedException();  }
        }

        public string this[string columnName]
        {
            get
            {
                return this.Errors.GetErrors(columnName).FirstOrDefault();
            }
        }

        public bool HasError
        {
            get
            {
                return this.Errors.HasErrors;
            }
        }
    }
}
