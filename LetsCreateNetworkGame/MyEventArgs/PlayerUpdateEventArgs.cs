using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LetsCreateNetworkGame.Library;

namespace LetsCreateNetworkGame.MyEventArgs
{
    class PlayerUpdateEventArgs : EventArgs
    {
        public List<Player> Players { get; set; }

        public PlayerUpdateEventArgs(List<Player> players)
        {
            Players = players; 
        }
    }
}
