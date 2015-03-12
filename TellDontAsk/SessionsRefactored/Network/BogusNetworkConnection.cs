using System.Collections.Generic;
using SessionsRefactored.Destinations;

namespace SessionsRefactored.Network
{
  public class BogusNetworkConnection : NetworkConnection
  {
    public void Send(IEnumerable<SessionInformationMessage> messages)
    {
      //bogus
    }
  }
}