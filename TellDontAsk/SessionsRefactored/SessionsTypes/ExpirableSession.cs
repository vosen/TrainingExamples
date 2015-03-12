using System;
using Microsoft.FSharp.Core;

namespace SessionsRefactored.SessionsTypes
{
  public class ExpirableSession : Session
  {
    private readonly Session _session;
    private readonly DateTime _expiryTime;

    public ExpirableSession(Session session, DateTime expiryTime)
    {
      _session = session;
      _expiryTime = expiryTime;
    }

    public bool CanAccess { get { return DateTime.Now < _expiryTime; } }

    public FSharpOption<T> Convert<T>(Func<SessionData, T> converter)
    {
      if (this.CanAccess)
        return _session.Convert(converter);
      return FSharpOption<T>.None;
    }

    public void Access(Action<SessionData> callb)
    {
      if (this.CanAccess)
        _session.Access(callb);
    }
  }
}