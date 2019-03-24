using Backgammon.Interfaces;
using Backgammon.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Views;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Navigation;

namespace Backgammon.ViewModel
{
    public class RegisterViewModel : ViewModelBase
    {
        #region Members
        public ClientUser registerUser { get; set; }
        private string _Message;
        public string Message
        {
            get { return _Message; }
            set
            {
                _Message = value;
                RaisePropertyChanged();
            }
        }


        #endregion

        #region Interfaces
        private IUserManager userManager;
        private IFrameNavigationService NavigationService;
        private IMessages messages;
        #endregion

        #region Commands
        public ICommand Register { get; set; }
        public ICommand LogIn { get; set; }
        #endregion

        #region Ctor
        public RegisterViewModel(IUserManager uManager, IFrameNavigationService frameNavigationService,
                                 IMessages imessages)
        {
            userManager = uManager;
            NavigationService = frameNavigationService;
            messages = imessages;
            registerUser = new ClientUser();
            
            Init();
        }
        #endregion

        #region Functions
        private void Init()
        {
            Register = new RelayCommand(() =>
            {
                Message = messages.CleanMessage();
                if (string.IsNullOrWhiteSpace(registerUser.UserName) ||
                    string.IsNullOrWhiteSpace(registerUser.Password))
                {
                    Message = messages.IsNullOrWhiteSpaceError();
                    return;
                }

                userManager.Register(registerUser).ContinueWith(r =>
                {
                    if (r.Result is true)
                    {
                        Message = messages.CleanMessage();                        
                        Application.Current.Dispatcher.Invoke(() => NavigationService.NavigateTo("Chat", registerUser));
                    }
                    else
                    {
                        Message = messages.RegisterErrorMessage();                       
                    }
                });
            });

            LogIn = new RelayCommand(() =>
              {
                  Message = messages.CleanMessage();
                  userManager.LogIn(registerUser).ContinueWith(r =>
                  {
                      if (r.Result is true)
                      {
                          Application.Current.Dispatcher.Invoke(() => NavigationService.NavigateTo("Chat", registerUser));
                      }
                      else
                      {
                          Message = messages.LogInErrorMessage();
                      }
                  });

              });
        }
        #endregion
    }
}