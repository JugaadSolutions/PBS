using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using UILibrary;

namespace PBS_POC
{
    /// <summary>
    /// Interaction logic for PortManageControl.xaml
    /// </summary>
    public partial class PortManageControl : UserControl
    {

        public event EventHandler<PortArgs> PortEvent;
        public PortManageControl()
        {
            InitializeComponent();
        }

        public PortManageControl(int ports)
        {
            InitializeComponent();

            for(int i = 0; i < ports;i++)
            {
                DockingPortManageControl uc = new DockingPortManageControl(i + 1);
                uc.CheckInEvent += uc_CheckInEvent;
                uc.CheckOutEvent += uc_CheckOutEvent;
                PortManageGrid.Children.Add(uc);

            }
        }

        void uc_CheckOutEvent(object sender, DockingPortArgs e)
        {
            if( PortEvent != null)
            {
                PortEvent(this, new PortArgs(e.ID, e.Action));
            }
        }

        void uc_CheckInEvent(object sender, DockingPortArgs e)
        {
            if (PortEvent != null)
            {
                PortEvent(this, new PortArgs(e.ID, e.Action));
            }
        }
    }

    public class PortArgs :EventArgs
    {
        public int ID { get; set; }
        public int Action { get; set; }

        public PortArgs(int id, int action)
        {
            ID = id;
            Action = action;
        }
    }
}
