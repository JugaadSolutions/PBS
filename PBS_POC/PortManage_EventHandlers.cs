using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PBS_POC
{
    public partial class MainWindow : Window
    {
        void _PortManageControl_PortEvent(object sender, PortArgs e)
        {

            //Socket bc = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            //bc.EnableBroadcast = true;
            ///bc.Connect("192,168,1,255", 6868);

            String response = e.ID.ToString();
            switch (e.Action)
            {
                case 1:
                    response += "|I|^";
                    break;

                case 2:
                    response += "|O|^";
                    break;




            }
            byte[] data = Encoding.ASCII.GetBytes(response);


            //bc.Send(data);


            foreach (TcpClient client in Clients)
            {
                try
                {
                    var nw = client.GetStream();
                    response = e.ID.ToString();
                    switch (e.Action)
                    {
                        case 1:
                            response += "|I|^";
                            break;

                        case 2:
                            response += "|O|^";
                            break;




                    }
                    var writer = new StreamWriter(nw); ;
                    writer.AutoFlush = true;

                    writer.WriteAsync(response);
                }
                catch (Exception exp)
                {
                    return;
                }
            }
            MessageBox.Show("Message Sent to Docking Station", "App Info", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
