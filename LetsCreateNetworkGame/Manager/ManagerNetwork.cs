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
using Lidgren.Network;

namespace LetsCreateNetworkGame
{
    class ManagerNetwork
    {
        private NetClient _client;
        public Player Player { get; set; }
        public List<Player> OtherPlayers { get; set; } 

        public bool Active { get; set; }

        public ManagerNetwork()
        {
            OtherPlayers = new List<Player>();
        }

        public bool Start()
        {
            var random = new Random(); 
            _client = new NetClient(new NetPeerConfiguration("networkGame"));
            _client.Start();
            Player = new Player("name_" + random.Next(0, 100),0,0);
            var outmsg = _client.CreateMessage();
            outmsg.Write((byte)PacketType.Login);
            outmsg.Write(Player.Name);
            _client.Connect("localhost", 14241, outmsg);
            return EsablishInfo(); 
        }

        private bool EsablishInfo()
        {
            var time = DateTime.Now;
            NetIncomingMessage inc;
            while (true)
            {
                if (DateTime.Now.Subtract(time).Seconds > 5)
                {
                    return false; 
                }

                if((inc = _client.ReadMessage()) == null) continue;

                switch (inc.MessageType)
                {
                    case NetIncomingMessageType.Data:
                        var data = inc.ReadByte();
                        if (data == (byte) PacketType.Login)
                        {
                            Active = inc.ReadBoolean();
                            if (Active)
                            {
                                Player.XPosition = inc.ReadInt32();
                                Player.YPosition = inc.ReadInt32();
                                ReceiveAllPlayers(inc);
                                return true;
                            }

                            return false;
                        }
                        return false;
                }
            }
        }

        public void Update()
        {
            NetIncomingMessage inc;
            while ((inc = _client.ReadMessage()) != null)
            {
                if(inc.MessageType != NetIncomingMessageType.Data) continue;
                var packageType = (PacketType)inc.ReadByte();
                switch (packageType)
                {
                    case PacketType.NewPlayer:
                        var player = new Player();
                        inc.ReadAllProperties(player); 
                        OtherPlayers.Add(player);
                        break;

                    case PacketType.AllPlayers:
                        ReceiveAllPlayers(inc);
                        break; 
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private void ReceiveAllPlayers(NetIncomingMessage inc)
        {
            var count = inc.ReadInt32();
            for (int n = 0; n < count; n++)
            {
                var player = new Player(); 
                inc.ReadAllProperties(player);
                if (player.Name == Player.Name)
                    continue;
                if (OtherPlayers.Any(p => p.Name == player.Name))
                {
                    var oldPlayer = OtherPlayers.FirstOrDefault(p => p.Name == player.Name);
                    oldPlayer.XPosition = player.XPosition;
                    oldPlayer.YPosition = player.YPosition;
                }
                else
                {
                    OtherPlayers.Add(player);
                }
            }
        }

    }
}
