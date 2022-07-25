using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServerManager.Services
{
    internal class IP
    {
        public static async Task<string> Get()
        {
            using HttpClient client = new ();
            HttpResponseMessage response = await client.GetAsync("https://www.trackip.net/ip");
            return await response.Content.ReadAsStringAsync();
        }
    }
}
