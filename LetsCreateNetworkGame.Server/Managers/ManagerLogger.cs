//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------
using System;
using System.Collections.Generic;
using LetsCreateNetworkGame.Server.MyEventArgs;
using LetsCreateNetworkGame.Server.Util;

namespace LetsCreateNetworkGame.Server.Managers
{
    class ManagerLogger
    {
        private List<LogMessage> _logMessages;
        public event EventHandler<LogMessageEventArgs> NewLogMessageEvent;

        public ManagerLogger()
        {
            _logMessages = new List<LogMessage>();
        }

        public void AddLogMessage(LogMessage logMessage)
        {
            _logMessages.Add(logMessage);

            if (NewLogMessageEvent != null)
            {
                NewLogMessageEvent(this,new LogMessageEventArgs(logMessage));
            }
        }

        public void AddLogMessage(string id, string message)
        {
           AddLogMessage(new LogMessage {Id = id, Message = message});
        }
    }
}
