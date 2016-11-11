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
    public partial class Form3 : Form
    {
        Form2 f2;
        Calculous f6;
        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

        public String operateur = "";

        public bool continuer = true;
        public bool fini = false;

        public Form3(int compterQ , int compterR, Form2 f2b)
        {
            InitializeComponent();
            if(compterQ > 1)
                label1.Text = compterR + " / " + compterQ +  "\ngood answers " ;
            else
                label1.Text = compterR + " / " + compterQ + "\ngood answer";

            f2 = f2b;
        }

        private void finTimer(object sender, EventArgs e)
        {
            timer1.Stop();
            jouerSon(nb1strg, operateur, nb2strg);
        }

        public String nb1strg = "";
        public String nb2strg = "";


        public void jouerSon(string nbre1, string operateur, string nbre2)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            fini = false;
            Task t = new Task(() =>
            {
                player.Stream = Properties.Resources.ResourceManager.GetStream("_" + f2.compterR);
                player.PlaySync();
            });
            t.Start();
            Task t2 = t.ContinueWith((continuation) =>
            {
                if (continuer == true)
                {
                    player.Stream = Properties.Resources.ResourceManager.GetStream("of");
                    player.PlaySync();
                }
            });

            Task t3 = t2.ContinueWith((continuation) =>
            {
                if (continuer == true)
                {
                    player.Stream = Properties.Resources.ResourceManager.GetStream("_" + f2.compterQ);
                    player.PlaySync();
                }
            });

            Task t4 = t3.ContinueWith((continuation) =>
            {
                if (continuer == true)
                {
                    player.Stream = Properties.Resources.ResourceManager.GetStream("good");
                    player.PlaySync();
                }
            });
            Task t5 = t4.ContinueWith((continuation) =>
            {
                if (continuer == true)
                {
                    player.Stream = Properties.Resources.ResourceManager.GetStream("answer");
                    player.PlaySync();
                }
            });
            Task t6 = t5.ContinueWith((continuation) =>
            {
                if (continuer == true)
                {
                    fini = true;
                }
            });
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            this.Location = f2.Location;
            fini = false;
            timer1.Tick += new EventHandler(finTimer);
            timer1.Interval = 100;
            timer1.Start();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            continuer = false;
            Application.Exit();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            f2.Location = this.Location;
            continuer = false;
            f2.fermer = false;
            Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (fini==true) jouerSon(nb1strg, operateur, nb2strg);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            f6 = new Calculous(f2);

            Hide();
            f2.Location = this.Location;
            f6.ShowDialog(); //bloquante (fenetre modale)
            Show();
        }
    }
}
