using Backgammon.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Backgammon.Services
{
    class MessagesSrvise : IMessages
    {
        public string CleanMessage()
        {
            return "";
        }

        public string IsNotYourTurn()
        {
            return "Is Not Your Turn";
        }

        public string IsNullOrWhiteSpaceError()
        {
            return "Please Enter your name and your password";
        }

        public string LogInErrorMessage()
        {
            return "please register first/User Allready Exist";
        }

        public string OpenMessage()
        {
            return "Hello,Please Log In or Enter Your Name&Password";
        }

        public bool PopYesOrNoMessage(string msg)
        {
            MessageBoxResult result = MessageBox.Show(msg,
                                                      "Confirmation",
                                                       MessageBoxButton.YesNo,
                                                       MessageBoxImage.Question);
            if (result.ToString().Equals("Yes"))
                return true;
            else return false;
        }

        public string RegisterErrorMessage()
        {
            return "user already exist";
        }

        public void WaitMassege()
        {
            MessageBox.Show("Please Wait to you Turn");
        }
    }
}
