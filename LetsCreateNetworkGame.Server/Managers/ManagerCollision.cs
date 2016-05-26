//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - http://www.speedcoding.net
//------------------------------------------------------
using System.Collections.Generic;
using LetsCreateNetworkGame.OpenGL.Library;
using Microsoft.Xna.Framework;

namespace LetsCreateNetworkGame.Server.Managers
{
    class ManagerCollision
    {
        public static bool CheckCollision(Rectangle rec, string username, List<Player> players)
        {
            foreach (var player in players)
            {
                if (player.Username != username)
                {
                    var playerRec = new Rectangle(player.Position.XPosition, player.Position.YPosition, 100, 50);
                    if (playerRec.Intersects(rec))
                    {
                        return true; 
                    }
                }
            }
            return false; 
        }
    }
}
