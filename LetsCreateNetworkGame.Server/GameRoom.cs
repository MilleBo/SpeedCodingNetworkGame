//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LetsCreateNetworkGame.Library;
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
        public List<Enemy> Enemies { get; set; } 

        public ManagerCamera ManagerCamera { get; private set; }

        public GameRoom(string gameRoomId, Server server,  ManagerLogger logger)
        {
            GameRoomId = gameRoomId;
            _server = server; 
            Players = new List<PlayerAndConnection>();
            Enemies = new List<Enemy>();
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
            //ManagerCamera.Move(Direction.Up); //For test
            ManagerCamera.Update(0);

            var list = new List<Entity>(); 
            list.AddRange(Players.Select(p => p.Player));
            list.AddRange(Enemies);
            foreach (var entity in list)
            {
                var position = new Vector2(entity.XPosition, entity.YPosition);
                if (ManagerCamera.InScreenCheck(position))
                {
                    var screenPosition =
                        ManagerCamera.WorldToScreenPosition(new Vector2(position.X, position.Y));

                    entity.ScreenXPosition = (int) screenPosition.X;
                    entity.ScreenYPosition = (int) screenPosition.Y; 
                }
            }

            var command = new AllPlayersCommand {CameraUpdate = true};
            command.Run(_logger, _server, null, null, this);
            var commandE = new AllEnemiesCommand { CameraUpdate = true };
            commandE.Run(_logger, _server, null, null, this);
        }

        private void WaitForPlayer()
        {
            if (Players.Count > 0)
            {
                _logger.AddLogMessage("Room - " + GameRoomId, "Got enough players, start run camera.");
                _roomState = RoomState.Run;
            }
        }

        public void AddEnemy()
        {
            var random = new Random();
            //Generate enemies for test
            Enemies.Add(new Enemy(0, random.Next(0, 600), random.Next(0, 400), 0, 0, true));
            _logger.AddLogMessage("Room - " + GameRoomId, 
                string.Format("Adding new enemy with Unique ID {0}", Enemies.Last().UniqueId));
        }

        public void CancelRoom()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
