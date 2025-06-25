using GuessingGameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGameDataService
{
    public class AdminDataService
    {   
        IAdminDataService adminDataService;

        public AdminDataService()
        {
            //adminDataService = new InMemoryAdminDataService();
            //adminDataService = new TextFileAdminDataService();
            //adminDataService = new JsonFileAdminDataService();
            adminDataService = new DBAdminDataService();
        }

        //--- READ ---
        public bool GetAdminAccount(AdminAccount adminAccount)
        {
            return adminDataService.GetAdminAccount(adminAccount);
        }

        public List<Player> GetAllPlayers()
        {
            return adminDataService.GetAllPlayers();
        }

        public Player SearchById(int playerId)
        {
            return adminDataService.SearchById(playerId);
        }

        public Player SearchByUsername(string userName)
        {
            return adminDataService.SearchByUsername(userName);
        }

        //--- DELETE ---
        public bool DeletePlayer(string userName)
        {
            return adminDataService.DeletePlayer(userName);
        }
    }
}
