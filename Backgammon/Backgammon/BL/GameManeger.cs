using Backgammon.Interfaces;
using Backgammon.Models;
using ClassLibrary1.Models;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.BL
{
    public class GameManeger : IGameManager
    {
        InitializeProxy proxy = InitializeProxy.Instance;

        public Action<Cube> onRollCube { get; set; }
        public Action<BoardState> onGetBoardState { get; set; }
        public Action<BoardState> onPrisonerBlocked { get; set; }
        public Action onInitCube { get; set; }
        public Action<BoardState> onMakeMovment { get; set; }

        public GameManeger()
        {
            proxy.hubProxy.On("UpdateCube", (Cube cube) => onRollCube(cube));

            proxy.hubProxy.On("getBoardState", (BoardState boardState) => onGetBoardState(boardState));

            proxy.hubProxy.On("prisonerBlocked", (BoardState boardState) => onPrisonerBlocked(boardState));

            proxy.hubProxy.On("InitCube", () => onInitCube());

            proxy.hubProxy.On("Movment", (BoardState turnchangedBoardState) => onMakeMovment(turnchangedBoardState));
        }

        public Task<BoardState> InitBoard(string firstPlayer, string secondPlayer)
        {
            return proxy.hubProxy.Invoke<BoardState>("InitBoard", firstPlayer, secondPlayer);
        }

        public Task<BoardState> RollCube(BoardState board)
        {
            return proxy.hubProxy.Invoke<BoardState>("RollCube", board);
        }

        public bool ValidateChekerColor(int selectedChecker, BoardState boardState)
        {
            if (boardState.CurrentPlayer == boardState.BlackPlayer)
            {
                return boardState.BlackLocation.ContainsKey(selectedChecker);
            }
            else
            {
                return boardState.WhiteLocation.ContainsKey(selectedChecker);
            }
        }

        public Task<bool> MoveCheker(BoardState boardState, int selectedChecker, int selectedPlace)
        {

            return proxy.hubProxy.Invoke<bool>("MakeMove", boardState, selectedChecker, selectedPlace);

        }

        public Task PrisonerCanFree(BoardState boardState)
        {
            return proxy.hubProxy.Invoke("PrisonerCanFree", boardState);
        }

    }
}
