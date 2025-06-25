using GuessingGame_BusinessLogic;
using GuessingGameCommon;

namespace GuessingGameDesktop
{
    public partial class frmLogin : Form
    {   
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            var userName = txtUserName.Text;
            var passWord = txtPassword.Text;

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(passWord))
            {
                MessageBox.Show("Username and Password cannot be empty. Please try again.",
                    "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtPassword.Clear();
                txtPassword.Focus();
                return;
            }

            GuessingGameProcess gameProcess = new GuessingGameProcess();

            if (gameProcess.VerifyLogin(userName, passWord))
            {
                MessageBox.Show("Player Login Successful!", "Success",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
                return;
            }

            else
            {
                AdminAccount adminAttempt = new AdminAccount { Username = userName, Password = passWord };

                if (gameProcess.GetAdminAccount(adminAttempt))
                {
                    MessageBox.Show($"Welcome, {txtUserName.Text}!", "Admin Login Successful",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);

                    this.Close();
                    frmAdminMenu adminMenu = new frmAdminMenu();
                    adminMenu.Show();
                    return;
                }
                else
                {
                    MessageBox.Show("Invalid username or password. Please try again.", "Login Failed",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtPassword.Clear();
                    txtPassword.Focus();
                }
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            this.Close();
            frmMainMenu mainMenu = Application.OpenForms.OfType<frmMainMenu>().FirstOrDefault();
            mainMenu.Show();
            //Hide();
        }
    }
}
