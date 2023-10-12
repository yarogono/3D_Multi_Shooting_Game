using Server.Session;
using ServerCore;

namespace Server
{
	public class GameRoom : IJobQueue
	{
		List<ClientSession> _sessions = new List<ClientSession>();
		JobQueue _jobQueue = new JobQueue();
		List<ArraySegment<byte>> _pendingList = new List<ArraySegment<byte>>();

		public void Push(Action job)
		{
			_jobQueue.Push(job);
		}

		public void Flush()
		{
			if (_sessions.Count == 0)
				return;

			// N ^ 2
			foreach (ClientSession s in _sessions)
				s.Send(_pendingList);

			_pendingList.Clear();
		}

		public void Broadcast(ArraySegment<byte> segment)
		{
			_pendingList.Add(segment);
		}

		public void Enter(ClientSession session)
		{
			_sessions.Add(session);
			session.Room = this;
		}

		public void Leave(ClientSession session)
		{
			_sessions.Remove(session);
		}
    }
}
