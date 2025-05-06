using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGameDataService
{
    public class AdminDataService
    {
        private string adminUserName = "Admin_Jayboy";
        private string adminPassWord = "password";

        public bool ValidateAdminLogin(string username, string password)
        {
            return username == adminUserName && password == adminPassWord;
        }
    }
}
