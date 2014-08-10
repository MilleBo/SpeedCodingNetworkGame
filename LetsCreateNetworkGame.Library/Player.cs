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

        public Player(string username, int xPosition, int yPosition)
        {
            Username = username;
            XPosition = xPosition;
            YPosition = yPosition;
        }

        public Player() { }

    }
}
