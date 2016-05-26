//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - http://www.speedcoding.net
//------------------------------------------------------
using System;

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
