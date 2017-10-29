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
        // 0 - Member variables
        // 0.1 - server status
        bool acceptConnections = true;
        bool serverListening = false;
        bool serverTerminating = false;

        // 0.2 - Server
        Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

        // 0.3 - Client data
        List<Socket> socketList = new List<Socket>();
        List<string> username_list = new List<string>();

        public FormServer()
        {
            InitializeComponent();
        }

        private string getLocalIP() //Returns the ip of the server
        {
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                    return ip.ToString();
            }
            return "127.0.0.1";
        }

        private void btnListen_Click(object sender, EventArgs e)
        {
            // Post-condition: Start listening for connections on the specified IP & Port

            int serverPort = Convert.ToInt32(txtPort.Text);

            // Start a thread responsible for listening for new connections
            Thread thrAccept;

            try
            {
                server.Bind(new IPEndPoint(IPAddress.Any, serverPort));
                server.Listen(3); // The parameter here is the max length of the pending connections queue!
                // start a thread responsible for accepting the connections
                thrAccept = new Thread(new ThreadStart(Accept));
                thrAccept.IsBackground = true; // so that the thread stops when the program terminates!
                thrAccept.Start();
                serverListening = true;
                txtInformation.Text = "Started listening for incoming connections\r\n";

                //added  IP and PORT info to txtInformation
                txtInformation.AppendText("With IP: " + getLocalIP() + "\r\n");
                txtInformation.AppendText("With Port: " + serverPort + "\r\n");
            }
            catch
            {
                txtInformation.Text = "Cannot create a server with the specified port number!\nTerminating";
            }
        }

        private void Accept()
        {
            while (serverListening)
            {
                try
                {
                    socketList.Add(server.Accept());
                    // Start a thread responsible for receiving data over the new socket
                    Thread thrReceive = new Thread(new ThreadStart(Receive));
                    thrReceive.IsBackground = true; // so that the thread stops when the program terminates!
                    thrReceive.Start();
                }
                catch
                {
                    if (serverTerminating)
                    {
                        serverListening = false;
                    }
                }
            }
        }

        private void Receive()
        {
            /* There are two message flags:
             * 1) "u|<username>" -> username input
             * 2) "g|" -> request to get the list of players
             */
            bool connected = true;
            int listPosition = socketList.Count - 1;
            Socket connection = socketList[listPosition];

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
                        string username = incoming_message.Substring(2);
                        username_list.Add(username); // add the username to the list of usernames
                        // display the username in the listbox
                        listBox1.Invoke((MethodInvoker)delegate
                        {
                           listBox1.Items.Add(username);
                        });

                        txtInformation.Invoke((MethodInvoker)delegate
                        {
                            txtInformation.AppendText("\n" + username + " has connected");
                        });
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
                        txtInformation.Invoke((MethodInvoker)delegate
                        {
                            txtInformation.AppendText("\nwhoopise");
                        });
                        
                    }
                }
                catch
                {
                    if (!serverTerminating)
                    {
                        txtInformation.Invoke((MethodInvoker)delegate
                        {
                            txtInformation.AppendText("\n" + username_list[listPosition] + " has disconnected");
                        });
                    }
                    // Close connection and remove all user data
                    connection.Close();
                    connected = false;
                    socketList.Remove(connection);
                    username_list.RemoveAt(listPosition);

                    // Remove displayed items
                    listBox1.Invoke((MethodInvoker)delegate
                    {
                        listBox1.Items.RemoveAt(listPosition);
                    });
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            txtInformation.AppendText("\nTerminating connections");
            serverTerminating = true;
            serverListening = false;
            foreach(Socket connection in socketList)
            {
                connection.Shutdown(SocketShutdown.Both);
                connection.Close();
            }
            System.Windows.Forms.Application.Exit();
        }
    }
}
