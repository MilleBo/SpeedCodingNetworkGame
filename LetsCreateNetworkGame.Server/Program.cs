//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Windows.Forms;
using LetsCreateNetworkGame.Library;
using LetsCreateNetworkGame.Server.Forms;
using Lidgren.Network;

namespace LetsCreateNetworkGame.Server
{
    static class Program
    {

        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            Application.Run(new MainForm());
        }
    }
}
