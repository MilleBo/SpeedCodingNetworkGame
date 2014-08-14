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
    class AllPlayersCommand : ICommand
    {
        public void Run(ManagerLogger managerLogger, NetServer server, NetIncomingMessage inc, Player player, List<Player> players)
        {
            managerLogger.AddLogMessage("server", "Sending full player list");
            var outmessage = server.CreateMessage();
            outmessage.Write((byte)PacketType.AllPlayers);
            outmessage.Write(players.Count);
            foreach (var p in players)
            {
                outmessage.WriteAllProperties(p);
            }
            server.SendToAll(outmessage, NetDeliveryMethod.ReliableOrdered);
        }
    }
}
