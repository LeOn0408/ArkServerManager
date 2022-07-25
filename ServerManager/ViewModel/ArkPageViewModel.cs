using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerManager.ViewModel
{
    internal class ArkPageViewModel : ViewModelBase
    {
        private string ip;

        public string IP
        {
            get => ip;
            set
            {
                ip = value;
                OnPropertyChanged(nameof(IP));
            }
        }

        
        public ArkPageViewModel()
        {
            Launch();
        }
        async void Launch()
        {
            IP = await Services.IP.Get();
        }

    }
}
