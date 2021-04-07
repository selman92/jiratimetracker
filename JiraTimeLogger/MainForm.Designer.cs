namespace JiraTimeLogger
{
    partial class MainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.BtnStartTracking = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.TxtComment = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.TxtApiToken = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.LblElapsedTime = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.BtnSubmit = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.BtnReset = new System.Windows.Forms.Button();
            this.TxtIssueId = new System.Windows.Forms.ComboBox();
            this.LblStatus = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.CmbWorktypes = new System.Windows.Forms.ComboBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // BtnStartTracking
            // 
            this.BtnStartTracking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnStartTracking.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnStartTracking.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnStartTracking.Location = new System.Drawing.Point(8, 188);
            this.BtnStartTracking.Name = "BtnStartTracking";
            this.BtnStartTracking.Size = new System.Drawing.Size(114, 33);
            this.BtnStartTracking.TabIndex = 7;
            this.BtnStartTracking.Text = "Start Tracking";
            this.BtnStartTracking.UseVisualStyleBackColor = true;
            this.BtnStartTracking.Click += new System.EventHandler(this.BtnStartTrackingClick);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 84F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 400F));
            this.tableLayoutPanel1.Controls.Add(this.TxtComment, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.TxtApiToken, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.BtnStartTracking, 0, 6);
            this.tableLayoutPanel1.Controls.Add(this.label3, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.LblElapsedTime, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.BtnSubmit, 1, 6);
            this.tableLayoutPanel1.Controls.Add(this.label5, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.BtnReset, 2, 6);
            this.tableLayoutPanel1.Controls.Add(this.TxtIssueId, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.LblStatus, 1, 5);
            this.tableLayoutPanel1.Controls.Add(this.label6, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.label4, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.CmbWorktypes, 1, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(5);
            this.tableLayoutPanel1.RowCount = 7;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 31F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(614, 229);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // TxtComment
            // 
            this.TxtComment.AutoCompleteCustomSource.AddRange(new string[] {
            "NS-100",
            "NS-125",
            "NS-235",
            "NS-100",
            ""});
            this.TxtComment.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.TxtComment.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.CustomSource;
            this.tableLayoutPanel1.SetColumnSpan(this.TxtComment, 2);
            this.TxtComment.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtComment.Location = new System.Drawing.Point(128, 68);
            this.TxtComment.Name = "TxtComment";
            this.TxtComment.Size = new System.Drawing.Size(478, 23);
            this.TxtComment.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label1.Location = new System.Drawing.Point(8, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "API Token:";
            // 
            // TxtApiToken
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.TxtApiToken, 2);
            this.TxtApiToken.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtApiToken.Location = new System.Drawing.Point(128, 8);
            this.TxtApiToken.Name = "TxtApiToken";
            this.TxtApiToken.Size = new System.Drawing.Size(478, 23);
            this.TxtApiToken.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label3.Location = new System.Drawing.Point(8, 125);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(39, 17);
            this.label3.TabIndex = 5;
            this.label3.Text = "Time:";
            // 
            // LblElapsedTime
            // 
            this.LblElapsedTime.AutoSize = true;
            this.LblElapsedTime.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblElapsedTime.Location = new System.Drawing.Point(128, 125);
            this.LblElapsedTime.Name = "LblElapsedTime";
            this.LblElapsedTime.Size = new System.Drawing.Size(63, 20);
            this.LblElapsedTime.TabIndex = 6;
            this.LblElapsedTime.Text = "00:00:00";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label2.Location = new System.Drawing.Point(8, 35);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(98, 17);
            this.label2.TabIndex = 3;
            this.label2.Text = "Issue Key or ID:";
            // 
            // BtnSubmit
            // 
            this.BtnSubmit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnSubmit.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BtnSubmit.Enabled = false;
            this.BtnSubmit.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnSubmit.Location = new System.Drawing.Point(128, 188);
            this.BtnSubmit.Name = "BtnSubmit";
            this.BtnSubmit.Size = new System.Drawing.Size(78, 33);
            this.BtnSubmit.TabIndex = 8;
            this.BtnSubmit.Text = "Submit";
            this.BtnSubmit.UseVisualStyleBackColor = true;
            this.BtnSubmit.Click += new System.EventHandler(this.BtnSubmit_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label5.Location = new System.Drawing.Point(8, 65);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(77, 17);
            this.label5.TabIndex = 13;
            this.label5.Text = "Description:";
            // 
            // BtnReset
            // 
            this.BtnReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.BtnReset.Dock = System.Windows.Forms.DockStyle.Left;
            this.BtnReset.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.BtnReset.Location = new System.Drawing.Point(212, 188);
            this.BtnReset.Name = "BtnReset";
            this.BtnReset.Size = new System.Drawing.Size(99, 33);
            this.BtnReset.TabIndex = 15;
            this.BtnReset.Text = "Reset Time";
            this.BtnReset.UseVisualStyleBackColor = true;
            this.BtnReset.Click += new System.EventHandler(this.BtnReset_Click);
            // 
            // TxtIssueId
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.TxtIssueId, 2);
            this.TxtIssueId.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TxtIssueId.FormattingEnabled = true;
            this.TxtIssueId.Location = new System.Drawing.Point(128, 38);
            this.TxtIssueId.Name = "TxtIssueId";
            this.TxtIssueId.Size = new System.Drawing.Size(478, 23);
            this.TxtIssueId.TabIndex = 16;
            // 
            // LblStatus
            // 
            this.LblStatus.AutoSize = true;
            this.tableLayoutPanel1.SetColumnSpan(this.LblStatus, 2);
            this.LblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LblStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.LblStatus.Location = new System.Drawing.Point(128, 155);
            this.LblStatus.Name = "LblStatus";
            this.LblStatus.Size = new System.Drawing.Size(478, 30);
            this.LblStatus.TabIndex = 12;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label6.Location = new System.Drawing.Point(8, 155);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 17);
            this.label6.TabIndex = 15;
            this.label6.Text = "Status:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.label4.Location = new System.Drawing.Point(8, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(72, 17);
            this.label4.TabIndex = 17;
            this.label4.Text = "Work Type:";
            // 
            // CmbWorktypes
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.CmbWorktypes, 2);
            this.CmbWorktypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CmbWorktypes.FormattingEnabled = true;
            this.CmbWorktypes.Location = new System.Drawing.Point(128, 98);
            this.CmbWorktypes.Name = "CmbWorktypes";
            this.CmbWorktypes.Size = new System.Drawing.Size(478, 23);
            this.CmbWorktypes.TabIndex = 18;
            // 
            // MainForm
            // 
            this.AcceptButton = this.BtnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 229);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(630, 311);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Jira Time Tracker";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BtnSt;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox TxtApiToken;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button BtnStartTracking;
        private System.Windows.Forms.Label LblElapsedTime;
        private System.Windows.Forms.Button BtnSubmit;
        private System.Windows.Forms.Label LblStatus;
        private System.Windows.Forms.TextBox TxtComment;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button BtnReset;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox TxtIssueId;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ComboBox CmbWorktypes;
	}
}

