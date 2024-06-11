using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoneSdkApp.Messages
{
    internal class ReturnMessage : ValueChangedMessage<string>
    {
        public ReturnMessage(string message) : base (message)
        {
                
        }
    }
}
