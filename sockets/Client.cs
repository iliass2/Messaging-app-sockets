using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace sockets
{
    public partial class Client : Form
    {
       public  Socket sender;
        public String ss;
        public int a;
        public Client()
        {
            InitializeComponent();
        }

        private void Client_Load(object sender, EventArgs e)
        {

            Task taskA = new Task(() => ExecuteClient(ss));
            taskA.Start();
            Task taskf = new Task(() => receive());
            taskf.Start();

        }
        public void ExecuteClient(String ss)
        {
        CheckForIllegalCrossThreadCalls = false;
            
            try
            {
                // Establish the remote endpoint
                // for the socket. This example
                // uses port 11111 on the local
                // computer.    
                IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
                IPAddress ipAddr = IPAddress.Parse("192.168.1.103");
                IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

                // Creation TCP/IP Socket using
                // Socket Class Constructor
                 sender = new Socket(ipAddr.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
                Console.WriteLine("ssssssssssss"+ ipHost.AddressList[0].ToString());
                try
                {

                    // Connect Socket to the remote
                    // endpoint using method Connect()
                    sender.Connect(localEndPoint);
                    Console.WriteLine("Socket connected to -> {0} ",
                              sender.RemoteEndPoint.ToString());
                   textBox2.Text+= "connected to ss here " + sender.RemoteEndPoint.ToString();
                    // We print EndPoint information
                    // that we are connected

                }

                // Manage of Socket's Exceptions
               
                catch (Exception e)
                {
                    Console.WriteLine(e.ToString());
                }
            }

            catch (Exception e)
            {

                Console.WriteLine(e.ToString());
            }
        }
        public void send(String msg)
        {
        CheckForIllegalCrossThreadCalls = false;
            try { 
         
            Console.WriteLine("ss is " + ss);
            // Creation of message that
            // we will send to Server
            byte[] messageSent = Encoding.ASCII.GetBytes(msg);
            int byteSent = sender.Send(messageSent);

            // Data buffer
          
            }
                catch(Exception ex)
            {
                Console.WriteLine(textBox1.Text + ex.Message);
            }

            }
        public void receive()
        {
          
            
                while (true)
                {
                    try
                    {

                   
                        byte[] messageReceived = new byte[1024];
                        if (sender != null) { 
                            int byteRecv = sender.Receive(messageReceived);
                           Console.WriteLine("Message from Server -> {0}",Encoding.ASCII.GetString(messageReceived,0, byteRecv));
                           textBox2.Text += "\r\n" + Encoding.ASCII.GetString(messageReceived,0, byteRecv);
                            // Close Socket using
                            // the method Close()
                        }
                    }
                    catch (Exception ex1)
                    {
                    a++;
                    if(a==1)
                    textBox2.Text = ex1.Message;
                        

                    }
                }

          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task taskb = new Task(() => send(textBox1.Text));
            taskb.Start();
        }
    }
}
