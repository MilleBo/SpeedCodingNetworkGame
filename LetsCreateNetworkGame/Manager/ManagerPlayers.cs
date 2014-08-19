using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using LetsCreateNetworkGame.Components;
using LetsCreateNetworkGame.Library;
using LetsCreateZelda.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace LetsCreateNetworkGame.Manager
{
    class ManagerPlayers
    {
        private List<BaseObject> _players;
        private ManagerNetwork _managerNetwork;
        private Texture2D _texture;
        private SpriteFont _font; 

        public ManagerPlayers(ManagerNetwork managerNetwork)
        {
            _players = new List<BaseObject>();
            _managerNetwork = managerNetwork; 
            managerNetwork.PlayerUpdateEvent += PlayerUpdate;
        }

        void PlayerUpdate(object sender, MyEventArgs.PlayerUpdateEventArgs e)
        {
            foreach (var player in e.Players)
            {
                var baseObject = _players.FirstOrDefault(b => b.Username == player.Username);
                if (baseObject != null)
                {
                    var sprite = baseObject.GetComponent<Sprite>(ComponentType.Sprite);
                    sprite.UpdatePosition(player.XPosition, player.YPosition);
                }
                else
                {
                    CreateObject(player); 
                }
            }
        }

        private void CreateObject(Player player)
        {
            var baseObject = new BaseObject {Username = player.Username}; 
            baseObject.AddComponent(new Sprite(_texture, 32,32, new Vector2(player.XPosition,player.YPosition)));
            baseObject.AddComponent(new Animation(16,16,2));
            if (player.Username == _managerNetwork.Username)
            {
                baseObject.AddComponent(new MainPlayer(_managerNetwork));
            }
            else
            {
                baseObject.AddComponent(new Name(_font));
            }
            _players.Add(baseObject);
        }

        public void LoadContent(ContentManager content)
        {
            _texture = content.Load<Texture2D>("Octorok");
            _font = content.Load<SpriteFont>("font");
        }

        public void Update(double gameTime)
        {
            foreach (var baseObject in _players)
            {
               baseObject.Update(gameTime);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var baseObject in _players)
            {
                baseObject.Draw(spriteBatch);
            }
        }
    }
}
