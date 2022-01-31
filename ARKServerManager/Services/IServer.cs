using ARKServerManager.Database;
using ARKServerManager.Models;

namespace ARKServerManager.Services
{
    public interface IServer
    {
        void Launch();
        Task Stop(DatabaseContext Db, ServerTask edit);
        Task UpdateAsync(DatabaseContext Db, ServerTask edit, ILogger<ServerService.ServerJob> logger);
    }
}