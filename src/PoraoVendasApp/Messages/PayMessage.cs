using CommunityToolkit.Mvvm.Messaging.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoraoVendasApp.Messages
{
    internal class PayMessage : ValueChangedMessage<string>
    {
        public PayMessage() : base ("")
        {
                
        }
    }
}
