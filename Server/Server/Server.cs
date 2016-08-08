using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace Server
{
    class Server
    {
        IPEndPoint end;
        Socket sock;

        public Server()
        {
            end = new IPEndPoint(IPAddress.Any, 2015);
            sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.IP);
            sock.Bind(end);
        }

        public static string path;
        public static string Message = "Stopped";

        public void StartServer()
        {
            try
            {
                Message = "Starting...";
                sock.Listen(100);
                Message = "Started";
                
                Socket clientSocket = sock.Accept();
                byte[] clientData = new byte[1024 * 5000];
                
                int receivedByteLen = clientSocket.Receive(clientData);
                int fNameLen = BitConverter.ToInt32(clientData, 0);
                string fName = Encoding.ASCII.GetString(clientData, 4, fNameLen);
                
                BinaryWriter bw = new BinaryWriter(File.Open(path + "/" + fName, FileMode.OpenOrCreate));
                Message = "Saving file...";
                bw.Write(clientData, 4 + fNameLen, receivedByteLen - 4 - fNameLen);                
                bw.Close();
                clientSocket.Close();
                Message = "Saved...";
            }
            catch (Exception exc)
            {
                Message = exc.Message;
            }
        }
    }
}
