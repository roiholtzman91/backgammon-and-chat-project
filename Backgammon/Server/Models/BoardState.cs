using ClassLibrary1.Interfaces;
using ClassLibrary1.Models;
using Server.BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.Models
{
    public class BoardState : IBoardState
    {
        
        ContexRepository _userManager = new ContexRepository();

        #region Properties
        public Dictionary<int, int> BlackLocation { get; set; }
        public Dictionary<int, int> WhiteLocation { get; set; }
        public Cube Cube { get; set; }
        public string CurrentPlayer { get; set; }
        public int BarredBlackCheckers { get; set; }
        public int BarredWhiteCheckers { get; set; }
        public bool IsDouble { get; set; }
        public string BlackPlayer { get; set; }
        public string WhitePlayer { get; set; }
        public int MoveFrom { get; set; }
        public int MoveTo { get; set; }
        public int LastMovement { get; set; }
        public int CountMovement { get; set; }
        public bool TurnChanged { get; set; }
        public bool IsBarred { get; set; }
        #endregion

        public BoardState(string whitePlayer, string blackPlayer)
        {
            BlackPlayer = blackPlayer;
            CurrentPlayer = WhitePlayer = whitePlayer;
            Cube = new Cube();

            BlackLocation = new Dictionary<int, int>()
            {
                {5,5},
                {7,3},
                {12,5},
                {23,2},
            };

            WhiteLocation = new Dictionary<int, int>()
            {
                {0,2},
                {11,5},
                {16,3},
                {18,5},
            };

        }

    }
}