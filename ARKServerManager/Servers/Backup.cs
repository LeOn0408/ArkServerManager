using ARKServerManager.Models;
using System.IO.Compression;
namespace ARKServerManager.Servers
{
    public class Backup
    {
        private readonly Server _server;

        public Backup(Server server)
        {
            _server = server;
            CheckDirectory();
        }
        public Task LaunchBackup()
        {
            if (!File.Exists(TargetPath))
            {
                ZipFile.CreateFromDirectory(SavePath, TargetPath);
            }
            return Task.CompletedTask;
        }

        /// <summary>
        /// TODO: Написать код восстановления
        /// </summary>
        //public Task RestoreBackup()
        //{
        //    return Task.CompletedTask;
        //}

        private void CheckDirectory()
        {
            if (!Directory.Exists(TargetDirectory))
            {
                Directory.CreateDirectory(TargetDirectory);
            }
        }
        private string SavePath => Path.Combine(_server.ServerPath, _server.SaveDataPath);
        private string TargetDirectory => Path.Combine(_server.ServerPath, "backup");
        private string TargetPath => Path.Combine(TargetDirectory, $"{_server.Name}{DateTime.Today:yyyyMMdd}.zip");
    }
}
