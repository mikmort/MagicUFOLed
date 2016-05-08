using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Collections.Specialized;


namespace MagicUFOController
{
    class LedTCPControl
    {

        public int port = 5577;

        // Default IP if none specified
        public string hubIP = "192.168.1.143";
        public System.Net.Sockets.TcpClient clientSocket;
        public StringCollection ipAddresses;            

        public LedTCPControl()
        {
             ipAddresses.Add(hubIP);
        }

        public LedTCPControl(string argIPs)
        {
            ipAddresses = new StringCollection();
            AddIPs(argIPs);
        }

        public void SendGroupCommand(string command)
        {
            foreach (string ipAddress in ipAddresses)
                SendCommand(command,ipAddress);
        }

        public void SendCommand(string command, string ipAddress)
        {

            try
            {
                clientSocket = new System.Net.Sockets.TcpClient();
                clientSocket.Connect(ipAddress, port);
                byte[] buf = StringToByteArrayWithChecksum(command);
                clientSocket.GetStream().Write(buf, 0, buf.Length);
            }
            catch
            {
                Console.WriteLine("Error connecting to device.");
            }
                clientSocket.Close();
        }

        public void AddIPs(string ipList)
        {
            char delimeter = ';';
            string[] subStrings = ipList.Split(delimeter);
            for (int i = 0; i < subStrings.Length; i++)
                ipAddresses.Add(subStrings[i]);
        }

        public MagicUFOController.LEDStatus GetStatus(string command, String ipAddress)
        {
            LEDStatus status = new LEDStatus();
            NetworkStream serverStream;
            byte[] outStream;
            
            SendCommand(command, ipAddress);

            clientSocket = new System.Net.Sockets.TcpClient();
            clientSocket.Connect(ipAddress, port);
            serverStream = clientSocket.GetStream();
            outStream = new byte[50];
            serverStream.Write(outStream, 0, outStream.Length);
        
            int bytesRead = 0;
            // 14 bytes is the size of the status stream
            while (bytesRead < 14)
            {
                bytesRead += serverStream.Read(outStream, bytesRead, 14);
            }
            status.red = outStream[6];
            status.green = outStream[7];
            status.blue = outStream[8];
            status.white = outStream[9];
            serverStream.Flush();
            clientSocket.Close();
            return status;
        }

        public  byte[] StringToByteArrayWithChecksum(String hex)
        {
            byte checksum = 0;

            int NumberChars = hex.Length;
            int byteLength = 1 + (NumberChars / 2);
            byte[] bytes = new byte[byteLength];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
                checksum += bytes[i / 2];
            }
            bytes[byteLength - 1] = checksum;

            return bytes;
        }
    }
}
