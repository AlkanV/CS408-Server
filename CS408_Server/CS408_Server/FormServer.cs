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
        private class Client
        {
            public Socket socket;
            public string username;
            public bool isInGame;

            public override bool Equals(object obj)
            {
                return obj is Client && this == (Client)obj;
            }
            public override int GetHashCode()
            {
                return socket.GetHashCode();
            }
            public static bool operator==(Client lhs, Client rhs)
            {
                return lhs.socket == rhs.socket;
            }
            public static bool operator !=(Client lhs, Client rhs)
            {
                return lhs.socket != rhs.socket;
            }
        }

        List<Client> clients = new List<Client>();

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
            txtPort.Clear();

            if (serverPort < 0 || serverPort > 9999)
            {
                MessageBox.Show("Port Number should be between 0 and 9999", "Invalid Port Number", MessageBoxButtons.OK);
            }
            else
            {
                txtPort.ReadOnly = true;
                btnListen.Enabled = false;
                // Start a thread responsible for listening for new connections
                Thread thrAccept = new Thread(() => { });

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

        }

        private void Accept()
        {
            while (serverListening)
            {
                try
                {
                    Socket newConnection = server.Accept();
                    Client newClient = new Client();
                    newClient.socket = newConnection;
                    clients.Add(newClient);
                    // Start a thread responsible for receiving data over the new socket
                    Thread thrReceive = new Thread(() => Receive(ref newClient));
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

        private void Broadcast(string message_flag, string message_content)
        {
            // message_flag is one of: "i", "e", "m"
            // message content is the string associated with message
            foreach (Client client in clients)
            {
                client.socket.Send(Encoding.ASCII.GetBytes(message_flag + "|[" + DateTime.Now.ToString(@"MM\/dd\/yyyy h\:mm tt") + "] "
                    + message_content));
            }
        }

        private void DisplayInfo(string message)
        {
            txtInformation.Invoke((MethodInvoker)delegate
            {
                txtInformation.AppendText(message + "\n");
            });
        }

        private void Receive(ref Client client)
        {
            /* There are two message flags:
             * 1) "u|<username>" -> username input
             * 2) "g|" -> request to get the list of players
             * 3) "m|" -> chat message
             * 4) "i|" -> info
             * 5) "v|" -> invite request
             * 6) "r|" -> invite response
             */
            
            bool connected = true;
            Socket connection = client.socket;

            string username = ""; // username of the current client
            Client invitation_sent_to = new Client();

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

                    string[] message_content = incoming_message.Split('|');
                    if (message_content.Count() == 0)
                    {
                        continue;
                    }
                    string message_flag = message_content[0], user_message = message_content[1];

                    if (message_flag == "u")
                    {
                        username = user_message;
                        // 1 - Check if the username is valid
                        bool isExistingUsername = false;
                        foreach (Client client_i in clients)
                        {
                            if (username == client_i.username)
                            {
                                connection.Send(Encoding.ASCII.GetBytes("e|username already exists"));
                                isExistingUsername = true;
                                connection.Close();
                            }
                        }
                        if (!isExistingUsername)
                        {
                            // 1 - Perform modificitions to server data
                            client.username = username; // add the username to the list of usernames
                                                        // display the username in the listbox
                            lstUsers.Invoke((MethodInvoker)delegate
                            {
                                lstUsers.Items.Add(username);
                            });

                            DisplayInfo(username + " has connected");
                            // 2 - Send back successful connection flag
                            connection.Send(Encoding.ASCII.GetBytes("i|connection successful"));

                            // 3 - Inform each client about the new connection
                            Broadcast("i", username + " has connected");
                        }
                    }
                    else if (message_flag == "g")
                    {
                        DisplayInfo("Sending user list to " + username);
                        for (int i = 0; i < clients.Count; i++)
                        {
                            string currently_sending = clients[i].username;
                            connection.Send(Encoding.ASCII.GetBytes("g|" + currently_sending));
                        }
                    }
                    else if (message_flag == "m")
                    {
                        Broadcast("m", username + ": " + user_message);
                        DisplayInfo(username + ": " + user_message);
                    }
                    else if (message_flag == "v" || message_flag == "r")
                    {
                        if (message_flag == "r")
                        {
                            if (message_content[1] == "1")
                            {
                                // response is accepted
                                client.socket.Send(Encoding.ASCII.GetBytes("r|1"));
                                DisplayInfo(invitation_sent_to.username + " has accepted invitation from " + username);
                            }
                            else
                            {
                                // response is declined
                                client.socket.Send(Encoding.ASCII.GetBytes("r|0"));
                            }
                        }
                        else
                        {
                            Client find_result = clients.Find(x => x.username == message_content[1]);
                            if (find_result.username.Length < 8)
                            {
                                // username not found!
                                client.socket.Send(Encoding.ASCII.GetBytes("i|" + message_content[1] + " not found"));
                            }
                            else if (find_result.isInGame)
                            {
                                // <username> is in game
                                client.socket.Send(Encoding.ASCII.GetBytes("i|" + message_content[1] + " is in game"));
                            }
                            else
                            {
                                // <username> is available, act according to response
                                string client_username = client.username;
                                find_result.socket.Send(Encoding.ASCII.GetBytes("v|" + client_username));
                                DisplayInfo(client_username + " has sent an invitation to " + find_result.username);
                                invitation_sent_to = find_result;
                            }
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
                            txtInformation.AppendText("\n" + username + " has disconnected");
                        });
                    }
                    // Close connection and remove all user data
                    connection.Close();
                    connected = false;
                    clients.Remove(client);

                    // Remove displayed items
                    lstUsers.Invoke((MethodInvoker)delegate
                    {
                        lstUsers.Items.Remove(username);
                    });

                    // Broadcast disconnection
                    Broadcast("i", username + " has disconnected");
                }
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            txtInformation.AppendText("\nTerminating connections");
            serverTerminating = true;
            serverListening = false;
            foreach (Client client in clients)
            {
                client.socket.Shutdown(SocketShutdown.Both);
                client.socket.Close();
            }
            System.Windows.Forms.Application.Exit();
        }

        private void txtPort_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnListen.PerformClick();
                // these last two lines will stop the beep sound
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }
    }
}
