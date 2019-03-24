using ClassLibrary1.Interfaces;
using ClassLibrary1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backgammon.Models
{
    public class BoardState : IBoardState
    {
        public Dictionary<int, int> BlackLocation { get; set; }
        public Dictionary<int, int> WhiteLocation { get; set; }
        public Cube Cube { get; set; }
        public string CurrentPlayer { get; set; }
        public string BlackPlayer { get; set; }
        public string WhitePlayer { get; set; }
        public int BarredBlackCheckers { get; set; }
        public int MoveFrom { get; set; }
        public int MoveTo { get; set; }
        public int BarredWhiteCheckers { get; set; }
        public bool IsDouble { get; set; }
        public bool TurnChanged { get; set; }
        public bool IsBarred { get; set; }
        public int CountMovement { get; set; }
    }
}
