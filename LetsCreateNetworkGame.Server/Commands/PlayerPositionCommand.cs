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
    class PlayerPositionCommand : ICommand
    {
        public void Run(ManagerLogger managerLogger, Server server, NetIncomingMessage inc, PlayerAndConnection playerAndConnection, GameRoom gameRoom)
        {
            if (playerAndConnection != null)
            {
                managerLogger.AddLogMessage("server", "Sending out new player position to all in group " + gameRoom.GameRoomId);
                var outmessage = server.NetServer.CreateMessage();
                outmessage.Write((byte) PacketType.PlayerPosition);
                outmessage.Write(playerAndConnection.Player.Username);
                outmessage.WriteAllProperties(playerAndConnection.Player.Position);
                server.NetServer.SendMessage(outmessage, gameRoom.Players.Select(p => p.Connection).ToList(),
                    NetDeliveryMethod.ReliableOrdered, 0);
            }
        }
    }
}
