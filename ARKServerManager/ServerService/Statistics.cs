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
        public Statistics()
        {
            
        }

        readonly DatabaseContext Db = new();
        

        public async void StartStatistics()
        {
            var loggerFactory = LoggerFactory.Create(builder =>
            {
                builder.AddConsole();
            });
            ILogger<OnlinePlayersController> logger = loggerFactory.CreateLogger<OnlinePlayersController>();
            TimeSpan time = new(0, 5, 0);
            while (true)
            {
                List<ServerApi> servers = await new OnlinePlayersController(Db, logger).Get();
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
                Thread.Sleep(time);
            }
        }
    }
}
