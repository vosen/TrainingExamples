using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SessionsRefactored.Network;

namespace SessionsRefactored
{
  static class Formatter
  {
    public static string RichFile(SessionData s)
    {
      var sb = new StringBuilder();
      sb.AppendLine("<SESSION>");
      sb.AppendLine(s.Target ?? "No Target");
      sb.AppendLine(s.Owner ?? "No Owner");
      sb.AppendLine(s.Id.ToString());
      sb.AppendLine("</SESSION>");
      return sb.ToString();
    }

    public static T Null<T>(SessionData s) where T : class
    {
      return null;
    }

    public static string LineByLine(SessionData s)
    {
      var sb = new StringBuilder();
      sb.AppendLine(s.Target);
      sb.AppendLine(s.Owner);
      sb.AppendLine(s.Id.ToString());
      return sb.ToString();
    }

    public static SessionInformationMessage Network(SessionData s)
    {
      return new SessionInformationMessage
      {
        Id = s.Id,
        Owner = s.Owner,
        Target = s.Target
      };
    }
  }
}
