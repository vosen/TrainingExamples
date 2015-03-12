using System;
using Microsoft.FSharp.Core;

namespace SessionsRefactored
{
  public interface Session
  {
    FSharpOption<T> Convert<T>(Func<SessionData, T> converter);
    void Access(Action<SessionData> callb);
  }
}