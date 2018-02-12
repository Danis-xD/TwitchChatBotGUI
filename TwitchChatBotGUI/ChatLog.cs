using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace TwitchChatBotGUI
{
    public partial class ChatLog : Form
    {
        public ChatLog()
        {
            InitializeComponent();
        }
        
        public string TextBox
        {
            get { return textBox1.Text; }
            set
            {
                textBox1.BeginInvoke(new MethodInvoker(delegate { textBox1.AppendText(value + Environment.NewLine); }));
            }
        }


        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void ChatLog_Load(object sender, EventArgs e)
        {
       
        }
    }
}
