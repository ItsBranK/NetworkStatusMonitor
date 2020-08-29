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

namespace Internet_Status_Monitor {
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

        private void checkBtn_Click(object sender, EventArgs e) {
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics) {
                // Finds the selected NIC and compares it's `OperationalStatus`
                if (adapter.Description == nicBox.Text) {
                    if (adapter.OperationalStatus == OperationalStatus.Up) {
                        MessageBox.Show("Information: The selected network interface is online!", "Internet Status Monitor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    } else {
                        MessageBox.Show("Warning: The selected network interface is not currently online, it may not work properly.", "Internet Status Monitor", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                }
            }
        }

        // Gets all the available NIC's and adds their `Description` to `nicBox`
        public void loadNetworks() {
            nicBox.Items.Clear();
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics)
                nicBox.Items.Add(adapter.Description);
        }

        public void log(bool status) {
            bool foundNic = false;
            NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface adapter in nics) {
                // Finds the selected NIC based on its `Description`
                if (adapter.Description == nicBox.Text) {
                    int totalItems = logList.Items.Count;
                    string connectionType = adapter.NetworkInterfaceType.ToString();
                    string currentStatus = "(null)";

                    if (totalItems > 0) {
                        if (!justStarted && logList.Items[totalItems - 1].SubItems[3].Text == "Pending") {
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

                    // This just "pretty prints" the connection type, as it `NetworkInterfaceType` doesn't always return a "simple" answer
                    // If no match is found, it will default to the provided "non pretty" `NetworkInterfaceType`
                    if (wirelessMatch.Success) {
                        connectionType = "WiFi/Wireless";
                    } else if (ethernetMatch.Success) {
                        connectionType = "Ethernet/Wired";
                    }

                    // If the interface was lost, we need to manually switch the connection type if it's changed.
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

                    ListViewItem item = new ListViewItem();
                    item.Text = connectionType;
                    item.SubItems.Add(currentStatus);
                    item.SubItems.Add(DateTime.Now.ToString("MM/dd/yyyy h:mm tt"));
                    item.SubItems.Add("Pending");
                    logList.Items.Add(item);
                    foundNic = true;
                    // We've already found the selected NIC so theres no need to continue the loop
                    return;
                }
            }

            // If for some reason the NIC was removed or not found, cancel monitoring and refresh the list (most likely the user didn't select a NIC in the first place)
            if (!foundNic) {
                monitorBtn.PerformClick();
                loadNetworks();
                MessageBox.Show("Error: Could not find the selected network interface, automatically refreshing list.", "Internet Status Monitor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void monitorBtn_Click(object sender, EventArgs e) {
            if (monitoring) {
                this.Text = "Internet Status Monitor [Inactive]";
                monitorBtn.Text = "Start Monitoring";
                justStarted = false;
                monitoring = false;
                // Stops all monitoring timers and re-enables all buttons and boxes
                monitorTmr.Stop();
                watchTmr.Stop();
                nicBox.Enabled = true;
                checkBtn.Enabled = true;
                clearBtn.Enabled = true;

                // If the last status was still pending, change its text from "Pending" to "Aborted" plus the last reported `durationTime`
                int totalItems = logList.Items.Count;
                if (totalItems > 0) {
                    if (logList.Items[totalItems - 1].SubItems[3].Text == "Pending")
                        logList.Items[totalItems - 1].SubItems[3].Text = "[Aborted] " + string.Format("{0:00}:{1:00}:{2:00}", durationTime.Hours, durationTime.Minutes, durationTime.Seconds); ;
                }

                // Set both `activeTime` and `durationTime` to zero for the next monitor
                activeTime = activeTime.Subtract(TimeSpan.FromMilliseconds(activeTime.TotalMilliseconds));
                durationTime = durationTime.Subtract(TimeSpan.FromMilliseconds(durationTime.TotalMilliseconds));
            } else {
                this.Text = "Internet Status Monitor [Active: 00:00:00]";
                monitorBtn.Text = "Stop Monitoring";
                justStarted = true;
                monitoring = true;
                // Starts all monitoring timers and disables the necessary buttons and boxes
                watchTmr.Start();
                monitorTmr.Start();
                nicBox.Enabled = false;
                checkBtn.Enabled = false;
                clearBtn.Enabled = false;
            }
        }

        private void clearBtn_Click(object sender, EventArgs e) {
            logList.Items.Clear();
        }

        // Turns all items in `logList` to a string and writes that to a new file on the users desktop
        private void saveBtn_Click(object sender, EventArgs e) {
            int totalItems = logList.Items.Count;
            if (totalItems > 0) {
                string saveData = "";
                saveData += "Connection Type, Status, Time, Duration\n";

                for (int i = 0; i < totalItems; i++) {
                    string connectionType = logList.Items[i].Text;
                    string status = logList.Items[i].SubItems[1].Text;
                    string time = logList.Items[i].SubItems[2].Text;
                    string duration = logList.Items[i].SubItems[3].Text;
                    saveData += connectionType + ", " + status + ", " + time + ", " + duration + "\n";
                }

                string fileName = "LOG " + DateTime.Now.ToString("MM/dd/yyyy h:mm tt");
                fileName = fileName.Replace("/", "");
                fileName = fileName.Replace(":", "");
                fileName = fileName.Replace(" ", "_");

                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\";
                using (StreamWriter sw = File.CreateText(desktopPath + fileName + ".txt")) {
                    sw.WriteLine(saveData);
                }

                MessageBox.Show("Sucessfully saved log to: \"" + desktopPath + fileName + ".txt\"");
            }
        }

        // Separate timer from `monitorTmr` to manager both TimeSpan's and upate `timeLbl` 
        private void watchTmr_Tick(object sender, EventArgs e) {
            activeTime = activeTime.Add(TimeSpan.FromMilliseconds(100));
            durationTime = durationTime.Add(TimeSpan.FromMilliseconds(100));
            this.Text = "Internet Status Monitor [Active: " + string.Format("{0:00}:{1:00}:{2:00}", activeTime.Hours, activeTime.Minutes, activeTime.Seconds) + "]";
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
