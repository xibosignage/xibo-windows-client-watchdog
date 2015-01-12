/*
 * Xibo - Digitial Signage - http://www.xibo.org.uk
 * Copyright (C) 2006 - 2014 Daniel Garner
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace XiboClientWatchdog
{
    public partial class Tray : Form
    {
        private Watcher _watcher;
        private Thread _watchThread;

        public Tray()
        {
            InitializeComponent();

            Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
            
            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
        
            // Start a thread for the watcher
            _watcher = new Watcher();
            _watchThread = new Thread(new ThreadStart(_watcher.Run));
            _watchThread.Start();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _watcher.Stop();
            Application.Exit();
        }
    }
}
