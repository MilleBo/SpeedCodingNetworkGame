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
        public void Run(ManagerLogger managerLogger, NetServer server, NetIncomingMessage inc, Player player, List<Player> players)
        {
            managerLogger.AddLogMessage("server", "New connection...");
            var data = inc.ReadByte();
            if (data == (byte)PacketType.Login)
            {
                managerLogger.AddLogMessage("server", "..connection accpeted.");
                player = CreatePlayer(inc,players);
                inc.SenderConnection.Approve();
                var outmsg = server.CreateMessage();
                outmsg.Write((byte)PacketType.Login);
                outmsg.Write(true);
                outmsg.Write(players.Count);
                for (int n = 0; n < players.Count; n++)
                {
                    outmsg.WriteAllProperties(players[n]);
                }
                server.SendMessage(outmsg, inc.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
                var command = new PlayerPositionCommand();
                command.Run(managerLogger, server,inc,player,players);
            }
            else
            {
                inc.SenderConnection.Deny("Didn't send correct information.");
            }
        }

        private Player CreatePlayer(NetIncomingMessage inc, List<Player> players)
        {
            var random = new Random();
            var player = new Player
            {
                Username = inc.ReadString(),
                XPosition = random.Next(0, 750),
                YPosition = random.Next(0, 420)
            };
            players.Add(player);
            return player;
        }

    }
}
