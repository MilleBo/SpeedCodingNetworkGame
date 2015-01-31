//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using LetsCreateNetworkGame.Library;
using LetsCreateNetworkGame.Server.Managers;
using Lidgren.Network;

namespace LetsCreateNetworkGame.Server.Commands
{
    class PlayerPositionCommand : ICommand
    {
        public void Run(ManagerLogger managerLogger, Server server, NetIncomingMessage inc, PlayerAndConnection playerAndConnection, GameRoom gameRoom)
        {
            if (playerAndConnection != null)
            {
                managerLogger.AddLogMessage("server", "Sending out new player position to all in group " + gameRoom.GameRoomId);
                var outmessage = server.NetServer.CreateMessage();
                outmessage.Write((byte) PacketType.PlayerPosition);
                outmessage.Write(playerAndConnection.Player.Username);
                outmessage.Write(playerAndConnection.Player.XPosition);
                outmessage.Write(playerAndConnection.Player.YPosition);
                outmessage.Write(playerAndConnection.Player.ScreenXPosition);
                outmessage.Write(playerAndConnection.Player.ScreenYPosition);
                outmessage.Write(playerAndConnection.Player.Visible);
                server.NetServer.SendMessage(outmessage, gameRoom.Players.Select(p => p.Connection).ToList(),
                    NetDeliveryMethod.ReliableOrdered, 0);
            }
        }
    }
}
