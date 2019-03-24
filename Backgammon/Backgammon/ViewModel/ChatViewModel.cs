using Backgammon.Interfaces;
using Backgammon.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Backgammon.ViewModel
{
    public class ChatViewModel : ViewModelBase
    {
        #region Members
        public ObservableCollection<string> OnlineUsers { get; set; }
        public ClientUser registerUser { get; set; }
        public string toClient { get; set; }
        public string Message { get; set; }
        public string MessagesBlock { get; set; }
        #endregion

        #region Commands
        public ICommand SendMassege { get; set; }
        public ICommand SendMassegeToAll { get; set; }
        public ICommand LogOff { get; set; }
        public ICommand Play { get; set; }
        #endregion

        #region Interfaces
        private IUserManager userManager;
        private IFrameNavigationService NavigationService;
        private IMessages messages;
        #endregion

        #region Ctor
        public ChatViewModel(IUserManager uManager, IFrameNavigationService service, IMessages msg)
        {
            userManager = uManager;
            NavigationService = service;
            messages = msg;
            registerUser = service.Parameter as ClientUser;
            OnlineUsers = new ObservableCollection<string>();
            Init();
        }
        #endregion
       
        private void Init()
        {
            userManager.onUsersUpdated = (UserList) =>
            {
                OnlineUsers = new ObservableCollection<string>(UserList);
                OnlineUsers.Remove(registerUser.UserName);
                RaisePropertyChanged(nameof(OnlineUsers));
            };
            userManager.LogIn(registerUser);

            userManager.onBrodacastMassage = (n, m) =>
            {
                MessagesBlock += ChatFormater.Format(n, m);
                RaisePropertyChanged(nameof(MessagesBlock));
            };

            userManager.onBrodacastMassageToAll = (n, m) =>
            {
                MessagesBlock += ChatFormater.FormatToAll(n, m);
                RaisePropertyChanged(nameof(MessagesBlock));
            };

            userManager.onSendGameRequest = (string requestingClientName) =>
            {
                if (messages.PopYesOrNoMessage($"Do You Want To Play With:{requestingClientName}"))
                {
                    registerUser.IsGame = true;
                    Application.Current.Dispatcher.Invoke(() => NavigationService.NavigateTo("Game", new string[] { registerUser.UserName,
                                                                                                                   requestingClientName }));

                    userManager.GetAnswerGameRequest(requestingClientName, registerUser.UserName);
                }
            };

            userManager.onSendGameRequestAnswer = () =>
            {
                registerUser.IsGame = true;
                Application.Current.Dispatcher.Invoke(() => NavigationService.NavigateTo("Game", new string[] {toClient,
                                                                                                                 registerUser.UserName}));
            };

            #region commands

            SendMassege = new RelayCommand(() =>
            {
                if (string.IsNullOrWhiteSpace(Message)) return;
                else if (toClient != null)
                {
                    userManager.SendMessage(toClient, Message);
                }
            });

            SendMassegeToAll = new RelayCommand(() =>
            {
                if (string.IsNullOrWhiteSpace(Message)) return;
                else
                {
                    userManager.SendMessageToAll(Message);
                }
            });

            LogOff = new RelayCommand(() =>
            {
                if (messages.PopYesOrNoMessage("Are You Sure"))
                {
                    userManager.LogOff();
                    Application.Current.Dispatcher.Invoke(() => NavigationService.NavigateTo("Register"));
                }

            });

            Play = new RelayCommand(() =>
            {
                if (toClient == null)
                {
                    MessageBox.Show("please select user");
                    return;
                }
                var user = userManager.GetClient(toClient).Result;
                if (!user.IsGame)
                {
                    userManager.SendGameRequest(toClient);
                }
            });
            #endregion
        }
        private bool CheckYesOrNo()
        {
            MessageBoxResult result = MessageBox.Show("are you shure?",
                                                            "Confirmation",
                                                             MessageBoxButton.YesNo,
                                                             MessageBoxImage.Question);
            if (result.ToString().Equals("Yes"))
                return true;
            else return false;
        }
    }
}

