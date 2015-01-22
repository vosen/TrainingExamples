﻿using System;
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
      var sessions = new Sessions();
      AddExemplaryDataTo(sessions);

      //different destinations where sessions can be dumped:
      var consoleDestination = new ConsoleDestination();
      var networkPacketBuilder = new NetworkPacketBuilder();
      var fileDestination1 = new FileStorageFormat1();
      var fileDestination2 = new FileStorageFormat2();
      var populationOfOwnersListOnGui = new PopulationOfOwnersListOnGui(new WpfBasedOwnersList());

      ///////////////////////////////////////
      // 1. These two prove that we have eliminated redundancy in:
      // a) what data belongs to session
      // b) in which order they are dumped
      sessions.DumpTo(consoleDestination);
      sessions.DumpTo(fileDestination1);

      ///////////////////////////////////////
      // 2. These three prove that we have not lost any of the flexibility we had
      // with the getters approach:

      //additionally these two always want to write _all_ of the session fields, so they belong to 1.a. as well
      sessions.DumpTo(fileDestination2); 
      sessions.DumpTo(networkPacketBuilder);
      var networkConnection = new BogusNetworkConnection();
      networkPacketBuilder.SendBuiltPacketsThrough(networkConnection);

      sessions.DumpTo(populationOfOwnersListOnGui);
    }

    private static void AddExemplaryDataTo(Sessions sessions)
    {
      //basic session
      sessions.Add(new BasicSession(new SessionData()
      {
        Duration = TimeSpan.FromDays(12),
        Owner = "Zenek",
        Target = "Astro device"
      }));

      // proves that sessions can take advantage of the control over dumping session data:
      // by dumping only if session is not expired
      sessions.Add(new ExpirableSession(new BasicSession(new SessionData()
      {
        Duration = TimeSpan.FromDays(11),
        Owner = "Janek",
        Target = "Dimetra device"
      }), DateTime.Now.AddDays(3)));

      // proves that sessions can take advantage of the control over dumping session data
      // by not dumping at all
      sessions.Add(new HiddenSession(new BasicSession(new SessionData()
      {
        Duration = TimeSpan.FromDays(1),
        Owner = "Czesiek",
        Target = "LTE device"
      })));
    }
  }
}