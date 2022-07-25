using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerManager.ViewModel
{
    internal class AppShellViewModel:ViewModelBase
    {
        public string Ark { get; set; }
        public string Minecraft { get; set; }
        public AppShellViewModel()
        {
            Ark = "Ark Server Launcher";
            Minecraft = "Minecraft Server Launcher";
        }
    }
}
