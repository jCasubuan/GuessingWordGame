namespace GuessingGameDesktop
{
    partial class frmMainMenu
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
            btnLogin = new Button();
            hdrMenu = new Label();
            btnRegister = new Button();
            btnLeaderboards = new Button();
            btnAbout = new Button();
            btnExit = new Button();
            SuspendLayout();
            // 
            // btnLogin
            // 
            btnLogin.Location = new Point(192, 127);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(214, 54);
            btnLogin.TabIndex = 0;
            btnLogin.Text = "Login";
            btnLogin.UseVisualStyleBackColor = true;
            btnLogin.Click += btnLogin_Click;
            // 
            // hdrMenu
            // 
            hdrMenu.AutoSize = true;
            hdrMenu.Font = new Font("Segoe UI", 19.8000011F, FontStyle.Bold, GraphicsUnit.Point, 0);
            hdrMenu.Location = new Point(192, 21);
            hdrMenu.Name = "hdrMenu";
            hdrMenu.Size = new Size(204, 46);
            hdrMenu.TabIndex = 1;
            hdrMenu.Text = "Main Menu";
            // 
            // btnRegister
            // 
            btnRegister.Location = new Point(192, 224);
            btnRegister.Name = "btnRegister";
            btnRegister.Size = new Size(214, 54);
            btnRegister.TabIndex = 2;
            btnRegister.Text = "Register";
            btnRegister.UseVisualStyleBackColor = true;
            // 
            // btnLeaderboards
            // 
            btnLeaderboards.Location = new Point(192, 324);
            btnLeaderboards.Name = "btnLeaderboards";
            btnLeaderboards.Size = new Size(214, 54);
            btnLeaderboards.TabIndex = 3;
            btnLeaderboards.Text = "Leaderboards";
            btnLeaderboards.UseVisualStyleBackColor = true;
            // 
            // btnAbout
            // 
            btnAbout.Location = new Point(192, 426);
            btnAbout.Name = "btnAbout";
            btnAbout.Size = new Size(214, 54);
            btnAbout.TabIndex = 4;
            btnAbout.Text = "About";
            btnAbout.UseVisualStyleBackColor = true;
            // 
            // btnExit
            // 
            btnExit.Location = new Point(192, 528);
            btnExit.Name = "btnExit";
            btnExit.Size = new Size(214, 54);
            btnExit.TabIndex = 5;
            btnExit.Text = "Exit";
            btnExit.UseVisualStyleBackColor = true;
            btnExit.Click += btnExit_Click;
            // 
            // frmMainMenu
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(608, 705);
            Controls.Add(btnExit);
            Controls.Add(btnAbout);
            Controls.Add(btnLeaderboards);
            Controls.Add(btnRegister);
            Controls.Add(hdrMenu);
            Controls.Add(btnLogin);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmMainMenu";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Menu";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnLogin;
        private Label hdrMenu;
        private Button btnRegister;
        private Button btnLeaderboards;
        private Button btnAbout;
        private Button btnExit;
    }
}