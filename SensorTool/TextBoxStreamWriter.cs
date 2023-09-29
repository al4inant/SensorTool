using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication
{
    public class TextBoxStreamWriter : TextWriter
    {
        TextBox textBox = null;
        
        public TextBoxStreamWriter(TextBox textBox)
        {
            this.textBox = textBox;
        }
        
        public override void Write(char value)
        {
            this.textBox.Invoke((MethodInvoker)delegate
            {
                this.textBox.AppendText(value.ToString());
            });
            
        }
        
        public override Encoding Encoding
        {
            get { return System.Text.Encoding.UTF8; }
        }
    }
}
