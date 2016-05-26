//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - http://www.speedcoding.net
//------------------------------------------------------

namespace LetsCreateNetworkGame.OpenGL.Library
{
    public class Entity
    {
        public Position Position { get; set; }

        public Entity(Position position)
        {
            Position = position;
        }

        public Entity() { }

        public virtual void Update(double gameTime)
        {
            
        }
    }
}
