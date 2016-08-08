using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Server
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Server.path.Length > 0)
                backgroundWorker1.RunWorkerAsync();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Server.path = Application.StartupPath;

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            label1.Text = Server.Message;
        }

        Server srv = new Server();

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            srv.StartServer();
        }
    }
}
