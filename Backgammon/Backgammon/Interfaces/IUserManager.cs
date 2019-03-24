using Backgammon.Models;
using Common.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.Interfaces
{
    public interface IUserManager
    {
        Task<bool> Register(ClientUser user);
        Task<bool> LogIn(ClientUser user);
        Task<bool> LogOff();
        Task<bool> SendMessage(string toclient, string message);
        Task<bool> SendMessageToAll(string message);
        Task SendGameRequest(string toclient);
        Task<ClientUser> GetClient(string username);
        Task GetAnswerGameRequest(string clientToRespondTo,string sender);

        Action<string> onSendGameRequest { get; set; }
        Action onSendGameRequestAnswer { get; set; }
        Action<string[]> onUsersUpdated { get; set; }
        Action<string, string> onBrodacastMassage { get; set; }
        Action<string, string> onBrodacastMassageToAll { get; set; }


    }
}
