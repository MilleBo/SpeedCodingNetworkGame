//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - http://www.speedcoding.net
//------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using LetsCreateNetworkGame.OpenGL.Library;
using LetsCreateNetworkGame.OpenGL.Library.AI.Movement;
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
            var lastIterationTime = DateTime.Now;
            var stepSize = TimeSpan.FromSeconds(0.01); 
            while (true)
            {
                if (_cancellationTokenSource.IsCancellationRequested)
                {
                    break;
                }

                while (lastIterationTime + stepSize < DateTime.Now)
                {
                    switch (_roomState)
                    {
                        case RoomState.WaitForPlayer:
                            WaitForPlayer(stepSize.Milliseconds);
                            break;
                        case RoomState.Run:
                            Run(stepSize.Milliseconds);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    lastIterationTime += stepSize; 
                }
            }
        }

        private void Run(double gameTime)
        {
            //ManagerCamera.Move(Direction.Up); //For test
            ManagerCamera.Update(gameTime);

            var list = new List<Entity>(); 
            list.AddRange(Players.Select(p => p.Player));
            list.AddRange(Enemies);
            foreach (var entity in list)
            {
                entity.Update(gameTime);


                var entityPosition = entity.Position;
                var position = new Vector2(entityPosition.XPosition, entityPosition.YPosition);
                if (ManagerCamera.InScreenCheck(position))
                {
                    var screenPosition =
                        ManagerCamera.WorldToScreenPosition(new Vector2(position.X, position.Y));

                    entityPosition.ScreenXPosition = (int)screenPosition.X;
                    entityPosition.ScreenYPosition = (int)screenPosition.Y; 
                }
            }

            var command = new AllPlayersCommand {CameraUpdate = true};
            command.Run(_logger, _server, null, null, this);
            var commandE = new AllEnemiesCommand { CameraUpdate = true };
            commandE.Run(_logger, _server, null, null, this);
        }

        private void WaitForPlayer(double gameTime)
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
            var enemy = new Enemy(0, new Position(random.Next(0, 600), random.Next(0, 400), 0, 0, true));
            enemy.BaseMovement = new RandomMovement(enemy.Position); //new AttackMovement(enemy.Position, Players.Select(p => p.Player).ToList());
            Enemies.Add(enemy);
            _logger.AddLogMessage("Room - " + GameRoomId, 
                string.Format("Adding new enemy with Unique ID {0}", Enemies.Last().UniqueId));
        }

        public void CancelRoom()
        {
            _cancellationTokenSource.Cancel();
        }
    }
}
