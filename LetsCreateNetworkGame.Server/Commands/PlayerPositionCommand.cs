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
    class PlayerPositionCommand : ICommand
    {
        public void Run(ManagerLogger managerLogger, NetServer server, NetIncomingMessage inc, Player player, List<Player> players)
        {
            if (player != null)
            {
                managerLogger.AddLogMessage("server", "Sending out new player position");
                var outmessage = server.CreateMessage();
                outmessage.Write((byte) PacketType.PlayerPosition);
                outmessage.WriteAllProperties(player);
                server.SendToAll(outmessage, NetDeliveryMethod.ReliableOrdered);
            }
        }
    }
}
