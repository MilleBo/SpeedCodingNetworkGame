//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using LetsCreateNetworkGame.Library;
using LetsCreateNetworkGame.Server.Managers;
using Lidgren.Network;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace LetsCreateNetworkGame.Server.Commands
{
    class InputCommand : ICommand
    {
        public void Run(ManagerLogger managerLogger, Server server, NetIncomingMessage inc, PlayerAndConnection playerAndConnection, GameRoom gameRoom)
        {
            managerLogger.AddLogMessage("server", "Received new input");
            var key = (Keys)inc.ReadByte();
            var name = inc.ReadString();
            playerAndConnection = gameRoom.Players.FirstOrDefault(p => p.Player.Username == name);
            if (playerAndConnection == null)
            {
                managerLogger.AddLogMessage("server", string.Format("Could not find player with name {0}", name));
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

            var player = playerAndConnection.Player; 
            if (!ManagerCollision.CheckCollision(new Rectangle(player.XPosition + x, player.YPosition + y, 100, 50),
                player.Username, gameRoom.Players.Select(p => p.Player).ToList()))
            {
                player.XPosition += x;
                player.YPosition += y;

                player.Visible = gameRoom.ManagerCamera.InScreenCheck(new Vector2(player.XPosition, player.YPosition));
                if (player.Visible)
                {
                    var screenPosition =
                        gameRoom.ManagerCamera.WorldToScreenPosition(new Vector2(player.XPosition, player.YPosition));
                    player.ScreenXPosition = (int) screenPosition.X;
                    player.ScreenYPosition = (int) screenPosition.Y; 
                }

                var command = new PlayerPositionCommand();
                command.Run(managerLogger, server, inc, playerAndConnection, gameRoom);
            }
        }
    }
}
