//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------
using LetsCreateNetworkGame.Library;
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
