using ClassLibrary1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary1.Interfaces
{
    public interface IBoardState
    {
        Dictionary<int, int> BlackLocation { get; set; }
        Dictionary<int, int> WhiteLocation { get; set; }
        Cube Cube { get; set; }
        string CurrentPlayer { get; set; }
        string BlackPlayer { get; set; }
        string WhitePlayer { get; set; }
        int BarredBlackCheckers { get; set; }
        int MoveFrom { get; set; }
        int MoveTo { get; set; }
        int BarredWhiteCheckers { get; set; }
        bool IsDouble { get; set; }
        bool TurnChanged { get; set; }
        bool IsBarred { get; set; }
        int CountMovement { get; set; }
    }
}
