using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;
using System.Diagnostics;
namespace TwitchChatBotGUI
{
    public class IrcClient
    {
        private string userName;
        private string channel;

        private Stopwatch stopwatch;
        public TcpClient tcpClient;
        public StreamReader inputStream;
        private StreamWriter outputStream;
        public bool GlobalTimeout()
        {
            stopwatch.Stop();
            if (Convert.ToInt32(stopwatch.Elapsed.TotalMilliseconds) > 1500)
            {
                stopwatch.Restart();
                    return true;
            }
            else
            {
                stopwatch.Start();
                return false;
            }
        }
        public IrcClient( string userName, string password)
        {
            this.userName = userName;
            tcpClient = new TcpClient("irc.chat.twitch.tv", 6667);
            inputStream = new StreamReader(tcpClient.GetStream());
            outputStream = new StreamWriter(tcpClient.GetStream());
            outputStream.WriteLine("PASS " + password);
            outputStream.WriteLine("NICK " + userName);
            outputStream.WriteLine("USER " + userName + " 8 * :" + userName);
            outputStream.Flush();
            stopwatch = new Stopwatch();
            stopwatch.Start();
        }
        public void JoinRoom(string channel)
        {
            
            this.channel = channel;
            outputStream.WriteLine("JOIN #" + channel);
            outputStream.Flush();
        }
        public void SendIrcMessage(string message)
        {
            string msg = message + "\r\n";
            Console.Write(msg);
            outputStream.WriteLine(msg);
            
            outputStream.Flush();
        }

        public void SendChatMessage(string message)
        {
            SendIrcMessage(":" + userName + "!" + userName + "@" + userName
                + ".tmi.twitch.tv PRIVMSG #" + channel + " :" + message);
        }
        public string ReadIrcMessage()
        {
            string message = inputStream.ReadLine();
            return message;
        }
    }
}
