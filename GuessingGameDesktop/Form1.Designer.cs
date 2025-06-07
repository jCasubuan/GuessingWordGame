namespace GuessingGameDesktop
{
    partial class frmLogin
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
            hdrLogin = new Label();
            lblUserName = new Label();
            lblPassword = new Label();
            txtUserName = new TextBox();
            txtPassword = new TextBox();
            btnLogin = new Button();
            btnBack = new Button();
            SuspendLayout();
            // 
            // hdrLogin
            // 
            hdrLogin.AutoSize = true;
            hdrLogin.Font = new Font("Segoe UI", 28.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            hdrLogin.Location = new Point(21, 27);
            hdrLogin.Name = "hdrLogin";
            hdrLogin.Size = new Size(150, 62);
            hdrLogin.TabIndex = 0;
            hdrLogin.Text = "Login";
            // 
            // lblUserName
            // 
            lblUserName.AutoSize = true;
            lblUserName.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblUserName.Location = new Point(70, 201);
            lblUserName.Name = "lblUserName";
            lblUserName.Size = new Size(78, 20);
            lblUserName.TabIndex = 1;
            lblUserName.Text = "Username:";
            // 
            // lblPassword
            // 
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(70, 327);
            lblPassword.Name = "lblPassword";
            lblPassword.Size = new Size(73, 20);
            lblPassword.TabIndex = 2;
            lblPassword.Text = "Password:";
            // 
            // txtUserName
            // 
            txtUserName.Location = new Point(172, 194);
            txtUserName.Name = "txtUserName";
            txtUserName.Size = new Size(310, 27);
            txtUserName.TabIndex = 3;
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(172, 320);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(310, 27);
            txtPassword.TabIndex = 4;
            txtPassword.UseSystemPasswordChar = true;
            // 
            // btnLogin
            // 
            btnLogin.Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0); 
            btnLogin.Location = new Point(172, 481);
            btnLogin.Name = "btnLogin";
            btnLogin.Size = new Size(254, 67); 
            btnLogin.TabIndex = 5;
            btnLogin.Text = "Login";
            btnLogin.BackColor = Color.FromArgb(0, 119, 212);
            btnLogin.ForeColor = Color.White;
            btnLogin.UseVisualStyleBackColor = false; 
            btnLogin.FlatStyle = FlatStyle.Flat;
            btnLogin.FlatAppearance.BorderSize = 0;
            btnLogin.Click += btnLogin_Click;
            // 
            // btnBack
            // 
            btnBack.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnBack.Location = new Point(32, 612);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(111, 34);
            btnBack.TabIndex = 6;
            btnBack.Text = "< BACK";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // frmLogin
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(608, 705);
            Controls.Add(btnBack);
            Controls.Add(btnLogin);
            Controls.Add(txtPassword);
            Controls.Add(txtUserName);
            Controls.Add(lblPassword);
            Controls.Add(lblUserName);
            Controls.Add(hdrLogin);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmLogin";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Login";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label hdrLogin;
        private Label lblUserName;
        private Label lblPassword;
        private TextBox txtUserName;
        private TextBox txtPassword;
        private Button btnLogin;
        private Button btnBack;
    }
}
