using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMCalc.Common
{
    public class Message
    {
        public object Body { get; private set; }
        public object Response { get; set; }
        
        public Message(object body)
        {
            this.Body = body;
        }
    }
}
