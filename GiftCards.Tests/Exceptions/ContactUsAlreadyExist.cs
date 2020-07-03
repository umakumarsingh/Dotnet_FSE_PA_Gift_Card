using System;
using System.Collections.Generic;
using System.Text;

namespace GiftCards.Tests.Exceptions
{
 public   class ContactUsAlreadyExist :Exception
    {
        public string Messages;

        public ContactUsAlreadyExist()
        {
            Messages = "Already Exist";
        }
        public ContactUsAlreadyExist(string message)
        {
            Messages = message;
        }
    }
}
