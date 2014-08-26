//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using LetsCreateNetworkGame.Server.Commands;
using LetsCreateNetworkGame.Server.Managers;
using Microsoft.Xna.Framework;

namespace LetsCreateNetworkGame.Server
{
    class GameRoom
    {
        private enum RoomState
        {
            WaitForPlayer,
            Run
        };

        private RoomState _roomState; 
        private Task _task;
        private CancellationTokenSource _cancellationTokenSource;
        private ManagerLogger _logger;
        private Server _server; 
        public string GameRoomId { get; set; }
        public List<PlayerAndConnection> Players { get; set; }

        public ManagerCamera ManagerCamera { get; private set; }

        public GameRoom(string gameRoomId, Server server,  ManagerLogger logger)
        {
            GameRoomId = gameRoomId;
            _server = server; 
            Players = new List<PlayerAndConnection>();
            _cancellationTokenSource = new CancellationTokenSource();
            _task = new Task(Update,_cancellationTokenSource.Token);
            _task.Start();
            _logger = logger; 
            ManagerCamera = new ManagerCamera();
        }

        private void Update()
        {
            while (true)
            {
                if (_cancellationTokenSource.IsCancellationRequested)
                {
                    break;
                }

                switch (_roomState)
                {
                    case RoomState.WaitForPlayer:
                        WaitForPlayer(); 
                        break;
                    case RoomState.Run:
                        Run();
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                Thread.Sleep(200);
            }
        }

        private void Run()
        {
            ManagerCamera.Move(Direction.Up); //For test
            ManagerCamera.Update(0);

            foreach (var playerAndConnection in Players)
            {
                var player = playerAndConnection.Player;
                var playerPosition = new Vector2(player.XPosition, player.YPosition);
                if (ManagerCamera.InScreenCheck(playerPosition))
                {
                    var screenPosition =
                        ManagerCamera.WorldToScreenPosition(new Vector2(playerPosition.X, playerPosition.Y));

                    player.ScreenXPosition = (int) screenPosition.X;
                    player.ScreenYPosition = (int) screenPosition.Y; 
                }
            }

            var command = new AllPlayersCommand {CameraUpdate = true};
            command.Run(_logger, _server, null, null, this);
        }

        private void WaitForPlayer()
        {
            if (Players.Count > 0)
            {
                _logger.AddLogMessage("Room - " + GameRoomId, "Got enough players, start run camera.");
                _roomState = RoomState.Run;
            }
        }

        public void CancelRoom()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
