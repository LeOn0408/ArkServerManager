using ARKServerManager.Controllers;
using ARKServerManager.Database;
using ARKServerManager.Models;
using ARKServerManager.ServerService;
using System.Diagnostics;

namespace ARKServerManager.Services
{
    public class ArkServer
    {
        
        ILogger<ServerJob> logger;
        public async Task UpdateAsync(ILogger<ServerService.ServerJob> _logger)
        {
            
            logger = _logger;
            var s = Environment.OSVersion;
            if (s.Platform == PlatformID.Unix)
            {
               
                logger.LogInformation("Обновляем после ркон");
                ProcessStartInfo steamcmd = new()
                {
                    FileName = @"/mnt/data/Projects/ArkServer/SteamCMD/UBUNTU/steamcmd.sh",
                    Arguments = "+login anonymous +force_install_dir /mnt/data/Projects/ArkServer/arkv3 +app_update 376030 +quit"
                };
                var steam = Process.Start(steamcmd);
                steam.WaitForExit();
                logger.LogInformation("Обновляем");
                
            }
            else if (s.Platform == PlatformID.Win32NT)
            {

                logger.LogInformation("Обновляем после ркон");
                ProcessStartInfo infoStartProcess = new()
                {
                    WorkingDirectory = @"P:\ArkServer\SteamCMD\WIN",
                    FileName = @"P:\ArkServer\SteamCMD\WIN\steamcmd.exe",
                    Arguments = "+login anonymous +force_install_dir P:/ArkServer/arkv3 +app_update 376030 +quit"
                };
                var steam = Process.Start(infoStartProcess);
                steam.WaitForExit();

            }
        }

        public async Task Save(Server server)
        {
            logger.LogInformation("Сохраняем мир");
            string rconResponse = await new RconCMD(server.LocalIP, server.RconPass, server.RconPort).GetRconAsync("saveworld");
            logger.LogInformation(rconResponse);
        }

        public int Launch(Server server)
        {
            string Arguments = $"{server.Map}?listen?Port={server.GamePort}?QueryPort=27018?SessionName='{server.PublicName}'?MaxPlayers={server.MaxPlayers}?AllowCrateSpawnsOnTopOfStructures=True -nosteamclient -server -game -log -crossplay -servergamelog -NotifyAdminCommandsInChat -PublicIPForEpic={server.Id}";
            //gnome-terminal -- bash -c \"./ARKServerManager\"
            var s = Environment.OSVersion;
            ProcessStartInfo procStartInfo;
            Process proc = new();
            if (s.Platform == PlatformID.Unix)
            {
                string FileName = $"{server.ServerPath}ShooterGame/Binaries/Linux/ShooterGameServer";
                //procStartInfo = new("/bin/bash", $"-c {FileName} {Arguments}")
                //{
                //    UseShellExecute = true,
                //    CreateNoWindow = false
                    
                //};
                procStartInfo = new()
                {
                    FileName = FileName,
                    Arguments = Arguments
                };
                proc.StartInfo = procStartInfo;
            }
            if (s.Platform == PlatformID.Win32NT)
            {
                string FileName = @"";
                procStartInfo = new("cmd", $"-c {FileName} {Arguments}")
                {
                    UseShellExecute = true,
                    CreateNoWindow = false

                };
                proc.StartInfo = procStartInfo;
            }
            proc.Start();
            logger.LogInformation("Запущено");
            return proc.Id;
        }
        
    }
}
