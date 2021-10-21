using ArkWeb.Models;
using CoreRCON;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace ArkWeb
{
    public class RconCmd
    {
        public async Task<string[]> GetPlayersListAsync()
        {

            string text = await GetRconAsync();
            if (text is "Not connected" or "No Players Connected")
                return null;
            else
            {
                text = text.Remove(0, 2);
                string[] words = text.Split("\r\n");
                int i = 0;
                string[] players = new string[words.Length];
                foreach (string w in words)
                {
                    string[] tempStr = w.Split(",");
                    tempStr = tempStr[0].Split(". ");
                    players[i] = tempStr[1];
                    i++;
                }
                return players;
            }
            string json = new WebClient().DownloadString("http://localhost:5005/api/getonlineplayers");
            var test = JsonConvert.DeserializeObject< List<Player>>(json);
            _ = test;
            return new string[]{"1","2"};
        }
        public async Task<string> GetRconAsync()//переделатб
        {
            var rcon = new RCON(IPAddress.Parse("192.168.0.9"), 32330, "Zxu9FS8WfkCqtcVh");
            try
            {
                await rcon.ConnectAsync();
                string rconReq = await rcon.SendCommandAsync("listplayers");
                return rconReq;
            }

            catch (Exception)
            {
                return "Not connected";
            }
        }
        public async Task<List<string>> TestGetPlayersListAsync()
        {

            string text = await GetRconAsync();
            if (text is "Not connected" or "No Players Connected")
                return null;
            else
            {
                text = text.Remove(0, 2);
                string[] words = text.Split("\r\n");
                
                List<string> players = new();
                foreach (string w in words)
                {
                    string[] tempStr = w.Split(",");
                    tempStr = tempStr[0].Split(". ");
                    players.Add(tempStr[1]);
                    
                }
                return players;
            }

        }
        //ClaimsPrincipal principal = HttpContext.Current.User as ClaimsPrincipal;
    }
}
