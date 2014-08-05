//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------
using System;
using System.Collections.Generic;
using LetsCreateNetworkGame.Library;
using Lidgren.Network;

namespace LetsCreateNetworkGame.Server
{
    class Server
    {
        private List<Player> _players;
        private NetPeerConfiguration _config;
        private NetServer _server; 

        public Server()
        {
            _players = new List<Player>();
            _config = new NetPeerConfiguration("networkGame") { Port = 14241 };
            _config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            _server = new NetServer(_config);
        }

        public void Run()
        {
            _server.Start();
            Console.WriteLine("Server started...");
            while (true)
            {
                NetIncomingMessage inc;
                if ((inc = _server.ReadMessage()) == null) continue;
                switch (inc.MessageType)
                {
                    case NetIncomingMessageType.Error:
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        break;
                    case NetIncomingMessageType.UnconnectedData:
                        break;
                    case NetIncomingMessageType.ConnectionApproval:
                        ConnectionApproval(inc);
                        break;
                    case NetIncomingMessageType.Data:

                        break;
                    case NetIncomingMessageType.Receipt:
                        break;
                    case NetIncomingMessageType.DiscoveryRequest:
                        break;
                    case NetIncomingMessageType.DiscoveryResponse:
                        break;
                    case NetIncomingMessageType.VerboseDebugMessage:
                        break;
                    case NetIncomingMessageType.DebugMessage:
                        break;
                    case NetIncomingMessageType.WarningMessage:
                        break;
                    case NetIncomingMessageType.ErrorMessage:
                        break;
                    case NetIncomingMessageType.NatIntroductionSuccess:
                        break;
                    case NetIncomingMessageType.ConnectionLatencyUpdated:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void ConnectionApproval(NetIncomingMessage inc)
        {
            Console.WriteLine("New connection...");
            var data = inc.ReadByte();
            if (data == (byte)PacketType.Login)
            {
                Console.WriteLine("..connection accpeted.");
                var player = CreatePlayer(inc);
                inc.SenderConnection.Approve();
                var outmsg = _server.CreateMessage();
                outmsg.Write((byte)PacketType.Login);
                outmsg.Write(true);
                outmsg.Write((int) player.XPosition);
                outmsg.Write((int) player.YPosition);
                outmsg.Write(_players.Count-1);
                foreach (var player1 in _players)
                {   
                    if(player.Name != player1.Name)
                        outmsg.WriteAllProperties(player1);
                }
                _server.SendMessage(outmsg, inc.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);          
                SendNewPlayer(player,inc);
            }
            else
            {
                inc.SenderConnection.Deny("Didn't send correct information.");
            }
        }

        private Player CreatePlayer(NetIncomingMessage inc)
        {
            var random = new Random();
            var player = new Player
                         {
                             Name = inc.ReadString(),
                             XPosition = random.Next(0, 750),
                             YPosition = random.Next(0, 420)
                         };
            _players.Add(player);
            return player;
        }

        private void SendNewPlayer(Player player, NetIncomingMessage inc)
        {
            Console.WriteLine("Sending out new player position");
            var outmessage = _server.CreateMessage();
            outmessage.Write((byte)PacketType.NewPlayer);
            outmessage.WriteAllProperties(player);
            _server.SendToAll(outmessage,inc.SenderConnection,NetDeliveryMethod.ReliableOrdered,0);
        }

        private void SendFullPlayerList()
        {
            Console.WriteLine("Sending full player list");
            var outmessage = _server.CreateMessage(); 
            outmessage.Write((byte)PacketType.AllPlayers);
            outmessage.Write(_players.Count);
            foreach (var player in _players)
            {
                outmessage.WriteAllProperties(player);
            }
            _server.SendToAll(outmessage, NetDeliveryMethod.ReliableOrdered);
        }
    }
}
