using Backgammon.Interfaces;
using Backgammon.Models;
using Common.Interfaces;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.BL
{
    public class UserManger : IUserManager
    {
        InitializeProxy proxy = InitializeProxy.Instance;

        public Action<string[]> onUsersUpdated { get; set; }
        public Action<string, string> onBrodacastMassage { get; set; }
        public Action<string, string> onBrodacastMassageToAll { get; set; }
        public Action<string> onSendGameRequest { get; set; }
        public Action onSendGameRequestAnswer { get; set; }

        public UserManger()
        {
            onUsersUpdated = (userList) => { };
            proxy.Connection.Start().Wait();
            proxy.hubProxy.On<string[]>("UpdateUsersList", usersarray => onUsersUpdated(usersarray));
            proxy.hubProxy.On<string, string>("broadcastMessage", (n, m) => onBrodacastMassage(n, m));
            proxy.hubProxy.On<string, string>("broadcastMessageToAll", (n, m) => onBrodacastMassageToAll(n, m));
            proxy.hubProxy.On("SendGameRequest", (string client) => onSendGameRequest(client));
            proxy.hubProxy.On("GameRequestAnswer", () => onSendGameRequestAnswer());
        }

        public Task<bool> LogIn(ClientUser user)
        {
            return proxy.hubProxy.Invoke<bool>("LogIn", user);
        }

        public Task<bool> LogOff()
        {
            return proxy.hubProxy.Invoke<bool>("LogOff");
        }

        public Task<bool> Register(ClientUser user)
        {
            return proxy.hubProxy.Invoke<bool>("Register", new ClientUser
            {
                UserName = user.UserName,
                Password = user.Password
            });
        }

        public Task<bool> SendMessage(string toclient, string message)
        {
            return proxy.hubProxy.Invoke<bool>("BroadcastTextMessage", toclient, message);
        }

        public Task<bool> SendMessageToAll(string message)
        {
            return proxy.hubProxy.Invoke<bool>("BroadcastTextMessageToAll", message);
        }

        public Task SendGameRequest(string toclient)
        {
            return proxy.hubProxy.Invoke("GameRequest", toclient);
        }

        public Task<ClientUser> GetClient(string username)
        {
            return proxy.hubProxy.Invoke<ClientUser>("GetClient", username);
        }

        public Task GetAnswerGameRequest(string requestingUserName, string sender)
        {
            return proxy.hubProxy.Invoke("SendGameRequestAnswer", requestingUserName, sender);
        }
    }
}
