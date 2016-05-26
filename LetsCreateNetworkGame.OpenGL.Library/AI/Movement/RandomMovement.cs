//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - http://www.speedcoding.net
//------------------------------------------------------

using System;

namespace LetsCreateNetworkGame.OpenGL.Library.AI.Movement
{
    public class RandomMovement : BaseMovement
    {

        private double _frequency;
        private double _count;
        private Direction _direction;
        private Random _rnd;

        public RandomMovement(Position position) : base(position)
        {
            _frequency = 200;
            _count = 0;
            _rnd = new Random();
            _direction = (Direction) _rnd.Next(0, 3);
            Speed = 1; 
        }

        public override void Update(double gameTime)
        {
            _count += gameTime;
            if (_count > _frequency)
            {
                _direction = (Direction) _rnd.Next(0, 3);
                _count = 0; 
            }

            switch (_direction)
            {
                case Direction.Left:
                    Position.XPosition -= Speed; 
                    break;
                case Direction.Right:
                    Position.XPosition += Speed; 
                    break;
                case Direction.Up:
                    Position.YPosition -= Speed;
                    break;
                case Direction.Down:
                    Position.YPosition += Speed; 
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
