namespace DummyClient
{
    class SessionManager
	{
		 static SessionManager _session = new SessionManager();
		public static SessionManager Instance { get { return _session; } }

		List<ServerSession> _sessions = new List<ServerSession>();
		object _lock = new object();
        int _dummyId = 1;

		public ServerSession Generate()
		{
			lock (_lock)
			{
				ServerSession session = new ServerSession();
                session.DummyId = _dummyId;
                _dummyId++;

				_sessions.Add(session);
				return session;
			}
		}

        public void Remove(ServerSession session)
        {
            lock (_lock)
            {
                _sessions.Remove(session);
            }
        }


		public void SendForEach()
		{
			lock (_lock)
			{
				foreach (ServerSession session in _sessions)
				{
					//C_Chat chatPacket = new C_Chat();
					//chatPacket.chat = $"Hello Server !";
					//ArraySegment<byte> segment = chatPacket.Write();

					//session.Send(segment);
				}
			}
		}

	}
}
