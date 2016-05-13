using EntityModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace PBS_POC
{
    public partial class MainWindow : Window
    {
        void _ReportsControl_ExportEvent(object sender, EventArgs e)
        {

        }

        void _ReportsControl_GenerateEvent(object sender, Models.PBSGenerateReportArgs e)
        {

            using (PBSContext dbContext = new PBSContext())
            {
                e.To = e.To.AddDays(1);
                var Transactions = dbContext.Transactions
                    .Include("CheckOut").Include("CheckIn")
                    .Where(t=>t.Created >= e.From && t.Created <= e.To).ToList();

                TransactionRecords.Clear();

                foreach (Transaction t in Transactions)
                {
                    if (t.CheckOut != null)
                    {
                        User User = dbContext.Users.SingleOrDefault(u => u.Tag == t.CheckOut.UserTag);
                        Cycle Cycle = dbContext.Cycles.SingleOrDefault(c => c.Tag == t.CheckOut.AssetTag);

                        TransactionRecord tr = new TransactionRecord
                            {
                                CheckOutBy = (User == null) ? "Unkown User" : User.Name,
                                CheckOutof = (Cycle == null) ? "Unknown Cycle": Cycle.No.ToString(),
                                CheckOutOn = t.CheckOut.Timestamp.ToString("dd-MM-yyyy HH:mm:ss"),
                                CheckOutFrom = "Unit:" + t.CheckOut.DockingUnitID.ToString() + " Port:" + t.CheckOut.DockingPort.ToString()

                            };

                        if (t.CheckIn != null)
                        {
                            tr.CheckInof = t.CheckIn.AssetTag;
                            tr.CheckInOn = t.CheckIn.Timestamp.ToString("dd-MM-yyyy HH:mm:ss");
                            tr.CheckInAt = "Unit:" + t.CheckIn.DockingUnitID.ToString() + " Port:" + t.CheckIn.DockingPort.ToString();
                        }
                        TransactionRecords.Add(tr);
                    }
                    else
                    {

                        if(t.CheckIn != null)
                        {
                            TransactionRecord tr = new TransactionRecord
                            {
                                 CheckInof = t.CheckIn.AssetTag,
                                 CheckInOn = t.CheckIn.Timestamp.ToString("dd-MM-yyyy HH:mm:ss"),
                                 CheckInAt = "Unit:" + t.CheckIn.DockingUnitID.ToString() + " Port:" + t.CheckIn.DockingPort.ToString()

                            };
                            TransactionRecords.Add(tr);
                        }
                    }
                }

                this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                      new Action(() =>
                      {
                          _ReportsControl.ReportGrid.DataContext = null;
                          _ReportsControl.ReportGrid.DataContext = TransactionRecords;


                      }));

            }
        }
    }
}
