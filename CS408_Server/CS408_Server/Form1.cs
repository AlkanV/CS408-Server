using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;
using System.Net;

namespace CS408_Server
{
    public partial class FormServer : Form
    {
        bool accept = true;
        bool listening = false;
        bool terminating = false;
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        List<Socket> socketList = new List<Socket>();
        List<string> username_list;
        public FormServer()
        {
            InitializeComponent();
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            int serverPort = Convert.ToInt32(textBox1.Text);
            Thread thrAccept;

            try
            {
                server.Bind(new IPEndPoint(IPAddress.Any, serverPort));
                Console.WriteLine("Started listening for incoming connections");
                txtInformation.Text = "Started listening for incoming connections";

                server.Listen(3); // The parameter here is the max length of the pending connections queue!
                thrAccept = new Thread(new ThreadStart(Accept));
                thrAccept.Start();
                listening = true;
            }
            catch
            {
                txtInformation.Text = "Cannot create a server with the specified port number!\nTerminating";
            }
        }

        private void Accept()
        {
            while (accept)
            {
                try
                {
                    socketList.Add(server.Accept());
                    Thread thrReceive = new Thread(new ThreadStart(Receive));
                    thrReceive.Start();
                }
                catch
                {
                    if (terminating)
                    {
                        accept = false;
                    }
                }
            }
        }

        private void Receive()
        {
            bool connected = true;
            Socket connection = socketList[socketList.Count - 1];

            while (connected)
            {
                try
                {
                    Byte[] buffer = new Byte[64];
                    int received = connection.Receive(buffer);

                    if (received <= 0)
                    {
                        throw new SocketException();
                    }

                    string incoming_message = Encoding.Default.GetString(buffer);
                    incoming_message = incoming_message.Substring(0, incoming_message.IndexOf('\0'));

                    string message_flag = incoming_message.Substring(0, incoming_message.IndexOf('|'));

                    if (message_flag == "u")
                    {
                        username_list.Add(incoming_message); // add the username to the list of usernames
                        listBox1.Items.Add(incoming_message); // display the username in the listbox
                        txtInformation.AppendText(incoming_message + " has connected");
                    }
                    else if (message_flag == "g")
                    {
                        for (int i = 0; i < username_list.Count; i++)
                        {
                            connection.Send(Encoding.ASCII.GetBytes(username_list[i]));
                        }
                    }
                    else
                    {
                        txtInformation.AppendText("\nwhoopise");
                    }
                }
                catch
                {
                    if (!terminating)
                    {
                        txtInformation.Text = "Client has disconnected!";
                    }
                    connection.Close();
                    socketList.Remove(connection);
                    connected = false;
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            txtInformation.AppendText("\nTerminating connections");
            foreach(Socket connection in socketList)
            {
                connection.Shutdown(SocketShutdown.Both);
                connection.Close();
            }
            System.Windows.Forms.Application.Exit();
        }
    }
}
