//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
// Youtube channel - https://www.youtube.com/user/Maloooon
//------------------------------------------------------
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using LetsCreateNetworkGame.Server.Managers;
using LetsCreateNetworkGame.Server.MyEventArgs;

namespace LetsCreateNetworkGame.Server.Forms
{
    public partial class MainForm : Form
    {
        private Task _task;
        private Server _server;
        private ManagerLogger _managerLogger;
        private CancellationTokenSource _cancellationTokenSource; 

        public MainForm()
        {

            _managerLogger = new ManagerLogger();
            _managerLogger.NewLogMessageEvent += NewLogMessageEvent;
            _server = new Server(_managerLogger);
            _server.NewPlayerEvent += NewPlayerEvent;
            InitializeComponent();
        }

        void NewPlayerEvent(object sender, NewPlayerEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<NewPlayerEventArgs>(NewPlayerEvent), sender, e);
                return; 
            }

            lstPlayers.Items.Add(e.Username); 
        }

        void NewLogMessageEvent(object sender, LogMessageEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke(new EventHandler<LogMessageEventArgs>(NewLogMessageEvent), sender, e);
                return; 
            }
            dgwServerStatusLog.Rows.Add(new[] {e.LogMessage.Id, e.LogMessage.Message}); 
        }

        private void btnStartServer_Click(object sender, EventArgs e)
        {
            btnStartServer.Enabled = false;
            btnStopServer.Enabled = true; 

            _cancellationTokenSource = new CancellationTokenSource();
            _task = new Task(_server.Run, _cancellationTokenSource.Token);
            _task.Start();
        }

        private void btnStopServer_Click(object sender, EventArgs e)
        {
            if (_task != null && _cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
                btnStartServer.Enabled = true;
                btnStopServer.Enabled = false; 
            }
        }

        #region Context Menu Players 

        private void cmnPlayersKick_Click(object sender, EventArgs e)
        {
            if (lstPlayers.SelectedIndex == -1)
            {
                MessageBox.Show("Select a player first");
                return; 
            }

            var usernameAndGroup = lstPlayers.Items[lstPlayers.SelectedIndex].ToString().Split(new[] {'[', ']'});
            _server.KickPlayer(usernameAndGroup[0],usernameAndGroup[1]); 
            lstPlayers.Items.RemoveAt(lstPlayers.SelectedIndex);

        }

        #endregion 

        private void btnAddEnemy_Click(object sender, EventArgs e)
        {
            _server.AddEnemy(); 
        }


    }
}
