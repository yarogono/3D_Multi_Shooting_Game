using Server.Data;
using Server.Game.Room;
using Server.Session;
using ServerCore;

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

            LoggerConfig.CreateLogger("../../../log/zlogFile.log");

            GameLogic.Instance.Push(() =>
            {
                GameRoom room = GameLogic.Instance.Add();
                room.Init();
            });

            NetworkService networkService = new NetworkService();
            networkService.Star();
            
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