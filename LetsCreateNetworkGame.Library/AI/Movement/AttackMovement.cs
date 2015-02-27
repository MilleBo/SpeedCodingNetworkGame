using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LetsCreateNetworkGame.Library.Utils;

namespace LetsCreateNetworkGame.Library.AI.Movement
{
    public class AttackMovement : BaseMovement
    {
        private readonly List<Player> _players;
        private Player _targetPlayer;
        private double _frequency;
        private double _counter; 

        public AttackMovement(Position position, List<Player> players) : base(position)
        {
            _players = players;
            _frequency = 1000;
            _counter = _frequency + 1; 
        }

        public override void Update(double gameTime)
        {
            _counter += gameTime;
            if (_counter > _frequency)
            {
                _counter = 0;
                double distance = int.MaxValue;
                foreach (var player in _players)
                {
                    var tempDistance = GeneralFunctions.Distance(Position, player.Position);
                    if (tempDistance < distance)
                    {
                        distance = tempDistance;
                        _targetPlayer = player;
                    }
                }
            }
            else
            {
                if (_targetPlayer.Position.XPosition > Position.XPosition)
                {
                    Position.XPosition += Speed; 
                }
                else if (_targetPlayer.Position.XPosition < Position.XPosition)
                {
                    Position.XPosition -= Speed;
                }
                if (_targetPlayer.Position.YPosition > Position.YPosition)
                {
                    Position.YPosition += Speed; 
                }
                else if (_targetPlayer.Position.YPosition < Position.YPosition)
                {
                    Position.YPosition -= Speed; 
                }
            }
        }
    }
}
