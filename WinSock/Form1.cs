using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace WinSock
{
    public partial class Form1 : Form
    {
        //服务器端的socket
        private Socket serverSocket = null;  
         //服务器端线程
        private Thread serverThread = null; 
        //保存已连接的客户端socket
        private List<Socket> clientSocketList = null;
        //保存已开启的接收数据的客户端线程
        private List<Thread> clientThreadList = null;
        // 接受数据缓冲区
        private byte[] result = new byte[1024];
        // 跨线程更新UI
        Action<String> AsyncUIDelegate;
        // 服务器IP
        private string IP = "";
        // 服务器开放的端口
        private int Port;
        // 最大用户连接数
        private int MAXCLIENTNUM = 5;
        // 当前用户连接数
        private int iClientNum = 0;

        public Form1()
        {
            InitializeComponent();
            initView();
            // System.Diagnostics.Debug.Write("hello,world!");

        }
        private void initView()
        {
            // 将本机所有可用ip加入下拉框内
            String[] ipAddress = getLocalAddress();
            for(int i = 0;i<ipAddress.Length;i++)
            {
                if(ipAddress[i].IndexOf(".") == 3)
                {
                    cboIP.Items.Add(ipAddress[i]);
                }
                
            }
            // 初始化文本显示
            tbMonitor.ReadOnly = true;
            tbSend.ReadOnly = false;

            // 初始化按钮状态
            btnStart.Enabled = true;
            btnStop.Enabled = false;

            // 初始化ip和port
            cboIP.SelectedIndex = 1;
            tbPort.Text = "8888";

            // 子线程更新UI
            AsyncUIDelegate = delegate (string message)
            {
                addMonitorMessage(message);
            };
            addMonitorMessage("聊天系统初始化完成.");
        }
        //向聊天框添加信息
        private void addMonitorMessage(String message)
        {
            if("".Equals(tbMonitor.Text))
            {
                tbMonitor.Text = message + "\r\n";
            }
            else
            {
                tbMonitor.Text += message + "\r\n";
            }
            return;
        }

        // 获得本机所有ip地址
        private String[] getLocalAddress()
        {
            // 获得主机名
            String hostName = Dns.GetHostName();

            // 根据主机名查找ip
            IPHostEntry iPHostEntry = Dns.GetHostEntry(hostName);
            String[] result = new String[iPHostEntry.AddressList.Length];
            int i = 0;
            foreach (IPAddress iPAddress in iPHostEntry.AddressList)
            {
                result[i] = iPHostEntry.AddressList[i].ToString();
                i++;
            }
            return result;

        }
        // 发送消息
        private void button1_Click(object sender, EventArgs e)
        {

            String msgServer = tbSend.Text.ToString().Trim();
            tbSend.Text = "";
            if ("".Equals(msgServer))
            {
                MessageBox.Show("消息为空，无法发送！");
            }
            if(btnStart.Enabled == true)
            {
                MessageBox.Show("服务器尚未启动或启动失败，无法发送消息！");
                return;
            }else if(clientSocketList.Count == 0)
            {
                MessageBox.Show("当前没有用户连接服务器，消息发送失败！");
                return;
            }
            else
            {
                msgServer = "[服务器]：" + msgServer;
                for (int i = 0; i < clientSocketList.Count; i++)
                {
                    clientSocketList[i].Send(Encoding.UTF8.GetBytes(msgServer));
                }
                tbMonitor.Invoke(AsyncUIDelegate, msgServer);
            }
        }


        private void btnStop_Click(object sender, EventArgs e)
        {
            addMonitorMessage("正在关闭服务器......");
            stopServer();
            btnStart.Enabled = true;
            btnStop.Enabled = false;

        }

        // 启动服务器
        private void btnStart_Click(object sender, EventArgs e)
        {
            addMonitorMessage("正在开启服务器......");
            startServer();
            btnStart.Enabled = false;
            btnStop.Enabled = true;
        }

        private void cboIP_SelectedIndexChanged(object sender, EventArgs e)
        {
            IP = cboIP.SelectedItem.ToString();
        }


        private void startServer()
        {
            // 初始化客户端socket管理列表
            clientSocketList = new List<Socket>();
            // 初始化客户端线程管理列表
            clientThreadList = new List<Thread>();

            IP = cboIP.SelectedItem.ToString();
            Port = Int32.Parse(tbPort.Text.ToString());

            IPAddress iPAddress = IPAddress.Parse(IP);

            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            IPEndPoint pEndPoint = new IPEndPoint(iPAddress, Port);

            serverSocket.Bind(pEndPoint);  
            serverSocket.Listen(10);
            serverThread = new Thread(LitenerClientConnect);
            serverThread.Start();
            tbMonitor.Invoke(AsyncUIDelegate, "服务器启动成功！");
        }

        // 监听客户端的链接
        private void LitenerClientConnect()
        {
            while(true)
            {
                Socket clientSocket = serverSocket.Accept();
                // 将接受到的客户端socket添加到clientSocketList中
                clientSocketList.Add(clientSocket);

                // 获取客户端的IP和端口号
                IPEndPoint clientIpEndPoint = clientSocket.RemoteEndPoint as IPEndPoint;
                IPAddress clientIP = clientIpEndPoint.Address;
                int clientPort = clientIpEndPoint.Port; 


                tbMonitor.Invoke(AsyncUIDelegate, "IP:" + clientIP.ToString()+"\t端口:" + clientPort.ToString() + 
                    "\t已成功连接到服务器.");
                // 向客户端发送信息
                // 使用UTF8编码
                clientSocket.Send(Encoding.UTF8.GetBytes("\t连接成功!\t本机IP:" + clientIP.ToString() + 
                                                             "\t\t端口:" + clientPort.ToString()));

                Thread receiverMessage = new Thread(receiverData);
                clientThreadList.Add(receiverMessage);
                receiverMessage.Start(clientSocket);
                iClientNum++;
            }
        }

        // 接受客户端的消息
        private void receiverData(Object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;

            while(true)
            {
                try // 防止服务器端接受消息堵塞
                {
                    int receiverNum = myClientSocket.Receive(result);
                    string message = Encoding.ASCII.GetString(result, 0, receiverNum);
                    // 判断收到EXIT，如果是空则是客户端断开连接
                    if ("EXIT".Equals(message))
                    {
                        clientSocketList.Remove(myClientSocket);
                        iClientNum--;


                        IPEndPoint clientIpEndPoint = myClientSocket.RemoteEndPoint as IPEndPoint;
                        IPAddress clientIP = clientIpEndPoint.Address;
                        int clientPort = clientIpEndPoint.Port;
                        tbMonitor.Invoke(AsyncUIDelegate, "IP:" + clientIP.ToString() + "\t端口:" + clientPort.ToString() +
                     "\t已断开连接.");

                        break;
                    }
                    else
                    {
                        for (int i = 0; i < clientSocketList.Count; i++)
                        {
                            clientSocketList[i].Send(Encoding.UTF8.GetBytes(message));
                        }
                        tbMonitor.Invoke(AsyncUIDelegate,  message);
                    }

                }
                catch(Exception)
                {
                    myClientSocket.Close();
                    break;
                }
            }
        }

        // 关闭服务器
        private void stopServer()
        {
            serverSocket.Close();
            serverThread.Abort();
            // 先检查连接到的客户端是否仍在连接，然后断开正在连接的客户端
            for (int i = 0; i < clientSocketList.Count; i++)
            {
                if(clientSocketList[i].Connected == true)
                {
                    clientSocketList[i].Disconnect(true);
                }
                clientSocketList.Remove(clientSocketList[i]);
            }
            // 关闭所有客户端线程
            for (int i = 0; i < clientThreadList.Count; i++)
            {
                clientThreadList[i].Abort();
            }
            tbMonitor.Invoke(AsyncUIDelegate, "服务器关闭！");
            btnStart.Enabled = true;
            btnStop.Enabled = false;
        }

        private void tbPort_TextChanged(object sender, EventArgs e)
        {
            Port = Int32.Parse(tbPort.Text.ToString());
        }

        private void btnAddClient_Click(object sender, EventArgs e)
        {

            if (iClientNum == MAXCLIENTNUM)
            {
                MessageBox.Show("用户数已达最大值，无法创建新的客户机！");
                return;
            }
            Form clientForm = new Form2();
            clientForm.Show();
        }
    }
}
