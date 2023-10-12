using Server.DB;
using ServerCore;
using System.Net;
using System.Text;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Server.Session
{
    public class ClientSession : PacketSession
    {
        public int SessionId { get; set; }
        public GameRoom Room { get; set; }

        #region SessionCore
        public override void OnConnected(EndPoint endPoint)
        {
            Console.WriteLine($"OnConnected : {endPoint}");
            Program.Room.Push(() => Program.Room.Enter(this));
        }

        public override void OnDisconnected(EndPoint endPoint)
        {
            SessionManager.Instance.Remove(this);
            if (Room != null)
            {
                GameRoom room = Room;
                room.Push(() => room.Leave(this));
                Room = null;
            }

            Console.WriteLine($"OnDisconnected : {endPoint}");
        }

        public override void OnRecvPacket(ArraySegment<byte> buffer)
        {
            PacketManager.Instance.OnRecvPacket(this, buffer);
        }

        public override void OnSend(int numOfBytes)
        {
            Console.WriteLine($"Transferred bytes: {numOfBytes}");
        }
        #endregion

        public void SavePlayer(PacketSession session, C_SavePlayer packet)
        {
            if (packet == null)
                return;

            using (AppDbContext db = new AppDbContext())
            {
                AccountDb findAccount = db.Accounts.Where(a => a.AccountName == packet.username).FirstOrDefault();

                if (findAccount != null)
                {
                    S_SavePlayer saveOk = new S_SavePlayer() { saveOk = 1 };
                    Send(saveOk.Write());
                }
                else
                {

                    SHA256Managed sha256Managed = new SHA256Managed();
                    byte[] encryptBytes = sha256Managed.ComputeHash(Encoding.UTF8.GetBytes(packet.password));

                    //base64
                    String encryptString = Convert.ToBase64String(encryptBytes);

                    AccountDb newAccount = new AccountDb() { AccountName = packet.username, AccountPassword = encryptString };
                    db.Accounts.Add(newAccount);
                    db.SaveChanges();
                }
            }
        }
    }
}
