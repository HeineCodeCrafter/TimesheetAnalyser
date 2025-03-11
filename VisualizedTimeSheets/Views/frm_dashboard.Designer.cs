namespace VisualizedTimeSheets.Views
{
    partial class frm_dashboard
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
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_export_current_period = new System.Windows.Forms.Button();
            this.lbl_report_summary = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dtp_report_start = new System.Windows.Forms.DateTimePicker();
            this.dtp_report_end = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.olv_report_selcted_index = new BrightIdeasSoftware.ObjectListView();
            this.olv_report_days = new BrightIdeasSoftware.ObjectListView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.nud_offset_comptime = new System.Windows.Forms.NumericUpDown();
            this.dtp_comp_time_due_date = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txt_dataDirectory = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.olv_day_entries = new BrightIdeasSoftware.ObjectListView();
            this.olv_imported = new BrightIdeasSoftware.ObjectListView();
            this.columnButtonRenderer1 = new BrightIdeasSoftware.ColumnButtonRenderer();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.olv_timesheet_overview = new BrightIdeasSoftware.ObjectListView();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olv_report_selcted_index)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.olv_report_days)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_offset_comptime)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olv_day_entries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.olv_imported)).BeginInit();
            this.tabPage3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olv_timesheet_overview)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1277, 707);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.btn_export_current_period);
            this.tabPage1.Controls.Add(this.lbl_report_summary);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.groupBox3);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1269, 681);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Report";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // btn_export_current_period
            // 
            this.btn_export_current_period.Location = new System.Drawing.Point(1162, 3);
            this.btn_export_current_period.Name = "btn_export_current_period";
            this.btn_export_current_period.Size = new System.Drawing.Size(104, 23);
            this.btn_export_current_period.TabIndex = 78;
            this.btn_export_current_period.Text = "Export to CSV";
            this.btn_export_current_period.UseVisualStyleBackColor = true;
            this.btn_export_current_period.Click += new System.EventHandler(this.btn_export_current_period_Click);
            // 
            // lbl_report_summary
            // 
            this.lbl_report_summary.Location = new System.Drawing.Point(878, 3);
            this.lbl_report_summary.Name = "lbl_report_summary";
            this.lbl_report_summary.Size = new System.Drawing.Size(383, 77);
            this.lbl_report_summary.TabIndex = 79;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dtp_report_start);
            this.groupBox2.Controls.Add(this.dtp_report_end);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Location = new System.Drawing.Point(5, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(433, 74);
            this.groupBox2.TabIndex = 81;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Report";
            // 
            // dtp_report_start
            // 
            this.dtp_report_start.Location = new System.Drawing.Point(14, 29);
            this.dtp_report_start.Name = "dtp_report_start";
            this.dtp_report_start.Size = new System.Drawing.Size(186, 20);
            this.dtp_report_start.TabIndex = 74;
            this.dtp_report_start.Value = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            this.dtp_report_start.ValueChanged += new System.EventHandler(this.dtp_report_start_ValueChanged);
            // 
            // dtp_report_end
            // 
            this.dtp_report_end.Location = new System.Drawing.Point(228, 29);
            this.dtp_report_end.Name = "dtp_report_end";
            this.dtp_report_end.Size = new System.Drawing.Size(186, 20);
            this.dtp_report_end.TabIndex = 73;
            this.dtp_report_end.Value = new System.DateTime(2030, 12, 31, 0, 0, 0, 0);
            this.dtp_report_end.ValueChanged += new System.EventHandler(this.dtp_report_end_ValueChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(225, 13);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 76;
            this.label4.Text = "End";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(11, 13);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(29, 13);
            this.label3.TabIndex = 77;
            this.label3.Text = "Start";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.olv_report_selcted_index);
            this.groupBox3.Controls.Add(this.olv_report_days);
            this.groupBox3.Location = new System.Drawing.Point(8, 83);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1253, 590);
            this.groupBox3.TabIndex = 81;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Report";
            // 
            // olv_report_selcted_index
            // 
            this.olv_report_selcted_index.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olv_report_selcted_index.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.olv_report_selcted_index.CellEditUseWholeCell = false;
            this.olv_report_selcted_index.Cursor = System.Windows.Forms.Cursors.Default;
            this.olv_report_selcted_index.GridLines = true;
            this.olv_report_selcted_index.HideSelection = false;
            this.olv_report_selcted_index.Location = new System.Drawing.Point(613, 19);
            this.olv_report_selcted_index.Name = "olv_report_selcted_index";
            this.olv_report_selcted_index.Size = new System.Drawing.Size(623, 565);
            this.olv_report_selcted_index.TabIndex = 72;
            this.olv_report_selcted_index.UseCompatibleStateImageBehavior = false;
            this.olv_report_selcted_index.View = System.Windows.Forms.View.Details;
            this.olv_report_selcted_index.SelectedIndexChanged += new System.EventHandler(this.olv_report_selcted_index_SelectedIndexChanged);
            // 
            // olv_report_days
            // 
            this.olv_report_days.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.olv_report_days.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.olv_report_days.CellEditUseWholeCell = false;
            this.olv_report_days.Cursor = System.Windows.Forms.Cursors.Default;
            this.olv_report_days.GridLines = true;
            this.olv_report_days.HideSelection = false;
            this.olv_report_days.Location = new System.Drawing.Point(6, 19);
            this.olv_report_days.Name = "olv_report_days";
            this.olv_report_days.Size = new System.Drawing.Size(601, 565);
            this.olv_report_days.TabIndex = 72;
            this.olv_report_days.UseCompatibleStateImageBehavior = false;
            this.olv_report_days.View = System.Windows.Forms.View.Details;
            this.olv_report_days.SelectedIndexChanged += new System.EventHandler(this.olv_report_days_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.nud_offset_comptime);
            this.groupBox1.Controls.Add(this.dtp_comp_time_due_date);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(444, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(428, 74);
            this.groupBox1.TabIndex = 81;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Comp. time";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(8, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 13);
            this.label1.TabIndex = 79;
            this.label1.Text = "Comp. time override";
            // 
            // nud_offset_comptime
            // 
            this.nud_offset_comptime.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.nud_offset_comptime.DecimalPlaces = 1;
            this.nud_offset_comptime.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.nud_offset_comptime.Location = new System.Drawing.Point(114, 23);
            this.nud_offset_comptime.Name = "nud_offset_comptime";
            this.nud_offset_comptime.Size = new System.Drawing.Size(74, 20);
            this.nud_offset_comptime.TabIndex = 80;
            this.nud_offset_comptime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.nud_offset_comptime.ValueChanged += new System.EventHandler(this.nud_offset_comptime_ValueChanged);
            // 
            // dtp_comp_time_due_date
            // 
            this.dtp_comp_time_due_date.Location = new System.Drawing.Point(225, 23);
            this.dtp_comp_time_due_date.Name = "dtp_comp_time_due_date";
            this.dtp_comp_time_due_date.Size = new System.Drawing.Size(186, 20);
            this.dtp_comp_time_due_date.TabIndex = 75;
            this.dtp_comp_time_due_date.Value = new System.DateTime(2024, 1, 1, 0, 0, 0, 0);
            this.dtp_comp_time_due_date.ValueChanged += new System.EventHandler(this.dtp_comp_time_due_date_ValueChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(194, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 78;
            this.label2.Text = "due";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txt_dataDirectory);
            this.tabPage2.Controls.Add(this.button1);
            this.tabPage2.Controls.Add(this.olv_day_entries);
            this.tabPage2.Controls.Add(this.olv_imported);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1269, 681);
            this.tabPage2.TabIndex = 0;
            this.tabPage2.Text = "Overview";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txt_dataDirectory
            // 
            this.txt_dataDirectory.Location = new System.Drawing.Point(88, 6);
            this.txt_dataDirectory.Name = "txt_dataDirectory";
            this.txt_dataDirectory.Size = new System.Drawing.Size(693, 20);
            this.txt_dataDirectory.TabIndex = 73;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 72;
            this.button1.Text = "import";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // olv_day_entries
            // 
            this.olv_day_entries.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olv_day_entries.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.olv_day_entries.CellEditUseWholeCell = false;
            this.olv_day_entries.Cursor = System.Windows.Forms.Cursors.Default;
            this.olv_day_entries.GridLines = true;
            this.olv_day_entries.HideSelection = false;
            this.olv_day_entries.Location = new System.Drawing.Point(539, 33);
            this.olv_day_entries.Name = "olv_day_entries";
            this.olv_day_entries.Size = new System.Drawing.Size(727, 645);
            this.olv_day_entries.TabIndex = 70;
            this.olv_day_entries.UseCompatibleStateImageBehavior = false;
            this.olv_day_entries.View = System.Windows.Forms.View.Details;
            // 
            // olv_imported
            // 
            this.olv_imported.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.olv_imported.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.olv_imported.CellEditUseWholeCell = false;
            this.olv_imported.Cursor = System.Windows.Forms.Cursors.Default;
            this.olv_imported.GridLines = true;
            this.olv_imported.HideSelection = false;
            this.olv_imported.Location = new System.Drawing.Point(5, 33);
            this.olv_imported.Name = "olv_imported";
            this.olv_imported.Size = new System.Drawing.Size(528, 645);
            this.olv_imported.TabIndex = 71;
            this.olv_imported.UseCompatibleStateImageBehavior = false;
            this.olv_imported.View = System.Windows.Forms.View.Details;
            this.olv_imported.SelectedIndexChanged += new System.EventHandler(this.olv_imported_SelectedIndexChanged);
            this.olv_imported.MouseClick += new System.Windows.Forms.MouseEventHandler(this.olv_imported_MouseClick);
            this.olv_imported.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.olv_imported_MouseDoubleClick);
            // 
            // columnButtonRenderer1
            // 
            this.columnButtonRenderer1.ButtonPadding = new System.Drawing.Size(10, 10);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.olv_timesheet_overview);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1269, 681);
            this.tabPage3.TabIndex = 1;
            this.tabPage3.Text = "TSN Overview";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // olv_timesheet_overview
            // 
            this.olv_timesheet_overview.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.olv_timesheet_overview.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.olv_timesheet_overview.CellEditUseWholeCell = false;
            this.olv_timesheet_overview.Cursor = System.Windows.Forms.Cursors.Default;
            this.olv_timesheet_overview.FullRowSelect = true;
            this.olv_timesheet_overview.GridLines = true;
            this.olv_timesheet_overview.HideSelection = false;
            this.olv_timesheet_overview.Location = new System.Drawing.Point(6, 6);
            this.olv_timesheet_overview.Name = "olv_timesheet_overview";
            this.olv_timesheet_overview.Size = new System.Drawing.Size(1255, 669);
            this.olv_timesheet_overview.TabIndex = 72;
            this.olv_timesheet_overview.UseCompatibleStateImageBehavior = false;
            this.olv_timesheet_overview.View = System.Windows.Forms.View.Details;
            // 
            // frm_dashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1277, 707);
            this.Controls.Add(this.tabControl1);
            this.Name = "frm_dashboard";
            this.Text = "TimeSheet Analyzer";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olv_report_selcted_index)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.olv_report_days)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nud_offset_comptime)).EndInit();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.olv_day_entries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.olv_imported)).EndInit();
            this.tabPage3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.olv_timesheet_overview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown nud_offset_comptime;
        private System.Windows.Forms.DateTimePicker dtp_comp_time_due_date;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.DateTimePicker dtp_report_end;
        private System.Windows.Forms.DateTimePicker dtp_report_start;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txt_dataDirectory;
        private System.Windows.Forms.Button button1;
        private BrightIdeasSoftware.ObjectListView olv_day_entries;
        private BrightIdeasSoftware.ObjectListView olv_imported;
        private System.Windows.Forms.GroupBox groupBox3;
        private BrightIdeasSoftware.ObjectListView olv_report_days;
        private BrightIdeasSoftware.ObjectListView olv_report_selcted_index;
        private System.Windows.Forms.Label lbl_report_summary;
        private System.Windows.Forms.Button btn_export_current_period;
        private BrightIdeasSoftware.ColumnButtonRenderer columnButtonRenderer1;
        private System.Windows.Forms.TabPage tabPage3;
        private BrightIdeasSoftware.ObjectListView olv_timesheet_overview;
    }
}