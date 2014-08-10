//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Configuration;
using LetsCreateNetworkGame.Library;
using LetsCreateNetworkGame.Server.Commands;
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
                    case NetIncomingMessageType.ConnectionApproval:
                        var login = new LoginCommand();
                        login.Run(_server,inc, null,_players);
                        break;
                    case NetIncomingMessageType.Data:
                        Data(inc); 
                        break;
                }
            }
        }

        private void Data(NetIncomingMessage inc)
        {
            var packetType = (PacketType) inc.ReadByte();
            var command = PacketFactory.GetCommand(packetType); 
            command.Run(_server,inc, null,_players);
        }
    }
}
