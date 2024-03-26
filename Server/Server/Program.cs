using Autofac;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Server.Data;
using Server.Game.Room;
using Server.Logging;
using Server.Session;
using ServerCore;
using System.Diagnostics;

namespace Server
{
    class Program
    {
        public static ServerOption ServerOpt { get; private set; }

        static void GameLogicTask()
        {
            while (true)
            {
                GameLogic.Instance.Update();
                Thread.Sleep(0);
            }
        }

        static void NetworkTask()
        {
            while (true)
            {
                List<ClientSession> sessions = SessionManager.Instance.GetSessions();
                foreach (ClientSession session in sessions)
                {
                    session.FlushSend();
                }

                Thread.Sleep(0);
            }
        }

        static void Main(string[] args)
        {
            ConfigManager.LoadConfig();
            DataManager.LoadData();
            LoggingModule.CreateFactory();

            //var services = new ServiceCollection();
            //services.AddScoped<IListener, Listener>();



            GameLogic.Instance.Push(() =>
            {
                GameRoom room = GameLogic.Instance.Add();
                room.Init();
            });

            var builder = new ContainerBuilder();
            builder.Register(c => new LoggingModule())
                    .As<ILogger>();

            builder.RegisterType<Listener>()
                       .As<IListener>()
                      .AsImplementedInterfaces();

            builder.RegisterType<NetworkService>()
                  .As<INetworkService>()
                  .OnActivated(e => e.Instance.Star());

            var container = builder.Build();

            Console.WriteLine("Listening...");

            // NetworkTask
            {
                Thread t = new Thread(NetworkTask);
                t.Name = "Network Send";
                t.Start();
            }

            // GameLogic
            Thread.CurrentThread.Name = "GameLogic";
            GameLogicTask();
        }
    }
}