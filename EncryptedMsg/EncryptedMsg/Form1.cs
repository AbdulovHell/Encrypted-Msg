using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace EncryptedMsg
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            MsgWnds = new List<Msg_wnd>();
            Clients = new List<TcpClient>();
        }

        ~Form1()
        {
            //TODO: завершить все потоки, закрыть все сокеты
            SrvThrd.Abort();
        }

        private Thread SrvThrd;
        private NetworkStream ntwrkstrm;
        private List<Msg_wnd> MsgWnds;
        private List<TcpClient> Clients;

        private void AcceptSrv()
        {
            TcpListener Listener = new TcpListener(IPAddress.Any, 5555);
            Listener.Start(10);

            do
            {
                Clients.Add(Listener.AcceptTcpClient());
                NetworkStream Stream = Clients[Clients.Count-1].GetStream();

                byte[] buffer = new byte[512];
                bool accepted=false;

                do
                {
                    int i = Stream.Read(buffer, 0, buffer.Length);
                    if (i > 0)
                    {
                        //string str = System.Text.Encoding.ASCII.GetString(buffer, 0, i);
                        //Invoke(new Action<string>(WriteLog), "Srv: " + str);
                        switch (buffer[0])
                        {
                            case 1:
                                {
                                    string str = System.Text.Encoding.ASCII.GetString(buffer, 1, i);
                                    Invoke(new Action<TcpClient,string>(NewWindow), Clients[Clients.Count-1], str);
                                    accepted = true;
                                }
                                break;
                            default:
                                break;
                        }
                    }
                } while (!accepted);
            } while (true);
        }

        private void WriteLog(string msg)
        {
            ProgLog.AppendText(msg + "\n");
        }

        private void NewWindow(TcpClient _client,string str)
        {
            MsgWnds.Add(new Msg_wnd(_client, str));
            MsgWnds[MsgWnds.Count - 1].Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SrvThrd = new Thread(new ThreadStart(AcceptSrv));
            SrvThrd.Start();
        }

        private void ConnectP2P_Click(object sender, EventArgs e)
        {
            int cnt = 0;
            TcpClient client;
            while (true)
            {
                cnt++;
                try
                {
                    client = new TcpClient(P2PAddrEdit.Text, 5555);
                    if (client.Connected) break;
                }
                catch (SocketException exception)
                {
                    ProgLog.AppendText(exception.Message);
                }
                if (cnt >= 10) return;
            }
            ntwrkstrm = client.GetStream();
            byte[] msg = new byte[1 + NameEdit.Text.Length];
            msg[0] = 1;
            System.Text.Encoding.ASCII.GetBytes(NameEdit.Text.ToCharArray(), 0, NameEdit.Text.Length, msg, 1);
            ntwrkstrm.Write(msg, 0, msg.Length);
        }
    }
}
