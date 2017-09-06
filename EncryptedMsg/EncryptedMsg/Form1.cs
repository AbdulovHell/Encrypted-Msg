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

        private Thread SrvThrd;
        private List<Msg_wnd> MsgWnds;
        private List<TcpClient> Clients;
        private TcpListener Listener;
        private NetworkStream Stream;

        private void AcceptSrv()
        {
            Listener = new TcpListener(IPAddress.Any, EncryptedMsg.Properties.Settings.Default.DefaultPort);

            byte[] input = new byte[512];
            string txt = "The quick brown fox jumps over the lazy dog";
            System.Text.Encoding.ASCII.GetBytes(txt.ToCharArray(), 0, txt.Length, input, 0);

            KeccakSum gh = new KeccakSum(input);        
            gh.FIPS202_SHA3_512();

            byte[] output = gh.Output;

            string str123 = System.Text.Encoding.ASCII.GetString(output, 0, 512);
            Invoke(new Action<string>(WriteLog), str123);
            try
            {
                Listener.Start(10);
            }
            catch (SocketException e)
            {
                MessageBox.Show(e.Message);
                Invoke(new Action(Close));
                return;
            }

            do
            {
                Clients.Add(Listener.AcceptTcpClient());
                Stream = Clients[Clients.Count - 1].GetStream();

                byte[] buffer = new byte[512];
                bool accepted = false;

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
                                    string str = System.Text.Encoding.ASCII.GetString(buffer, 1, i - 1);
                                    Invoke(new Action<TcpClient, string, string>(NewWindow), Clients[Clients.Count - 1], str, NameEdit.Text);
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

        private void NewWindow(TcpClient _client, string str, string _name)
        {
            MsgWnds.Add(new Msg_wnd(_client, str, _name));
            MsgWnds[MsgWnds.Count - 1].Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            SrvThrd = new Thread(new ThreadStart(AcceptSrv));
            SrvThrd.Start();

            //byte[] mas = TextCoder.Encode("тестовая строка");
            //string str = TextCoder.Decode(mas);
        }

        private void ConnectP2P_Click(object sender, EventArgs e)
        {
            MsgWnds.Add(new Msg_wnd(P2PAddrEdit.Text, NameEdit.Text));
            MsgWnds[MsgWnds.Count - 1].Show();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Stream != null) Stream.Close();
            if (Listener != null) Listener.Stop();
            SrvThrd.Abort();
        }
    }
}
