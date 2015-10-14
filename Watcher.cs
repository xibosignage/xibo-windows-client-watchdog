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
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using XiboClientWatchdog.Properties;

namespace XiboClientWatchdog
{
    class Watcher
    {
        public static object _locker = new object();

        // Members to stop the thread
        private bool _forceStop = false;
        private ManualResetEvent _manualReset = new ManualResetEvent(false);

        /// <summary>
        /// Stops the thread
        /// </summary>
        public void Stop()
        {
            _forceStop = true;
            _manualReset.Set();
        }

        /// <summary>
        /// Runs the agent
        /// </summary>
        public void Run()
        {
            while (!_forceStop)
            {
                lock (_locker)
                {
                    try
                    {
                        // If we are restarting, reset
                        _manualReset.Reset();

                        string status = null;

                        // Look in the Xibo library for the status.json file
                        if (File.Exists(Path.Combine(Settings.Default.ClientLibrary, "status.json")))
                        {
                            using (StreamReader reader = new StreamReader(Path.Combine(Settings.Default.ClientLibrary, "status.json")))
                            {
                                status = reader.ReadToEnd();
                            }
                        }

                        // Compare the last accessed date with the current date and threshold
                        if (string.IsNullOrEmpty(status))
                            throw new Exception("Unable to find status file in " + Settings.Default.ClientLibrary);

                        // Load the status file in to a JSON string
                        var dict = new JavaScriptSerializer().Deserialize<Dictionary<string, object>>(status);

                        DateTime lastActive = DateTime.Parse(dict["lastActivity"].ToString());

                        // Set up the threshold
                        DateTime threshold = DateTime.Now.AddSeconds(Settings.Default.Threshold * -1.0);

                        if (lastActive < threshold)
                        {
                            // We need to do something about this - client hasn't checked in recently enough

                            // Check to see if XiboClient.exe is still running
                            Process[] proc = Process.GetProcessesByName(Path.GetFileNameWithoutExtension(Settings.Default.ProcessPath));

                            if (proc.Length > 0)
                            {
                                // Stop the exe's (kill them)
                                foreach (Process process in proc)
                                {
                                    process.Kill();
                                }
                            }

                            WriteToXiboLog(string.Format("Client inactive with {0} processes", proc.Length));

                            // Start the exe's
                            Process.Start(Settings.Default.ProcessPath);
                        }
                    }
                    catch (Exception e)
                    {
                        Trace.WriteLine(e.Message);
                    }

                    // Sleep this thread until the next collection interval
                    _manualReset.WaitOne((int)Settings.Default.PollingInterval * 1000);
                }
            }
        }

        private void WriteToXiboLog(string message)
        {
            // The log is contained in the library folder
            try
            {
                string _logPath = Path.Combine(Settings.Default.ClientLibrary, Settings.Default.LogFileName);

                // Open the Text Writer
                using (StreamWriter tw = new StreamWriter(File.Open(string.Format("{0}_{1}", _logPath, DateTime.Now.ToFileTimeUtc().ToString()), FileMode.Append, FileAccess.Write, FileShare.Read), Encoding.UTF8))
                {
                    tw.WriteLine(string.Format("<trace date=\"{0}\" category=\"{1}\">{2}</trace>", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "Watchdog", message));
                }
            }
            catch
            {
                // What can we do?
            }
        }
    }
}
