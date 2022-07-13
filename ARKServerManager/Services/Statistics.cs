using ARKServerManager.Controllers;
using ARKServerManager.Database;
using ARKServerManager.DataProvider;
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
        private readonly IServiceScopeFactory scopeFactory;
        public Statistics(IServiceScopeFactory _scopeFactory)
        {
            scopeFactory = _scopeFactory;

        }

        public void StartStatistics()
        {
            using IServiceScope _scope = scopeFactory.CreateScope();

            using var Db = _scope.ServiceProvider.GetRequiredService<DatabaseContext>();
            TimeSpan time = new(0, 1, 0);
            List<ServerApi> servers = new ServerDataProvider(Db).GetServers(); 
            foreach (ServerApi server in servers)
            {
                if (!server.IsConnected)
                {
                    continue;
                }
                List<Player> players = server.Players;
                if(players is null)
                {
                    continue;
                }
                foreach (Player player in players)
                {
                    PlayerStatistics ps = Db.Statistics.FirstOrDefault(x => x.PlayerId == player.Id && x.ServerId == server.Id);
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
