using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TwitchChatBotGUI
{
    
    class ChatBot
    {
        private IrcClient irc;
        public bool Connect(string oauth, string name)
        {
            
            irc = new IrcClient( name, oauth);
            irc.tcpClient.ReceiveTimeout = 3000;
            try
            {
                while (true)
                {
                    string message = irc.ReadIrcMessage();
                    if (irc.ReadIrcMessage().Contains("You are in a maze of twisty passages, all alike."))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                irc.tcpClient.ReceiveTimeout = 24 * 60 * 60 * 1000;
            }

        }
        public bool JoinRoom(string channel)
        {
            irc.tcpClient.ReceiveTimeout = 3000;
            irc.JoinRoom(channel);
            try
            {
                while (true)
                {
                    string message = irc.ReadIrcMessage();

                    if (message.Contains("End of /NAMES list"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
            finally
            {
                irc.tcpClient.ReceiveTimeout = 24 * 60 * 60 * 1000;
            }

        }
        public Tuple<string,string> ReadMessage()
        {
            while (true)
            {
                string rawMessage = irc.ReadIrcMessage();
                Console.WriteLine(rawMessage);
                if (rawMessage == "PING :tmi.twitch.tv")
                {
                     
                    irc.SendIrcMessage("PONG tmi.twitch.tv");
                }
                else
                {
                    rawMessage = rawMessage.Remove(0, 1);
                    string[] message = rawMessage.Split(':');
                    string user = message[0].Split('!')[0];
                    string msg = rawMessage.Remove(0, message[0].Length);
                    Tuple<string, string> UsrMsgPair = new Tuple<string, string>(user, msg);
                    return UsrMsgPair;
                }
            }
        }
        public bool CheckTimeout()
        {
            return irc.GlobalTimeout();
        }
        public void SendMessage(string message)
        {
            irc.SendChatMessage(message);
        }
        public void Start()
        {
            while (true)
            {
                Tuple<string, string> message = ReadMessage();
                if (CheckTimeout())
                {
                    SendMessage(message.Item1 + "asdf");
                }
            }
        }
    }
}
