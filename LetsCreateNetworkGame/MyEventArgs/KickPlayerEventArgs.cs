using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LetsCreateNetworkGame.MyEventArgs
{
    class KickPlayerEventArgs : EventArgs
    {
        public string Username { get; set; }

        public KickPlayerEventArgs(string username)
        {
            Username = username; 
        }
    }
}
