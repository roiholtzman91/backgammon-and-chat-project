using Backgammon.Interfaces;
using Backgammon.Models;
using Backgammon.Services;
using ClassLibrary1.Models;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Backgammon.ViewModel
{
    public class GameViewModel : ViewModelBase
    {
        #region Members
        BoardState boardState;
        PropertyChange propertyChange;
        #endregion

        #region Fields
        bool rollCubeOnce;
        bool canMakePlay;
        int selectedChecker;
        int selectedPlace;
        #endregion

        #region props
        public string WhitePlayer { get; set; }
        public string BlackPlayer { get; set; }

        private ObservableCollection<Ellipse>[] _cells;
        public ObservableCollection<Ellipse>[] Cells
        {
            get
            {
                return _cells;
            }
            set
            {
                _cells = value;
                propertyChange.OnPropertyChanged();
            }
        }

        //Cube
        public string ImgCube1 { get; set; }

        public string ImgCube2 { get; set; }

        private string _visCubeGroup1;
        public string VisCubeGroup1
        {
            get { return _visCubeGroup1; }
            set
            {
                _visCubeGroup1 = value;
                propertyChange.OnPropertyChanged();

            }
        }

        private string _visCubeGroup2;
        public string VisCubeGroup2


        {
            get { return _visCubeGroup2; }
            set
            {
                _visCubeGroup2 = value;
                propertyChange.OnPropertyChanged();
            }
        }

        //barred cheker
        private string _whiteBarredVisibility;
        public string WhiteBarredVisibility
        {
            get { return _whiteBarredVisibility; }
            set
            {
                _whiteBarredVisibility = value;
                RaisePropertyChanged();
            }
        }

        private string _blackBarredVisibility;
        public string BlackBarredVisibility
        {
            get { return _blackBarredVisibility; }
            set
            {
                _blackBarredVisibility = value;
                RaisePropertyChanged();
            }
        }

        private int _blackBarredCount;
        public int BlackBarredCount
        {
            get { return _blackBarredCount; }
            set
            {
                _blackBarredCount = value;
                RaisePropertyChanged();
            }
        }

        private int _whiteBarredCount;
        public int WhiteBarredCount
        {
            get { return _whiteBarredCount; }
            set
            {
                _whiteBarredCount = value;
                RaisePropertyChanged();
            }
        }
        #endregion

        #region Commnds
        public ICommand ChooseCheckerCommand { get; set; }
        public ICommand RoleCubeCommand { get; set; }
        #endregion

        #region Interfaces
        IMessages messages;
        IFrameNavigationService NavigationService;
        IGameManager GameManager;
        #endregion

        #region Ctor
        public GameViewModel(IFrameNavigationService frameNavigationService, IMessages imessages,
                             IGameManager gameManager)
        {
            messages = imessages;
            NavigationService = frameNavigationService;
            GameManager = gameManager;
            boardState = new BoardState();
            propertyChange = new PropertyChange();
            InitDelegats();
            InitGame();
            InitCommands();
            InitBoard();
        }
        #endregion

        #region InitFunctions

        private void InitGame()
        {
            var pleyers = NavigationService.Parameter as string[];
            boardState = GameManager.InitBoard(pleyers[0], pleyers[1]).Result;
            Cells = new ObservableCollection<Ellipse>[24];
            for (int i = 0; i < 24; i++)
            {
                Cells[i] = new ObservableCollection<Ellipse>();
            }

            WhitePlayer = boardState.WhitePlayer;
            BlackPlayer = boardState.BlackPlayer;
            selectedChecker = -1;
            selectedPlace = -1;
        }

        private void InitDelegats()
        {
            GameManager.onRollCube = (cube) =>
            {
                boardState.Cube = cube;
                ImgCube1 = $"/Assets/{cube.Cube1.ToString()}.png";
                ImgCube2 = $"/Assets/{cube.Cube2.ToString()}.png";
                RaisePropertyChanged(nameof(ImgCube1));
                RaisePropertyChanged(nameof(ImgCube2));
            };

            GameManager.onMakeMovment = (newBoardState) =>
            {
                boardState = newBoardState;
               
                canMakePlay = false;
                rollCubeOnce = false;
            };

            GameManager.onInitCube = () =>
            {
                rollCubeOnce = true;
                canMakePlay = true;
            };

            GameManager.onGetBoardState = (boardState) =>
            {
                UpdateBoard(boardState);
                if ((boardState.IsDouble && boardState.CountMovement == 4)
                    || (!boardState.IsDouble && boardState.CountMovement == 2))
                    if (!canMakePlay) canMakePlay = true;
            };

            GameManager.onPrisonerBlocked = (boardState) =>
            {
                PrisonerGetBlocked(boardState);
            };
        }

        private void InitCommands()
        {
            ChooseCheckerCommand = new RelayCommandWithParameter<string>((Select) =>
            {
                if (!canMakePlay)
                    MoveChecker(Select);
                else messages.IsNotYourTurn();
            });

            RoleCubeCommand = new RelayCommand(() =>
            {
                if (!rollCubeOnce)
                {
                    if ((boardState.IsDouble && boardState.CountMovement != 4)
                        || (!boardState.IsDouble && boardState.CountMovement != 2))
                    {
                        boardState = GameManager.RollCube(boardState).Result;
                        ImgCube1 = $"/Assets/{boardState.Cube.Cube1.ToString()}.png";
                        ImgCube2 = $"/Assets/{boardState.Cube.Cube2.ToString()}.png";
                        RaisePropertyChanged(nameof(ImgCube1));
                        RaisePropertyChanged(nameof(ImgCube2));
                        rollCubeOnce = true;
                    }
                }
                else messages.IsNotYourTurn();
            });
        }

        private void InitBoard()
        {
            ComponentsDefult();

            for (int i = 0; i < 24; i++)
            {
                if (boardState.WhiteLocation.ContainsKey(i))
                {
                    for (int j = 0; j < boardState.WhiteLocation[i]; j++)
                    {
                        _cells[i].Add(Checker(false));
                    }
                }
            }

            for (int i = 0; i < 24; i++)
            {
                if (boardState.BlackLocation.ContainsKey(i))
                {
                    for (int j = 0; j < boardState.BlackLocation[i]; j++)
                    {
                        _cells[i].Add(Checker(true));
                    }
                }
            }
        }

        #endregion

        #region PrivetFunctions

        private Ellipse Checker(bool isBlack)
        {
            Ellipse ellipse = new Ellipse
            {
                Height = 20,
                Width = 20,
                Stroke = new SolidColorBrush(Colors.Black)
            };

            if (isBlack)
                ellipse.Fill = new SolidColorBrush(Colors.Black);
            else
                ellipse.Fill = new SolidColorBrush(Colors.White);

            return ellipse;
        }

        private void ComponentsDefult()
        {
            ImgCube1 = $"/Assets/6.png";
            ImgCube2 = $"/Assets/5.png";

            VisCubeGroup1 = "Visible";
            VisCubeGroup2 = "Hidden";

            WhiteBarredVisibility = "Hidden";
            BlackBarredVisibility = "Hidden";
        }

        private void MoveChecker(string selected)
        {
            if (rollCubeOnce == false)
            {
                MessageBox.Show("Please roll cubes first");
                return;
            }

            if (selectedChecker == -1)
            {
                if (boardState.CurrentPlayer == boardState.BlackPlayer && boardState.BarredBlackCheckers != 0)
                {
                    selectedChecker = 24;
                }
                else if (boardState.CurrentPlayer == boardState.WhitePlayer && boardState.BarredWhiteCheckers != 0)
                {

                }
                else
                {
                    int.TryParse(selected, out selectedChecker);
                    if (GameManager.ValidateChekerColor(selectedChecker, boardState))
                    {
                        Mouse.OverrideCursor = Cursors.Hand;
                    }
                    else
                    {
                        MessageBox.Show("you can't move oponnent's cheker!!!");
                        selectedChecker = -1;
                    }
                    return;
                }
            }
            int.TryParse(selected, out selectedPlace);
            if (selectedChecker != selectedPlace)
            {
                if (!GameManager.MoveCheker(boardState, selectedChecker, selectedPlace).Result)
                {
                    MessageBox.Show("Can't move cheker to specified location");
                }
            }
            Mouse.OverrideCursor = Cursors.Arrow;
            selectedChecker = -1;
            selectedPlace = -1;
        }

        private void PrisonerGetBlocked(BoardState board)
        {
            boardState = board;
            MessageBox.Show("Player cant move, switch turn");
        }

        private void UpdateBoard(BoardState newBoardState)
        {
            boardState = newBoardState;

            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                if (boardState.MoveFrom != 24 && boardState.MoveFrom != -1)
                {
                    Cells[boardState.MoveFrom].RemoveAt(0);

                }

                if (boardState.TurnChanged)
                {
                    Cells[boardState.MoveTo].Add(Checker((boardState.CurrentPlayer == boardState.BlackPlayer) ? false : true));
                }
                else
                    Cells[boardState.MoveTo].Add(Checker((boardState.CurrentPlayer == boardState.BlackPlayer) ? true : false));

                if (boardState.IsBarred)
                {
                    Cells[boardState.MoveTo].RemoveAt(0);
                }

                DisplayBarredCheckers();

            }));
        }

        private void DisplayBarredCheckers()
        {
            if (boardState.BarredBlackCheckers == 0)
            {
                BlackBarredVisibility = "Hidden";
            }
            else
            {
                BlackBarredVisibility = "Visible";
                BlackBarredCount = boardState.BarredBlackCheckers;
            }

            if (boardState.BarredWhiteCheckers == 0)
            {
                WhiteBarredVisibility = "Hidden";
            }
            else
            {
                WhiteBarredVisibility = "Visible";
                WhiteBarredCount = boardState.BarredWhiteCheckers;
            }
        }

        #endregion


    }
}
