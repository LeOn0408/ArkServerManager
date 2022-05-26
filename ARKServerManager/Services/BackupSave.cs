using System.IO.Compression;

namespace ARKServerManager.ServerService
{
    public class BackupSave
    {

        public Task Backup(Database.DatabaseContext db, int serverId)
        {
            string ServerPath = db.Server.Where(x=>x.Id==serverId).FirstOrDefault()?.ServerPath;
            string savepath = ServerPath + "ShooterGame/Saved/SavedArks/";
            string backuppath = ServerPath + "backup/ARK" + DateTime.Today.ToString("yyyyMMdd") + ".zip";
            if (!File.Exists(backuppath))
            {

                ZipFile.CreateFromDirectory(savepath, backuppath);
            }
            return Task.CompletedTask;
        }

    }
}
