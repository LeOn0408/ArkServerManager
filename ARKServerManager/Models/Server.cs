namespace ARKServerManager.Models
{
    public enum GameServer
    {
        Ark = 1,
        Minecraft,
        Rust,
        Valheim,
    }
    public class Server
    {
        public int Id {  get; set; }    
        
        public string Name {  get; set; }
        
        public string PublicName { get; set; }
        
        public int MaxPlayers { get; set; }
        
        public string LocalIP { get; set; }
        
        public string RemoteIP {  get; set; }
       
        public int LocalPort { get; set; }
        /// <summary>
        /// Порт для удаленного подключения(мониторинг серверов)
        /// </summary>
       
        public int RemotePort { get; set; }
        /// <summary>
        /// Порт игры
        /// </summary>
        
        public int GamePort {  get; set; }
        
        public string Map {  get; set; }
        
        public string Version {  get; set; }
        
        public string ServerPath { get; set; }
        
        public ushort RconPort {  get; set; }
        
        public string RconPass {  get; set; }
        
        //TODO: Переделать на bool 
        public int Visible { get; set; }
        
        public string SaveDataPath { get; set; }

        public GameServer TypeServer { get; set; }
        
    }
}
