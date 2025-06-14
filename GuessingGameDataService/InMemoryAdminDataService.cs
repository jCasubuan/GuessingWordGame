﻿using GuessingGameCommon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuessingGameDataService
{
    public class InMemoryAdminDataService : IAdminDataService
    {
        private const string adminUserName = "Admin_Jayboy";
        private const string adminPassWord = "password";
        private Dictionary<string, Player> players => InMemoryPlayerDataService.players;

        //READ
        public bool GetAdminAccount(AdminAccount adminAccount)
        {
            return adminAccount.Username == adminUserName && adminAccount.Password == adminPassWord;
        }

        public List<Player> GetAllPlayers()
        {
            return players.Values.ToList();
        }

        public Player SearchById(int playerId)
        {
            foreach (Player player in players.Values)
            {
                if (player.PlayerId == playerId)
                {
                    return player;
                }
            }
            return null;
        }

        public Player SearchByUsername(string userName)
        {
            if (players.ContainsKey(userName))
            {
                return players[userName];
            }
            return null;
        }

        //DELETE
        public bool DeletePlayer(string userName)
        {
            if (players.ContainsKey(userName))
            {
                players.Remove(userName);
                return true;
            }
            return false;
        }
    }
}
