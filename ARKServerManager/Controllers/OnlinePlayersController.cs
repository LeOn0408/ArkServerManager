using ARKServerManager.Database;
using ARKServerManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ARKServerManager.Controllers
{
    [Route("api/getonlineplayers")]
    [ApiController]
    public class OnlinePlayersController : ControllerBase
    {
        DatabaseContext Db {  get; set; }
        ILogger<OnlinePlayersController> Logger {  get; set; }   

        public OnlinePlayersController(DatabaseContext database, ILogger<OnlinePlayersController> logger)
        {
            Db = database;
            Logger = logger;
        }

        //
        [HttpGet]
        [Produces("application/json")]
        public async Task<List<ServerApi>> Get()
        {
            //Logger.LogInformation("Получаем данные");
            List<Server> serversList = Db.Server.AsNoTracking().ToList();
            List<ServerApi> servers = new();
            
           

            foreach (Server server in serversList)
            {
                if (server.Visible == 0)
                {
                    continue;
                }
                ServerApi serverApi = new();
                List<Player> players = new();

                Server serverInformation = new ServerInformation().GetServersInformation(server);
                if (serverInformation.Id < 0)
                {
                    serverApi.Id = serverInformation.Id;
                    serverApi.Name = server.Name;
                    serverApi.Version = server.Version;
                    serverApi.Map = server.Map;
                }
                else
                {
                    serverApi.Id = server.Id;
                    serverApi.Name = serverInformation.Name;
                    serverApi.Version = serverInformation.Version;
                    serverApi.Map = serverInformation.Map;
                    string rconCMD = await new RconCMD(server.LocalIP, server.RconPass, server.RconPort).GetRconAsync("listplayers");
                    if (rconCMD is "Not connected" or "No Players Connected")
                    {
                        Player player = new();
                        player.Id = "0";
                        player.User = rconCMD;
                        players.Add(player);

                    }
                    else
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
                            players.Add(player);

                        }
                    }
                }
                serverApi.Players = players;
                serverApi.RemoteIP=server.RemoteIP;
                serverApi.RemotePort = server.RemotePort;
                servers.Add(serverApi);
            }
            return servers;
        }
    }
}
