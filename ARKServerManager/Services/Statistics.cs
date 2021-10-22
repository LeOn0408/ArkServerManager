using ARKServerManager.Controllers;
using ARKServerManager.Database;
using ARKServerManager.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace ARKServerManager.ServerService
{
    public class Statistics
    {
        readonly DatabaseContext Db;
        private readonly IServiceScopeFactory scopeFactory;
        public Statistics(IServiceScopeFactory _scopeFactory)
        {
            scopeFactory = _scopeFactory;

        }


        public async void StartStatistics()
        {
            using IServiceScope _scope = scopeFactory.CreateScope();

            using var Db = _scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            TimeSpan time = new(0, 1, 0);
            List<ServerApi> servers = await new OnlinePlayersController(Db).Get();
            foreach (ServerApi server in servers)
            {
                if (server.Id < 0) continue;//server.Id<0 это ошибки- . Необходимо прервать

                List<Player> players = server.Players;
                foreach (Player player in players)
                {
                    if(player.Id == "0")
                    {
                        continue;
                    }
                    PlayerStatistics ps = Db.Statistics.FirstOrDefault(x=>x.PlayerId==player.Id);
                    if (ps == null)
                    {
                        ps = new PlayerStatistics
                        {
                            PlayerId = player.Id,
                            TimeGame = time,
                            LastGame = DateTime.Now,
                            PlayerName = player.User,
                            ServerId = server.Id
                        };
                        Db.Statistics.Add(ps);
                    }
                    else
                    {
                        ps.LastGame = DateTime.Now;
                        ps.TimeGame = ps.TimeGame.Add(time);
                    }
                    Db.SaveChanges();
                }
                
            }
        }
    }
}
