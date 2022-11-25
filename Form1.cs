using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Memory32;

namespace WindowsFormsApp2
{
    public partial class Form1 : Form
    {
        Point Pos;
        bool mDown = false;
        Memory mem = new Memory();
        IntPtr module;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mem.GetProcess("amtrucks");
            module = mem.GetModuleBase("amtrucks.exe");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int money = int.Parse(textBox1.Text);
            var buffer = mem.ReadPointer((long)module, 0x1B20C58);
            buffer = mem.ReadPointer(buffer, 0x10);
            mem.WriteBytes(buffer, 0x10, BitConverter.GetBytes(money));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            int xp = int.Parse(textBox2.Text);
            var buffer = mem.ReadPointer((long)module, 0x1B20C58);
            mem.WriteBytes(buffer, 0x195C, BitConverter.GetBytes(xp));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            Pos.X = e.X;
            Pos.Y = e.Y;
            mDown = true;
        }

        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            if(mDown)
            {
                Point currPos = PointToScreen(e.Location);
                Location = new Point(currPos.X - Pos.X, currPos.Y - Pos.Y);
            }
        }

        private void Form1_MouseUp(object sender, MouseEventArgs e)
        {
            mDown = false;
        }
    }
}
