//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using LetsCreateNetworkGame.Library;
using Lidgren.Network;
using Microsoft.Xna.Framework.Input;

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
                        Data(inc); 
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

        private void Data(NetIncomingMessage inc)
        {
            var packetType = (PacketType) inc.ReadByte();

           
            switch (packetType)
            {
                case PacketType.Input:
                    Input(inc);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }


        }

        private void Input(NetIncomingMessage inc)
        {
            Console.WriteLine("Received new input");
            var key = (Keys)inc.ReadByte();
            var name = inc.ReadString();
            var player = _players.FirstOrDefault(p => p.Name == name);
            if (player == null)
            {
                Console.WriteLine("Could not find player with name {0}", name);
                return;
            }
 
            switch (key)
            {
                case Keys.Down:
                    player.YPosition++;
                    break;

                case Keys.Up:
                    player.YPosition--;
                    break;

                case Keys.Left:
                    player.XPosition--;
                    break;

                case Keys.Right:
                    player.XPosition++;
                    break;
            }

            SendPlayerPosition(player,inc); 
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
                outmsg.Write(_players.Count);
                for(int n = 0; n < _players.Count; n++)
                {   
                   outmsg.WriteAllProperties(_players[n]);
                }
                _server.SendMessage(outmsg, inc.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);          
                SendPlayerPosition(player,inc);
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

        private void SendPlayerPosition(Player player, NetIncomingMessage inc)
        {
            Console.WriteLine("Sending out new player position");
            var outmessage = _server.CreateMessage();
            outmessage.Write((byte)PacketType.PlayerPosition);
            outmessage.WriteAllProperties(player);
            _server.SendToAll(outmessage,NetDeliveryMethod.ReliableOrdered);
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
