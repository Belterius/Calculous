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

using System.Runtime.InteropServices;



namespace WindowsFormsApplication5
{

    public partial class Form2 : Form
    {


        Form1 f1;
        Form3 f3 = null;
        Form4 f4 = null;
        Form5 f5 = null;
        Calculous f6;

        public int compterQ = 0;
        public int compterR = 0;
        public int res = 0;
        public String textSansEgal = "";
        public int nbre1;
        public int nbre2;

        bool debut = true;

        public bool fermer = true;
        public bool fini = false;

        System.Windows.Forms.Timer timer1 = new System.Windows.Forms.Timer();

        public String operateur="";

        public bool continuer = true;
        
        public Form2(Form1 f1b)
        {
            InitializeComponent();
            f1 = f1b;
            
        }


        private void finTimer(object sender, EventArgs e)
        {
            timer1.Stop();
            nb1strg = Convert.ToString(nbre1);
            nb2strg = Convert.ToString(nbre2);
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
                player.Stream = Properties.Resources.ResourceManager.GetStream("_" + nbre1);
                player.PlaySync();
            });
            t.Start();
            Task t2 = t.ContinueWith((continuation) =>
            {
                if (continuer == true)
                {
                    player.Stream = Properties.Resources.ResourceManager.GetStream(operateur);
                    player.PlaySync();
                }
            });

            Task t3 = t2.ContinueWith((continuation) =>
            {
                if (continuer == true)
                {
                    player.Stream = Properties.Resources.ResourceManager.GetStream("_" + nbre2);
                    player.PlaySync();
                }
            });

            Task t4 = t3.ContinueWith((continuation) =>
            {
                if (continuer == true)
                {
                    player.Stream = Properties.Resources.ResourceManager.GetStream("equal");
                    player.PlaySync();
                }
            });
            Task t5 = t4.ContinueWith((continuation) =>
            {
                if (continuer == true)
                {
                    fini = true;
                }
            });

        }
         
        
        private void Form2_Load(object sender, EventArgs e)
        {
            this.Location = f1.Location;
            fini = false;
            creerCalcul();

            timer1.Tick += new EventHandler(finTimer);
            timer1.Interval = 100;
            timer1.Start();

        }

        private int creerCalcul()
        {
            Random rand = new Random();

            if(rand.Next(0, 2) == 1)
            {
                // Operation utilisant les + et - de 2 puis 3 chiffres à partir de 5 questions
                nbre1 = rand.Next(1, 16);
                nbre2 = rand.Next(1, 16);
                
                
                if(nbre1 < nbre2)
                {
                    int tmp = nbre1;
                    nbre1 = nbre2;
                    nbre2 = tmp;
                }

                label1.Text = System.Convert.ToString(nbre1);

                if (rand.Next(0, 2) == 0)
                {
                    // on a +
                    operateur = "plus";
                    label1.Text += " + ";
                    res = nbre1 + nbre2;
                }
                else
                {
                    operateur = "minus";
                    // on a -
                    label1.Text += " - ";
                    res = nbre1 - nbre2;
                }

                label1.Text += System.Convert.ToString(nbre2);

            }
            else
            {
                // Operation : * 

                // Operation utilisant les + et - de 2 puis 3 chiffres à partir de 5 questions
                nbre1 = rand.Next(1, 11);
                nbre2 = rand.Next(1, 11);

                operateur = "times";

                label1.Text = System.Convert.ToString(nbre1);

                label1.Text += " x ";
                res = nbre1 * nbre2;

                
                label1.Text += System.Convert.ToString(nbre2);
               

            }

            textSansEgal = label1.Text;
            label1.Text += " = ";

           

            return res;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            f3 = new Form3(compterQ, compterR, this);

            Hide();
            continuer = false;
            f3.ShowDialog(); //bloquante (fenetre modale)
            Show();
            continuer = true;

            if (fermer)
                Application.Exit();
            else
                fermer = true;
        }


        private bool Numeric(string tastring)
        {
            bool isNum = true;
            for (Int32 i = 0; i <= tastring.Length - 1; i++)
            {
                if (!char.IsDigit(tastring, i))
                {
                    isNum = false;
                    i = tastring.Length - 1;
                }
            }
            return isNum;
        }



        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Enter && Numeric(textBox1.Text) && System.Convert.ToInt32(textBox1.Text) < 1000)//e.KeyCode.Equals(13)
            {
                compterQ++;
                if (System.Convert.ToInt32(textBox1.Text) == res) // on affiche la fenetre de felicitations
                {
                    compterR++;

                    f5 = new Form5(System.Convert.ToInt16(textBox1.Text), this);

                    continuer = false;
                    Hide();
                    f5.ShowDialog(); //bloquante (fenetre modale)

                    if (fermer)
                        Application.Exit();
                    else
                        fermer = true;

                    creerCalcul();

                    textBox1.Text = "";

                    Show();
                    continuer = true;

                    timer1.Interval = 100;
                    timer1.Start();

                    //Close();

                }
                else // on affiche la fenetre de dommage
                {

                    f4 = new Form4(System.Convert.ToInt16(textBox1.Text), this);

                    continuer = false;
                    Hide();
                    f4.ShowDialog(); //bloquante (fenetre modale)

                    if (fermer)
                        Application.Exit();
                    else
                        fermer = true;

                    creerCalcul();
                    textBox1.Text = "";

                    Show();
                    continuer = true;

                    timer1.Interval = 100;
                    timer1.Start();


                    //Close();
                }
            }
           

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (fini == true) jouerSon(nb1strg, operateur, nb2strg);
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            f6 = new Calculous(this);


            Hide();
            f6.ShowDialog(); //bloquante (fenetre modale)
            Show();
        }


    }
}
