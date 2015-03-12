using System;
using System.Collections.Generic;
using Microsoft.FSharp.Core;

namespace SessionsRefactored
{
  public interface Sessions
  {
    void Add(Session session);
    IEnumerable<T> Convert<T>(Func<SessionData, T> converter);
    void Access(Action<SessionData> callb);
  }
}