using ServerManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerManager.ViewModel
{
    internal class AppShellViewModel:ViewModelBase
    {
        internal List<ServerShellInfo> AppShellList;
        
        public AppShellViewModel()
        {
            AppShellList = new List<ServerShellInfo>() { new ServerShellInfo() {Name = "Ark" } };
        }
    }
}
