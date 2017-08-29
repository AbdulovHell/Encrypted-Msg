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

        public Msg_wnd(TcpClient _client,string str)
        {
            ntwrkstrm = _client.GetStream();
            name = str;
            InitializeComponent();
            this.Text = name;
            ListenerThread = new Thread(new ThreadStart(Listener));
            ListenerThread.Start();
        }

        private NetworkStream ntwrkstrm;
        private Thread ListenerThread;
        private string name;

        private void WriteLog(string msg)
        {
            OutMsgEdit.AppendText(msg + "\n");
        }

        private void Listener()
        {
            byte[] buffer = new byte[512];

            do
            {
                int i = ntwrkstrm.Read(buffer, 0, buffer.Length);
                if (i > 0)
                {
                    string str = System.Text.Encoding.ASCII.GetString(buffer, 0, i);
                    Invoke(new Action<string>(WriteLog), name + ": " + str);
                }
            } while (true);
        }

        private void SendMsgBtn_Click(object sender, EventArgs e)
        {
            byte[] buf = new byte[OutMsgEdit.Text.Length];
            System.Text.Encoding.ASCII.GetBytes(OutMsgEdit.Text.ToCharArray(), 0, OutMsgEdit.Text.Length, buf, 0);
            ntwrkstrm.Write(buf, 0, buf.Length);
        }
    }
}
