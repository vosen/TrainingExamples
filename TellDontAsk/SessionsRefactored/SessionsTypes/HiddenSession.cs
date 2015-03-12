using System;
using Microsoft.FSharp.Core;

namespace SessionsRefactored.SessionsTypes
{
  public class HiddenSession : Session
  {
    private readonly Session _session;

    public HiddenSession(Session session)
    {
      _session = session;
    }

    public FSharpOption<T> Convert<T>(Func<SessionData, T> converter)
    {
      return FSharpOption<T>.None;
    }

    public void Access(Action<SessionData> callb)
    {
    }
  }
}