using EntityModel;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace PBS_POC
{
    public class DBSeed
    {
        static public void SeedDB(EntityModel.PBSContext context)
        {
            context.Cities.AddOrUpdate(new City
            {
                CityID = 1,
                Name = "Mysuru",
                Zones = new List<Zone> {
                     new Zone {
                         ZoneID = 1,
                         Name = "Zone 1",
                          DockingStations = new List<DockingStation> 
                          {
                              new DockingStation 
                              { 
                                  DockingStationID = 1,
                                  Name = "Jayanagar MCC",
                                  DockingUnits = new List<DockingUnit> 
                                  {
                                    new DockingUnit { DockingUnitID = 1, No = 1},
                                    new DockingUnit { DockingUnitID = 2 , No = 2}
                                  }
                              },
                              new DockingStation 
                              { 
                                  DockingStationID = 2,
                                  Name = "Lingaiah Circle",
                                  DockingUnits = new List<DockingUnit> 
                                  {
                                    new DockingUnit { DockingUnitID = 3, No = 3},
                                    new DockingUnit { DockingUnitID = 4 , No = 4}
                                  }
                              }
                          }
                     }
                 }
            }



               );
        }
    }
}
