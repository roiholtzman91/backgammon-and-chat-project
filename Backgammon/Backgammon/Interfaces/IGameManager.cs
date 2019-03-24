using Backgammon.Models;
using ClassLibrary1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.Interfaces
{
    public interface IGameManager
    {
        Task<BoardState> RollCube(BoardState board);
        Task<BoardState> InitBoard(string firstPlayer, string secondPlayer);       
        Task<bool> MoveCheker(BoardState boardState, int selectedChecker, int selectedPlace);
        Task PrisonerCanFree(BoardState boardState);

        Action<Cube> onRollCube { get; set; }
        Action<BoardState> onGetBoardState { get; set; }
        Action<BoardState> onPrisonerBlocked { get; set; }
        Action onInitCube { get; set; }
        Action<BoardState> onMakeMovment { get; set; }

        bool ValidateChekerColor(int selectedChecker, BoardState boardState);
    }
}
