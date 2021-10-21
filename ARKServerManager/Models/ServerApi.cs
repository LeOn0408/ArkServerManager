using System.Collections.Generic;

namespace ARKServerManager.Models
{
    public class ServerApi
    {
        /// <summary>
        /// ИД Сервера
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Имя сервера
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Игроки на сервере
        /// </summary>
        public List<Player> Players { get; set; }
        public string Map {  get; set; }
        public string Version {  get; set; }
        public string RemoteIP {  get; set; }
        public int RemotePort { get; set; }

    }
}
