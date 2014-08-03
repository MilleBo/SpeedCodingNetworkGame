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
        public string Name { get; set; }
        public int XPosition { get; set; }
        public int YPosition { get; set; }

        public Player(string name, int xPosition, int yPosition)
        {
            Name = name;
            XPosition = xPosition;
            YPosition = yPosition;
        }

        public Player() { }
    }
}
