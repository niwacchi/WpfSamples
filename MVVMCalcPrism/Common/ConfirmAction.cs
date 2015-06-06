using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interactivity;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;

namespace MVVMCalcPrism.Common
{
    public class ConfirmAction : TriggerAction<DependencyObject>
    {
        protected override void Invoke(object parameter)
        {
            var args = parameter as InteractionRequestedEventArgs;
            if(args == null)
            {
                return;
            }

            var context = args.Context as Confirmation;
            if(context == null)
            {
                return;
            }

            var result = MessageBox.Show(
                args.Context.Content.ToString(),
                args.Context.Title,
                MessageBoxButton.OKCancel);

            context.Confirmed = result == MessageBoxResult.OK;
            args.Callback();
        }
    }
}
