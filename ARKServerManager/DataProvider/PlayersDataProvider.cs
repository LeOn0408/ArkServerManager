using ARKServerManager.Controllers;
using ARKServerManager.Models;

namespace ARKServerManager.DataProvider
{
    public class PlayersDataProvider
    {
        private readonly Server _server;

        public List<Player> Players { get; private set; }

        public PlayersDataProvider(Server server)
        {
            _server = server;
            GetPlayers();
        }

        private void GetPlayers()
        {
            Players = new();
            try
            {
                string rconCMD = new Rcon(_server.LocalIP, _server.RconPass, _server.RconPort).GetRconAsync("listplayers").Result;
                if (rconCMD is not "No Players Connected")
                {
                    rconCMD = rconCMD.Remove(0, 2);
                    string[] words = rconCMD.Split("\r\n");
                    foreach (string w in words)
                    {
                        Player player = new();
                        string[] idSplit = w.Split(", ");
                        string[] nameSplit = idSplit[0].Split(". ");
                        player.Id = idSplit[1];
                        player.User = nameSplit[1];
                        Players.Add(player);

                    }
                }
            }
            catch (Exception)
            {
                //TODO: Обработать отсутствие соединения с сервером
            }
            
        }
    }
}
