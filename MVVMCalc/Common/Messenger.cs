using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCalc.Common
{
    public class Messenger
    {
        public event EventHandler<MessageEventArgs> Raised;

        public void Raise(Message message, Action<Message> callback)
        {
            var h = this.Raised;
            if(h != null)
            {
                h(this, new MessageEventArgs(message, callback));
            }
        }
    }
}
