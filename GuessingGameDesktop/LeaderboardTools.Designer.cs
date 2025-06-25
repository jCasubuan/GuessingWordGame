namespace GuessingGameDesktop
{
    partial class frmLeaderboardTools
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
            btnBack = new Button();
            dgvLeaderboard = new DataGridView();
            btnViewLeaderboard = new Button();
            txtSearchQuery = new TextBox();
            btnApplyFilter = new Button();
            btnClearFilter = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvLeaderboard).BeginInit();
            SuspendLayout();
            // 
            // btnBack
            // 
            btnBack.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBack.ForeColor = SystemColors.ControlText;
            btnBack.Location = new Point(30, 635);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(111, 34);
            btnBack.TabIndex = 8;
            btnBack.Text = "< Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // dgvLeaderboard
            // 
            dgvLeaderboard.BackgroundColor = SystemColors.Control;
            dgvLeaderboard.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvLeaderboard.Location = new Point(30, 92);
            dgvLeaderboard.Name = "dgvLeaderboard";
            dgvLeaderboard.RowHeadersWidth = 51;
            dgvLeaderboard.Size = new Size(549, 473);
            dgvLeaderboard.TabIndex = 9;
            // 
            // btnViewLeaderboard
            // 
            btnViewLeaderboard.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnViewLeaderboard.ForeColor = SystemColors.ControlText;
            btnViewLeaderboard.Location = new Point(241, 620);
            btnViewLeaderboard.Name = "btnViewLeaderboard";
            btnViewLeaderboard.Size = new Size(135, 49);
            btnViewLeaderboard.TabIndex = 10;
            btnViewLeaderboard.Text = "View Leaderboard";
            btnViewLeaderboard.UseVisualStyleBackColor = true;
            btnViewLeaderboard.Click += btnViewLeaderboard_Click;
            // 
            // txtSearchQuery
            // 
            txtSearchQuery.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSearchQuery.Location = new Point(30, 37);
            txtSearchQuery.Multiline = true;
            txtSearchQuery.Name = "txtSearchQuery";
            txtSearchQuery.Size = new Size(309, 42);
            txtSearchQuery.TabIndex = 12;
            // 
            // btnApplyFilter
            // 
            btnApplyFilter.Location = new Point(345, 37);
            btnApplyFilter.Name = "btnApplyFilter";
            btnApplyFilter.Size = new Size(114, 42);
            btnApplyFilter.TabIndex = 13;
            btnApplyFilter.Text = "Search 🔍";
            btnApplyFilter.UseVisualStyleBackColor = true;
            btnApplyFilter.Click += btnApplyFilter_Click;
            // 
            // btnClearFilter
            // 
            btnClearFilter.Location = new Point(465, 37);
            btnClearFilter.Name = "btnClearFilter";
            btnClearFilter.Size = new Size(114, 42);
            btnClearFilter.TabIndex = 14;
            btnClearFilter.Text = "Clear ";
            btnClearFilter.UseVisualStyleBackColor = true;
            btnClearFilter.Click += btnClearFilter_Click;
            // 
            // frmLeaderboardTools
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(608, 705);
            Controls.Add(btnClearFilter);
            Controls.Add(btnApplyFilter);
            Controls.Add(txtSearchQuery);
            Controls.Add(btnViewLeaderboard);
            Controls.Add(dgvLeaderboard);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmLeaderboardTools";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Leaderboard";
            ((System.ComponentModel.ISupportInitialize)dgvLeaderboard).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnBack;
        private DataGridView dgvLeaderboard;
        private Button btnViewLeaderboard;
        private TextBox txtSearchQuery;
        private Button btnApplyFilter;
        private Button btnClearFilter;
    }
}