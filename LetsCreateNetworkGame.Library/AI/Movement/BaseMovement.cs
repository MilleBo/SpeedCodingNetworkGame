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

namespace LetsCreateNetworkGame.Library.AI.Movement
{
    public abstract class BaseMovement
    {
        protected int Speed; 
        public Position Position { get; set; }

        protected BaseMovement(Position position)
        {
            Position = position;
            Speed = 1;
        }

        public abstract void Update(double gameTime);
    }
}
