namespace Internet_Status_Monitor
{
    partial class mainFrm
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
            this.components = new System.ComponentModel.Container();
            this.monitorTmr = new System.Windows.Forms.Timer(this.components);
            this.monitorBtn = new System.Windows.Forms.Button();
            this.logList = new System.Windows.Forms.ListView();
            this.typeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.statusColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.timeColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.durationColumn = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.saveBtn = new System.Windows.Forms.Button();
            this.clearBtn = new System.Windows.Forms.Button();
            this.watchTmr = new System.Windows.Forms.Timer(this.components);
            this.nicLbl = new System.Windows.Forms.Label();
            this.nicBox = new System.Windows.Forms.ComboBox();
            this.checkBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // monitorTmr
            // 
            this.monitorTmr.Interval = 1000;
            this.monitorTmr.Tick += new System.EventHandler(this.monitorTmr_Tick);
            // 
            // monitorBtn
            // 
            this.monitorBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.monitorBtn.Location = new System.Drawing.Point(12, 246);
            this.monitorBtn.Name = "monitorBtn";
            this.monitorBtn.Size = new System.Drawing.Size(160, 30);
            this.monitorBtn.TabIndex = 0;
            this.monitorBtn.Text = "Start Monitoring";
            this.monitorBtn.UseVisualStyleBackColor = true;
            this.monitorBtn.Click += new System.EventHandler(this.monitorBtn_Click);
            // 
            // logList
            // 
            this.logList.BackColor = System.Drawing.Color.White;
            this.logList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.logList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.typeColumn,
            this.statusColumn,
            this.timeColumn,
            this.durationColumn});
            this.logList.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.logList.ForeColor = System.Drawing.Color.Black;
            this.logList.FullRowSelect = true;
            this.logList.GridLines = true;
            this.logList.HideSelection = false;
            this.logList.Location = new System.Drawing.Point(12, 40);
            this.logList.MultiSelect = false;
            this.logList.Name = "logList";
            this.logList.Size = new System.Drawing.Size(490, 200);
            this.logList.TabIndex = 18;
            this.logList.UseCompatibleStateImageBehavior = false;
            this.logList.View = System.Windows.Forms.View.Details;
            // 
            // typeColumn
            // 
            this.typeColumn.Text = "Connection Type";
            this.typeColumn.Width = 120;
            // 
            // statusColumn
            // 
            this.statusColumn.Text = "Status";
            this.statusColumn.Width = 136;
            // 
            // timeColumn
            // 
            this.timeColumn.Text = "Time";
            this.timeColumn.Width = 121;
            // 
            // durationColumn
            // 
            this.durationColumn.Text = "Duration";
            this.durationColumn.Width = 112;
            // 
            // saveBtn
            // 
            this.saveBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.saveBtn.Location = new System.Drawing.Point(177, 246);
            this.saveBtn.Name = "saveBtn";
            this.saveBtn.Size = new System.Drawing.Size(160, 30);
            this.saveBtn.TabIndex = 28;
            this.saveBtn.Text = "Save log";
            this.saveBtn.UseVisualStyleBackColor = true;
            this.saveBtn.Click += new System.EventHandler(this.saveBtn_Click);
            // 
            // clearBtn
            // 
            this.clearBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.clearBtn.Location = new System.Drawing.Point(342, 246);
            this.clearBtn.Name = "clearBtn";
            this.clearBtn.Size = new System.Drawing.Size(160, 30);
            this.clearBtn.TabIndex = 29;
            this.clearBtn.Text = "Clear log";
            this.clearBtn.UseVisualStyleBackColor = true;
            this.clearBtn.Click += new System.EventHandler(this.clearBtn_Click);
            // 
            // watchTmr
            // 
            this.watchTmr.Tick += new System.EventHandler(this.watchTmr_Tick);
            // 
            // nicLbl
            // 
            this.nicLbl.BackColor = System.Drawing.Color.Transparent;
            this.nicLbl.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nicLbl.ForeColor = System.Drawing.Color.Black;
            this.nicLbl.Location = new System.Drawing.Point(12, 9);
            this.nicLbl.Name = "nicLbl";
            this.nicLbl.Size = new System.Drawing.Size(110, 25);
            this.nicLbl.TabIndex = 30;
            this.nicLbl.Text = "Network Interface:";
            this.nicLbl.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // nicBox
            // 
            this.nicBox.BackColor = System.Drawing.Color.White;
            this.nicBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.nicBox.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.nicBox.ForeColor = System.Drawing.Color.Black;
            this.nicBox.FormattingEnabled = true;
            this.nicBox.Location = new System.Drawing.Point(128, 10);
            this.nicBox.Name = "nicBox";
            this.nicBox.Size = new System.Drawing.Size(293, 23);
            this.nicBox.TabIndex = 31;
            // 
            // checkBtn
            // 
            this.checkBtn.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.checkBtn.Location = new System.Drawing.Point(427, 9);
            this.checkBtn.Name = "checkBtn";
            this.checkBtn.Size = new System.Drawing.Size(75, 25);
            this.checkBtn.TabIndex = 32;
            this.checkBtn.Text = "Check";
            this.checkBtn.UseVisualStyleBackColor = true;
            this.checkBtn.Click += new System.EventHandler(this.checkBtn_Click);
            // 
            // mainFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(514, 286);
            this.Controls.Add(this.checkBtn);
            this.Controls.Add(this.clearBtn);
            this.Controls.Add(this.saveBtn);
            this.Controls.Add(this.nicBox);
            this.Controls.Add(this.nicLbl);
            this.Controls.Add(this.logList);
            this.Controls.Add(this.monitorBtn);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "mainFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Internet Status Monitor [Inactive]";
            this.Load += new System.EventHandler(this.mainFrm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer monitorTmr;
        private System.Windows.Forms.Button monitorBtn;
        internal System.Windows.Forms.ListView logList;
        private System.Windows.Forms.ColumnHeader statusColumn;
        internal System.Windows.Forms.ColumnHeader timeColumn;
        internal System.Windows.Forms.ColumnHeader durationColumn;
        private System.Windows.Forms.Button saveBtn;
        private System.Windows.Forms.Button clearBtn;
        private System.Windows.Forms.ColumnHeader typeColumn;
        private System.Windows.Forms.Timer watchTmr;
        private System.Windows.Forms.Label nicLbl;
        private System.Windows.Forms.ComboBox nicBox;
        private System.Windows.Forms.Button checkBtn;
    }
}

