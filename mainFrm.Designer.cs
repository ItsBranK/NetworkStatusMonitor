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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            AdapterLbl = new Label();
            AdapterBx = new ComboBox();
            StatusBtn = new Button();
            LogList = new ListView();
            TypeColumn = new ColumnHeader();
            StatusColumn = new ColumnHeader();
            TimeColumn = new ColumnHeader();
            DurationColumn = new ColumnHeader();
            MonitorBtn = new Button();
            SaveBtn = new Button();
            ClearBtn = new Button();
            StatusTmr = new System.Windows.Forms.Timer(components);
            DurationTmr = new System.Windows.Forms.Timer(components);
            SuspendLayout();
            // 
            // AdapterLbl
            // 
            AdapterLbl.Location = new Point(12, 12);
            AdapterLbl.Name = "AdapterLbl";
            AdapterLbl.Size = new Size(100, 25);
            AdapterLbl.TabIndex = 0;
            AdapterLbl.Text = "Network Adapter:";
            AdapterLbl.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // AdapterBx
            // 
            AdapterBx.DropDownStyle = ComboBoxStyle.DropDownList;
            AdapterBx.Font = new Font("Segoe UI", 9.75F, FontStyle.Regular, GraphicsUnit.Point);
            AdapterBx.FormattingEnabled = true;
            AdapterBx.Location = new Point(115, 12);
            AdapterBx.Name = "AdapterBx";
            AdapterBx.Size = new Size(326, 25);
            AdapterBx.TabIndex = 1;
            // 
            // StatusBtn
            // 
            StatusBtn.Location = new Point(447, 12);
            StatusBtn.Name = "StatusBtn";
            StatusBtn.Size = new Size(100, 25);
            StatusBtn.TabIndex = 2;
            StatusBtn.Text = "Check status";
            StatusBtn.UseVisualStyleBackColor = true;
            StatusBtn.Click += StatusBtn_Click;
            // 
            // LogList
            // 
            LogList.BorderStyle = BorderStyle.FixedSingle;
            LogList.Columns.AddRange(new ColumnHeader[] { TypeColumn, StatusColumn, TimeColumn, DurationColumn });
            LogList.FullRowSelect = true;
            LogList.GridLines = true;
            LogList.Location = new Point(12, 43);
            LogList.Name = "LogList";
            LogList.Size = new Size(535, 215);
            LogList.TabIndex = 3;
            LogList.UseCompatibleStateImageBehavior = false;
            LogList.View = View.Details;
            // 
            // TypeColumn
            // 
            TypeColumn.Text = "Type";
            TypeColumn.Width = 120;
            // 
            // StatusColumn
            // 
            StatusColumn.Text = "Status";
            StatusColumn.Width = 135;
            // 
            // TimeColumn
            // 
            TimeColumn.Text = "Time";
            TimeColumn.Width = 135;
            // 
            // DurationColumn
            // 
            DurationColumn.Text = "Duration";
            DurationColumn.Width = 145;
            // 
            // MonitorBtn
            // 
            MonitorBtn.Location = new Point(12, 264);
            MonitorBtn.Name = "MonitorBtn";
            MonitorBtn.Size = new Size(200, 30);
            MonitorBtn.TabIndex = 4;
            MonitorBtn.Text = "Start monitoring";
            MonitorBtn.UseVisualStyleBackColor = true;
            MonitorBtn.Click += MonitorBtn_Click;
            // 
            // SaveBtn
            // 
            SaveBtn.Location = new Point(218, 264);
            SaveBtn.Name = "SaveBtn";
            SaveBtn.Size = new Size(200, 30);
            SaveBtn.TabIndex = 5;
            SaveBtn.Text = "Save log";
            SaveBtn.UseVisualStyleBackColor = true;
            SaveBtn.Click += SaveBtn_Click;
            // 
            // ClearBtn
            // 
            ClearBtn.Location = new Point(424, 263);
            ClearBtn.Name = "ClearBtn";
            ClearBtn.Size = new Size(123, 30);
            ClearBtn.TabIndex = 6;
            ClearBtn.Text = "Clear log";
            ClearBtn.UseVisualStyleBackColor = true;
            ClearBtn.Click += ClearBtn_Click;
            // 
            // StatusTmr
            // 
            StatusTmr.Tick += StatusTmr_Tick;
            // 
            // DurationTmr
            // 
            DurationTmr.Tick += DurationTmr_Tick;
            // 
            // MainFrm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(559, 305);
            Controls.Add(ClearBtn);
            Controls.Add(SaveBtn);
            Controls.Add(MonitorBtn);
            Controls.Add(LogList);
            Controls.Add(StatusBtn);
            Controls.Add(AdapterBx);
            Controls.Add(AdapterLbl);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainFrm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "NetworkStatusMonitor.NET";
            Load += MainFrm_Load;
            ResumeLayout(false);
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