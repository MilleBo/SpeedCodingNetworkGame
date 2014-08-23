using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsCreateNetworkGame.Library;

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
