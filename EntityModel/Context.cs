using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityModel
{
    public class PBSContext : DbContext
    {
        public DbSet<City> Cities { get; set; }
        public DbSet<Zone> Zones { get; set; }
        public DbSet<DockingStation> DockingStations { get; set; }
        public DbSet<DockingUnit> DockingUnits { get; set; }
        public DbSet<DockingPort> DockingPorts { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Cycle> Cycles { get; set; }
        public DbSet<TransactionStep> TransactionSteps { get; set; }
        public DbSet<Transaction> Transactions { get; set; }



        public PBSContext()
            : base("name=PBSConnection")
        {
            this.Database.Log = s => System.Diagnostics.Debug.WriteLine(s);
        }
    }
}
