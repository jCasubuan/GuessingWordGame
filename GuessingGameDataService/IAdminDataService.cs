using GuessingGameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGameDataService
{
    public interface IAdminDataService
    {
        //--- READ ---
        bool ValidateAdminLogin(string username, string password);
        List<Player> GetAllPlayers();
        Player SearchById(int playerId);
        Player SearchByUsername(string userName);

        //--- DELETE ---
        bool DeletePlayer(string userName);
    }
}
