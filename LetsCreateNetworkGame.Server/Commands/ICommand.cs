using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsCreateNetworkGame.Library;
using Lidgren.Network;

namespace LetsCreateNetworkGame.Server.Commands
{
    interface ICommand
    {
        void Run(NetServer server, NetIncomingMessage inc, Player player, List<Player> players);
    }
}
