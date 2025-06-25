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
    public partial class frmLeaderboardTools : Form
    {
        private GuessingGameProcess guessingGameProcess;
        private List<LeaderboardEntry> playerList;
        private const string placeHolderText = "Search Player by username or ID";

        public frmLeaderboardTools()
        {
            InitializeComponent();

            guessingGameProcess = new GuessingGameProcess();

            dgvLeaderboard.AutoGenerateColumns = true;
            dgvLeaderboard.ReadOnly = true;
            dgvLeaderboard.AllowUserToAddRows = false;
            dgvLeaderboard.RowHeadersVisible = false;
            dgvLeaderboard.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvLeaderboard.SelectionChanged += dgvPlayers_SelectionChanged;

            txtSearchQuery.Text = placeHolderText;
            txtSearchQuery.ForeColor = System.Drawing.SystemColors.GrayText;

            txtSearchQuery.GotFocus += TxtSearchQuery_GotFocus;
            txtSearchQuery.LostFocus += TxtSearchQuery_LostFocus;

            playerList = new List<LeaderboardEntry>();

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

        private void dgvPlayers_SelectionChanged(object sender, EventArgs e)
        {
            bool rowSelected = dgvLeaderboard.SelectedRows.Count > 0;
        }

        private void btnViewLeaderboard_Click(object sender, EventArgs e)
        {
            playerList = guessingGameProcess.GetLeaderboard();
            LoadPlayerGridView(playerList);
            dgvLeaderboard.ClearSelection();
            dgvPlayers_SelectionChanged(this, EventArgs.Empty);
            txtSearchQuery.Clear();
            TxtSearchQuery_LostFocus(txtSearchQuery, EventArgs.Empty);
            btnClearFilter.Enabled = false;
        }

        private void LoadPlayerGridView(List<LeaderboardEntry> playersToDisplay)
        {
            dgvLeaderboard.DataSource = null;
            dgvLeaderboard.Columns.Clear();
            dgvLeaderboard.DataSource = playersToDisplay;

            if (playersToDisplay != null && playersToDisplay.Any())
            {
                if (dgvLeaderboard.Columns.Contains("PlayerId")) dgvLeaderboard.Columns["PlayerId"].HeaderText = "Player Id";
                if (dgvLeaderboard.Columns.Contains("Rank")) dgvLeaderboard.Columns["Rank"].HeaderText = "Rank";
                if (dgvLeaderboard.Columns.Contains("UserName")) dgvLeaderboard.Columns["UserName"].HeaderText = "Username";
                if (dgvLeaderboard.Columns.Contains("HighScore")) dgvLeaderboard.Columns["HighScore"].HeaderText = "High Score";

                if (dgvLeaderboard.Columns.Contains("PlayerId")) dgvLeaderboard.Columns["PlayerId"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (dgvLeaderboard.Columns.Contains("Rank")) dgvLeaderboard.Columns["Rank"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (dgvLeaderboard.Columns.Contains("UserName")) dgvLeaderboard.Columns["UserName"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
                if (dgvLeaderboard.Columns.Contains("HighScore")) dgvLeaderboard.Columns["HighScore"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

                if (dgvLeaderboard.Columns.Contains("PlayerId")) dgvLeaderboard.Columns["PlayerId"].DisplayIndex = 0;
                if (dgvLeaderboard.Columns.Contains("Rank")) dgvLeaderboard.Columns["Rank"].DisplayIndex = 1;
                if (dgvLeaderboard.Columns.Contains("UserName")) dgvLeaderboard.Columns["UserName"].DisplayIndex = 2;
                if (dgvLeaderboard.Columns.Contains("HighScore")) dgvLeaderboard.Columns["HighScore"].DisplayIndex = 3;
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnApplyFilter_Click(object sender, EventArgs e)
        {
            string searchPlayerQuery = txtSearchQuery.Text;

            if (searchPlayerQuery.Equals(placeHolderText, StringComparison.OrdinalIgnoreCase))
            {
                searchPlayerQuery = "";
            }

            if (string.IsNullOrWhiteSpace(searchPlayerQuery))
            {
                MessageBox.Show("Please enter a username or Player ID to search.",
                                "Search Input Missing",
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                dgvLeaderboard.DataSource = null;
                dgvLeaderboard.Columns.Clear();
                btnClearFilter.Enabled = false; 
                dgvLeaderboard.ClearSelection();
                dgvPlayers_SelectionChanged(this, EventArgs.Empty);
                return;
            }

            playerList = guessingGameProcess.GetLeaderboard();
            List<LeaderboardEntry> filteredList = new List<LeaderboardEntry>();

            bool isIdSearch = int.TryParse(searchPlayerQuery, out int searchId);

            foreach (LeaderboardEntry entry in playerList)
            {
                if (isIdSearch)
                {
                    if (entry.PlayerId == searchId)
                    {
                        filteredList.Add(entry);
                        break;
                    }
                }
                else
                {
                    if (entry.UserName.Equals(searchPlayerQuery, StringComparison.OrdinalIgnoreCase))
                    {
                        filteredList.Add(entry);
                        break;
                    }
                }
            }

            if (filteredList.Count > 0)
            {
                LoadPlayerGridView(filteredList); 
                btnClearFilter.Enabled = true;
            }
            else
            {
                MessageBox.Show($"Player '{searchPlayerQuery}' not found.", "Player Not Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
                dgvLeaderboard.DataSource = null; 
                dgvLeaderboard.Columns.Clear();
                btnClearFilter.Enabled = false;
            }

                btnClearFilter.Enabled = true;
                dgvLeaderboard.ClearSelection();
                dgvPlayers_SelectionChanged(this, EventArgs.Empty);
        }

        private void btnClearFilter_Click(object sender, EventArgs e)
        {
            txtSearchQuery.Clear();
            TxtSearchQuery_LostFocus(txtSearchQuery, EventArgs.Empty); 

            dgvLeaderboard.DataSource = null;
            dgvLeaderboard.Columns.Clear();  

            dgvLeaderboard.ClearSelection();
            dgvPlayers_SelectionChanged(this, EventArgs.Empty); 

            btnClearFilter.Enabled = false; 
        }
    }
}
