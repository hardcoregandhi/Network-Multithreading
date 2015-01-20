using System;

using System;
using System.Windows.Forms;
using System.Threading;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.Drawing;

using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

public class ConsoleClient : Form
{

    private ListBox listBox1;
    private Label label1;
    private Label label2;
    private Label label3;
    private CheckBox checkBox1;
    private CheckBox checkBox2;
    private CheckBox checkBox3;


    // Incoming data from the client.
    public static string data = null;

    public static void StartClient()
    {
        // Data buffer for incoming data.
        byte[] bytes = new byte[1024];

        // Connect to a remote device.
        // Establish the remote endpoint for the socket.
        // This example uses port 888 on the local computer.

        //IPHostEntry ipHostInfo = Dns.GetHostEntry(Dns.GetHostName());
        //IPAddress ipAddress = ipHostInfo.AddressList[0];
        //IPEndPoint remoteEP = new IPEndPoint(ipAddress, 888);

        IPAddress localAddr = IPAddress.Parse("127.0.0.1");
        IPEndPoint remoteEP = new IPEndPoint(localAddr, 5000);

        // Create a TCP/IP  socket.
        try
        {
            Socket sender = new Socket(AddressFamily.InterNetwork,
                                       SocketType.Stream, ProtocolType.Tcp);

            // Connect the socket to the remote endpoint. Catch any errors.
            try
            {
                sender.Connect(remoteEP);

                Console.WriteLine("Connected to {0}\n", sender.RemoteEndPoint.ToString());

                // input string - later gets converted into bytes
                string input;
                Console.WriteLine("Embed <EOF> in input to terminate\n");
                do
                {
                    Console.Write("input?");
                    input = Console.ReadLine();
                    // Send the data through the socket to Server.
                    int bytesSent = sender.Send(Encoding.ASCII.GetBytes(input));

                    // Receive the response from the remote device.
                    int bytesRec = sender.Receive(bytes);
                    Console.WriteLine("Echoed text from Server \n{0}",
                                      Encoding.ASCII.GetString(bytes, 0, bytesRec));


                }
                while (input.IndexOf("<EOF>") == -1); // check for no 
                // embedded <EOF>

                // Release socket.
                sender.Shutdown(SocketShutdown.Both);
                sender.Close();
                Console.WriteLine("\nClient terminated");

            }
            catch (ArgumentNullException ane)
            {
                Console.WriteLine("ArgumentNullException : {0}", ane.ToString());
            }
            catch (SocketException se)
            {
                Console.WriteLine("SocketException : {0}", se.ToString());
            }
            catch (Exception e)
            {
                Console.WriteLine("Unexpected exception : {0}", e.ToString());
            }

        }
        catch (Exception e)
        {
            Console.WriteLine("Outer error:- " + e.ToString());
        }

    }







    public static void Main(String[] args)
    {
        
        Thread client = new Thread(new ThreadStart(StartClient));
        Console.WriteLine("Client started");
        client.Start();
        //Application.Run(new ConsoleClient());
        Console.ReadLine();

    }

    private void InitializeComponent()
    {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(32, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(400, 134);
            this.listBox1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(488, 33);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(57, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Red Plane";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(488, 72);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Blue Plane";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(488, 112);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Green Plane";
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(582, 32);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(15, 14);
            this.checkBox1.TabIndex = 4;
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(582, 71);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(15, 14);
            this.checkBox2.TabIndex = 5;
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Location = new System.Drawing.Point(582, 111);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(15, 14);
            this.checkBox3.TabIndex = 6;
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // ConsoleClient
            // 
            this.ClientSize = new System.Drawing.Size(793, 239);
            this.Controls.Add(this.checkBox3);
            this.Controls.Add(this.checkBox2);
            this.Controls.Add(this.checkBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.listBox1);
            this.Name = "ConsoleClient";
            this.ResumeLayout(false);
            this.PerformLayout();

    }
}