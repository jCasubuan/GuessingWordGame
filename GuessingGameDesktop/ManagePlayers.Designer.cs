namespace GuessingGameDesktop
{
    partial class frmManagePlayers
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
            dgvPlayers = new DataGridView();
            btnBack = new Button();
            btnViewPlayers = new Button();
            btnPlayerStats = new Button();
            btnDeletePlayer = new Button();
            txtSearchQuery = new TextBox();
            btnApplyFilter = new Button();
            btnClearFilter = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvPlayers).BeginInit();
            SuspendLayout();
            // 
            // dgvPlayers
            // 
            dgvPlayers.BackgroundColor = SystemColors.Control;
            dgvPlayers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvPlayers.Location = new Point(30, 92);
            dgvPlayers.Name = "dgvPlayers";
            dgvPlayers.RowHeadersWidth = 51;
            dgvPlayers.Size = new Size(549, 473);
            dgvPlayers.TabIndex = 0;
            // 
            // btnBack
            // 
            btnBack.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBack.ForeColor = SystemColors.ControlText;
            btnBack.Location = new Point(30, 635);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(111, 34);
            btnBack.TabIndex = 7;
            btnBack.Text = "< Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // btnViewPlayers
            // 
            btnViewPlayers.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnViewPlayers.ForeColor = SystemColors.ControlText;
            btnViewPlayers.Location = new Point(185, 620);
            btnViewPlayers.Name = "btnViewPlayers";
            btnViewPlayers.Size = new Size(112, 49);
            btnViewPlayers.TabIndex = 8;
            btnViewPlayers.Text = "View Players";
            btnViewPlayers.UseVisualStyleBackColor = true;
            btnViewPlayers.Click += btnViewPlayers_Click;
            // 
            // btnPlayerStats
            // 
            btnPlayerStats.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnPlayerStats.ForeColor = SystemColors.ControlText;
            btnPlayerStats.Location = new Point(329, 620);
            btnPlayerStats.Name = "btnPlayerStats";
            btnPlayerStats.Size = new Size(111, 49);
            btnPlayerStats.TabIndex = 9;
            btnPlayerStats.Text = "View Player Stats";
            btnPlayerStats.UseVisualStyleBackColor = true;
            btnPlayerStats.Click += btnPlayerStats_Click;
            // 
            // btnDeletePlayer
            // 
            btnDeletePlayer.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDeletePlayer.ForeColor = SystemColors.ControlText;
            btnDeletePlayer.Location = new Point(468, 620);
            btnDeletePlayer.Name = "btnDeletePlayer";
            btnDeletePlayer.Size = new Size(111, 49);
            btnDeletePlayer.TabIndex = 10;
            btnDeletePlayer.Text = "Delete Player";
            btnDeletePlayer.UseVisualStyleBackColor = true;
            btnDeletePlayer.Click += btnDeletePlayer_Click_1;
            // 
            // txtSearchQuery
            // 
            txtSearchQuery.Font = new Font("Segoe UI", 10.2F, FontStyle.Regular, GraphicsUnit.Point, 0);
            txtSearchQuery.Location = new Point(30, 37);
            txtSearchQuery.Multiline = true;
            txtSearchQuery.Name = "txtSearchQuery";
            txtSearchQuery.Size = new Size(309, 42);
            txtSearchQuery.TabIndex = 11;
            // 
            // btnApplyFilter
            // 
            btnApplyFilter.Location = new Point(345, 37);
            btnApplyFilter.Name = "btnApplyFilter";
            btnApplyFilter.Size = new Size(114, 42);
            btnApplyFilter.TabIndex = 12;
            btnApplyFilter.Text = "Search 🔍";
            btnApplyFilter.UseVisualStyleBackColor = true;
            btnApplyFilter.Click += btnSearch_Click;
            // 
            // btnClearFilter
            // 
            btnClearFilter.Location = new Point(465, 37);
            btnClearFilter.Name = "btnClearFilter";
            btnClearFilter.Size = new Size(114, 42);
            btnClearFilter.TabIndex = 13;
            btnClearFilter.Text = "Clear ";
            btnClearFilter.UseVisualStyleBackColor = true;
            btnClearFilter.Click += btnClearFilter_Click;
            // 
            // frmManagePlayers
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(608, 705);
            Controls.Add(btnClearFilter);
            Controls.Add(btnApplyFilter);
            Controls.Add(txtSearchQuery);
            Controls.Add(btnDeletePlayer);
            Controls.Add(btnPlayerStats);
            Controls.Add(btnViewPlayers);
            Controls.Add(btnBack);
            Controls.Add(dgvPlayers);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmManagePlayers";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manage Players";
            ((System.ComponentModel.ISupportInitialize)dgvPlayers).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvPlayers;
        private Button btnBack;
        private Button btnViewPlayers;
        private Button btnPlayerStats;
        private Button btnDeletePlayer;
        private TextBox txtSearchQuery;
        private Button btnApplyFilter;
        private Button btnClearFilter;
    }
}