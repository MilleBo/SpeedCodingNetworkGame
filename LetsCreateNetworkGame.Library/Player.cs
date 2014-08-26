//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------

using System.Xml;


namespace LetsCreateNetworkGame.Library
{
    public class Player
    {
        public string Username { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }

        public int ScreenXPosition { get; set; }

        public int ScreenYPosition { get; set; }

        public bool Visible { get; set; }
        public Player(string username, int xPosition, int yPosition, int screenXPosition, int screenYPosition, bool visible)
        {
            Username = username;
            XPosition = xPosition;
            YPosition = yPosition;
            ScreenXPosition = screenXPosition;
            ScreenYPosition = screenYPosition;
            Visible = visible; 
        }

        public Player() { }

    }
}
