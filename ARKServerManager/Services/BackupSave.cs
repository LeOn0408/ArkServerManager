using System.IO.Compression;

namespace ARKServerManager.ServerService
{
    public class BackupSave
    {

        public Task Backup(Database.DatabaseContext db, int serverId)
        {
            var server  = db.Server.Where(x=>x.Id==serverId).FirstOrDefault();
            if (server == null)
            {
                return Task.CompletedTask;
            }

            string savepath = Path.Combine(server.ServerPath,server.SaveDataPath);
            string backupDirectory = Path.Combine(server.ServerPath, "backup");
            if (!Directory.Exists(backupDirectory))
            {
                Directory.CreateDirectory(backupDirectory);
            }
            string backuppath = Path.Combine(backupDirectory,$"{server.Name}{DateTime.Today:yyyyMMdd}.zip");
            if (!File.Exists(backuppath))
            {

                ZipFile.CreateFromDirectory(savepath, backuppath);
            }
            return Task.CompletedTask;
        }

    }
}
