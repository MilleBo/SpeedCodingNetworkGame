using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsCreateNetworkGame.Library;
using Lidgren.Network;

namespace LetsCreateNetworkGame.Server.Commands
{
    class AllPlayersCommand : ICommand
    {
        public void Run(NetServer server, NetIncomingMessage inc, Player player, List<Player> players)
        {
            Console.WriteLine("Sending full player list");
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
