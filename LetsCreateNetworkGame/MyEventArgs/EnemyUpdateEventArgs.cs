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
using LetsCreateNetworkGame.Library;

namespace LetsCreateNetworkGame.MyEventArgs
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
