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
    public partial class frmAdminMenu : Form
    {
        public frmAdminMenu()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "Are you sure you want to logout?",
                "Confirm Logout",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
                );

            if (result == DialogResult.Yes)
            {
                PerformLogout();
            }

        }

        private void btnManagePlayers_Click(object sender, EventArgs e)
        {
            frmManagePlayers managePlayersForm = new frmManagePlayers();
            managePlayersForm.ShowDialog();
        }

        private void PerformLogout()
        {
            MessageBox.Show("You have been logged out successfully.", "Logout", MessageBoxButtons.OK, MessageBoxIcon.Information);

            this.Close();
            frmLogin loginForm = new frmLogin();
            loginForm.Show();
        }

        private void btnManageWords_Click(object sender, EventArgs e)
        {
            frmManageWords manageWordsForm = new frmManageWords();
            manageWordsForm.ShowDialog();
        }

        private void btnLeaderboardTools_Click(object sender, EventArgs e)
        {
            frmLeaderboardTools leaderboardTools = new frmLeaderboardTools();
            leaderboardTools.ShowDialog();
        }
    }
}
