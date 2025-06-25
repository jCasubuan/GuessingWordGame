namespace GuessingGameDesktop
{
    partial class frmAddEditWord
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
            txtWord = new TextBox();
            lblWord = new Label();
            lblHint = new Label();
            lblDifficulty = new Label();
            txtHint = new TextBox();
            cmbDifficulty = new ComboBox();
            btnCancel = new Button();
            btnSave = new Button();
            SuspendLayout();
            // 
            // btnBack
            // 
            btnBack.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnBack.ForeColor = SystemColors.ControlText;
            btnBack.Location = new Point(26, 392);
            btnBack.Name = "btnBack";
            btnBack.Size = new Size(111, 34);
            btnBack.TabIndex = 8;
            btnBack.Text = "< Back";
            btnBack.UseVisualStyleBackColor = true;
            btnBack.Click += btnBack_Click;
            // 
            // txtWord
            // 
            txtWord.Location = new Point(211, 101);
            txtWord.Name = "txtWord";
            txtWord.Size = new Size(264, 27);
            txtWord.TabIndex = 9;
            // 
            // lblWord
            // 
            lblWord.AutoSize = true;
            lblWord.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblWord.Location = new Point(97, 101);
            lblWord.Name = "lblWord";
            lblWord.Size = new Size(60, 23);
            lblWord.TabIndex = 10;
            lblWord.Text = "Word:";
            // 
            // lblHint
            // 
            lblHint.AutoSize = true;
            lblHint.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblHint.Location = new Point(97, 185);
            lblHint.Name = "lblHint";
            lblHint.Size = new Size(50, 23);
            lblHint.TabIndex = 11;
            lblHint.Text = "Hint:";
            // 
            // lblDifficulty
            // 
            lblDifficulty.AutoSize = true;
            lblDifficulty.Font = new Font("Segoe UI", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lblDifficulty.Location = new Point(97, 274);
            lblDifficulty.Name = "lblDifficulty";
            lblDifficulty.Size = new Size(91, 23);
            lblDifficulty.TabIndex = 12;
            lblDifficulty.Text = "Difficulty:";
            // 
            // txtHint
            // 
            txtHint.Location = new Point(211, 185);
            txtHint.Multiline = true;
            txtHint.Name = "txtHint";
            txtHint.Size = new Size(264, 27);
            txtHint.TabIndex = 13;
            // 
            // cmbDifficulty
            // 
            cmbDifficulty.FormattingEnabled = true;
            cmbDifficulty.Location = new Point(211, 269);
            cmbDifficulty.Name = "cmbDifficulty";
            cmbDifficulty.Size = new Size(264, 28);
            cmbDifficulty.TabIndex = 14;
            // 
            // btnCancel
            // 
            btnCancel.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnCancel.Location = new Point(311, 392);
            btnCancel.Name = "btnCancel";
            btnCancel.Size = new Size(94, 29);
            btnCancel.TabIndex = 15;
            btnCancel.Text = "Cancel";
            btnCancel.UseVisualStyleBackColor = true;
            btnCancel.Click += btnCancel_Click;
            // 
            // btnSave
            // 
            btnSave.Font = new Font("Segoe UI", 9F, FontStyle.Bold, GraphicsUnit.Point, 0);
            btnSave.Location = new Point(441, 392);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(94, 29);
            btnSave.TabIndex = 16;
            btnSave.Text = "Save";
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // frmAddEditWord
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(608, 457);
            Controls.Add(btnSave);
            Controls.Add(btnCancel);
            Controls.Add(cmbDifficulty);
            Controls.Add(txtHint);
            Controls.Add(lblDifficulty);
            Controls.Add(lblHint);
            Controls.Add(lblWord);
            Controls.Add(txtWord);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmAddEditWord";
            StartPosition = FormStartPosition.CenterScreen;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnBack;
        private TextBox txtWord;
        private Label lblWord;
        private Label lblHint;
        private Label lblDifficulty;
        private TextBox txtHint;
        private ComboBox cmbDifficulty;
        private Button btnCancel;
        private Button btnSave;
    }
}