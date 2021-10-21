using CoreRCON;
using System;
using System.Net;
using System.Threading.Tasks;

namespace ARKServerManager.Controllers
{
    public class RconCMD
    {
        public RconCMD(string ip, string pass, ushort port)
        {
            Ip = ip;
            Pass = pass;
            Port = port;
        }

        public string Ip { get; }
        public string Pass { get; }
        public ushort Port { get; }

        public async Task<string> GetRconAsync(string cmd)
        {

            try
            {
                RCON rcon = new(IPAddress.Parse(Ip), Port, Pass);
                await rcon.ConnectAsync();
                string rconReq = await rcon.SendCommandAsync(cmd);
                return rconReq;
            }

            catch (Exception)
            {
                return "Not connected";
            }
        }
        public async Task<string> SetRconAsync(string cmd)
        {
            try
            {
                RCON rcon = new(IPAddress.Parse(Ip), Port, Pass);
                await rcon.ConnectAsync();

                return "OK";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
