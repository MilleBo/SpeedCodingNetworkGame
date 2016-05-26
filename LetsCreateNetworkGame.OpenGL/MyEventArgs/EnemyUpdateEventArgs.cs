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
    class EnemyUpdateEventArgs : EventArgs
    {
        public List<Enemy> Enemies { get; set; }
        public bool CameraUpdate { get; set; }

        public EnemyUpdateEventArgs(List<Enemy> enemies, bool cameraUpdate)
        {
            Enemies = enemies;
            CameraUpdate = cameraUpdate;
        }
        
    }
}
