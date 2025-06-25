namespace GuessingGameDesktop
{
    partial class frmAdminMenu
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
            hdrAdminMenu = new Label();
            btnLogout = new Button();
            btnManagePlayers = new Button();
            btnManageWords = new Button();
            btnLeaderboardTools = new Button();
            SuspendLayout();
            // 
            // hdrAdminMenu
            // 
            hdrAdminMenu.AutoSize = true;
            hdrAdminMenu.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            hdrAdminMenu.Location = new Point(185, 40);
            hdrAdminMenu.Name = "hdrAdminMenu";
            hdrAdminMenu.Size = new Size(229, 46);
            hdrAdminMenu.TabIndex = 0;
            hdrAdminMenu.Text = "Admin Menu";
            // 
            // btnLogout
            // 
            btnLogout.Location = new Point(29, 642);
            btnLogout.Name = "btnLogout";
            btnLogout.Size = new Size(95, 36);
            btnLogout.TabIndex = 4;
            btnLogout.Text = "Log out";
            btnLogout.UseVisualStyleBackColor = true;
            btnLogout.Click += btnLogout_Click;
            // 
            // btnManagePlayers
            // 
            btnManagePlayers.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnManagePlayers.Location = new Point(205, 152);
            btnManagePlayers.Name = "btnManagePlayers";
            btnManagePlayers.Size = new Size(184, 64);
            btnManagePlayers.TabIndex = 5;
            btnManagePlayers.Text = "Manage Players";
            btnManagePlayers.UseVisualStyleBackColor = true;
            btnManagePlayers.Click += btnManagePlayers_Click;
            // 
            // btnManageWords
            // 
            btnManageWords.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnManageWords.Location = new Point(205, 278);
            btnManageWords.Name = "btnManageWords";
            btnManageWords.Size = new Size(184, 64);
            btnManageWords.TabIndex = 6;
            btnManageWords.Text = "Manage Words";
            btnManageWords.UseVisualStyleBackColor = true;
            btnManageWords.Click += btnManageWords_Click;
            // 
            // btnLeaderboardTools
            // 
            btnLeaderboardTools.Font = new Font("Segoe UI", 10.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnLeaderboardTools.Location = new Point(205, 405);
            btnLeaderboardTools.Name = "btnLeaderboardTools";
            btnLeaderboardTools.Size = new Size(184, 64);
            btnLeaderboardTools.TabIndex = 7;
            btnLeaderboardTools.Text = "Leaderboard Tools";
            btnLeaderboardTools.UseVisualStyleBackColor = true;
            btnLeaderboardTools.Click += btnLeaderboardTools_Click;
            // 
            // frmAdminMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(608, 705);
            Controls.Add(btnLeaderboardTools);
            Controls.Add(btnManageWords);
            Controls.Add(btnManagePlayers);
            Controls.Add(btnLogout);
            Controls.Add(hdrAdminMenu);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmAdminMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Admin Menu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label hdrAdminMenu;
        private Button btnLogout;
        private Button btnManagePlayers;
        private Button btnManageWords;
        private Button btnLeaderboardTools;
    }
}