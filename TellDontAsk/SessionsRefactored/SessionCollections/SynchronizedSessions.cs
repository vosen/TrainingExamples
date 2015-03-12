using System;
using System.Collections.Generic;

namespace SessionsRefactored
{
  public class SynchronizedSessions : Sessions
  {
    private readonly Sessions _sessions;
    readonly object _syncRoot = new object();

    public SynchronizedSessions(Sessions sessions)
    {
      _sessions = sessions;
    }

    public void Add(Session session)
    {
      lock (_syncRoot)
      {
        _sessions.Add(session);
      }
    }

    public IEnumerable<T> Convert<T>(Func<SessionData, T> converter)
    {
      Func<SessionData, T> lockedConverter = s => { lock (_syncRoot) { return converter(s); } };
      return _sessions.Convert(lockedConverter);
    }

    public void Access(Action<SessionData> callb)
    {
      Action<SessionData> lockedAccess = s => { lock (_syncRoot) { callb(s); } };
      _sessions.Access(lockedAccess);
    }
  }
}