using EntityModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using UILibrary;

namespace PBS_POC
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DashboardControl _DashboardControl;
        GenericTabControl _TabControl;
        LogControl _LogControl;

        List<TcpClient> Clients;

        PortManageControl _PortManageControl;
        PBS_ReportsControl _ReportsControl;

        PBS_UserManagementControl _UserManagementControl;

        String[] tabs = { "Log", "Manage", "Reports" , "UserManagement" };

        TcpListener Server;

        BackgroundWorker ServerThread;

        Dictionary<String,String> Users;
        Dictionary<String, int> Cycles;

        Dictionary<int,String> DockingUnits;

        List<TransactionRecord> TransactionRecords;

        public MainWindow()
        {
            InitializeComponent();

            try
            {
                _DashboardControl = new DashboardControl();
                _TabControl = new GenericTabControl(tabs);
                _LogControl = new LogControl();
                _PortManageControl = new PortManageControl(4);
                _PortManageControl.PortEvent += _PortManageControl_PortEvent;

                _ReportsControl = new PBS_ReportsControl();
                _ReportsControl.GenerateEvent += _ReportsControl_GenerateEvent;
                _ReportsControl.ExportEvent += _ReportsControl_ExportEvent;
                _UserManagementControl = new PBS_UserManagementControl();
                

                _TabControl.AddUserControltoItem(_LogControl, "Log");
                _TabControl.AddUserControltoItem(_PortManageControl, "Manage");
                _TabControl.AddUserControltoItem(_ReportsControl, "Reports");
                _TabControl.AddUserControltoItem(_UserManagementControl, "UserManagement");

                TransactionRecords = new List<TransactionRecord>();

                DockingUnits = new Dictionary<int, string>();
                Users = new Dictionary<string, string>();
                Cycles = new Dictionary<string, int>();
                using (PBSContext DbContext = new PBSContext())
                {
                    List<DockingUnit> duList = new List<DockingUnit>();
                    duList = DbContext.DockingUnits.ToList();
                    foreach (DockingUnit du in duList)
                    {
                        DockingUnits.Add(du.DockingUnitID, du.ToString());
                    }

                    var uList = DbContext.Users.ToList();
                    foreach(User u in uList)
                    {
                        Users.Add(u.Tag, u.Name);
                    }

                    var cList = DbContext.Cycles.ToList();
                    {
                        foreach (Cycle c in cList)
                            Cycles.Add(c.Tag, c.No);
                    }

                }


                String localip = GetLocalIPAddress();


                _DashboardControl.Present(_TabControl);
                this.BaseGrid.Children.Clear();
                this.BaseGrid.Children.Add(_DashboardControl);
                Server = new TcpListener(IPAddress.Parse("192.168.0.100"), 6868);


                Clients = new List<TcpClient>();
                ServerThread = new BackgroundWorker();
                ServerThread.DoWork += ServerThread_DoWork;
                ServerThread.RunWorkerAsync();
            }
            catch (Exception e)
            {
                _LogControl.AddLogEntry(e.Message);
            }

        }

    }
}
