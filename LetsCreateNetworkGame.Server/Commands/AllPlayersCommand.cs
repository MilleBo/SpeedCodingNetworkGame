//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - http://www.speedcoding.net
//------------------------------------------------------

using System.Linq;
using LetsCreateNetworkGame.OpenGL.Library;
using LetsCreateNetworkGame.Server.Managers;
using Lidgren.Network;

namespace LetsCreateNetworkGame.Server.Commands
{
    class AllPlayersCommand : ICommand
    {

        public bool CameraUpdate { get; set; }

        public void Run(ManagerLogger managerLogger, Server server, NetIncomingMessage inc, PlayerAndConnection playerAndConnection, GameRoom gameRoom)
        {
            managerLogger.AddLogMessage("server", "Sending full player list");
            var outmessage = server.NetServer.CreateMessage();
            outmessage.Write((byte)PacketType.AllPlayers);
            outmessage.Write(CameraUpdate);
            outmessage.Write(gameRoom.Players.Count);
            foreach (var p in gameRoom.Players)
            {
                outmessage.Write(p.Player.Username);
                outmessage.WriteAllProperties(p.Player.Position);
            }
            server.NetServer.SendMessage(outmessage, gameRoom.Players.Select(p => p.Connection).ToList(), NetDeliveryMethod.ReliableOrdered, 0);
        }
    }
}
