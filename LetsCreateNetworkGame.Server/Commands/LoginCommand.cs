//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------
using System;
using System.Collections.Generic;
using LetsCreateNetworkGame.Library;
using LetsCreateNetworkGame.Server.Managers;
using Lidgren.Network;

namespace LetsCreateNetworkGame.Server.Commands
{
    class LoginCommand : ICommand
    {
        public void Run(ManagerLogger managerLogger, Server server, NetIncomingMessage inc, PlayerAndConnection playerAndConnection, GameRoom gameRoom)
        {
            managerLogger.AddLogMessage("server", "New connection...");
            var data = inc.ReadByte();
            if (data == (byte)PacketType.Login)
            {
                managerLogger.AddLogMessage("server", "..connection accpeted.");
                playerAndConnection = CreatePlayer(inc, gameRoom.Players);
                inc.SenderConnection.Approve();
                var outmsg = server.NetServer.CreateMessage();
                outmsg.Write((byte)PacketType.Login);
                outmsg.Write(true);
                outmsg.Write(gameRoom.Players.Count);
                for (int n = 0; n < gameRoom.Players.Count; n++)
                {
                    outmsg.WriteAllProperties(gameRoom.Players[n].Player);
                }
                server.NetServer.SendMessage(outmsg, inc.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
                var command = new PlayerPositionCommand();
                command.Run(managerLogger, server,inc,playerAndConnection,gameRoom);
                server.SendNewPlayerEvent(playerAndConnection.Player.Username, gameRoom.GameRoomId);
            }
            else
            {
                inc.SenderConnection.Deny("Didn't send correct information.");
            }
        }

        private PlayerAndConnection CreatePlayer(NetIncomingMessage inc, List<PlayerAndConnection> players)
        {
            var random = new Random();
            var player = new Player
            {
                Username = inc.ReadString(),
                XPosition = random.Next(0, 750),
                YPosition = random.Next(0, 420)
            };
            var playerAndConnection = new PlayerAndConnection(player, inc.SenderConnection);
            players.Add(playerAndConnection);
            return playerAndConnection;
        }

    }
}
