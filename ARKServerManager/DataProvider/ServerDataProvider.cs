using ARKServerManager.Database;
using ARKServerManager.Models;
using Microsoft.EntityFrameworkCore;

namespace ARKServerManager.DataProvider
{
    public class ServerDataProvider
    {
        private readonly DatabaseContext _db;

        public ServerDataProvider(DatabaseContext db)
        {
            _db = db;
        }

        public List<ServerApi> GetServers()
        {
            List<Server> serversList = _db.Server.AsNoTracking().ToList();
            List<ServerApi> servers = new();
            foreach (Server server in serversList)
            {
                if (server.Visible == 0)
                {
                    continue;
                }
                ServerApi serverApi = new()
                {
                    Id = server.Id,
                    RemoteIP = server.RemoteIP,
                    RemotePort = server.RemotePort
                };


                try
                {
                    serverApi.Players = new PlayersDataProvider(server).Players;
                    Server serverInformation;
                    switch (server.TypeServer)
                    {
                        case GameServer.Ark: serverInformation = GetServerArkInformation(server);
                            break;

                        default: continue;
                    }
                    

                    serverApi.Name = serverInformation.Name;
                    serverApi.Version = serverInformation.Version;
                    serverApi.Map = serverInformation.Map;
                    serverApi.IsConnected = true;
                    servers.Add(serverApi);
                }
                catch (Exception ex)
                {
                    serverApi.Name = server.Name;
                    serverApi.Map = server.Map;
                    serverApi.IsConnected=false;
                    serverApi.Result = ex.Message;
                    servers.Add(serverApi);
                    //TODO:Обработать отсутсвие сервера
                }
            }
            return servers;
        }
        private static Server GetServerArkInformation(Server serverdata)
        {
            dynamic server = A2S.Server.Query(serverdata.LocalIP, serverdata.LocalPort, 5);
            Server serverInfo = new();

            if (server is not Exception and not null)
            {

                serverInfo.Name = server.Name;
                serverInfo.Map = server.Map;
                string[] MapAndVersion = server.Name.Split("- (v");
                serverInfo.Version = MapAndVersion[1].Trim(')');

                #region
                //    var response = $@"
                //Protocol: {server.Protocol}
                //Name: {server.Name}
                //Map: {server.Map}
                //Folder: {server.Folder}
                //Game: {server.Game}
                //ID: {server.Id}
                //Players: {server.Players}
                //Max Players: {server.MaxPlayers}
                //Bots: {server.Bots}
                //Server Type: {server.ServerType}
                //Environment: {server.Environment}
                //Visibility: {server.Visibility}
                //VAC: {server.Vac}
                //Version: {server.Version}
                //ExtraDataFlags:
                //    Port: {server.Port}
                //    SteamId: {server.SteamId}
                //    Spectator:
                //        Name: {server.Spectator}
                //        Port: {server.SpectatorPort}
                //    Keywords: {server.Keywords}
                //    GameId: {server.GameId}";

                #endregion
            }
            else
            {
                throw new Exception("Not server data");
            }

            return serverInfo;
        }
    }
}
