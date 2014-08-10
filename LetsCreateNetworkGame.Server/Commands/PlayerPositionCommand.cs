using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsCreateNetworkGame.Library;
using Lidgren.Network;

namespace LetsCreateNetworkGame.Server.Commands
{
    class PlayerPositionCommand : ICommand
    {
        public void Run(NetServer server, NetIncomingMessage inc, Player player, List<Player> players)
        {
            if (player != null)
            {
                Console.WriteLine("Sending out new player position");
                var outmessage = server.CreateMessage();
                outmessage.Write((byte) PacketType.PlayerPosition);
                outmessage.WriteAllProperties(player);
                server.SendToAll(outmessage, NetDeliveryMethod.ReliableOrdered);
            }
        }
    }
}
