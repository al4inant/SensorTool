using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace WindowsFormsApplication
{
    public class Serial
    {
        public static SerialPort serialPort;
        public static int ComTimeout = 500;

        public Serial()
        {
            serialPort = new SerialPort();
        }

        private void serialPort_DataReceived(object sender,
         SerialDataReceivedEventArgs e)
        {

            byte[] data = new byte[serialPort.BytesToRead];

            try
            {
               serialPort.Read(data, 0, data.Length);
               // this.Invoke((MethodInvoker)delegate
              //  {
                    //this.textBox1.AppendText(BitConverter.ToString(data) + "\n");
              //  });

            }
            catch (TimeoutException)
            {
            }

            //string indata = serialPort1.ReadLine();
            // MessageBox.Show(indata,"Data Received");

        }

    }
}
