using ARKServerManager.Models;

namespace ARKServerManager.Controllers
{
    public class ServerInformation
    {
        public Server GetServersInformation(Server serverdata)
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
                serverInfo.Id = -1;
            }
                
            return serverInfo;
        }
    }
}
