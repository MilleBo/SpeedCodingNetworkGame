//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - http://www.speedcoding.net
//------------------------------------------------------

namespace LetsCreateNetworkGame.OpenGL.Library
{
    public class Position
    {
        public int XPosition { get; set; }
        public int YPosition { get; set; }
        public int ScreenXPosition { get; set; }
        public int ScreenYPosition { get; set; }
        public bool Visible { get; set; }

        public Position(int xPosition, int yPosition, int screenXPosition, int screenYPosition, bool visible)
        {
            XPosition = xPosition;
            YPosition = yPosition;
            ScreenXPosition = screenXPosition;
            ScreenYPosition = screenYPosition;
            Visible = visible;
        }

        public Position()
        {
            
        }
    }
}
