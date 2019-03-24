using ClassLibrary1.Interfaces;
using ClassLibrary1.Models;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Server.BL
{
    public class GameContext
    {
        Random rnd = new Random();
        BoardState tmpBoard;

        public BoardState InitBoard(string firstPlayer, string secondPlayer)
        {
            BoardState board = new BoardState(firstPlayer, secondPlayer);
            return board;
        }

        public BoardState RollCube(BoardState board)
        {
            tmpBoard = board;
            if (board != null)
            {
                tmpBoard.Cube.Cube1 = rnd.Next(1, 7);
                tmpBoard.Cube.Cube2 = rnd.Next(1, 7);
                if (tmpBoard.Cube.Cube1 == tmpBoard.Cube.Cube2) tmpBoard.IsDouble = true;
            }
            return tmpBoard;
        }

        public BoardState GetBoard()
        {
            return tmpBoard;
        }

        internal bool MakeMove(BoardState board, int selectedChecker, int selectedPlace)
        {
            tmpBoard = board;
            tmpBoard.IsBarred = false;
            tmpBoard.MoveFrom = selectedChecker;
            tmpBoard.MoveTo = selectedPlace;
            bool isBlack = tmpBoard.CurrentPlayer == tmpBoard.BlackPlayer;

            if (isBlack)
            {
                if (tmpBoard.BarredBlackCheckers != 0 && tmpBoard.MoveFrom != 24) return false; //security check

                if (tmpBoard.WhiteLocation.ContainsKey(selectedPlace))
                {
                    if (tmpBoard.WhiteLocation[selectedPlace] == 1)
                    {
                        SendCheckerToJail(tmpBoard, isBlack);
                    }
                    else return false;
                }
            }
            else
            {
                if (tmpBoard.BarredWhiteCheckers != 0 && tmpBoard.MoveFrom != -1) return false;

                if (tmpBoard.BlackLocation.ContainsKey(selectedPlace))
                {
                    if (tmpBoard.BlackLocation[selectedPlace] == 1)
                    {
                        SendCheckerToJail(tmpBoard, isBlack);
                    }
                    else return false;
                }
            }

            if (CheckCubeMovement(tmpBoard))
            {
                tmpBoard.CountMovement++;
                AddCheker(selectedPlace, isBlack ? tmpBoard.BlackLocation : tmpBoard.WhiteLocation);
                if (isBlack && tmpBoard.BarredBlackCheckers != 0)
                {
                    tmpBoard.BarredBlackCheckers--;
                }
                else if (!isBlack && tmpBoard.BarredWhiteCheckers != 0)
                {
                    tmpBoard.BarredWhiteCheckers--;
                }
                else
                {
                    RemoveChecker(selectedChecker, isBlack ? tmpBoard.BlackLocation : tmpBoard.WhiteLocation);
                }
            }
            else return false;
            return true;
        }

        public BoardState IfTurnChange(BoardState board)
        {
            ChangeTurn(board);
            return board;
        }

        internal bool PrisonerCanEscape(BoardState boardcurrent)
        {
            BoardState board = boardcurrent;
            bool isBlack = board.CurrentPlayer == board.BlackPlayer;

            if (isBlack)
            {
                if (board.LastMovement == 0)
                {
                    if (!board.WhiteLocation.ContainsKey(24 - board.Cube.Cube1)
                        || board.WhiteLocation[24 - board.Cube.Cube1] == 1)
                    {
                        return true;
                    }
                    else if (!board.WhiteLocation.ContainsKey(24 - board.Cube.Cube2)
                            || board.WhiteLocation[24 - board.Cube.Cube2] == 1)
                    {
                        return true;
                    }

                    ChangeTurn(board);
                    return false;
                }
                else
                {
                    if (board.Cube.Cube1 == board.LastMovement)
                    {
                        if (!board.WhiteLocation.ContainsKey(24 - board.Cube.Cube2)
                            || board.WhiteLocation[24 - board.Cube.Cube2] == 1)
                        {
                            return true;
                        }
                    }
                    else
                    {
                        if (!board.WhiteLocation.ContainsKey(24 - board.Cube.Cube1)
                           || board.WhiteLocation[24 - board.Cube.Cube1] == 1)
                        {
                            return true;
                        }
                    }

                    ChangeTurn(board);
                    return false;
                }
            }
            else
            {
                if (board.LastMovement == 0)
                {
                    if (!board.BlackLocation.ContainsKey(-1 + board.Cube.Cube1)
                        || board.BlackLocation[-1 + board.Cube.Cube1] == 1)
                    {
                        return true;
                    }
                    else if (!board.BlackLocation.ContainsKey(24 - board.Cube.Cube2)
                            || board.BlackLocation[-1 + board.Cube.Cube2] == 1)
                    {
                        return true;
                    }

                    ChangeTurn(board);
                    return false;
                }
                else
                {
                    if (board.Cube.Cube1 == board.LastMovement)
                    {
                        if (!board.BlackLocation.ContainsKey(-1 + board.Cube.Cube2)
                            || board.BlackLocation[-1 + board.Cube.Cube2] == 1)
                            return true;
                    }
                    else
                    {
                        if (!board.BlackLocation.ContainsKey(-1 + board.Cube.Cube1)
                           || board.BlackLocation[-1 + board.Cube.Cube1] == 1)
                            return true;
                    }

                    ChangeTurn(board);
                    return false;
                }
            }
        }

        private bool CheckCubeMovement(BoardState tmpBoard)
        {
            int thisMovement;

            if (tmpBoard.CurrentPlayer == tmpBoard.BlackPlayer)
                thisMovement = tmpBoard.MoveFrom - tmpBoard.MoveTo;
            else
                thisMovement = tmpBoard.MoveTo - tmpBoard.MoveFrom;

            if (tmpBoard.LastMovement == 0)
            {
                tmpBoard.TurnChanged = false;
                if (tmpBoard.Cube.Cube1 == thisMovement || tmpBoard.Cube.Cube2 == thisMovement)
                {
                    tmpBoard.LastMovement = thisMovement;
                    return true;
                }
                return false;
            }
            else
            {
                if (tmpBoard.Cube.Cube1 == tmpBoard.LastMovement)
                {
                    return tmpBoard.Cube.Cube2 == thisMovement;
                }
                else
                {
                    return tmpBoard.Cube.Cube1 == thisMovement;
                }
            }
        }

        private void SendCheckerToJail(BoardState tmpBoard, bool isBlack)
        {
            if (isBlack)
            {
                RemoveChecker(tmpBoard.MoveTo, tmpBoard.WhiteLocation);
                tmpBoard.BarredWhiteCheckers++;
            }
            else
            {
                RemoveChecker(tmpBoard.MoveTo, tmpBoard.BlackLocation);
                tmpBoard.BarredBlackCheckers++; //<----check this place
            }
            tmpBoard.IsBarred = true;

        }

        private void ChangeTurn(BoardState tmpBoard)
        {
            tmpBoard.LastMovement = 0;
            tmpBoard.CountMovement = 0;
            tmpBoard.IsDouble = false;
            tmpBoard.TurnChanged = true;

            if (tmpBoard.CurrentPlayer == tmpBoard.BlackPlayer)
                tmpBoard.CurrentPlayer = tmpBoard.WhitePlayer;
            else
                tmpBoard.CurrentPlayer = tmpBoard.BlackPlayer;
        }

        private static void AddCheker(int selectedPlace, Dictionary<int, int> Location)
        {
            if (Location.ContainsKey(selectedPlace))
            {
                Location[selectedPlace]++;
            }
            else
            {
                Location.Add(selectedPlace, 1);
            }
        }

        private void RemoveChecker(int selectedChecker, Dictionary<int, int> playerLocation)
        {
            if (playerLocation.ContainsKey(selectedChecker))
            {
                if (playerLocation[selectedChecker] == 1)
                {
                    playerLocation.Remove(selectedChecker);
                }
                else
                {
                    playerLocation[selectedChecker]--;
                }
            }

        }

    }
}