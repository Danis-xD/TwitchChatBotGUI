using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TwitchChatBotGUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default["username"].ToString() ;
            textBox2.Text = Properties.Settings.Default["oauth"].ToString();
            textBox3.Text = Properties.Settings.Default["channel"].ToString();


        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                Properties.Settings.Default["username"] = textBox1.Text;
                Properties.Settings.Default["oauth"] = textBox2.Text;
                Properties.Settings.Default["channel"] = textBox3.Text;
                Properties.Settings.Default.Save();
            }
            ChatBot bot = new ChatBot();

            if (bot.Connect(textBox2.Text, textBox1.Text))
            {
                if (bot.JoinRoom(textBox3.Text))
                {
                    ChatLog chatLog = new ChatLog();
                    chatLog.Show();
                    Thread t = new Thread(() => bot.Start(chatLog));
                    t.Start();
                    
                    

                }
                else
                {
                    MessageBox.Show("Error connectiong to " + textBox3.Text);
                }
            }
            else
            {
                MessageBox.Show("Error connection to twitch servers");
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel1_LinkClicked_1(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://twitchapps.com/tmi/");
        }
    }
}
