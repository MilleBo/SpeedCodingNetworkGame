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
        private List<GameRoom> _gameRooms;  
        private NetPeerConfiguration _config;
        public NetServer NetServer { get; private set; }

        public Server(ManagerLogger managerLogger)
        {
            _managerLogger = managerLogger;
            _gameRooms = new List<GameRoom>();
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
                        var gameRoom = GetGameRoomById(inc.ReadString());
                        login.Run(_managerLogger, this, inc, null, gameRoom);
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
            var gameRoom = GetGameRoomById(inc.ReadString());
            var command = PacketFactory.GetCommand(packetType); 
            command.Run(_managerLogger, this,inc, null, gameRoom);
        }


        public void SendNewPlayerEvent(string username, string gameGroupId)
        {
            if(NewPlayerEvent != null)
                NewPlayerEvent(this,new NewPlayerEventArgs(string.Format("{0}[{1}]",username, gameGroupId)));
        }

        public void KickPlayer(string username, string gameGroupId)
        {
            var command = PacketFactory.GetCommand(PacketType.Kick);
            var gameGroup = GetGameRoomById(gameGroupId);
            command.Run(_managerLogger,this, null, gameGroup.Players.FirstOrDefault(p => p.Player.Username == username),gameGroup);
        }

        private GameRoom GetGameRoomById(string id)
        {
            var gameRoom = _gameRooms.FirstOrDefault(g => g.GameRoomId == id);
            if (gameRoom == null)
            {
                gameRoom = new GameRoom(id, this, _managerLogger);
                _gameRooms.Add(gameRoom);
            }
            return gameRoom; 
        }

    }
}
