using ARKServerManager.Models;

namespace ARKServerManager.DataProvider
{
    public record ServerCommand
    {
        public string GetPlayers { get; private set; }

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
        }

        private void CreateCommandArk()
        {
            GetPlayers = "listplayers";
        }
    }
}
