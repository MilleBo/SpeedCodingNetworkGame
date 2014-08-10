using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsCreateNetworkGame.Library;
using LetsCreateNetworkGame.Server.Managers;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LetsCreateNetworkGame.Server.Commands
{
    class InputCommand : ICommand
    {
        public void Run(NetServer server, NetIncomingMessage inc, Player player, List<Player> players)
        {
            Console.WriteLine("Received new input");
            var key = (Keys)inc.ReadByte();
            var name = inc.ReadString();
            player = players.FirstOrDefault(p => p.Username == name);
            if (player == null)
            {
                Console.WriteLine("Could not find player with name {0}", name);
                return;
            }

            int x = 0;
            int y = 0; 

            switch (key)
            {
                case Keys.Down:
                    y++;
                    break;

                case Keys.Up:
                    y--;
                    break;

                case Keys.Left:
                    x--;
                    break;

                case Keys.Right:
                    x++;
                    break;
            }

            if (!ManagerCollision.CheckCollision(new Rectangle(player.XPosition + x, player.YPosition + y, 100, 50),
                player.Username, players))
            {
                player.XPosition += x;
                player.YPosition += y;


                var command = new PlayerPositionCommand();
                command.Run(server, inc, player, players);
            }
        }
    }
}
