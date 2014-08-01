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
    class NetworkConnection
    {
        private NetClient _client; 

        public bool Start()
        {
            var loginInformation = new NetworkLoginInformation() {Name = "RandomName"}; 
            _client = new NetClient(new NetPeerConfiguration("networkGame"));
            _client.Start();
            var outmsg = _client.CreateMessage();
            outmsg.Write((byte)PacketType.Login);
            outmsg.WriteAllProperties(loginInformation);
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
                            var accepted = inc.ReadBoolean();
                            if (accepted)
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else
                        {
                            return false; 
                        }
                }
            }

            

        }
    }
}
