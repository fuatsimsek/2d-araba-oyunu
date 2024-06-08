using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Button;

namespace razercar
{
    public partial class Form1 : Form
    {
        int konumX = 220;
        int konumY = 385;
        int konumArt = 160;
        int seritNumarasi = 0;
        Random R = new Random();
        int yolMesafesi = 0 , hizim = 70; 
        class rArac
        {
            public bool sahteAraclar = false;//fakehavecar
            public PictureBox sahteArac;//fakecar
            public bool zaman = false;//vakit
        }
        rArac[] rndArac = new rArac[3];//iki tane dizi olcak rndcar
        //class ile dizimi ilişkilendirdim
        
        void randomcars(PictureBox pb)
        {
            //Random arac atama.
            int rnd = R.Next(0, 3);
            switch (rnd)
            {
             
                case 0:
                    pb.Image = Properties.Resources.bigblackcar2;
                    pb.Size = new Size(115, 178);
                   
                    break;
                case 1:
                    pb.Image = Properties.Resources.graycar;
                    pb.Size = new Size(115, 178);
                    break;
                case 2:
                    pb.Image = Properties.Resources.redcar;
                    pb.Size = new Size(135, 180);
                    break;

                    
               
            }
            //Random Aracin boyutunu streetch yapmak
            pb.SizeMode = PictureBoxSizeMode.StretchImage;
        }
        
        public Form1()
        {
            InitializeComponent();
        }
      //aracim serit degistirdiginde yeni konumunu atamak.
        public void aracKonum()
        {
            if (seritNumarasi == 0)
            {
                myFord.Location = new Point((konumX+3), konumY);
            }
            else if (seritNumarasi == 1)
            {
                myFord.Location = new Point((konumX-3) + konumArt, konumY);
            }
            else if (seritNumarasi == 2)
            {
                myFord.Location = new Point(konumX + (konumArt * 2), konumY);
            }
            else if (seritNumarasi == 3)
            {
                myFord.Location = new Point(konumX + (konumArt * 3), konumY);
            }
        }
        //aracimin serit degistirmesi.
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Right || e.KeyCode == Keys.D)
            {
                if (seritNumarasi < 3)
                {
                    seritNumarasi++;
                }
            }
            else if (e.KeyCode == Keys.Left || e.KeyCode == Keys.A)
            {
                if (seritNumarasi > 0)
                {
                    seritNumarasi--;
                }
            }
            aracKonum();

        }
        //oyunici random muzik eklemek
        private void Rmusicekle()
        {
            int musicSayi = R.Next(1, 3);
            axWindowsMediaPlayer1.URL = @"music/"+musicSayi.ToString()+".mp3";
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }
        

        //form yuklenirken caliscak kodlar aracin yeni konumu ve muzik
        public void Form1_Load(object sender, EventArgs e)
        {
            

            label8.Text = giris.SecilenRadioButtonText;
            if (label8.Text == "Bugatti")
            {
                
                Image img = razercar.Properties.Resources.bugattiveyronmycar;
                myFord.Image = img;
                myFord.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else if(label8.Text=="Ford")
            {
                Image img = razercar.Properties.Resources.bigfordmycar;
                myFord.Image = img;
                myFord.SizeMode = PictureBoxSizeMode.StretchImage;
            }
            else if (label8.Text == "Mustang")
            {
                Image img = razercar.Properties.Resources.mustangmycar;
                myFord.Image = img;
                myFord.SizeMode = PictureBoxSizeMode.StretchImage;
            }

            
            label6.Text = Settings1.Default.HighScore.ToString()+" m";
          for(var i = 0; i < rndArac.Length;i++)
            {
                rndArac[i] = new rArac();
            }
          rndArac[0].zaman = true;
          aracKonum();
          Rmusicekle();
          
        }
        bool sesKontrolEt = true;

        //ses butonuna tikladiginda kapanmasi ve acilmasi
        private void pictureBox3_Click(object sender, EventArgs e)
        {
            if (sesKontrolEt == true)
            {
                sesKontrolEt = false;
                axWindowsMediaPlayer1.Ctlcontrols.pause();
                sesKontrol.Image = Properties.Resources.mute;
            }
            else if (sesKontrolEt == false)
            {
                sesKontrolEt = true;
                axWindowsMediaPlayer1.Ctlcontrols.play();
                sesKontrol.Image = Properties.Resources.unmute;

            }

        }
        bool seritHiz = false;
        //random araclarin kodlari
        private void randomArac_Tick(object sender, EventArgs e)
        {
            for (int i = 0; i < rndArac.Length; i++)
            {
                if (!rndArac[i].sahteAraclar && rndArac[i].zaman)
                {

                    rndArac[i].sahteArac = new PictureBox();
                    randomcars(rndArac[i].sahteArac);

                    rndArac[i].sahteArac.BackgroundImageLayout = ImageLayout.Stretch;
                    rndArac[i].sahteArac.BackColor = Color.Transparent;
                    rndArac[i].sahteArac.Top = -rndArac[i].sahteArac.Height * 3;//araçlar bir araç boyu daha mesafeli gelsinler diye yapıldı.


                    int Seriteoturt = R.Next(0, 4);
                    //Random Araçlar Random Şeritlere Oturtuldu.

                    if (Seriteoturt == 0)
                    {
                        rndArac[i].sahteArac.Left = 220;
                    }
                    else if (Seriteoturt == 1)
                    {
                        rndArac[i].sahteArac.Left = 370;
                    }
                    else if (Seriteoturt == 2)
                    {
                        rndArac[i].sahteArac.Left = 540;
                    }
                    else if (Seriteoturt == 3)
                    {
                        rndArac[i].sahteArac.Left = 695;
                    }

                    bool carpisma = false;
                    foreach (var mevcutArac in rndArac.Where((arac, index) => arac.sahteAraclar && index != i))
                    {
                        if (rndArac[i].sahteArac.Bounds.IntersectsWith(mevcutArac.sahteArac.Bounds))
                        {
                            carpisma = true;
                            break;
                        }
                    }

                    // Eğer çarpışma varsa, yeni aracı tekrar konumlandır
                    if (carpisma)
                    {
                        rndArac[i].sahteArac.Dispose();
                        rndArac[i].sahteAraclar = false;
                        rndArac[i].zaman = false;
                    }
                    else
                    {
                        this.Controls.Add(rndArac[i].sahteArac);
                        rndArac[i].sahteAraclar = true;
                    }



                }
                else
                {
                    if (rndArac[i].zaman)
                    {
                        rndArac[i].sahteArac.Top += 20;
                        if (rndArac[i].sahteArac.Top >= 154)
                        {
                            for (int j = 0; j < rndArac.Length; j++)
                            {
                                if (!rndArac[j].zaman)
                                {
                                    rndArac[j].zaman = true;
                                    break;
                                }
                            }
                        }
                        if (rndArac[i].sahteArac.Top >= this.Height - 20)
                        {
                            rndArac[i].sahteArac.Dispose();//yoketme komutu
                            rndArac[i].sahteAraclar = false;//ilk haline getirme
                            rndArac[i].zaman = false;//ilk haline getirme
                        }

                    }
                    if (rndArac[i].zaman)
                    {
                        float MutlakX = Math.Abs((myFord.Left + (myFord.Width / 2)) - (rndArac[i].sahteArac.Left + (rndArac[i].sahteArac.Width / 2)));
                        float MutlakY = Math.Abs((myFord.Top + (myFord.Height / 2)) - (rndArac[i].sahteArac.Top + (rndArac[i].sahteArac.Height / 2)));
                        float FarkGenislik = (myFord.Width / 2) + (rndArac[i].sahteArac.Width / 2);
                        float FarkYukseklik = (myFord.Height / 2) + (rndArac[i].sahteArac.Height / 2);

                        if ((FarkGenislik > MutlakX) && (FarkYukseklik > MutlakY))
                        {
                            randomArac.Enabled = false;
                            oyunHizi.Enabled = false;
                            axWindowsMediaPlayer1.Ctlcontrols.pause();
                            axWindowsMediaPlayer1.URL = @"music/araba-kazasi-ses-efekti-araba-kaza-sesi.mp3";
                            axWindowsMediaPlayer1.Ctlcontrols.play();
                            if (yolMesafesi > Settings1.Default.HighScore)
                            {
                                MessageBox.Show("Yeni SKOR ==> " + yolMesafesi.ToString() + " m", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Settings1.Default.HighScore = yolMesafesi;
                                Settings1.Default.Save();
                            }






                            DialogResult dr = MessageBox.Show("Oyun Bitti! Yeniden Denemek İster Misiniz?", "Game Over!",MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                            if(dr == DialogResult.Yes )
                            {
                                
                                aracKonum();
                                for(int j=0;j < rndArac.Length;j++)
                                {
                                    rndArac[j].sahteArac.Dispose();
                                    rndArac[j].sahteAraclar = false;
                                    rndArac[j].zaman = false;
                                    
                                }
                                yolMesafesi = 0;
                                hizim = 70;
                                rndArac[0].zaman = true;
                                randomArac.Enabled=true;
                                randomArac.Interval = 200;
                                oyunHizi.Enabled=true;
                                oyunHizi.Interval = 200;
                                Rmusicekle();
                                axWindowsMediaPlayer1.Ctlcontrols.play();


                            }
                            else
                            {
                                this.Hide();
                                giris oyunFormu2 = new giris();
                                oyunFormu2.Show();
                            }
                        }
                    }
                   
                }
            }
        }
        void hizLevel()
        {
            if (yolMesafesi > 150 && yolMesafesi < 300)
            {
                hizim = 100;
                oyunHizi.Interval = 125;
                randomArac.Interval = 100;
            }
            if (yolMesafesi > 300 && yolMesafesi < 550)
            {
                hizim = 130;
                oyunHizi.Interval = 100;
                randomArac.Interval = 80;
            }
            if (yolMesafesi > 550)
            {
                hizim = 170;
                oyunHizi.Interval = 80;
                randomArac.Interval = 20;
            }


        }

        //seritlerin asagi dogru inerken hizi ve hareketi
        private void oyunHizi_Tick(object sender, EventArgs e)
        {   
            hizLevel();
            yolMesafesi++;
            if(seritHiz == false) 
            {
                for(int i = 1;i< 7;i++)
                {
                    this.Controls.Find("solSerit" + i.ToString(), true)[0].Top -= 60;
                    
                    this.Controls.Find("sagSerit" + i.ToString(), true)[0].Top -= 60;

                }
                for (int i = 1; i < 12; i++)
                {
                    this.Controls.Find("kullanilmazSeritSol" + i.ToString(), true)[0].Top -= 60;

                    this.Controls.Find("kullanilmazSeritSag" + i.ToString(), true)[0].Top -= 60;
                }
             

                seritHiz = true;
            }
            else if(seritHiz == true)
            {

                for (int i = 1; i < 7; i++)
                {
                    this.Controls.Find("solSerit" + i.ToString(), true)[0].Top +=60;

                    this.Controls.Find("sagSerit" + i.ToString(), true)[0].Top += 60;
                }
                for (int i = 1; i < 12; i++)
                {
                    this.Controls.Find("kullanilmazSeritSol" + i.ToString(), true)[0].Top += 60;

                    this.Controls.Find("kullanilmazSeritSag" + i.ToString(), true)[0].Top += 60;
                }

                seritHiz = false;
            }
            label3.Text = yolMesafesi.ToString()+" m";
            label4.Text = hizim.ToString()+" km/h";

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void myFord_Click(object sender, EventArgs e)
        {

        }
        Boolean durdurBasla = false;
        Boolean hareketediyor = true;
        private void oyunKontrol_Click(object sender, EventArgs e)
        {
            
            if(durdurBasla==false)
            {
                
                durdurBasla = true;
                randomArac.Enabled = false;
                oyunHizi.Enabled = false;
                oyunKontrol.Image = Properties.Resources.play;
                axWindowsMediaPlayer1.Ctlcontrols.pause();
                
            }
            else if(durdurBasla==true)
            {
                
                durdurBasla = false;
                randomArac.Enabled = true;
                oyunHizi.Enabled = true;
                oyunKontrol.Image = Properties.Resources.pause;
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }
    }
}
