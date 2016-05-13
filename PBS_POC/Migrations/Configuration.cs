namespace PBS_POC.Migrations
{
    using EntityModel;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EntityModel.PBSContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EntityModel.PBSContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            context.Cities.AddOrUpdate( new City{
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

            context.Cycles.AddOrUpdate(
                new Cycle { CycleID = 1, No = 1, Tag = "D5-72-23-50" },
                new Cycle { CycleID = 2, No = 2, Tag = "25-B5-2E-50" }
                );

            context.Users.AddOrUpdate(
                new User {UserID=1, Name = "Mahesha", Tag = "B3-33-EE-C5" }
                );
        }
    }
}
