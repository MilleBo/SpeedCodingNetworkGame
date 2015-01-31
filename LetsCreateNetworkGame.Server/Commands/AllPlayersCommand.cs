﻿//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LetsCreateNetworkGame.Library;
using LetsCreateNetworkGame.Server.Managers;
using Lidgren.Network;

namespace LetsCreateNetworkGame.Server.Commands
{
    class AllPlayersCommand : ICommand
    {

        public bool CameraUpdate { get; set; }

        public void Run(ManagerLogger managerLogger, Server server, NetIncomingMessage inc, PlayerAndConnection playerAndConnection, GameRoom gameRoom)
        {
            managerLogger.AddLogMessage("server", "Sending full player list");
            var outmessage = server.NetServer.CreateMessage();
            outmessage.Write((byte)PacketType.AllPlayers);
            outmessage.Write(CameraUpdate);
            outmessage.Write(gameRoom.Players.Count);
            foreach (var p in gameRoom.Players)
            {
                outmessage.Write(p.Player.Username);
                outmessage.Write(p.Player.XPosition);
                outmessage.Write(p.Player.YPosition);
                outmessage.Write(p.Player.ScreenXPosition);
                outmessage.Write(p.Player.ScreenYPosition);
                outmessage.Write(p.Player.Visible);
            }
            server.NetServer.SendMessage(outmessage, gameRoom.Players.Select(p => p.Connection).ToList(), NetDeliveryMethod.ReliableOrdered, 0);
        }
    }
}
