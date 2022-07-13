using ARKServerManager.Controllers;
using ARKServerManager.DataProvider;
using ARKServerManager.Models;
using ARKServerManager.Models.Ark;
using ARKServerManager.Services;
using System.Globalization;
using System.Text.RegularExpressions;

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
            ParserLog(File.ReadAllLines(Path.Combine(logPath,"ServerGame.211179.2022.07.01_07.48.01.log")));
        }
        private void ParserLog(string[] logfile)
        {
            foreach (string line in logfile)
            {
                ArkLogRow arkLog = new();
                DateTime? logDate = GetLogDate(line);
                if(logDate == null)
                {
                    continue;
                }
                int recordID = GetRecordId(line);
                if (recordID == 0)
                {
                    continue;
                }
                arkLog.Date = (DateTime)logDate;
                arkLog.RecordID = recordID;
                arkLog.RecordRow = GetRecordRow(line);
            }
        }

        private string GetRecordRow(string line)
        {
            MatchCollection matchData = Regex.Matches(line, new string(@"_[0-2][0-9].[0-6][0-9].[0-6][0-9]\:"));
            if(matchData.Count == 0)
            {
                return line;
            }
            return line.Split(matchData[0].Value).Last().Trim();
        }

        private static int GetRecordId(string line)
        {
            string pattern = new(@"\[.{3}\]");
            if (Regex.Matches(line, pattern).Count == 0)
            {
                return 0;
            }
            if (int.TryParse(Regex.Matches(line, pattern)[0].Value.Substring(1, 3).Trim(), out int recordID))
            {
                return recordID;
            }
            else
            {
                return 0;
            }
        }

        private static DateTime? GetLogDate(string line)
        {
            MatchCollection matchData = Regex.Matches(line, new string(@"[2][0][0-9][0-2].[0-1][0-9].[0-3][0-9]_[0-2][0-9].[0-6][0-9].[0-6][0-9]"));
            if(matchData.Count == 0)
            {
                return null;
            }
            CultureInfo provider = CultureInfo.InvariantCulture;
            return DateTime.ParseExact(matchData[0].Value, "yyyy.MM.dd_hh.mm.ss", provider);
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
