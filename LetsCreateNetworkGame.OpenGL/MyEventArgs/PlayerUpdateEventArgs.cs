//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - http://www.speedcoding.net
//------------------------------------------------------

using System;
using System.Collections.Generic;
using LetsCreateNetworkGame.OpenGL.Library;

namespace LetsCreateNetworkGame.OpenGL.MyEventArgs
{
    class PlayerUpdateEventArgs : EventArgs
    {
        public List<Player> Players { get; set; }
        public bool CameraUpdate { get; set; }

        public PlayerUpdateEventArgs(List<Player> players, bool cameraUpdate)
        {
            Players = players;
            CameraUpdate = cameraUpdate;
        }
    }
}
