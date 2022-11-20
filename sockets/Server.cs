using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sockets
{
    public partial class Server : Form
    {
        Form1 dd;
        String info;
        public Server()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 ff = new Form1();
            ff.Show();
          
         
        }

      

        public void button2_Click(object sender, EventArgs e)
        {
            Client cc = new Client();
            cc.Show();
        }

        private void Server_Load(object sender, EventArgs e)
        {
          
        }
    }
}
