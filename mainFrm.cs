using System.Net.NetworkInformation;
using System.Text.RegularExpressions;
using Windows.Networking.Connectivity;

namespace NetworkStatusMonitor
{
    public partial class MainFrm : Form
    {
        private enum StatusTypes : byte
        {
            Unknown,
            Connected,
            Disconnected
        }

        private TimeSpan _MonitorTime = new TimeSpan(); // How long the user has been monitoring for.
        private TimeSpan _DurationTime = new TimeSpan(); // Current time of when the current status was changed.
        private bool _IsMonitoring = false;
        private StatusTypes _CurrentStatus = StatusTypes.Unknown;

        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            RefreshAdapters();
        }

        private void RefreshAdapters()
        {
            LogList.Items.Clear();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adapter in nics)
            {
                AdapterBx.Items.Add(adapter.Description);
            }
        }
        
        private void LogStatus(StatusTypes newStatus)
        {
            _CurrentStatus = newStatus;
            bool foundNic = false;
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.Description == AdapterBx.Text)
                {
                    string connectionType = adapter.NetworkInterfaceType.ToString();
                    string currentStatus = "(null)";

                    if (LogList.Items.Count > 0)
                    {
                        Int32 pendingIndex = (LogList.Items.Count - 1);

                        if (!(_CurrentStatus != StatusTypes.Unknown) && LogList.Items[pendingIndex].SubItems[3].Text.Contains("[Pending]"))
                        {
                            LogList.Items[pendingIndex].SubItems[3].Text = string.Format("{0:00}:{1:00}:{2:00}", _DurationTime.Hours, _DurationTime.Minutes, _DurationTime.Seconds);
                        }
                    }

                    currentStatus = ((_CurrentStatus == StatusTypes.Disconnected) ? "Disconnected/Offline" : "Connected/Online");
                    Regex wirelessEx = new Regex("(Wireless)");
                    Regex ethernetEx = new Regex("(Ethernet)");
                    Match wirelessMatch = wirelessEx.Match(connectionType);
                    Match ethernetMatch = ethernetEx.Match(connectionType);

                    // This just "pretty prints" the connection type, as it "NetworkInterfaceType" doesn't always return a "simple" answer.
                    // If no match is found, it will default to the provided "non pretty" "NetworkInterfaceType".

                    if (wirelessMatch.Success)
                    {
                        connectionType = "WiFi/Wireless";
                    }
                    else if (ethernetMatch.Success)
                    {
                        connectionType = "Ethernet/Wired";
                    }

                    if (LogList.Items.Count > 0)
                    {
                        // If the interface was lost, we need to manually switch the connection type if it's changed.
                        // If we don't do this and the user reconnects to a different interface, from lets say ethernet to wifi, it will report the wrong "disconnection type".

                        if ((_CurrentStatus != StatusTypes.Unknown) && (currentStatus == "Disconnected/Offline"))
                        {
                            if (LogList.Items[LogList.Items.Count - 1].Text != connectionType)
                            {
                                if (connectionType == "WiFi/Wireless")
                                {
                                    connectionType = "Ethernet/Wired";
                                }
                                else
                                {
                                    connectionType = "WiFi/Wireless";
                                }
                            }
                        }
                    }

                    // Adds the new log to "logList".

                    ListViewItem item = new ListViewItem();
                    item.Text = connectionType;
                    item.SubItems.Add(currentStatus);
                    item.SubItems.Add(DateTime.Now.ToString("MM/dd/yyyy h:mm tt"));
                    item.SubItems.Add("[Pending]");
                    LogList.Items.Add(item);
                    foundNic = true;
                    return;
                }
            }

            // If for some reason the adapter was removed or not found, cancel monitoring and refresh the adapter list.

            if (!foundNic)
            {
                MonitorBtn.PerformClick();
                RefreshAdapters();
                MessageBox.Show("Error: Could not find the selected network interface, automatically refreshing list.", "NetworkStatusMonitor.NET", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Finds the selected adapter and displays its "OperationalStatus".
        private void StatusBtn_Click(object sender, EventArgs e)
        {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();

            foreach (NetworkInterface adapter in nics)
            {
                if (adapter.Description == AdapterBx.Text)
                {
                    MessageBox.Show("The selected network adapters status is: \"" + adapter.OperationalStatus.ToString() + "\"", "NetworkStatusMonitor.NET", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void MonitorBtn_Click(object sender, EventArgs e)
        {
            if (_IsMonitoring)
            {
                this.Text = "NetworkStatusMonitor.NET";
                MonitorBtn.Text = "Start monitoring";
                StatusTmr.Stop();
                DurationTmr.Stop();

                _CurrentStatus = StatusTypes.Unknown;
                _IsMonitoring = !_IsMonitoring;
                AdapterBx.Enabled = !_IsMonitoring;
                StatusBtn.Enabled = !_IsMonitoring;

                _MonitorTime = new TimeSpan();
                _DurationTime = new TimeSpan();
            }
            else if (!String.IsNullOrEmpty(AdapterBx.Text))
            {
                this.Text = "NetworkStatusMonitor.NET";
                MonitorBtn.Text = "Stop monitoring";

                _CurrentStatus = StatusTypes.Unknown;
                _IsMonitoring = !_IsMonitoring;
                AdapterBx.Enabled = !_IsMonitoring;
                StatusBtn.Enabled = !_IsMonitoring;

                StatusTmr.Start();
                DurationTmr.Start();
            }
            else
            {
                MessageBox.Show("Warning: Please select the current network adapter you're using to get accurate results.", "NetworkStatusMonitor.NET", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            if (LogList.Items.Count > 0)
            {
                string saveData = "Type, Status, Time, Duration\n";

                foreach (ListViewItem logItem in LogList.Items)
                {
                    string connectionType = logItem.Text;
                    string status = logItem.SubItems[1].Text;
                    string time = logItem.SubItems[2].Text;
                    string duration = logItem.SubItems[3].Text;
                    saveData += (connectionType + ", " + status + ", " + time + ", " + duration + "\n");
                }

                // Formats the file name for windows.

                string fileName = ("LOG " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt"));
                fileName = fileName.Replace("/", "");
                fileName = fileName.Replace(":", "");
                fileName = fileName.Replace(" ", "_");

                // Promps the folder picker dialog.

                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                DialogResult folderResult = folderBrowser.ShowDialog();

                // Makes sure the selected path is valid before creating a file and writing to it.

                if ((folderResult == DialogResult.OK) && (Directory.Exists(folderBrowser.SelectedPath)))
                {
                    string logPath = (folderBrowser.SelectedPath + "\\" + fileName + ".txt");

                    using (StreamWriter sw = File.CreateText(logPath))
                    {
                        sw.WriteLine(saveData);
                    }

                    MessageBox.Show("Sucessfully saved log to: \n" + logPath, "Network Status Monitor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void ClearBtn_Click(object sender, EventArgs e)
        {
            LogList.Items.Clear();
        }

        private void StatusTmr_Tick(object sender, EventArgs e)
        {
            if (_IsMonitoring)
            {
                StatusTypes newStatus = StatusTypes.Unknown;
                ConnectionProfile profile = NetworkInformation.GetInternetConnectionProfile();

                if (profile != null)
                {
                    NetworkConnectivityLevel networkLevel = profile.GetNetworkConnectivityLevel();
                    newStatus = (networkLevel == NetworkConnectivityLevel.InternetAccess ? StatusTypes.Connected : StatusTypes.Disconnected);
                }
                else
                {
                    newStatus = StatusTypes.Disconnected;
                }

                if ( newStatus != _CurrentStatus)
                {
                    LogStatus(newStatus);
                    _DurationTime = new TimeSpan();
                }
            }
        }

        // Manages both TimeSpans and upate the forms text/title, as well as update the active duration in "LogList".
        private void DurationTmr_Tick(object sender, EventArgs e)
        {
            if (_IsMonitoring)
            {
                _MonitorTime = _MonitorTime.Add(TimeSpan.FromMilliseconds(100));
                _DurationTime = _DurationTime.Add(TimeSpan.FromMilliseconds(100));
                this.Text = "NetworkStatusMonitor.NET [Monitoring: " + string.Format("{0:00}:{1:00}:{2:00}", _MonitorTime.Hours, _MonitorTime.Minutes, _MonitorTime.Seconds) + "]";

                if ((_CurrentStatus != StatusTypes.Unknown) && (LogList.Items.Count > 0))
                {
                    Int32 pendingIndex = (LogList.Items.Count - 1);

                    if (LogList.Items[pendingIndex].SubItems[3].Text.Contains("[Pending]"))
                    {
                        LogList.Items[pendingIndex].SubItems[3].Text = "[Pending] " + string.Format("{0:00}:{1:00}:{2:00}", _DurationTime.Hours, _DurationTime.Minutes, _DurationTime.Seconds);
                    }
                }
            }
        }
    }
}