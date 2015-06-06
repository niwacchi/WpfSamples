using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;

namespace MVVMCalc.Common
{
    public class ConfirmAction : TriggerAction<DependencyObject>
    {
        protected override void Invoke(object parameter)
        {
            var args = parameter as MessageEventArgs;
            if(args == null)
            {
                return;
            }

            var result = MessageBox.Show(
                args.Message.Body.ToString(),
                "確認",
                MessageBoxButton.OKCancel);

            args.Message.Response = result == MessageBoxResult.OK;
            args.Callback(args.Message);
        }
    }
}
