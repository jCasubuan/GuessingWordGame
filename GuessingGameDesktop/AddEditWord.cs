using GuessingGameCommon;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GuessingGameDesktop
{
    public partial class frmAddEditWord : Form
    {
        public WordHint CurrentWordHint { get; private set; }

        public frmAddEditWord()
        {
            InitializeComponent();
            InitializeDifficultyComboBox();
            this.Text = "Add New Word";
        }

        public frmAddEditWord(WordHint wordToEdit) : this()
        {
            this.Text = "Update Word";
            LoadWordHintForEditing(wordToEdit);
        }

        public void InitializeDifficultyComboBox()
        {
            cmbDifficulty.Items.Add("Easy");
            cmbDifficulty.Items.Add("Medium");
            cmbDifficulty.Items.Add("Hard");
            cmbDifficulty.Items.Add("Extra Hard");
            cmbDifficulty.SelectedIndex = 0;
        }

        private void LoadWordHintForEditing(WordHint wordToEdit)
        {

            txtWord.Text = wordToEdit.Word;
            txtHint.Text = wordToEdit.Hint;

            if (cmbDifficulty.Items.Contains(wordToEdit.Difficulty))
            {
                cmbDifficulty.SelectedItem = wordToEdit.Difficulty;
            }
            else
            {
                cmbDifficulty.SelectedIndex = 0; 
            }

            CurrentWordHint = wordToEdit;
        }

        private void ResetFields()
        {
            txtWord.Clear();
            txtHint.Clear();
            cmbDifficulty.SelectedIndex = 0; 
            CurrentWordHint = null; 
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtWord.Text))
            {
                MessageBox.Show("Please enter a word.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtHint.Text))
            {
                MessageBox.Show("Please enter a hint.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbDifficulty.SelectedItem == null)
            {
                MessageBox.Show("Please select a difficulty.", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string word = txtWord.Text.Trim();
            string hint = txtHint.Text.Trim();
            string difficulty = cmbDifficulty.SelectedItem.ToString();

            string action = (CurrentWordHint == null) ? "add" : "update"; 

            string confirmationMessage = $"Are you sure you want to {action} this word?\n\n" +
                                         $"Word: {word}\n" +
                                         $"Hint: {hint}\n" + 
                                         $"Difficulty: {difficulty}";

            DialogResult confirmResult = MessageBox.Show(
                confirmationMessage,
                $"Confirm {char.ToUpper(action[0]) + action.Substring(1)} Word", 
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirmResult == DialogResult.No)
            {
                return; 
            }

            if (CurrentWordHint == null) 
            {
                CurrentWordHint = new WordHint();
            }

            CurrentWordHint.Word = word; 
            CurrentWordHint.Hint = hint;   
            CurrentWordHint.Difficulty = difficulty; 

            this.DialogResult = DialogResult.OK; 
            this.Close(); 
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            ResetFields();
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; 
            this.Close();
        }
    }
}
