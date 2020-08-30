using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.IO;
using System.Text.RegularExpressions;

namespace Network_Status_Monitor {
    public partial class mainFrm : Form {
        private TimeSpan activeTime = new TimeSpan();
        private TimeSpan durationTime = new TimeSpan();
        private bool monitoring = false;
        private bool justStarted = true;
        private bool activeStatus = false;

        public mainFrm() {
            InitializeComponent();
        }

        private void mainFrm_Load(object sender, EventArgs e) {
            loadNetworks();
        }

        // Finds the selected adapter and displays its `OperationalStatus`
        private void checkBtn_Click(object sender, EventArgs e) {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics) {
                if (adapter.Description == adapterBox.Text) {
                    MessageBox.Show("The selected network adapters status is: \"" + adapter.OperationalStatus.ToString() + "\"", "Network Status Monitor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // Gets all the available adapters and adds their `Description` to `nicBox`
        public void loadNetworks() {
            adapterBox.Items.Clear();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
                adapterBox.Items.Add(adapter.Description);
        }

        public void log(bool status) {
            bool foundNic = false;
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics) {
                // Finds the selected adapter based on its `Description`
                if (adapter.Description == adapterBox.Text) {
                    int totalItems = logList.Items.Count;
                    string connectionType = adapter.NetworkInterfaceType.ToString();
                    string currentStatus = "(null)";

                    if (totalItems > 0) {
                        if (!justStarted && logList.Items[totalItems - 1].SubItems[3].Text.Contains("[Pending]")) {
                            logList.Items[totalItems - 1].SubItems[3].Text = string.Format("{0:00}:{1:00}:{2:00}", durationTime.Hours, durationTime.Minutes, durationTime.Seconds);
                            durationTime = durationTime.Subtract(TimeSpan.FromMilliseconds(durationTime.TotalMilliseconds));
                        }
                    }

                    if (status) { currentStatus = "Connected/Online"; }
                    else { currentStatus = "Disconnected/Offline"; }

                    Regex wirelessEx = new Regex("(Wireless)");
                    Regex ethernetEx = new Regex("(Ethernet)");
                    Match wirelessMatch = wirelessEx.Match(connectionType);
                    Match ethernetMatch = ethernetEx.Match(connectionType);

                    // This just "pretty prints" the connection type, as `NetworkInterfaceType` doesn't always return a simple answer, which is why we also need to use regular expressions
                    // If no match is found, it will default to the provided "non pretty" `NetworkInterfaceType`
                    if (wirelessMatch.Success) {
                        connectionType = "WiFi/Wireless";
                    } else if (ethernetMatch.Success) {
                        connectionType = "Ethernet/Wired";
                    }

                    // If the interface was lost, we need to manually switch the connection type if it was changed
                    // If we don't do this and the user reconnects to a different interface, from lets say ethernet to wifi, it will report the wrong "disconnection type"
                    if (!justStarted && currentStatus == "Disconnected/Offline") {
                        if (logList.Items[totalItems - 1].Text != connectionType) {
                            if (connectionType == "WiFi/Wireless") {
                                connectionType = "Ethernet/Wired";
                            } else {
                                connectionType = "WiFi/Wireless";
                            }
                        }
                    }

                    // Adds the new log to `logList`
                    ListViewItem item = new ListViewItem();
                    item.Text = connectionType;
                    item.SubItems.Add(currentStatus);
                    item.SubItems.Add(DateTime.Now.ToString("MM/dd/yyyy h:mm tt"));
                    item.SubItems.Add("[Pending]");
                    logList.Items.Add(item);
                    foundNic = true;
                    // We've already found the selected adapter so theres no need to continue the loop
                    return;
                }
            }

            // If for some reason the adapter was removed or not found, cancel monitoring and refresh the list
            if (!foundNic) {
                monitorBtn.PerformClick();
                loadNetworks();
                MessageBox.Show("Error: Could not find the selected network interface, automatically refreshing list.", "Network Status Monitor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void monitorBtn_Click(object sender, EventArgs e) {
            if (monitoring) {
                this.Text = "Network Status Monitor [Inactive]";
                monitorBtn.Text = "Start Monitoring";
                justStarted = false;
                monitoring = false;
                // Stops all monitoring timers and re-enables all buttons and boxes
                monitorTmr.Stop();
                watchTmr.Stop();
                adapterBox.Enabled = true;
                checkBtn.Enabled = true;
                clearBtn.Enabled = true;

                // If the last status was still pending, change its text from "Pending" to "Aborted" plus the last reported `durationTime`
                int totalItems = logList.Items.Count;
                if (totalItems > 0) {
                    if (logList.Items[totalItems - 1].SubItems[3].Text.Contains("[Pending]"))
                        logList.Items[totalItems - 1].SubItems[3].Text = "[Aborted] " + string.Format("{0:00}:{1:00}:{2:00}", durationTime.Hours, durationTime.Minutes, durationTime.Seconds); ;
                }

                // Set both `activeTime` and `durationTime` to zero for the next monitor
                activeTime = activeTime.Subtract(TimeSpan.FromMilliseconds(activeTime.TotalMilliseconds));
                durationTime = durationTime.Subtract(TimeSpan.FromMilliseconds(durationTime.TotalMilliseconds));
            } else {
                if (string.IsNullOrEmpty(adapterBox.Text)) {
                    MessageBox.Show("Warning: Please select the current network adapter you're using to get accurate results.", "Network Status Monitor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                } else {
                    this.Text = "Network Status Monitor [Active: 00:00:00]";
                    monitorBtn.Text = "Stop Monitoring";
                    justStarted = true;
                    monitoring = true;
                    // Starts all monitoring timers and disables the necessary buttons and boxes
                    watchTmr.Start();
                    monitorTmr.Start();
                    adapterBox.Enabled = false;
                    checkBtn.Enabled = false;
                    clearBtn.Enabled = false;
                }
            }
        }

        private void clearBtn_Click(object sender, EventArgs e) {
            logList.Items.Clear();
        }

        private void saveBtn_Click(object sender, EventArgs e) {
            int totalItems = logList.Items.Count;
            if (totalItems > 0) {
                string saveData = "";
                saveData += "Connection Type, Status, Time, Duration\n";

                 // Turns each item in `logList` to a string and adds it to `saveData`, lines are seperated with the "\n" at the end
                for (int i = 0; i < totalItems; i++) {
                    string connectionType = logList.Items[i].Text;
                    string status = logList.Items[i].SubItems[1].Text;
                    string time = logList.Items[i].SubItems[2].Text;
                    string duration = logList.Items[i].SubItems[3].Text;
                    saveData += connectionType + ", " + status + ", " + time + ", " + duration + "\n";
                }

                // Formats the file name for windows
                string fileName = "LOG " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
                fileName = fileName.Replace("/", "");
                fileName = fileName.Replace(":", "");
                fileName = fileName.Replace(" ", "_");

                // Promps the folder picker dialog
                FolderBrowserDialog folderBrowser = new FolderBrowserDialog();
                DialogResult folderResult = folderBrowser.ShowDialog();

                // Makes sure the selected path is valid before creating a file and writing to it
                if (folderResult == DialogResult.OK && Directory.Exists(folderBrowser.SelectedPath)) {

                    string logPath = folderBrowser.SelectedPath + "\\" + fileName + ".txt";
                    using (StreamWriter sw = File.CreateText(logPath))
                        sw.WriteLine(saveData);

                    MessageBox.Show("Sucessfully saved log to: \n" + logPath, "Network Status Monitor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        // Separate timer from `monitorTmr` to manager both TimeSpans and upate the forms text/title, as well as update the active duration in `logList`
        private void watchTmr_Tick(object sender, EventArgs e) {
            activeTime = activeTime.Add(TimeSpan.FromMilliseconds(100));
            durationTime = durationTime.Add(TimeSpan.FromMilliseconds(100));
            this.Text = "Network Status Monitor [Active: " + string.Format("{0:00}:{1:00}:{2:00}", activeTime.Hours, activeTime.Minutes, activeTime.Seconds) + "]";

            int totalItems = logList.Items.Count;
            if (totalItems > 0) {
                if (!justStarted && logList.Items[totalItems - 1].SubItems[3].Text.Contains("[Pending]")) {
                    logList.Items[totalItems - 1].SubItems[3].Text = "[Pending] " + string.Format("{0:00}:{1:00}:{2:00}", durationTime.Hours, durationTime.Minutes, durationTime.Seconds);
                }
            }
        }

        private void monitorTmr_Tick(object sender, EventArgs e) {
            if (monitoring) {
                // Returns if accessing the web is possible, aka if you're connected to the internet or not
                bool currentStatus = NetworkInterface.GetIsNetworkAvailable();
                // If the user just pressed "Start Monitoring" log the initial connection type and set the `activeStatus`
                if (justStarted) {
                    log(currentStatus);
                    activeStatus = currentStatus;
                    justStarted = false;
                }
                // If there is a difference between the last updated `activeStatus` and the one just grabbed by the timer tick (`currentStatus`), log that the status has changed
                if (currentStatus != activeStatus) {
                    log(currentStatus);
                    activeStatus = currentStatus;
                }
            }
        }
    }
}
