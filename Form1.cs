using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace scale2
{
    public partial class Form1 : Form
    {
        string result;

        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
       
        private void serialPort1_DataReceived(object sender, System.IO.Ports.SerialDataReceivedEventArgs e)
        {
            SerialPort Ports = (SerialPort)sender;
            int bytes = Ports.BytesToRead;
            byte[] buffer = new byte[19];
            int offset = 0, toRead = 19;
            int read;
            while (toRead > 0 && (read = Ports.Read(buffer, offset, toRead)) > 0)
            {
                offset += read;
                toRead -= read;
            }
            result = System.Text.Encoding.UTF8.GetString(buffer);
            this.Invoke(new EventHandler(showdata));
            
            Ports.DiscardInBuffer();
            
            
        }

        private void showdata(object sender, EventArgs e)
        {
            try
            {
                textBox1.AppendText(result);
               
            }
            catch (Exception error)
            {
                MessageBox.Show(error.Message);
            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            serialPort1.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Task.Delay(3000).Wait();
            SendKeys.Send(result);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            serialPort1.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            serialPort1.PortName = "COM1";
            serialPort1.BaudRate = 9600;
            serialPort1.DataBits = 8;
            serialPort1.StopBits = (StopBits)Enum.Parse(typeof(StopBits), "One");
            serialPort1.Parity = (Parity)Enum.Parse(typeof(Parity), "None");
            serialPort1.Open();
        }

        

        
       
     
    }
}
