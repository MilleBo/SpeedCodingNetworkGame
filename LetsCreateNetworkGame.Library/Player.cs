//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------

using System.Xml;


namespace LetsCreateNetworkGame.Library
{
    public class Player : Entity
    {
        public string Username { get; set; }

        public Player(string username, int xPosition, int yPosition, int screenXPosition, int screenYPosition, bool visible)
            :base(xPosition, yPosition, screenXPosition, screenYPosition, visible)
        {
            Username = username;
        }

        public Player() { }

    }
}
