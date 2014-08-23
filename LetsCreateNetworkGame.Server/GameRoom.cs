//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------
using System.Collections.Generic;

namespace LetsCreateNetworkGame.Server
{
    class GameRoom
    {
        public string GameRoomId { get; set; }
        public List<PlayerAndConnection> Players { get; set; }

        public GameRoom(string gameRoomId)
        {
            GameRoomId = gameRoomId; 
            Players = new List<PlayerAndConnection>();
        }

        
    }
}
