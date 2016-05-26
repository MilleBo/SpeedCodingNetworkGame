//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - http://www.speedcoding.net
//------------------------------------------------------

namespace LetsCreateNetworkGame.OpenGL.Library.AI.Movement
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
