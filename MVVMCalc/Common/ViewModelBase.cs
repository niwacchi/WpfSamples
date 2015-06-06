using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace MVVMCalc.Common
{
    public class ViewModelBase : INotifyPropertyChanged, IDataErrorInfo
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            var h = this.PropertyChanged;
            if(h != null)
            {
                h(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        private Dictionary<string, string> errors = new Dictionary<string, string>();

        string IDataErrorInfo.Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get
            {
                if(this.errors.ContainsKey(columnName))
                {
                    return this.errors[columnName];
                }

                return null;
            }
        }

        protected void SetError(string propertyName, string errorMessage)
        {
            this.errors[propertyName] = errorMessage;
            this.RaisePropertyChanged("HasError");
        }

        protected void ClearError(String propertyName)
        {
            if(this.errors.ContainsKey(propertyName))
            {
                this.errors.Remove(propertyName);
                this.RaisePropertyChanged("HasError");
            }
        }

        protected void ClearErrors()
        {
            this.errors.Clear();
            this.RaisePropertyChanged("HasError");
        }

        public bool HasError
        {
            get
            {
                return this.errors.Count != 0;
            }
        }
    }
}
