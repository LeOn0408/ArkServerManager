using ARKServerManager.Controllers;
using ARKServerManager.DataProvider;
using ARKServerManager.Models;
using ARKServerManager.Services;

namespace ARKServerManager.Servers.Ark
{
    public class ArkLogs
    {
        private readonly Server _server;

        public ArkLogs(Server server)
        {
            _server = server;
        }

        /// <summary>
        /// Получаем файловые логи
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void ReadFileLog()
        {
            string logPath = Path.Combine(_server.ServerPath, @"ShooterGame\Saved\Logs");
            if (File.Exists(logPath))
            {
                throw new Exception($"Error log path: {logPath}");
            }
            var logfile = File.ReadAllLines(Path.Combine(logPath,"ServerGame.211179.2022.07.01_07.48.01.log"));
        }

        /// <summary>
        /// Получаем логи по ркон
        /// </summary>
        //void ReadRconLog()
        //{
        //    string command = new ServerCommand(_server.TypeServer).GetLog;
        //    string rconCMD = new Rcon(_server.LocalIP, _server.RconPass, _server.RconPort).GetRconAsync(command).Result;
        //}
    }
}
