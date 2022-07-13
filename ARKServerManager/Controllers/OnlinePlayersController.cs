using ARKServerManager.Database;
using ARKServerManager.DataProvider;
using ARKServerManager.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ARKServerManager.Controllers
{
    [Route("api/onlineplayers")]
    [ApiController]
    public class OnlinePlayersController : ControllerBase
    {
        DatabaseContext Db {  get; set; }   
        public OnlinePlayersController(DatabaseContext database)
        {
            Db = database;
        }

        [HttpGet]
        [Produces("application/json")]
        public List<ServerApi> Get()
        {
            return new ServerDataProvider(Db).GetServers();
        }
    }
}
