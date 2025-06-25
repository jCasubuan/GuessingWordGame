using Microsoft.Data.SqlClient;
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
    public partial class frmManagePlayers : Form
    {
        private GuessingGameProcess guessingGameProcess;
        private List<Player> allPlayers;
        private const string placeHolderText = "Search Player by username or ID";

        public frmManagePlayers()
        {
            InitializeComponent();

            guessingGameProcess = new GuessingGameProcess();

            dgvPlayers.AutoGenerateColumns = true;
            dgvPlayers.ReadOnly = true;
            dgvPlayers.AllowUserToAddRows = false;
            dgvPlayers.RowHeadersVisible = false;
            dgvPlayers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            btnPlayerStats.Enabled = false;
            btnDeletePlayer.Enabled = false;

            dgvPlayers.SelectionChanged += dgvPlayers_SelectionChanged;

            txtSearchQuery.Text = placeHolderText;
            txtSearchQuery.ForeColor = System.Drawing.SystemColors.GrayText;

            txtSearchQuery.GotFocus += TxtSearchQuery_GotFocus;
            txtSearchQuery.LostFocus += TxtSearchQuery_LostFocus;

            allPlayers = new List<Player>();

            btnClearFilter.Enabled = false;
        }

        private void TxtSearchQuery_GotFocus(object sender, EventArgs e)
        {
            if (txtSearchQuery.Text == placeHolderText)
            {
                txtSearchQuery.Text = "";
                txtSearchQuery.ForeColor = System.Drawing.SystemColors.WindowText;
            }
        }

        private void TxtSearchQuery_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearchQuery.Text))
            {
                txtSearchQuery.Text = placeHolderText;
                txtSearchQuery.ForeColor = System.Drawing.SystemColors.GrayText;
            }
        }

        private void btnViewPlayers_Click(object sender, EventArgs e)
        {
            allPlayers = guessingGameProcess.GetAllPlayers();
            UpdateDataGridView(allPlayers);
            dgvPlayers.ClearSelection();
            dgvPlayers_SelectionChanged(this, EventArgs.Empty);
            txtSearchQuery.Clear();
            TxtSearchQuery_LostFocus(txtSearchQuery, EventArgs.Empty);
            btnClearFilter.Enabled = false;
        }

        private void dgvPlayers_SelectionChanged(object sender, EventArgs e)
        {
            bool rowSelected = dgvPlayers.SelectedRows.Count > 0;
            btnPlayerStats.Enabled = rowSelected;
            btnDeletePlayer.Enabled = rowSelected;
        }

        private void btnPlayerStats_Click(object sender, EventArgs e)
        {
            if (dgvPlayers.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvPlayers.SelectedRows[0];

                string playerId = selectedRow.Cells["PlayerId"].Value?.ToString();
                string fullName = selectedRow.Cells["FullName"].Value?.ToString();
                string userName = selectedRow.Cells["Username"].Value?.ToString();
                string score = selectedRow.Cells["Scores"].Value?.ToString();
                string highScore = selectedRow.Cells["HighScore"].Value?.ToString();
                string lastLevelReached = selectedRow.Cells["LastCompletedLevel"].Value?.ToString();

                string playerStatsMessage = $"Player ID: {playerId}\n" +
                                            $"Full Name: {fullName}\n" +
                                            $"Username: {userName}\n" +
                                            $"Score: {score}\n" +
                                            $"High Score: {highScore}\n" +
                                            $"Last Level Reached: {lastLevelReached}";

                MessageBox.Show(playerStatsMessage, $"Stats for {userName}", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please select a player to view stats.",
                                "No Player Selected",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);
            }
        }

        private void UpdateDataGridView(List<Player> playersToDisplay)
        {
            dgvPlayers.DataSource = null; 
            dgvPlayers.Columns.Clear();   
            dgvPlayers.DataSource = playersToDisplay; 

            if (playersToDisplay != null && playersToDisplay.Any())
            {
                if (dgvPlayers.Columns.Contains("Password"))
                {
                    dgvPlayers.Columns["Password"].Visible = false;
                }
                if (dgvPlayers.Columns.Contains("LastCompletedLevel"))
                {
                    dgvPlayers.Columns["LastCompletedLevel"].Visible = false;
                }

                if (dgvPlayers.Columns.Contains("PlayerId")) dgvPlayers.Columns["PlayerId"].HeaderText = "Player ID";
                if (dgvPlayers.Columns.Contains("FullName")) dgvPlayers.Columns["FullName"].HeaderText = "Full Name";
                if (dgvPlayers.Columns.Contains("UserName")) dgvPlayers.Columns["UserName"].HeaderText = "Username";
                if (dgvPlayers.Columns.Contains("Scores")) dgvPlayers.Columns["Scores"].HeaderText = "Score";
                if (dgvPlayers.Columns.Contains("HighScore")) dgvPlayers.Columns["HighScore"].HeaderText = "High Score";
                if (dgvPlayers.Columns.Contains("LastCompletedLevel")) dgvPlayers.Columns["LastCompletedLevel"].HeaderText = "Last Level Reached";

                if (dgvPlayers.Columns.Contains("PlayerId")) dgvPlayers.Columns["PlayerId"].DisplayIndex = 0;
                if (dgvPlayers.Columns.Contains("FullName")) dgvPlayers.Columns["FullName"].DisplayIndex = 1;
                if (dgvPlayers.Columns.Contains("UserName")) dgvPlayers.Columns["UserName"].DisplayIndex = 2;
                if (dgvPlayers.Columns.Contains("Scores")) dgvPlayers.Columns["Scores"].DisplayIndex = 3;
                if (dgvPlayers.Columns.Contains("HighScore")) dgvPlayers.Columns["HighScore"].DisplayIndex = 4;
            }
        }

        private void btnDeletePlayer_Click_1(object sender, EventArgs e)
        {
            if (dgvPlayers.SelectedRows.Count > 0)
            {   
                string idToDelete = dgvPlayers.SelectedRows[0].Cells["PlayerID"].Value?.ToString();
                string userNameToDelete = dgvPlayers.SelectedRows[0].Cells["UserName"].Value?.ToString();

                DialogResult confirmResult = MessageBox.Show(
                    $"Are you sure you want to delete {userNameToDelete} (Player ID: {idToDelete})?\n\n This action cannot be undone.",
                    "Confirm Delete",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning
                );

                if (confirmResult == DialogResult.Yes)
                {
                    bool isDeleted = guessingGameProcess.DeletePlayer(userNameToDelete);

                    if (isDeleted)
                    {
                        MessageBox.Show("Player deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        allPlayers = guessingGameProcess.GetAllPlayers();
                    }
                    else
                    {
                        MessageBox.Show("Player not found or could not be deleted.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }

                    UpdateDataGridView(allPlayers);
                    dgvPlayers.ClearSelection();
                    dgvPlayers_SelectionChanged(this, EventArgs.Empty);
                    txtSearchQuery.Clear();
                    TxtSearchQuery_LostFocus(this, EventArgs.Empty);
                    btnClearFilter.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("Please select a player to delete.", "No Player Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchPlayer = txtSearchQuery.Text.Trim();

            if (searchPlayer == placeHolderText)
            {
                searchPlayer = "";
            }

            if (string.IsNullOrEmpty(searchPlayer))
            {
                MessageBox.Show("Please enter a username or Player ID to search.",
                                "Search Input Missing",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                dgvPlayers.DataSource = null;
                dgvPlayers.Columns.Clear();
                btnClearFilter.Enabled = false;
                dgvPlayers.ClearSelection();
                dgvPlayers_SelectionChanged(this, EventArgs.Empty);
                return;
            }

            Player foundPlayer = guessingGameProcess.SearchPlayerByInput(searchPlayer);

            List<Player> filteredList = new List<Player>();
            if (foundPlayer != null)
            {
                filteredList.Add(foundPlayer);
            }

            UpdateDataGridView(filteredList);

            if (filteredList.Count == 0)
            {
                MessageBox.Show($"No Player found matching {searchPlayer}.",
                                 "No Results",
                                 MessageBoxButtons.OK,
                                 MessageBoxIcon.Information);
                btnClearFilter.Enabled = true;
                dgvPlayers.ClearSelection();
                dgvPlayers_SelectionChanged(this, EventArgs.Empty);
            }

            btnClearFilter.Enabled = true;
            dgvPlayers.ClearSelection(); 
            dgvPlayers_SelectionChanged(this, EventArgs.Empty);
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtSearchQuery.Clear();
            TxtSearchQuery_LostFocus(txtSearchQuery, EventArgs.Empty);

            dgvPlayers.DataSource = null;
            dgvPlayers.Columns.Clear();

            dgvPlayers.ClearSelection();
            dgvPlayers_SelectionChanged(this, EventArgs.Empty); 

            btnClearFilter.Enabled = false;
        }
    }
}