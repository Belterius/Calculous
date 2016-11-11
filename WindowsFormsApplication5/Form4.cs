using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


using System.Media;
using System.Threading;

namespace WindowsFormsApplication5
{
    public partial class Form4 : Form
    {
        int sonRes;

        Form3 f3 = null;
        Form2 f2;
        Calculous f6;

        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();
        public bool continuer = true;
        public bool fini = false;

        public Form4(int sR, Form2 f2b)
        {
            InitializeComponent();

            f2 = f2b;

            sonRes = sR;

        }

        private void Form4_Load(object sender, EventArgs e)
        {
            this.Location = f2.Location;
            fini = false;
            label1.Text = f2.textSansEgal +" ≠ ";
            label2.Text = "= " + f2.res;
            label3.Text = System.Convert.ToString(sonRes);

            timer1.Tick += new EventHandler(finTimer);
            timer1.Interval = 100;
            timer1.Start();
        }
        

        public String nb1strg = "";
        public String nb2strg = "";

        private void finTimer(object sender, EventArgs e)
        {
            timer1.Stop();
            nb1strg = Convert.ToString(f2.nbre1);
            nb2strg = Convert.ToString(f2.nbre2);
            jouerSon(nb1strg, f2.operateur, nb2strg);
        }

        public void jouerSon(string nbre1, string operateur, string nbre2)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer();
            fini = false;
            Task t = new Task(() =>
            {
                player.Stream = Properties.Resources.ResourceManager.GetStream("wrong");
                player.PlaySync();
            });
            t.Start();
            Task t2 = t.ContinueWith((continuation) =>
            {
                if (continuer == true)
                {
                    player.Stream = Properties.Resources.ResourceManager.GetStream("_" + f2.nbre1);
                    player.PlaySync();
                }
            });

            Task t3 = t2.ContinueWith((continuation) =>
            {
                if (continuer == true)
                {
                    player.Stream = Properties.Resources.ResourceManager.GetStream(f2.operateur);
                    player.PlaySync();
                }
            });

            Task t4 = t3.ContinueWith((continuation) =>
            {
                if (continuer == true)
                {
                    player.Stream = Properties.Resources.ResourceManager.GetStream("_" + f2.nbre2);
                    player.PlaySync();
                }
            });

            Task t5 = t4.ContinueWith((continuation) =>
            {
                if (continuer == true)
                {
                    player.Stream = Properties.Resources.ResourceManager.GetStream("equal");
                    player.PlaySync();
                }
            });

            Task t6 = t5.ContinueWith((continuation) =>
            {
                if (continuer == true)
                {
                    player.Stream = Properties.Resources.ResourceManager.GetStream("_" + f2.res);
                    player.PlaySync();
                }
            });


            if (sonRes <= 100)
            {
                Task t7 = t6.ContinueWith((continuation) =>
                {
                    if (continuer == true)
                    {
                        player.Stream = Properties.Resources.ResourceManager.GetStream("not");
                        player.PlaySync();
                    }
                });

                Task t8 = t7.ContinueWith((continuation) =>
                {
                    if (continuer == true)
                    {
                        player.Stream = Properties.Resources.ResourceManager.GetStream("_" + sonRes);
                        player.PlaySync();
                        fini = true;
                    }
                });
            }
            else fini = true;
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            f3 = new Form3(f2.compterQ, f2.compterR, f2);

            continuer = false;
            f2.Location = this.Location;
            Hide();
            f3.ShowDialog(); //bloquante (fenetre modale)
            
            //Show();
            Close();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            f2.Location = this.Location;
            continuer = false;
            f2.fermer = false;
            Close();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (fini == true) jouerSon(nb1strg, f2.operateur, nb2strg);
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
