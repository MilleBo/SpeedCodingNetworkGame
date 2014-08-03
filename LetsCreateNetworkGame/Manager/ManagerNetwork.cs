//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------
using System;
using LetsCreateNetworkGame.Library;
using Lidgren.Network;

namespace LetsCreateNetworkGame
{
    class ManagerNetwork
    {
        private NetClient _client;
        public Player Player { get; set; }

        public bool Active { get; set; }

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
                                return true;
                            }

                            return false;
                        }
                        return false;
                }
            }

            

        }
    }
}
