namespace TimeTrack
{
    partial class TimeRecordsForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TimeRecordsForm));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pnlPoints = new System.Windows.Forms.Panel();
            this.vScrollBar1 = new System.Windows.Forms.VScrollBar();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblCurrentDay = new System.Windows.Forms.Label();
            this.btnFriday = new System.Windows.Forms.Button();
            this.btnSunday = new System.Windows.Forms.Button();
            this.btnSaturday = new System.Windows.Forms.Button();
            this.btnThursday = new System.Windows.Forms.Button();
            this.btnWednesday = new System.Windows.Forms.Button();
            this.btnTuesday = new System.Windows.Forms.Button();
            this.btnMonday = new System.Windows.Forms.Button();
            this.btnNextWeek = new System.Windows.Forms.Button();
            this.btnPrevWeek = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pnlPoints.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            this.dataGridView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dataGridView1.Location = new System.Drawing.Point(0, 0);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(322, 424);
            this.dataGridView1.TabIndex = 0;
            // 
            // Column1
            // 
            this.Column1.DataPropertyName = "Date";
            this.Column1.HeaderText = "Date";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            this.Column1.Width = 55;
            // 
            // Column2
            // 
            this.Column2.DataPropertyName = "Time";
            this.Column2.HeaderText = "Time";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 55;
            // 
            // Column3
            // 
            this.Column3.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
            this.Column3.DataPropertyName = "What";
            this.Column3.HeaderText = "Details";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dataGridView1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlPoints);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(904, 424);
            this.splitContainer1.SplitterDistance = 322;
            this.splitContainer1.TabIndex = 1;
            // 
            // pnlPoints
            // 
            this.pnlPoints.Controls.Add(this.vScrollBar1);
            this.pnlPoints.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlPoints.Location = new System.Drawing.Point(0, 106);
            this.pnlPoints.Name = "pnlPoints";
            this.pnlPoints.Size = new System.Drawing.Size(578, 318);
            this.pnlPoints.TabIndex = 1;
            this.pnlPoints.Resize += new System.EventHandler(this.pnlPoints_Resize);
            // 
            // vScrollBar1
            // 
            this.vScrollBar1.Dock = System.Windows.Forms.DockStyle.Right;
            this.vScrollBar1.Location = new System.Drawing.Point(561, 0);
            this.vScrollBar1.Name = "vScrollBar1";
            this.vScrollBar1.Size = new System.Drawing.Size(17, 318);
            this.vScrollBar1.TabIndex = 0;
            this.vScrollBar1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.vScrollBar1_Scroll);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblCurrentDay);
            this.panel1.Controls.Add(this.btnFriday);
            this.panel1.Controls.Add(this.btnSunday);
            this.panel1.Controls.Add(this.btnSaturday);
            this.panel1.Controls.Add(this.btnThursday);
            this.panel1.Controls.Add(this.btnWednesday);
            this.panel1.Controls.Add(this.btnTuesday);
            this.panel1.Controls.Add(this.btnMonday);
            this.panel1.Controls.Add(this.btnNextWeek);
            this.panel1.Controls.Add(this.btnPrevWeek);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(578, 106);
            this.panel1.TabIndex = 0;
            // 
            // lblCurrentDay
            // 
            this.lblCurrentDay.AutoSize = true;
            this.lblCurrentDay.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentDay.Location = new System.Drawing.Point(2, 65);
            this.lblCurrentDay.Name = "lblCurrentDay";
            this.lblCurrentDay.Size = new System.Drawing.Size(73, 37);
            this.lblCurrentDay.TabIndex = 9;
            this.lblCurrentDay.Text = "Day";
            // 
            // btnFriday
            // 
            this.btnFriday.Location = new System.Drawing.Point(328, 3);
            this.btnFriday.Name = "btnFriday";
            this.btnFriday.Size = new System.Drawing.Size(65, 60);
            this.btnFriday.TabIndex = 8;
            this.btnFriday.Tag = "Friday\r\n{0}\r\n{1}";
            this.btnFriday.Text = "Friday\r\n5/2/3";
            this.btnFriday.UseVisualStyleBackColor = true;
            this.btnFriday.Click += new System.EventHandler(this.btnFriday_Click);
            // 
            // btnSunday
            // 
            this.btnSunday.Location = new System.Drawing.Point(472, 3);
            this.btnSunday.Name = "btnSunday";
            this.btnSunday.Size = new System.Drawing.Size(65, 60);
            this.btnSunday.TabIndex = 6;
            this.btnSunday.Tag = "Sunday\r\n{0}\r\n{1}";
            this.btnSunday.Text = "Sunday\r\n7/2/3";
            this.btnSunday.UseVisualStyleBackColor = true;
            this.btnSunday.Click += new System.EventHandler(this.btnSunday_Click);
            // 
            // btnSaturday
            // 
            this.btnSaturday.Location = new System.Drawing.Point(400, 3);
            this.btnSaturday.Name = "btnSaturday";
            this.btnSaturday.Size = new System.Drawing.Size(65, 60);
            this.btnSaturday.TabIndex = 7;
            this.btnSaturday.Tag = "Saturday\r\n{0}\r\n{1}";
            this.btnSaturday.Text = "Saturday\r\n6/2/3";
            this.btnSaturday.UseVisualStyleBackColor = true;
            this.btnSaturday.Click += new System.EventHandler(this.btnSaturday_Click);
            // 
            // btnThursday
            // 
            this.btnThursday.Location = new System.Drawing.Point(256, 3);
            this.btnThursday.Name = "btnThursday";
            this.btnThursday.Size = new System.Drawing.Size(65, 60);
            this.btnThursday.TabIndex = 5;
            this.btnThursday.Tag = "Thursday\r\n{0}\r\n{1}";
            this.btnThursday.Text = "Thursday\r\n4/2/3";
            this.btnThursday.UseVisualStyleBackColor = true;
            this.btnThursday.Click += new System.EventHandler(this.btnThursday_Click);
            // 
            // btnWednesday
            // 
            this.btnWednesday.Location = new System.Drawing.Point(184, 3);
            this.btnWednesday.Name = "btnWednesday";
            this.btnWednesday.Size = new System.Drawing.Size(65, 60);
            this.btnWednesday.TabIndex = 4;
            this.btnWednesday.Tag = "Wed\r\n{0}\r\n{1}";
            this.btnWednesday.Text = "Wed\r\n3/2/3";
            this.btnWednesday.UseVisualStyleBackColor = true;
            this.btnWednesday.Click += new System.EventHandler(this.btnWednesday_Click);
            // 
            // btnTuesday
            // 
            this.btnTuesday.Location = new System.Drawing.Point(112, 3);
            this.btnTuesday.Name = "btnTuesday";
            this.btnTuesday.Size = new System.Drawing.Size(65, 60);
            this.btnTuesday.TabIndex = 3;
            this.btnTuesday.Tag = "Tuesday\r\n{0}\r\n{1}";
            this.btnTuesday.Text = "Tuesday\r\n2/2/3";
            this.btnTuesday.UseVisualStyleBackColor = true;
            this.btnTuesday.Click += new System.EventHandler(this.btnTuesday_Click);
            // 
            // btnMonday
            // 
            this.btnMonday.Location = new System.Drawing.Point(40, 3);
            this.btnMonday.Name = "btnMonday";
            this.btnMonday.Size = new System.Drawing.Size(65, 60);
            this.btnMonday.TabIndex = 2;
            this.btnMonday.Tag = "Monday\r\n{0}\r\n{1}";
            this.btnMonday.Text = "Monday\r\n1/2/3";
            this.btnMonday.UseVisualStyleBackColor = true;
            this.btnMonday.Click += new System.EventHandler(this.btnMonday_Click);
            // 
            // btnNextWeek
            // 
            this.btnNextWeek.Location = new System.Drawing.Point(544, 3);
            this.btnNextWeek.Name = "btnNextWeek";
            this.btnNextWeek.Size = new System.Drawing.Size(30, 61);
            this.btnNextWeek.TabIndex = 1;
            this.btnNextWeek.Text = ">";
            this.btnNextWeek.UseVisualStyleBackColor = true;
            this.btnNextWeek.Click += new System.EventHandler(this.btnNextWeek_Click);
            // 
            // btnPrevWeek
            // 
            this.btnPrevWeek.Location = new System.Drawing.Point(3, 3);
            this.btnPrevWeek.Name = "btnPrevWeek";
            this.btnPrevWeek.Size = new System.Drawing.Size(30, 61);
            this.btnPrevWeek.TabIndex = 0;
            this.btnPrevWeek.Text = "<";
            this.btnPrevWeek.UseVisualStyleBackColor = true;
            this.btnPrevWeek.Click += new System.EventHandler(this.btnPrevWeek_Click);
            // 
            // TimeRecordsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(904, 424);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(920, 140);
            this.Name = "TimeRecordsForm";
            this.Text = "TimeTracker History";
            this.Shown += new System.EventHandler(this.TimeRecordsForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.pnlPoints.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel pnlPoints;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblCurrentDay;
        private System.Windows.Forms.Button btnFriday;
        private System.Windows.Forms.Button btnSunday;
        private System.Windows.Forms.Button btnSaturday;
        private System.Windows.Forms.Button btnThursday;
        private System.Windows.Forms.Button btnWednesday;
        private System.Windows.Forms.Button btnTuesday;
        private System.Windows.Forms.Button btnMonday;
        private System.Windows.Forms.Button btnNextWeek;
        private System.Windows.Forms.Button btnPrevWeek;
        private System.Windows.Forms.VScrollBar vScrollBar1;

    }
}