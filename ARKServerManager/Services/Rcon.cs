using CoreRCON;
using System.Net;

namespace ARKServerManager.Services
{
    public class Rcon
    {
        public Rcon(string ip, string pass, ushort port)
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
                throw new Exception("Not connect");
            }
        }
        public async void SetRconAsync(string cmd)
        {
            try
            {
                using RCON rcon = new(IPAddress.Parse(Ip), Port, Pass);
                await rcon.ConnectAsync();
                string rconReq = await rcon.SendCommandAsync(cmd);

            }
            catch (Exception)
            {

            }
        }
    }
}
