using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace WinSock
{
    
    public partial class Form2 : Form
    {
        // 客户端线程
        private Thread threadClient = null;
        // 服务器socket
        private Socket socketClient = null;
        // 客户名字
        private String clientName = "";
        // 客户端端口
        private int clientPort;
        // 客户端IP
        private IPAddress clientIP = null;
        // 客户端IPEndPoint
        private IPEndPoint clientIpEndPoint = null;
        // 消息缓存区
        byte[] recvBuffer = null;


        public Form2()
        {
            InitializeComponent();
            System.Windows.Forms.Control.CheckForIllegalCrossThreadCalls = false;
            initView();
        }
        private void initView()
        {
            btnConnectToServer.Enabled = true;
            btnExit.Enabled = false;
            tbMonitor.ReadOnly = true;
            tbIP.ForeColor = Color.Red;
            tbPort.ForeColor = Color.Red;
            tbName.ForeColor = Color.Red;
            tbIP.Text = "192.168.137.1";
            tbPort.Text = "8888";
            tbName.Text = "Tom";
        }
        private void btnConnectToServer_Click(object sender, EventArgs e)
        {
            try
            {
                clientIP = IPAddress.Parse(tbIP.Text.ToString().Trim());
                clientPort = Int32.Parse(tbPort.Text.ToString().Trim());
                clientName = tbName.Text.ToString().Trim();
            }
            catch(Exception)
            {
                MessageBox.Show("输入信息不合法，请重新输入！");
                return;
            }
            btnConnectToServer.Enabled = false;
            btnExit.Enabled = true;

            clientIpEndPoint = new IPEndPoint(clientIP, clientPort);

            socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);


            tbIP.ReadOnly = true;   
            tbPort.ReadOnly = true; 
            tbName.ReadOnly = true; 
            try
            {
                socketClient.Connect(clientIpEndPoint);
            }
            catch
            {
                MessageBox.Show("连接失败\r\n");
                btnConnectToServer.Enabled = true;
                btnExit.Enabled = false;
                return;
            }
            threadClient = new Thread(recvMsg);
            threadClient.Start();
        }

        // 接受服务器端发来的消息
        private void recvMsg()
        {
            while(true)
            {
                try
                {
                    // 临时存储接收到的信息  1kb
                    recvBuffer = new byte[1024];

                    //将客户端套接字接收到的数据存入内存缓冲区，并获取长度
                    int length = socketClient.Receive(recvBuffer);  
                    string strRecvMsg = Encoding.UTF8.GetString(recvBuffer, 0, length);
                    addMonitorMessage(strRecvMsg);
                }
                catch
                {
                    addMonitorMessage("远程服务器已经中断连接\r\n");
                    btnConnectToServer.Enabled = true;
                    break;
                }
            }
            
        }

        private void addMonitorMessage(String message)
        {
            if ("".Equals(tbMonitor.Text))
            {
                tbMonitor.Text = message + "\r\n";
            }
            else
            {
                tbMonitor.Text += message + "\r\n";
            }
            return;
        }
        //  客户端发送消息，流程：客户端->服务器->所有客户端
        private void btnCSend_Click(object sender, EventArgs e)
        {
            try
            {
                string strMsg = tbSendMsg.Text.ToString().Trim();
                if("".Equals(strMsg))
                {
                    MessageBox.Show("消息不能为空！");
                    return;
                }
                if(btnConnectToServer.Enabled == true)
                {
                    MessageBox.Show("请先连接到服务器，再发送消息");
                    return;
                }
                tbSendMsg.Text = "";
                strMsg = "[" + clientName + "]:" + strMsg;
                socketClient.Send(Encoding.UTF8.GetBytes(strMsg));
            }
            catch
            {
                MessageBox.Show("消息发送失败，请检查是否连接到服务器");
                return;
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            try
            {
                // 通知服务器即将断开连接
                socketClient.Send(Encoding.UTF8.GetBytes("EXIT"));

                socketClient.Dispose();
                socketClient.Close();
                btnExit.Enabled = false;
                btnConnectToServer.Enabled = true;
                tbName.ReadOnly = false;
            }
            catch
            {
                MessageBox.Show("客户机关闭失败！");
                return;
            }
        }
    }
}
