using Models;
using System;
using System.Collections;
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

namespace PBS_POC
{
    /// <summary>
    /// Interaction logic for PBS_ReportsControl.xaml
    /// </summary>
    public partial class PBS_ReportsControl : UserControl
    {
        public event EventHandler<PBSGenerateReportArgs> GenerateEvent;
        public event EventHandler ExportEvent;


        public PBS_ReportsControl()
        {
            InitializeComponent();

        }

        public void SetDataContext(IList gridData)
        {
            ReportGrid.DataContext = null;
            ReportGrid.DataContext = gridData;
        }

        private void btnExport_Click(object sender, RoutedEventArgs e)
        {
            if (ExportEvent != null)
            {
                ExportEvent(this, new EventArgs());
            }
        }

        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            DateTime from = dpFrom.SelectedDate.Value;
            DateTime to = dpTo.SelectedDate.Value;

            if (GenerateEvent != null)
            {
                GenerateEvent(this, new PBSGenerateReportArgs { From = from, To = to });
            }
        }
    }
}
