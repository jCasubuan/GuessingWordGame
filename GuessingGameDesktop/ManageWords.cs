using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GuessingGame_BusinessLogic;
using GuessingGameCommon;

namespace GuessingGameDesktop
{
    public partial class frmManageWords : Form
    {
        private GuessingGameProcess guessingGameProcess;
        private List<WordHint> wordHints;
        private const string wordPlaceHolder = "Search Words";

        public frmManageWords()
        {
            InitializeComponent();

            guessingGameProcess = new GuessingGameProcess();

            dgvWords.AutoGenerateColumns = true;
            dgvWords.ReadOnly = true;
            dgvWords.AllowUserToAddRows = false;
            dgvWords.RowHeadersVisible = false;
            dgvWords.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            btnUpdateWord.Enabled = false;
            btnDeleteWord.Enabled = false;

            dgvWords.SelectionChanged += dgvWords_SelectionChanged;

            txtSearchQuery.Text = wordPlaceHolder;
            txtSearchQuery.ForeColor = System.Drawing.SystemColors.GrayText;
            txtSearchQuery.GotFocus += TxtSearchQuery_GotFocus;
            txtSearchQuery.LostFocus += TxtSearchQuery_LostFocus;

            wordHints = new List<WordHint>();

            btnClearFilter.Enabled = false;
        }

        private void TxtSearchQuery_GotFocus(object sender, EventArgs e)
        {
            if (txtSearchQuery.Text == wordPlaceHolder)
            {
                txtSearchQuery.Text = "";
                txtSearchQuery.ForeColor = SystemColors.WindowText;
            }
        }

        private void TxtSearchQuery_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSearchQuery.Text))
            {
                txtSearchQuery.Text = wordPlaceHolder;
                txtSearchQuery.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        private void dgvWords_SelectionChanged(object sender, EventArgs e)
        {
            bool rowsSelected = dgvWords.SelectedRows.Count > 0;
            btnUpdateWord.Enabled = rowsSelected;
            btnDeleteWord.Enabled = rowsSelected;
        }

        private void btnViewWords_Click(object sender, EventArgs e)
        {
            wordHints = guessingGameProcess.GetWordHints();
            LoadWordGridView(wordHints);
            dgvWords.ClearSelection();
            dgvWords_SelectionChanged(this, EventArgs.Empty);
            txtSearchQuery.Clear();
            TxtSearchQuery_LostFocus(txtSearchQuery, EventArgs.Empty);
            btnClearFilter.Enabled = false;
        }

        private void btnAddWord_Click(object sender, EventArgs e)
        {
            using (frmAddEditWord addEditWordForm = new frmAddEditWord())
            {
                if (addEditWordForm.ShowDialog() == DialogResult.OK)
                {
                    WordHint newWordHint = addEditWordForm.CurrentWordHint;

                    if (guessingGameProcess.WordExists(newWordHint.Word))
                    {
                        MessageBox.Show("This word already exists. Please enter a unique word.", "Duplicate Word", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    bool success = guessingGameProcess.AddWordHint(
                        newWordHint.Word,
                        newWordHint.Hint,
                        newWordHint.Difficulty
                    );

                    if (success)
                    {
                        MessageBox.Show("Word added successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        btnViewWords_Click(this, EventArgs.Empty);
                    }
                    else
                    {
                        MessageBox.Show("Failed to add word.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void LoadWordGridView(List<WordHint> wordsToDisplay)
        {
            dgvWords.DataSource = null;
            dgvWords.Columns.Clear();
            dgvWords.DataSource = wordsToDisplay;

            if (wordsToDisplay != null && wordsToDisplay.Any())
            {

                if (dgvWords.Columns.Contains("ID")) dgvWords.Columns["ID"].HeaderText = "ID.";
                if (dgvWords.Columns.Contains("No")) dgvWords.Columns["No"].HeaderText = "No.";
                if (dgvWords.Columns.Contains("Word")) dgvWords.Columns["Word"].HeaderText = "Word";
                if (dgvWords.Columns.Contains("Hint")) dgvWords.Columns["Hint"].HeaderText = "Hint";
                if (dgvWords.Columns.Contains("Difficulty")) dgvWords.Columns["Difficulty"].HeaderText = "Difficulty";

                if (dgvWords.Columns.Contains("ID")) dgvWords.Columns["ID"].DisplayIndex = 0;
                if (dgvWords.Columns.Contains("No")) dgvWords.Columns["No"].DisplayIndex = 1;
                if (dgvWords.Columns.Contains("Word")) dgvWords.Columns["Word"].DisplayIndex = 2;
                if (dgvWords.Columns.Contains("Hint")) dgvWords.Columns["Hint"].DisplayIndex = 3;
                if (dgvWords.Columns.Contains("Difficulty")) dgvWords.Columns["Difficulty"].DisplayIndex = 4;

                if (dgvWords.Columns.Contains("Word"))
                {
                    dgvWords.Columns["Word"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
                if (dgvWords.Columns.Contains("Hint"))
                {
                    dgvWords.Columns["Hint"].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                }
            }
        }

        private void btnUpdateWord_Click(object sender, EventArgs e)
        {
            if (dgvWords.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a word to update.", "No Word Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            WordHint selectedWord = dgvWords.SelectedRows[0].DataBoundItem as WordHint;

            string oldWord = selectedWord.Word;

            using (frmAddEditWord editWordForm = new frmAddEditWord(selectedWord))
            {
                if (editWordForm.ShowDialog() == DialogResult.OK)
                {
                    WordHint updatedWordHint = editWordForm.CurrentWordHint;
                    if (!oldWord.Equals(updatedWordHint.Word, StringComparison.OrdinalIgnoreCase))
                    {
                        WordHint existingWord = guessingGameProcess.SearchForWord(updatedWordHint.Word);
                        if (existingWord != null)
                        {
                            MessageBox.Show("The new word '" + updatedWordHint.Word + "' already exists. Please choose a different word.", "Duplicate Word", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    WordUpdateRequest updateRequest = new WordUpdateRequest
                    {
                        NewWord = updatedWordHint.Word,
                        NewHint = updatedWordHint.Hint,
                        NewDifficulty = updatedWordHint.Difficulty
                    };

                    bool success = guessingGameProcess.UpdateWord(oldWord, updateRequest);

                    if (success)
                    {
                        MessageBox.Show("Word updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnViewWords_Click(this, EventArgs.Empty);
                    }
                    else
                    {
                        MessageBox.Show("Failed to update word. An unexpected error occurred.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDeleteWord_Click(object sender, EventArgs e)
        {
            if (dgvWords.SelectedRows.Count > 0)
            {
                string wordToDelete = dgvWords.SelectedRows[0].Cells["Word"].Value?.ToString();
                string hintToDelete = dgvWords.SelectedRows[0].Cells["Hint"].Value?.ToString();
                string difficultyToDelete = dgvWords.SelectedRows[0].Cells["Difficulty"].Value?.ToString();

                DialogResult confirmDelete = MessageBox.Show(
                    $"Are you sure you want to delete this? \n\n Word: {wordToDelete}\n Hint: {hintToDelete}\n Difficulty: {difficultyToDelete}\n\n This action cannot be undone.",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirmDelete == DialogResult.Yes)
                {
                    bool isDeleted = guessingGameProcess.DeleteWord(wordToDelete);

                    if (isDeleted)
                    {
                        MessageBox.Show("Word deleted succesfully", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //wordHints = guessingGameProcess.GetWordHints();
                        btnViewWords_Click(this, EventArgs.Empty);
                    }
                    else
                    {
                        MessageBox.Show("Word not found or could not be deleted", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    LoadWordGridView(wordHints);
                    dgvWords.ClearSelection();
                    dgvWords_SelectionChanged(this, EventArgs.Empty);
                    txtSearchQuery.Clear();
                    TxtSearchQuery_LostFocus(this, EventArgs.Empty);
                    btnClearFilter.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Please select word to delete", "No word selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnSearchWord_Click(object sender, EventArgs e)
        {
            string searchWord = txtSearchQuery.Text.Trim();

            if (searchWord == wordPlaceHolder)
            {
                searchWord = "";
            }

            if (string.IsNullOrWhiteSpace(searchWord))
            {
                MessageBox.Show("Please enter word to search",
                    "Searh input missing",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                dgvWords.DataSource = null;
                dgvWords.Columns.Clear();
                btnClearFilter.Enabled = false;
                dgvWords.ClearSelection();
                dgvWords_SelectionChanged(this, EventArgs.Empty);
                return;
            }

            WordHint foundWord = guessingGameProcess.SearchForWord(searchWord);

            List<WordHint> filteredList = new List<WordHint>();

            if (foundWord != null)
            {
                filteredList.Add(foundWord);
                LoadWordGridView(filteredList);
                btnClearFilter.Enabled = true;
            }
            else // No word found
            {
                MessageBox.Show($"No Word found matching '{searchWord}'.", // Added quotes for clarity in message
                    "No result",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Explicitly clear the grid and columns without loading anything
                dgvWords.DataSource = null;
                dgvWords.Columns.Clear();

                btnClearFilter.Enabled = true; // Still allow clearing the filter
            }

            // These lines can be placed here once to ensure consistent state after any search
            dgvWords.ClearSelection();
            dgvWords_SelectionChanged(this, EventArgs.Empty);


            //if (filteredList.Count == 0)
            //{
            //    MessageBox.Show($"No Word found matching {searchWord}.",
            //        "No result",
            //        MessageBoxButtons.OK, MessageBoxIcon.Information);

            //    btnClearFilter.Enabled = true;
            //    dgvWords.ClearSelection();
            //    dgvWords_SelectionChanged(this, EventArgs.Empty);
            //}

            //btnClearFilter.Enabled = true;
            //dgvWords.ClearSelection();
            //dgvWords_SelectionChanged(this, EventArgs.Empty);

        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtSearchQuery.Clear();
            TxtSearchQuery_LostFocus(txtSearchQuery, EventArgs.Empty);

            dgvWords.DataSource = null;
            dgvWords.Columns.Clear();

            dgvWords.ClearSelection();
            dgvWords_SelectionChanged(this, EventArgs.Empty);

            btnClearFilter.Enabled = false;
        }
    }
}
