using System;
using Microsoft.FSharp.Core;

namespace SessionsRefactored.SessionsTypes
{
  public class BasicSession : Session
  {
    private readonly SessionData _sessionData;

    public BasicSession(SessionData sessionData)
    {
      _sessionData = sessionData;
    }

    FSharpOption<T> Session.Convert<T>(Func<SessionData, T> converter)
    {
      return FSharpOption<T>.Some(converter(_sessionData));
    }

    public void Access(Action<SessionData> callb)
    {
      callb(_sessionData);
    }
  }
}