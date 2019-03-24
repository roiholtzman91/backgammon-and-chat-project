using Backgammon.BL;
using Backgammon.Interfaces;
using Backgammon.Services;
using CommonServiceLocator;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;

namespace Backgammon.ViewModel
{
    public class ViewModelLocator
    {
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SetupNavigation();
            SimpleIoc.Default.Register<RegisterViewModel>();
            SimpleIoc.Default.Register<ChatViewModel>();
            SimpleIoc.Default.Register<GameViewModel>();
            SimpleIoc.Default.Register<IUserManager,UserManger>();
            SimpleIoc.Default.Register<IMessages, MessagesSrvise>();
            SimpleIoc.Default.Register<IGameManager, GameManeger>();

        }

        public RegisterViewModel Register
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RegisterViewModel>();
            }
        }

        public ChatViewModel Chat
        {
            get
            {
                return ServiceLocator.Current.GetInstance<ChatViewModel>();
            }
        }

        public GameViewModel Game
        {
            get
            {
                return ServiceLocator.Current.GetInstance<GameViewModel>();
            }
        }


        public static void Cleanup()
        {
        }

        private static void SetupNavigation()
        {
            var navigationService = new FrameNavigationService();
            navigationService.Configure("Register", new Uri("../Views/RegisterPage.xaml", UriKind.Relative));
            navigationService.Configure("Chat", new Uri("../Views/ChatPage.xaml", UriKind.Relative));
            navigationService.Configure("Game", new Uri("../Views/GamePage.xaml", UriKind.Relative));
            SimpleIoc.Default.Register<IFrameNavigationService>(() => navigationService);
        }
    }
}