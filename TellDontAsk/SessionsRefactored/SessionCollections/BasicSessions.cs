using System.Collections.Generic;
using Microsoft.FSharp.Core;

namespace SessionsRefactored
{
  public class BasicSessions : Sessions
  {
    readonly List<Session> _sessions = new List<Session>();
    public void Add(Session session)
    {
      _sessions.Add(session);
    }

    public IEnumerable<T> Convert<T>(System.Func<SessionData, T> converter)
    {
      foreach (var s in _sessions)
      {
        var option = s.Convert(converter);
        if(FSharpOption<T>.get_IsSome(option))
          yield return option.Value;
      }
    }

    public void Access(System.Action<SessionData> callb)
    {
      foreach (var s in _sessions)
      {
        s.Access(callb);
      }
    }
  }
}

