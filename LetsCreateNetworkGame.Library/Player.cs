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

        public Player(string username, Position position)
            :base(position)
        {
            Username = username;
        }

        public Player()
        {
            Position = new Position();
        }

    }
}
