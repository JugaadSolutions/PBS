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
using System.Windows.Threading;

namespace PBS_POC
{
    public partial class MainWindow : Window
    {
        public static string[] TRANSACTION_TYPE = 
        { 
            "",
            "CHECK_OUT",
            "CHECK_IN"
    };
        public static string[] TRANSACTION_STATUS =
        {
            "",
            "CHECK_OUT_OPEN",
            "CHECK_IN_CLOSED",
            "CHECK_IN_OPEN",
            "CHECK_OUT_CLOSED"
        };
        void ServerThread_DoWork(object sender, DoWorkEventArgs e)
        {
            StartServer();

        }

        private async void StartServer()
        {
            try
            {
                Server.Start();
                await this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                  new Action(() =>
                                  {
                                      _LogControl.AddLogEntry("Server Started ");
                                  }));
                while (true)
                {
                    TcpClient client = await Server.AcceptTcpClientAsync();
                    Clients.Add(client);
                    HandleConnectionAsync(client);
                }
            }
            catch (Exception e)
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                                new Action(() =>
                                {
                                    _LogControl.AddLogEntry(e.Message);
                                }));
            }
        }

        private async void HandleConnectionAsync(TcpClient tcpClient)
        {
            byte[] dataBuffer = new byte[256];
            string clientInfo = tcpClient.Client.RemoteEndPoint.ToString();
            this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                           new Action(() =>
                           {
                               _LogControl.AddLogEntry(string.Format("Client Connected - ", clientInfo));
                           }));
            try
            {

                using (var networkStream = tcpClient.GetStream())
                {
                    while (true)
                    {
                        var data = await networkStream.ReadAsync(dataBuffer, 0, 15);
                        String requestDetails = "";

                        foreach (KeyValuePair<int, String> du in DockingUnits)
                        {
                            if (du.Key == dataBuffer[0])
                            {
                                requestDetails = du.Value;
                            }
                        }

                        int Port = Convert.ToInt32(dataBuffer[1]);

                        requestDetails += "Port:" + Port.ToString();


                        switch (dataBuffer[2])
                        {
                            case 0:
                                requestDetails += " Cycle Found with ID ";

                                requestDetails += BitConverter.ToString(dataBuffer, 3, 4) + " at --" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                                break;

                            case 1: requestDetails += " Check Out By ";
                                String userTag = BitConverter.ToString(dataBuffer, 3, 4);
                                String assetTag = BitConverter.ToString(dataBuffer, 7, 4);
                                if (Users.ContainsKey(userTag))
                                {
                                    requestDetails += Users[userTag] + " at --" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                                }
                                else
                                {

                                    requestDetails += "Unknown User" + " at --" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                                }

                                Transaction t = new Transaction();
                                TransactionStep cOut = new TransactionStep
                                {
                                    Timestamp = DateTime.Now,
                                    UserTag = userTag,
                                    AssetTag = assetTag,
                                    DockingUnitID = dataBuffer[0],
                                    DockingPort = dataBuffer[1],
                                    Type = "CHECK_OUT"

                                };
                                t.CheckOut = cOut;

                                t.Status = "CHECK_OUT_OPEN";

                                using (PBSContext dbContext = new PBSContext())
                                {
                                    dbContext.Transactions.Add(t);
                                    dbContext.SaveChanges();
                                }

                                break;

                            case 2: requestDetails += " Check In of Cycle No:";
                                // var dataarray = dataBuffer.Skip(3).Take(12).ToArray();
                                String cycleTag = BitConverter.ToString(dataBuffer, 3, 4);

                                if (Cycles.ContainsKey(cycleTag))
                                {
                                    requestDetails += Cycles[cycleTag] + " at --" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                                }
                                else
                                {
                                    requestDetails += "Unknown Cycle" + " at --" + DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss");
                                }
                                using (PBSContext DbContext = new PBSContext())
                                {
                                    var tran = DbContext.Transactions.SingleOrDefault(
                                        c => c.CheckOut.AssetTag == cycleTag 
                                            && c.Status == "CHECK_OUT_OPEN");
                                    TransactionStep cIn;

                                    cIn = new TransactionStep
                                    {
                                        Timestamp = DateTime.Now,
                                        AssetTag = cycleTag,
                                        DockingUnitID = dataBuffer[0],
                                        DockingPort = dataBuffer[1],
                                        Type = "CHECK_IN"
                                    };
                                    if (tran != null)
                                    {
                                        tran.CheckIn = cIn;
                                        tran.Status =
                                            "CHECK_IN_CLOSED";
                                        DbContext.SaveChanges();
                                    }
                                    else
                                    {
                                        Transaction tIn = new Transaction();
                                        tIn.Status = "CHECK_IN_OPEN";
                                        tIn.CheckIn = cIn;
                                        DbContext.Transactions.Add(tIn);
                                        DbContext.SaveChanges();
                                    }

                                    
                                }

                                break;
                        }


                        this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                       new Action(() =>
                       {
                           _LogControl.AddLogEntry(requestDetails);
                       }));



                        var writer = new StreamWriter(networkStream);
                        writer.AutoFlush = true;
                        if (dataBuffer[2] != 0)
                        {
                            String response = Port.ToString() + "|Y|^";
                            await writer.WriteAsync(response);
                        }

                    }
                }

            }
            catch (Exception exp)
            {
                this.Dispatcher.BeginInvoke(DispatcherPriority.Background,
                           new Action(() =>
                           {
                               _LogControl.AddLogEntry(exp.Message);
                           }));
            }


        }



        private static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "127.0.0.1";
        }


    }
}
