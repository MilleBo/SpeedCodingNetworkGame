//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetsCreateNetworkGame.Server.MyEventArgs
{
    class NewPlayerEventArgs : EventArgs
    {
        public string Username { get; set; }

        public NewPlayerEventArgs(string username)
        {
            Username = username;
        }
    }
}
