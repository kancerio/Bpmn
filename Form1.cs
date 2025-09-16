using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BpmnEditor.WinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            flowLayoutPanel2.RightToLeft = RightToLeft.Yes;
            this.Resize += Form1_Resize;
            Form1_Resize(this, EventArgs.Empty);
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            List<Button> buttons = new List<Button>();
            for (int i = 23; i <= 28; i++)
            {
                var button = this.Controls.Find($"button{i}", true).FirstOrDefault() as Button;
                if (button != null) 
                {
                    button.Width = 72;
                    button.Height = 35;
                    buttons.Add(button); 
                }
            }
            int topMargin = 3;

            int totalButtonsWidth = buttons.Sum(b => b.Width);
            int totalAvailableSpace = this.ClientSize.Width - totalButtonsWidth;
            int equalSpacing = totalAvailableSpace / (buttons.Count + 1);
            int currentX = equalSpacing;
            foreach (var button in buttons)
            { 
                button.Location = new Point(currentX, topMargin);
                currentX += button.Width + equalSpacing;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void button14_Click(object sender, EventArgs e)
        {

        }

        private void button19_Click(object sender, EventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {

        }

        private void button27_Click(object sender, EventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {

        }

        private void button26_Click(object sender, EventArgs e)
        {

        }

        private void button23_Click(object sender, EventArgs e)
        {

        }
    }
}
