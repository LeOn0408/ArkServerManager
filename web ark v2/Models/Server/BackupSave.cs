using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using Microsoft.Extensions.Logging;

namespace ArkWeb
{
    public class BackupSave
    {
       
        public Task Backup()
        {
            string savepath = ServerSettings.ServerPath + "ShooterGame/Saved/SavedArks/";
            string backuppath = ServerSettings.ServerPath + "backup/ARK" + DateTime.Today.ToString("yyyyMMdd") + ".zip";
            if (!File.Exists(backuppath))
            {
                
                ZipFile.CreateFromDirectory(savepath, backuppath);
               
            }



            return Task.CompletedTask;
        }

        public static async void Restore()
        {
           

        }
    }
}
