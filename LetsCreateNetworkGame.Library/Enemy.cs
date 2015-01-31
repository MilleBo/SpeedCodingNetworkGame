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

namespace LetsCreateNetworkGame.Library
{
    public class Enemy : Entity
    {
        public int EnemyId { get; set; }

        public int UniqueId { get; set; }

        public Enemy(int enemyId, int xPosition, int yPosition, int screenXPosition, int screenYPosition, bool visible)
            :base(xPosition, yPosition, screenXPosition, screenYPosition, visible)
        {
            EnemyId = enemyId;
            UniqueId = new Random().Next(0, 90000);
        }

        public Enemy() { }
    }
}
