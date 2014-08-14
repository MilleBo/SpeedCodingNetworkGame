namespace LetsCreateNetworkGame.Server.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.grpServerOperations = new System.Windows.Forms.GroupBox();
            this.btnStopServer = new System.Windows.Forms.Button();
            this.btnStartServer = new System.Windows.Forms.Button();
            this.grpServerStatusLog = new System.Windows.Forms.GroupBox();
            this.dgwServerStatusLog = new System.Windows.Forms.DataGridView();
            this.clmId = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.clmMessage = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.grpServerOperations.SuspendLayout();
            this.grpServerStatusLog.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgwServerStatusLog)).BeginInit();
            this.SuspendLayout();
            // 
            // grpServerOperations
            // 
            this.grpServerOperations.Controls.Add(this.btnStopServer);
            this.grpServerOperations.Controls.Add(this.btnStartServer);
            this.grpServerOperations.Location = new System.Drawing.Point(12, 12);
            this.grpServerOperations.Name = "grpServerOperations";
            this.grpServerOperations.Size = new System.Drawing.Size(148, 82);
            this.grpServerOperations.TabIndex = 0;
            this.grpServerOperations.TabStop = false;
            this.grpServerOperations.Text = "Server Operations";
            // 
            // btnStopServer
            // 
            this.btnStopServer.Enabled = false;
            this.btnStopServer.Location = new System.Drawing.Point(6, 48);
            this.btnStopServer.Name = "btnStopServer";
            this.btnStopServer.Size = new System.Drawing.Size(135, 23);
            this.btnStopServer.TabIndex = 1;
            this.btnStopServer.Text = "Stop Server";
            this.btnStopServer.UseVisualStyleBackColor = true;
            this.btnStopServer.Click += new System.EventHandler(this.btnStopServer_Click);
            // 
            // btnStartServer
            // 
            this.btnStartServer.Location = new System.Drawing.Point(6, 19);
            this.btnStartServer.Name = "btnStartServer";
            this.btnStartServer.Size = new System.Drawing.Size(135, 23);
            this.btnStartServer.TabIndex = 0;
            this.btnStartServer.Text = "Start Server";
            this.btnStartServer.UseVisualStyleBackColor = true;
            this.btnStartServer.Click += new System.EventHandler(this.btnStartServer_Click);
            // 
            // grpServerStatusLog
            // 
            this.grpServerStatusLog.Controls.Add(this.dgwServerStatusLog);
            this.grpServerStatusLog.Location = new System.Drawing.Point(166, 12);
            this.grpServerStatusLog.Name = "grpServerStatusLog";
            this.grpServerStatusLog.Size = new System.Drawing.Size(490, 333);
            this.grpServerStatusLog.TabIndex = 1;
            this.grpServerStatusLog.TabStop = false;
            this.grpServerStatusLog.Text = "Server status log";
            // 
            // dgwServerStatusLog
            // 
            this.dgwServerStatusLog.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgwServerStatusLog.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.clmId,
            this.clmMessage});
            this.dgwServerStatusLog.Location = new System.Drawing.Point(6, 19);
            this.dgwServerStatusLog.Name = "dgwServerStatusLog";
            this.dgwServerStatusLog.Size = new System.Drawing.Size(478, 308);
            this.dgwServerStatusLog.TabIndex = 0;
            // 
            // clmId
            // 
            this.clmId.HeaderText = "Id";
            this.clmId.Name = "clmId";
            // 
            // clmMessage
            // 
            this.clmMessage.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.clmMessage.HeaderText = "Message";
            this.clmMessage.Name = "clmMessage";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(668, 357);
            this.Controls.Add(this.grpServerStatusLog);
            this.Controls.Add(this.grpServerOperations);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.grpServerOperations.ResumeLayout(false);
            this.grpServerStatusLog.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgwServerStatusLog)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox grpServerOperations;
        private System.Windows.Forms.Button btnStopServer;
        private System.Windows.Forms.Button btnStartServer;
        private System.Windows.Forms.GroupBox grpServerStatusLog;
        private System.Windows.Forms.DataGridView dgwServerStatusLog;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmId;
        private System.Windows.Forms.DataGridViewTextBoxColumn clmMessage;
    }
}