using System;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Threading;

namespace EncryptedMsg
{
    public partial class Msg_wnd : Form
    {
        public Msg_wnd()
        {
            InitializeComponent();
        }

        public Msg_wnd(TcpClient _client, string str,string _myname)
        {
            ntwrkstrm = _client.GetStream();
            name = str;
            myname = _myname;
            InitializeComponent();
            this.Text = name;
            ListenerThread = new Thread(new ThreadStart(Listener));
            ListenerThread.Start();

            byte[] msg2 = new byte[1 + myname.Length];
            msg2[0] = 2;
            System.Text.Encoding.ASCII.GetBytes(myname.ToCharArray(), 0, myname.Length, msg2, 1);
            ntwrkstrm.Write(msg2, 0, msg2.Length);
        }

        public Msg_wnd(string _ip, string _name)
        {
            InitializeComponent();
            TryConnect(_ip, _name);
        }

        private NetworkStream ntwrkstrm;
        private Thread ListenerThread;
        private string name;
        private string myname;

        private void TryConnect(string _ip, string _name)
        {
            int cnt = 0;
            TcpClient client;
            while (true)
            {
                cnt++;
                try
                {
                    client = new TcpClient(_ip, EncryptedMsg.Properties.Settings.Default.DefaultPort);
                    if (client.Connected) break;
                }
                catch (SocketException exception)
                {
                    ChatLogEdit.AppendText(exception.Message);
                }
                if (cnt >= 10) return;
            }
            ntwrkstrm = client.GetStream();
            byte[] msg = new byte[1 + _name.Length];
            msg[0] = 1;
            System.Text.Encoding.ASCII.GetBytes(_name.ToCharArray(), 0, _name.Length, msg, 1);
            ntwrkstrm.Write(msg, 0, msg.Length);
            myname = _name;

            ListenerThread = new Thread(new ThreadStart(Listener));
            ListenerThread.Start();
        }

        private void WriteLog(string msg)
        {
            ChatLogEdit.AppendText(msg + "\n");
        }

        private void SetName(string _name)
        {
            name = _name;
            this.Text = name;
        }

        private void Listener()
        {
            byte[] buffer = new byte[512];

            do
            {
                int i = ntwrkstrm.Read(buffer, 0, buffer.Length);
                if (i > 0)
                {
                    switch (buffer[0])
                    {
                        case 2:
                            {
                                string name = System.Text.Encoding.ASCII.GetString(buffer, 1, i - 1);
                                Invoke(new Action<string>(SetName), name);
                            }
                            break;
                        default:
                            //string str = System.Text.Encoding.ASCII.GetString(buffer, 0, i);
                            string str = TextCoder.Decode(buffer);
                            Invoke(new Action<string>(WriteLog), name + ": " + str);
                            break;
                    }
                }
            } while (true);
        }

        private void SendMsgBtn_Click(object sender, EventArgs e)
        {
            byte[] buf = TextCoder.Encode(SendMsgEdit.Text);

            ntwrkstrm.Write(buf, 0, buf.Length);
            ChatLogEdit.AppendText(myname + ": " + SendMsgEdit.Text + "\n");
            SendMsgEdit.Clear();
        }

        private void SendMsgEdit_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '\r')
            {
                SendMsgBtn_Click(sender, e);
                SendMsgEdit.Clear();
            }
        }
    }
}
