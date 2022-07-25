using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerManager.Services
{
    internal class IP
    {
        private static string iPAddress;

        internal static string IPAddress { get => iPAddress; set => iPAddress = "1.1.1.1"; }

    }
}
