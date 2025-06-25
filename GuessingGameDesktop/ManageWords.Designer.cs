namespace GuessingGameDesktop
{
    partial class frmManageWords
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
            dgvWords = new DataGridView();
            txtSearchQuery = new TextBox();
            btnSearchWord = new Button();
            btnClearFilter = new Button();
            btnViewWords = new Button();
            btnAddWord = new Button();
            btnUpdateWord = new Button();
            btnDeleteWord = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvWords).BeginInit();
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
            // dgvWords
            // 
            dgvWords.BackgroundColor = SystemColors.Control;
            dgvWords.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvWords.Location = new Point(30, 92);
            dgvWords.Name = "dgvWords";
            dgvWords.RowHeadersWidth = 51;
            dgvWords.Size = new Size(549, 456);
            dgvWords.TabIndex = 9;
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
            // btnSearchWord
            // 
            btnSearchWord.Location = new Point(345, 37);
            btnSearchWord.Name = "btnSearchWord";
            btnSearchWord.Size = new Size(114, 42);
            btnSearchWord.TabIndex = 13;
            btnSearchWord.Text = "Search 🔍";
            btnSearchWord.UseVisualStyleBackColor = true;
            btnSearchWord.Click += btnSearchWord_Click;
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
            // btnViewWords
            // 
            btnViewWords.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnViewWords.ForeColor = SystemColors.ControlText;
            btnViewWords.Location = new Point(29, 568);
            btnViewWords.Name = "btnViewWords";
            btnViewWords.Size = new Size(112, 49);
            btnViewWords.TabIndex = 15;
            btnViewWords.Text = "View Words";
            btnViewWords.UseVisualStyleBackColor = true;
            btnViewWords.Click += btnViewWords_Click;
            // 
            // btnAddWord
            // 
            btnAddWord.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnAddWord.ForeColor = SystemColors.ControlText;
            btnAddWord.Location = new Point(173, 568);
            btnAddWord.Name = "btnAddWord";
            btnAddWord.Size = new Size(112, 49);
            btnAddWord.TabIndex = 16;
            btnAddWord.Text = "Add Word";
            btnAddWord.UseVisualStyleBackColor = true;
            btnAddWord.Click += btnAddWord_Click;
            // 
            // btnUpdateWord
            // 
            btnUpdateWord.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnUpdateWord.ForeColor = SystemColors.ControlText;
            btnUpdateWord.Location = new Point(317, 568);
            btnUpdateWord.Name = "btnUpdateWord";
            btnUpdateWord.Size = new Size(112, 49);
            btnUpdateWord.TabIndex = 17;
            btnUpdateWord.Text = "Update Word";
            btnUpdateWord.UseVisualStyleBackColor = true;
            btnUpdateWord.Click += btnUpdateWord_Click;
            // 
            // btnDeleteWord
            // 
            btnDeleteWord.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnDeleteWord.ForeColor = SystemColors.ControlText;
            btnDeleteWord.Location = new Point(467, 568);
            btnDeleteWord.Name = "btnDeleteWord";
            btnDeleteWord.Size = new Size(112, 49);
            btnDeleteWord.TabIndex = 18;
            btnDeleteWord.Text = "Delete Word";
            btnDeleteWord.UseVisualStyleBackColor = true;
            btnDeleteWord.Click += btnDeleteWord_Click;
            // 
            // frmManageWords
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(608, 705);
            Controls.Add(btnDeleteWord);
            Controls.Add(btnUpdateWord);
            Controls.Add(btnAddWord);
            Controls.Add(btnViewWords);
            Controls.Add(btnClearFilter);
            Controls.Add(btnSearchWord);
            Controls.Add(txtSearchQuery);
            Controls.Add(dgvWords);
            Controls.Add(btnBack);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            Name = "frmManageWords";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Manage Words";
            ((System.ComponentModel.ISupportInitialize)dgvWords).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnBack;
        private DataGridView dgvWords;
        private TextBox txtSearchQuery;
        private Button btnSearchWord;
        private Button btnClearFilter;
        private Button btnViewWords;
        private Button btnAddWord;
        private Button btnUpdateWord;
        private Button btnDeleteWord;
    }
}