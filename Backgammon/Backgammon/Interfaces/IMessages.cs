using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.Interfaces
{
    public interface IMessages
    {
        string CleanMessage();
        string RegisterErrorMessage();
        string LogInErrorMessage();
        bool PopYesOrNoMessage(string msg);
        string OpenMessage();
        void WaitMassege();
        string IsNullOrWhiteSpaceError();
        string IsNotYourTurn();
    }
}
