using ARKServerManager.Models;

namespace ARKServerManager.DataProvider
{
    public record ServerCommand
    {
        public string GetPlayers { get; private set; }
        public string GetLog { get; private set; }

        public ServerCommand(GameServer typeServer)
        {
            switch (typeServer)
            {
                case GameServer.Ark:
                    CreateCommandArk();
                    break;
                case GameServer.Minecraft:
                    CreateCommandMinecraft();
                    break;
                default: 
                    break;
            }
        }

        private void CreateCommandMinecraft()
        {
            GetPlayers = "players";
            GetLog = "GetGameLog";
        }

        private void CreateCommandArk()
        {
            GetPlayers = "listplayers";
        }
    }
}
