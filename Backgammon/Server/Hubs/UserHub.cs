using ClassLibrary1.Models;
using Common.Interfaces;
using Microsoft.AspNet.SignalR;
using Server.BL;
using Server.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Server.Hubs
{
    public class UserHub : Hub
    {
        ContexRepository manager = new ContexRepository();
        GameContext Game = new GameContext();
        static Dictionary<string, ServerUser> OnLineUsers = new Dictionary<string, ServerUser>();

        #region LogIn functions

        public bool Register(ServerUser user)
        {
            if (manager.Register(user))
            {
                OnLineUsers.Add(Context.ConnectionId, user);
                Clients.All.UpdateUsersList(OnLineUsers.Values.Select(u => u.UserName).ToArray());
                return true;
            }
            return false;
        }

        public bool LogIn(ServerUser user)
        {
            if (manager.IsUserExist(user))
            {
                if (!OnLineUsers.Any(p => p.Value.UserName == user.UserName && p.Value.Password == user.Password))
                {
                    OnLineUsers.Add(Context.ConnectionId, user);
                    Clients.All.UpdateUsersList(OnLineUsers.Values.Select(u => u.UserName).ToArray());
                    return true;
                }
                Clients.All.UpdateUsersList(OnLineUsers.Values.Select(u => u.UserName).ToArray());
                return false;
            }
            else return false;
        }

        public bool LogOff()
        {
            OnLineUsers.Remove(OnLineUsers.First(user => user.Key == Context.ConnectionId).Key);
            Clients.All.UpdateUsersList(OnLineUsers.Values.Select(u => u.UserName).ToArray());
            return true;
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            OnLineUsers.Remove(OnLineUsers.First(user => user.Key == Context.ConnectionId).Key);
            Clients.All.UpdateUsersList(OnLineUsers.Values.Select(u => u.UserName).ToArray());
            return base.OnDisconnected(stopCalled);
        }
        #endregion

        #region ChatFunctions

        public bool BroadcastTextMessage(string ToClient, string message)
        {
            KeyValuePair<string, ServerUser> toSessionId = OnLineUsers.FirstOrDefault(user => user.Value.UserName == ToClient);
            string fromclient = OnLineUsers[Context.ConnectionId].UserName;
            if (toSessionId.Key != null)
            {
                Clients.Client(toSessionId.Key).broadcastMessage(fromclient, message);
                return true;
            }
            return false;
        }

        public bool BroadcastTextMessageToAll(string message)
        {
            string fromclient = OnLineUsers[Context.ConnectionId].UserName;
            Clients.All.broadcastMessageToAll(fromclient, message);
            return true;
        }

        public void GameRequest(string toclint)
        {
            KeyValuePair<string, ServerUser> toSessionId = OnLineUsers.FirstOrDefault(user => user.Value.UserName == toclint);
            string fromclient = OnLineUsers[Context.ConnectionId].UserName;
            Clients.Client(toSessionId.Key).SendGameRequest(fromclient);
        }

        public void SendGameRequestAnswer(string toCleint,string sender)
        {
            KeyValuePair<string, ServerUser> toSessionId = OnLineUsers.FirstOrDefault(user => user.Value.UserName == toCleint);
            toSessionId.Value.IsGame = true;
            KeyValuePair<string, ServerUser> toSessionId1 = OnLineUsers.FirstOrDefault(user => user.Value.UserName == sender);
            toSessionId1.Value.IsGame = true;
            Clients.Client(toSessionId.Key).GameRequestAnswer();
        }

        #endregion

        #region GameLogicFunction

        public ServerUser GetClient(string name)
        {
            return OnLineUsers.FirstOrDefault(n => n.Value.UserName == name).Value;
        }
        public BoardState InitBoard(string firstPlayer, string secondPlayer)
        {
            KeyValuePair<string, ServerUser> toSessionId = OnLineUsers.FirstOrDefault
                                                             (user => user.Value.UserName == secondPlayer);
            Clients.Client(toSessionId.Key).InitCube();
            return Game.InitBoard(firstPlayer, secondPlayer);
        }

        public BoardState RollCube(BoardState board)
        {
            var b = Game.RollCube(board);
            if (board.CurrentPlayer == board.WhitePlayer)
            {
                KeyValuePair<string, ServerUser> toSessionId = OnLineUsers.FirstOrDefault
                                                              (user => user.Value.UserName == board.BlackPlayer);
                Clients.Client(toSessionId.Key).UpdateCube(b.Cube);
            }
            else
            {
                KeyValuePair<string, ServerUser> toSessionId = OnLineUsers.FirstOrDefault
                                                                 (user => user.Value.UserName == board.WhitePlayer);

                Clients.Client(toSessionId.Key).UpdateCube(b.Cube);
            }
            return b;
        }

        public bool MakeMove(BoardState board, int selectedChecker, int selectedPlace)
        {
            if (Game.MakeMove(board, selectedChecker, selectedPlace))
            {
                KeyValuePair<string, ServerUser> blackToSessionId = OnLineUsers.FirstOrDefault
                                                              (user => user.Value.UserName == board.BlackPlayer);
                Clients.Client(blackToSessionId.Key).getBoardState(Game.GetBoard());

                KeyValuePair<string, ServerUser> whiteToSessionId1 = OnLineUsers.FirstOrDefault
                                                                 (user => user.Value.UserName == board.WhitePlayer);
                Clients.Client(whiteToSessionId1.Key).getBoardState(Game.GetBoard());
                if ((Game.GetBoard().IsDouble && Game.GetBoard().CountMovement == 4)
                    || (!Game.GetBoard().IsDouble && Game.GetBoard().CountMovement == 2))
                {
                    if (board.CurrentPlayer == board.WhitePlayer)
                        Clients.Client(blackToSessionId.Key).Movment(Game.IfTurnChange(Game.GetBoard()));
                    else
                        Clients.Client(whiteToSessionId1.Key).Movment(Game.IfTurnChange(Game.GetBoard()));
                }
                return true;
            }
            return false;
        }

        public void PrisonerCanFree(BoardState board)
        {
            if (!Game.PrisonerCanEscape(board))
            {
                KeyValuePair<string, ServerUser> toSessionId = OnLineUsers.FirstOrDefault
                                                              (user => user.Value.UserName == board.CurrentPlayer);
                Clients.Client(toSessionId.Key).prisonerBlocked(board);
            }
        }

        #endregion
    }
}