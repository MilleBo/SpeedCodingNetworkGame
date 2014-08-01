//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------
using System;
using LetsCreateNetworkGame.Library;
using Lidgren.Network;

namespace LetsCreateNetworkGame.Server
{
    class Program
    {

        static void Main(string[] args)
        {
            var config = new NetPeerConfiguration("networkGame") { Port = 14241 };
            config.EnableMessageType(NetIncomingMessageType.ConnectionApproval);
            var server = new NetServer(config);
            server.Start();
            Console.WriteLine("Server started...");
            while (true)
            {
                NetIncomingMessage inc; 
                if((inc = server.ReadMessage()) == null) continue;
                switch (inc.MessageType)
                {
                    case NetIncomingMessageType.Error:
                        break;
                    case NetIncomingMessageType.StatusChanged:
                        break;
                    case NetIncomingMessageType.UnconnectedData:
                        break;
                    case NetIncomingMessageType.ConnectionApproval:
                        Console.WriteLine("New connection...");
                        var data = inc.ReadByte();
                        if (data == (byte) PacketType.Login)
                        {
                            Console.WriteLine("..connection accpeted.");
                            var loginInformation = new NetworkLoginInformation();
                            inc.ReadAllProperties(loginInformation);
                            inc.SenderConnection.Approve();

                            var outmsg = server.CreateMessage();
                            outmsg.Write((byte)PacketType.Login);
                            outmsg.Write(true);
                            server.SendMessage(outmsg, inc.SenderConnection, NetDeliveryMethod.ReliableOrdered, 0);
                        }
                        else
                        {
                            inc.SenderConnection.Deny("Didn't send correct information.");
                        }
                        break;
                    case NetIncomingMessageType.Data:
                        break;
                    case NetIncomingMessageType.Receipt:
                        break;
                    case NetIncomingMessageType.DiscoveryRequest:
                        break;
                    case NetIncomingMessageType.DiscoveryResponse:
                        break;
                    case NetIncomingMessageType.VerboseDebugMessage:
                        break;
                    case NetIncomingMessageType.DebugMessage:
                        break;
                    case NetIncomingMessageType.WarningMessage:
                        break;
                    case NetIncomingMessageType.ErrorMessage:
                        break;
                    case NetIncomingMessageType.NatIntroductionSuccess:
                        break;
                    case NetIncomingMessageType.ConnectionLatencyUpdated:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }
    }
}
