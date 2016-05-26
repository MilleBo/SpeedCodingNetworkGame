using System;
using LetsCreateNetworkGame.OpenGL.Manager;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace LetsCreateNetworkGame.OpenGL.Components
{
    class MainPlayer : Component
    {
        private ManagerNetwork ManagerNetwork { get; set; }

        public override ComponentType ComponentType
        {
            get { return ComponentType.MainPlayer; }
        }

        public MainPlayer(ManagerNetwork managerNetwork)
        {
            ManagerNetwork = managerNetwork;
            ManagerInput.FireNewInput += ManagerInput_FireNewInput;
        }

        void ManagerInput_FireNewInput(object sender, MyEventArgs.NewInputEventArgs e)
        {
            switch (e.Input)
            {
                case Input.Left:
                    ManagerNetwork.SendInput(Keys.Left);
                    break;
                case Input.Right:
                    ManagerNetwork.SendInput(Keys.Right);
                    break;
                case Input.Up:
                    ManagerNetwork.SendInput(Keys.Up);
                    break;
                case Input.Down:
                    ManagerNetwork.SendInput(Keys.Down);
                    break;
                case Input.None:
                    break;
                case Input.Enter:
                    break;
                case Input.A:
                    break;
                case Input.S:
                    break;
                case Input.Select:
                    break;
                case Input.Start:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void Update(double gameTime)
        {
           
        }

        public override void Draw(SpriteBatch spritebatch)
        {
           
        }
    }
}
