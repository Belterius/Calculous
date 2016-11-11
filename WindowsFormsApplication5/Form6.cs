using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication5
{
    public partial class Calculous : Form
    {
        Form2 f2;

        public Calculous(Form2 f2b)
        {
            InitializeComponent();
            f2 = f2b;
        }

        private void Form6_Load(object sender, EventArgs e)
        {
            this.Location = f2.Location;
        }

        private void label1_Click(object sender, EventArgs e)
        {
            f2.Location = this.Location;
            Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
