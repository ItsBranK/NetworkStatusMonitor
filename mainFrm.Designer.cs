namespace NetworkStatusMonitor
{
    partial class MainFrm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.AdapterLbl = new System.Windows.Forms.Label();
            this.AdapterBx = new System.Windows.Forms.ComboBox();
            this.StatusBtn = new System.Windows.Forms.Button();
            this.LogList = new System.Windows.Forms.ListView();
            this.TypeColumn = new System.Windows.Forms.ColumnHeader();
            this.StatusColumn = new System.Windows.Forms.ColumnHeader();
            this.TimeColumn = new System.Windows.Forms.ColumnHeader();
            this.DurationColumn = new System.Windows.Forms.ColumnHeader();
            this.MonitorBtn = new System.Windows.Forms.Button();
            this.SaveBtn = new System.Windows.Forms.Button();
            this.ClearBtn = new System.Windows.Forms.Button();
            this.StatusTmr = new System.Windows.Forms.Timer(this.components);
            this.DurationTmr = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // AdapterLbl
            // 
            this.AdapterLbl.Location = new System.Drawing.Point(12, 12);
            this.AdapterLbl.Name = "AdapterLbl";
            this.AdapterLbl.Size = new System.Drawing.Size(100, 25);
            this.AdapterLbl.TabIndex = 0;
            this.AdapterLbl.Text = "Network Adapter:";
            this.AdapterLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AdapterBx
            // 
            this.AdapterBx.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.AdapterBx.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.AdapterBx.FormattingEnabled = true;
            this.AdapterBx.Location = new System.Drawing.Point(115, 12);
            this.AdapterBx.Name = "AdapterBx";
            this.AdapterBx.Size = new System.Drawing.Size(326, 25);
            this.AdapterBx.TabIndex = 1;
            // 
            // StatusBtn
            // 
            this.StatusBtn.Location = new System.Drawing.Point(447, 12);
            this.StatusBtn.Name = "StatusBtn";
            this.StatusBtn.Size = new System.Drawing.Size(100, 25);
            this.StatusBtn.TabIndex = 2;
            this.StatusBtn.Text = "Check status";
            this.StatusBtn.UseVisualStyleBackColor = true;
            this.StatusBtn.Click += new System.EventHandler(this.StatusBtn_Click);
            // 
            // LogList
            // 
            this.LogList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LogList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.TypeColumn,
            this.StatusColumn,
            this.TimeColumn,
            this.DurationColumn});
            this.LogList.FullRowSelect = true;
            this.LogList.GridLines = true;
            this.LogList.Location = new System.Drawing.Point(12, 43);
            this.LogList.Name = "LogList";
            this.LogList.Size = new System.Drawing.Size(535, 215);
            this.LogList.TabIndex = 3;
            this.LogList.UseCompatibleStateImageBehavior = false;
            this.LogList.View = System.Windows.Forms.View.Details;
            // 
            // TypeColumn
            // 
            this.TypeColumn.Text = "Type";
            this.TypeColumn.Width = 120;
            // 
            // StatusColumn
            // 
            this.StatusColumn.Text = "Status";
            this.StatusColumn.Width = 135;
            // 
            // TimeColumn
            // 
            this.TimeColumn.Text = "Time";
            this.TimeColumn.Width = 135;
            // 
            // DurationColumn
            // 
            this.DurationColumn.Text = "Duration";
            this.DurationColumn.Width = 145;
            // 
            // MonitorBtn
            // 
            this.MonitorBtn.Location = new System.Drawing.Point(12, 264);
            this.MonitorBtn.Name = "MonitorBtn";
            this.MonitorBtn.Size = new System.Drawing.Size(200, 30);
            this.MonitorBtn.TabIndex = 4;
            this.MonitorBtn.Text = "Start monitoring";
            this.MonitorBtn.UseVisualStyleBackColor = true;
            this.MonitorBtn.Click += new System.EventHandler(this.MonitorBtn_Click);
            // 
            // SaveBtn
            // 
            this.SaveBtn.Location = new System.Drawing.Point(218, 264);
            this.SaveBtn.Name = "SaveBtn";
            this.SaveBtn.Size = new System.Drawing.Size(200, 30);
            this.SaveBtn.TabIndex = 5;
            this.SaveBtn.Text = "Save log";
            this.SaveBtn.UseVisualStyleBackColor = true;
            this.SaveBtn.Click += new System.EventHandler(this.SaveBtn_Click);
            // 
            // ClearBtn
            // 
            this.ClearBtn.Location = new System.Drawing.Point(424, 263);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(123, 30);
            this.ClearBtn.TabIndex = 6;
            this.ClearBtn.Text = "Clear log";
            this.ClearBtn.UseVisualStyleBackColor = true;
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // StatusTmr
            // 
            this.StatusTmr.Tick += new System.EventHandler(this.StatusTmr_Tick);
            // 
            // DurationTmr
            // 
            this.DurationTmr.Tick += new System.EventHandler(this.DurationTmr_Tick);
            // 
            // MainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(559, 305);
            this.Controls.Add(this.ClearBtn);
            this.Controls.Add(this.SaveBtn);
            this.Controls.Add(this.MonitorBtn);
            this.Controls.Add(this.LogList);
            this.Controls.Add(this.StatusBtn);
            this.Controls.Add(this.AdapterBx);
            this.Controls.Add(this.AdapterLbl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "NetworkStatusMonitor.NET";
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Label AdapterLbl;
        private ComboBox AdapterBx;
        private Button StatusBtn;
        private ListView LogList;
        private ColumnHeader TypeColumn;
        private ColumnHeader StatusColumn;
        private ColumnHeader TimeColumn;
        private ColumnHeader DurationColumn;
        private Button MonitorBtn;
        private Button SaveBtn;
        private Button ClearBtn;
        private System.Windows.Forms.Timer StatusTmr;
        private System.Windows.Forms.Timer DurationTmr;
    }
}