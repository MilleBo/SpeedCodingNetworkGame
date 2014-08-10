using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LetsCreateNetworkGame.Library;
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
                    var playerRec = new Rectangle(player.XPosition, player.YPosition, 100, 50);
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
