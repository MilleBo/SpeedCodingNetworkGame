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
using LetsCreateNetworkGame.Server.Managers;
using LetsCreateNetworkGame.Server.MyEventArgs;
using Lidgren.Network;
using Microsoft.Xna.Framework.Input;

namespace LetsCreateNetworkGame.Server
{
    class Server
    {
        public event EventHandler<NewPlayerEventArgs> NewPlayerEvent;
        private readonly ManagerLogger _managerLogger;
        private List<PlayerAndConnection> _players;
        private NetPeerConfiguration _config;
        public NetServer NetServer { get; private set; }

        public Server(ManagerLogger managerLogger)
        {
            _managerLogger = managerLogger;
            _players = new List<PlayerAndConnection>();
            _config = new NetPeerConfiguration("networkGame") { Port = 14241 };
            _config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            NetServer = new NetServer(_config);      
        }

        public void Run()
        {
            NetServer.Start();
            Console.WriteLine("Server started...");
            _managerLogger.AddLogMessage("Server","Server started...");
            while (true)
            {
                NetIncomingMessage inc;
                if ((inc = NetServer.ReadMessage()) == null) continue;
                switch (inc.MessageType)
                {
                    case NetIncomingMessageType.ConnectionApproval:
                        var login = new LoginCommand();
                        login.Run(_managerLogger, this, inc, null,_players);
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
            command.Run(_managerLogger, this,inc, null,_players);
        }


        public void SendNewPlayerEvent(string username)
        {
            if(NewPlayerEvent != null)
                NewPlayerEvent(this,new NewPlayerEventArgs(username));
        }

        public void KickPlayer(int playerIndex)
        {
            var command = PacketFactory.GetCommand(PacketType.Kick);
            command.Run(_managerLogger,this, null, _players[playerIndex],_players);
        }
    }
}
