﻿/*
 * Xibo - Digitial Signage - http://xibo.org.uk
 * Copyright (C) 2006 - 2021 Xibo Signage Ltd
 *
 * This file is part of Xibo.
 *
 * Xibo is free software: you can redistribute it and/or modify
 * it under the terms of the GNU Affero General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * any later version. 
 *
 * Xibo is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU Affero General Public License for more details.
 *
 * You should have received a copy of the GNU Affero General Public License
 * along with Xibo.  If not, see <http://www.gnu.org/licenses/>.
 */
using Nini.Config;
using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;
using XiboClientWatchdog.Properties;

namespace XiboClientWatchdog
{
    public partial class Tray : Form
    {
        public delegate void StatusDelegate(string status);

        private Watcher _watcher;
        private Thread _watchThread;

        private NotifyIcon notifyIcon;

        public Tray()
        {
            InitializeComponent();

            // Create a notify icon
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIcon.ContextMenuStrip = this.notifyContextMenu;
            this.notifyIcon.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            this.notifyIcon.Text = "Watch Dog";
            this.notifyIcon.Visible = true;

            FormClosing += Tray_FormClosing;

            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
            MinimizeBox = false;
            MaximizeBox = false;

            // Process any command line parameters
            ArgvConfigSource source = new ArgvConfigSource(Environment.GetCommandLineArgs());
            source.AddSwitch("Main", "watch-process", "p");
            source.AddSwitch("Main", "library", "l");
                    
            // Create a watcher
            _watcher = new Watcher(source);
            _watcher.OnNotifyActivity += _watcher_OnNotifyActivity;
            _watcher.OnNotifyRestart += _watcher_OnNotifyRestart;
            _watcher.OnNotifyError += _watcher_OnNotifyError;

            // Start a thread for the watcher
            _watchThread = new Thread(new ThreadStart(_watcher.Run));
            _watchThread.Start();

            // Watch params
            libraryLabel.Text = source.Configs["Main"].GetString("library", Settings.Default.ClientLibrary);
            processLabel.Text = source.Configs["Main"].GetString("watch-process", Settings.Default.ProcessPath);
        }

        void _watcher_OnNotifyError(string message)
        {
            if (InvokeRequired)
                BeginInvoke(new StatusDelegate(setErrorText), message);
            else
                setErrorText(message);
        }

        void _watcher_OnNotifyRestart(string message)
        {
            if (InvokeRequired)
                BeginInvoke(new StatusDelegate(setRestartText), message);
            else
                setRestartText(message);
        }

        void _watcher_OnNotifyActivity()
        {
            if (InvokeRequired)
                BeginInvoke(new StatusDelegate(setLastActivityText), "");
            else
                setLastActivityText("");
        }

        void setLastActivityText(string message)
        {
            lastAccessedLabel.Text = "Checked: " + DateTime.Now.ToString();
        }

        void setRestartText(string message)
        {
            string formattedMessage = "Restarted: " + DateTime.Now.ToString() + " " + message;

            showBalloon("Restarting", formattedMessage);
            
            // Also store on the tool strip
            lastRestartLabel.Text = formattedMessage;
        }

        void setErrorText(string message)
        {
            showBalloon("Error", message);

            // Also store on the tool strip
            errorTextBox.Text = message;
        }

        void showBalloon(string title, string message)
        {
            if (Settings.Default.IsShowBalloons)
            {
                notifyIcon.BalloonTipTitle = title;
                notifyIcon.BalloonTipText = message;
                notifyIcon.Visible = true;
                notifyIcon.ShowBalloonTip(3000);
            }
        }

        void Tray_FormClosing(object sender, FormClosingEventArgs e)
        {
            _watcher.Stop();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _watcher.Stop();
            Application.Exit();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Normal;
            TopMost = true;
        }

        private void closeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
            TopMost = false;
        }
    }
}
