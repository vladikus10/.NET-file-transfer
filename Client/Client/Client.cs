using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Client
{
    class Client
    {
        public static string Message = "Idle";
        public static void sendFile(string fileName)
        {
            try
            {
                IPAddress ip = IPAddress.Parse("127.0.0.1");
                IPEndPoint end = new IPEndPoint(ip, 2015);
                Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);

                byte[] fileNameBytes = Encoding.ASCII.GetBytes(Path.GetFileName(fileName));
                Message = "Buffering...";

                byte[] fileData = File.ReadAllBytes(fileName);
                byte[] clientData = new byte[4 + fileNameBytes.Length + fileData.Length];
                byte[] fileNameLen = BitConverter.GetBytes(Path.GetFileName(fileName).Length);
                
                fileNameLen.CopyTo(clientData, 0);
                fileNameBytes.CopyTo(clientData, 4);
                fileData.CopyTo(clientData, 4 + fileNameBytes.Length);
                
                Message = "Connecting to server...";
                sock.Connect(end);
                Message = "Sending file...";
                sock.Send(clientData);
                Message = "File is sent";
                sock.Close();
            }
            catch (Exception exc)
            {
                Message = exc.Message;
            }
        }
    }
}
