using System;
using System.Collections;
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

namespace sockets
{
    public partial class Form1 : Form
    {
        String ss;int i=0;
        ArrayList sos;
        public Form1()
        {
            InitializeComponent();
            sos = new ArrayList();
        }

      

        private void Form1_Load(object sender, EventArgs e)
        {

            Task taskA = new Task(() => ExecuteServer());
            taskA.Start();
           

        }
        public void ExecuteServer()
        {
            CheckForIllegalCrossThreadCalls = false;
            // Establish the local endpoint
            // for the socket. Dns.GetHostName
            // returns the name of the host
            // running the application.
            IPHostEntry ipHost = Dns.GetHostEntry(Dns.GetHostName());
            IPAddress ipAddr = IPAddress.Parse("192.168.1.103");
            IPEndPoint localEndPoint = new IPEndPoint(ipAddr, 11111);

            // Creation TCP/IP Socket using
            // Socket Class Constructor
            Socket listener = new Socket(ipAddr.AddressFamily,
                         SocketType.Stream, ProtocolType.Tcp);

            try
            {

                // Using Bind() method we associate a
                // network address to the Server Socket
                // All client that will connect to this
                // Server Socket must know this network
                // Address
                listener.Bind(localEndPoint);

                // Using Listen() method we create
                // the Client list that will want
                // to connect to Server
                listener.Listen(10);
                while (true)
                {

                    Console.WriteLine("Waiting connection ... ");
                    //  info.Text += "Waiting connection... \n";
                    // Suspend while waiting for
                    // incoming connection Using
                    // Accept() method the server
                    // will accept connection of client

                    Socket clientSocket = listener.Accept();
                    sos.Add(clientSocket);
                    new Task(() =>
                    {
                        int here = i;
                        i++;

                        // Data buffer


                        while (true)
                        {
                            byte[] bytes = new Byte[1024];
                            string data = null;
                            int numByte = clientSocket.Receive(bytes);
                            data = Encoding.ASCII.GetString(bytes,
                                                       0, numByte);
                            textBox1.Text +="Number "+(here+1)+" : "+ data + "\r\n";
                            Console.WriteLine("Text received -> {0} ", data);
                            byte[] message = Encoding.ASCII.GetBytes("IP:"+clientSocket.RemoteEndPoint.ToString()+" : "+data);

                            // Send a message to Client
                            // using Send() method
                         //   clientSocket.Send(message);
                            for(int j=0;j<i;j++)
                            {
                                Socket hello = (Socket)sos[j];
                                hello.Send(message);
                                Console.WriteLine(j);
                            }
                        }



                        // Close client Socket using the
                        // Close() method. After closing,
                        // we can use the closed Socket
                        // for a new Client Connection
                    


                    }).Start();
                }
              
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
