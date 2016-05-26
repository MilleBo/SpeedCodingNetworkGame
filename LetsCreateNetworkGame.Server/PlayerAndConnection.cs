//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - http://www.speedcoding.net
//------------------------------------------------------

using LetsCreateNetworkGame.OpenGL.Library;
using Lidgren.Network;

namespace LetsCreateNetworkGame.Server
{
    class PlayerAndConnection
    {
        public Player Player { get; set; }
        public NetConnection Connection { get; set; }

        public PlayerAndConnection(Player player, NetConnection connection)
        {
            Player = player;
            Connection = connection;
        }
    }
}
