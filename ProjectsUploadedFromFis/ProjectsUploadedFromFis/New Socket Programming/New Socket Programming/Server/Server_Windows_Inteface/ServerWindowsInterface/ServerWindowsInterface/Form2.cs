using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ServerWindowsInterface
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
           pictureBox1.Image = ServerDLL.Utility.tempImg;

        }

        private void Form2_Load(object sender, EventArgs e)
        {
         
        }
    }
}