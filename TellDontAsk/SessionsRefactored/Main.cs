using System;
using System.IO;
using SessionsRefactored.Destinations;
using SessionsRefactored.GUI;
using SessionsRefactored.Network;
using SessionsRefactored.SessionsTypes;

namespace SessionsRefactored
{
  public class Main
  {
    public void Program()
    {
      var wpfList = new WpfBasedOwnersList();

      ///////////////////////////////////////
      // 1. Hiding access to sessions allows protecting the collection against
      //    concurrent additions with synchronized wrapper
      Sessions sessions = new SynchronizedSessions(new BasicSessions());
      AddExempleDataTo(sessions);

      ///////////////////////////////////////
      // 2. These two prove that we have eliminated redundancy in:
      //    a) what data belongs to session
      //    b) in which order they are dumped
      Console.WriteLine(String.Join(Environment.NewLine, sessions.Convert(Formatter.LineByLine)));
      File.AppendAllText("zenek.txt", String.Join(Environment.NewLine, sessions.Convert(Formatter.LineByLine)));

      ///////////////////////////////////////
      // 3. These three prove that we have not lost flexibility we had with the getters approach:

      //    additionally these two always want to write _all_ of the session fields, so they belong to 2.a. as well
      File.AppendAllText("lolek.txt", String.Join(Environment.NewLine, sessions.Convert(Formatter.RichFile)));
      new BogusNetworkConnection().Send(sessions.Convert(Formatter.Network));
      sessions.Access(d => wpfList.AddVisible(d.Owner));
      sessions.Convert(Formatter.Null<string>);
    }

    private static void AddExempleDataTo(Sessions sessions)
    {
      //basic session
      sessions.Add(new BasicSession(new SessionData()
      {
        Id = 1,
        Owner = "Zenek",
        Target = "Astro device"
      }));

      // proves that sessions can take advantage of the control over dumping session data:
      // by dumping only if session is not expired
      sessions.Add(new ExpirableSession(new BasicSession(new SessionData()
      {
        Id = 2,
        Owner = "Janek",
        Target = "Dimetra device"
      }), DateTime.Now.AddDays(3)));

      // proves that sessions can take advantage of the control over dumping session data
      // by not dumping at all
      sessions.Add(new HiddenSession(new BasicSession(new SessionData()
      {
        Id = 3,
        Owner = "Czesiek",
        Target = "LTE device"
      })));
    }
  }
}