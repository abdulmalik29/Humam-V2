using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace myGame
{
    public partial class test : UserControl
    {
        Form1 MainForm;
        public test(Form1 main)
        {
            InitializeComponent();
            MainForm = main;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                string s = null;
                s.ToString();
            }
            catch (NullReferenceException)
            {
            this.Visible = false;
            MainForm.reset_game();
            }

        }
    }
}
