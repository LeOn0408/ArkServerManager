using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerManager.ViewModel
{
    internal class AppShellViewModel:ViewModelBase
    {
        public string Title { get; set; }
        public AppShellViewModel()
        {
            Title = "Ark";
        }
    }
}
