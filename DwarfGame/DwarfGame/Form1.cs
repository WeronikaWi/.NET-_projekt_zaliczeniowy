using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DwarfGame
{
    public partial class Form1 : Form
    {
        private PictureBox[,] tiles = new PictureBox[4, 4];
        bool koniecGry = false;
        int poz_x = 2, poz_y = 1; //pozycja Gerwazego
        int zycia = 1;
        bool idz_lewo, idz_prawo, idz_gora, idz_dol;
        bool kop_lewo, kop_prawo, kop_gora, kop_dol;
        int punkty = 0;
        bool koniec_tury;
        Random random = new Random();
        int trudnosc = 1, trudnosc2 = 1; //trudnosc - zwiększa szansę, ze pojawi się kamień, trudnosc2 - zwiększa prawdopodobieństwo, ze kamień będzie duży
        bool czy_serce;
        int[] tablica_wynikow = new int[10];
       

        private void tablicaWynikówToolStripMenuItem_Click(object sender, EventArgs e) //wyświetlanie tablicy wyników
        {
            string Top_lista = "";
            for(int i = 0; i< 10; i++)
            {
                Top_lista += $"{i+1}. {tablica_wynikow[i]}\n";
            }
            MessageBox.Show($"Najlepsze wyniki: \n{Top_lista}", "TOP 10");
           
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e) //zdarzenie naciśnięcie przycisku
        {
            if (e.KeyCode == Keys.Left)
            {
                idz_lewo = false;
            }
            if (e.KeyCode == Keys.Right)
            {
                idz_prawo = false;
            }
            if (e.KeyCode == Keys.Up)
            {
                idz_gora = false;
            }
            if (e.KeyCode == Keys.Down)
            {
                idz_dol = false;
            }

            if (e.KeyCode == Keys.W)
            {
                kop_gora = false;
            }
            if (e.KeyCode == Keys.A)
            {
                kop_lewo = false;
            }
            if (e.KeyCode == Keys.S)
            {
                kop_dol = false;
            }
            if (e.KeyCode == Keys.D)
            {
                kop_prawo = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e) //Właściwa gra
        {
            
            pkt.Text = "Wynik: " + punkty; //wyświetlanie wyniku
            zycie.Text = "Życia: " + zycia; //wyśweitlanie ilości żyć
            if(idz_lewo || idz_prawo || idz_gora || idz_dol) //poruszanie sie - następuje po wciśnięciu strzałki, jeżeli nie spowoduje to wyjścia z planszy oraz droga nie jest zastawiona kamieniem
            {
                tiles[poz_x, poz_y].Image = null;
                if(idz_lewo && poz_y>0 && (String)tiles[poz_x,poz_y-1].Tag != "Kamien" && (String)tiles[poz_x, poz_y - 1].Tag != "Kamien2" && !koniec_tury)
                {
                    poz_y--;
                    idz_lewo = false;
                    koniec_tury = true;
                }
                if(idz_prawo && poz_y<3 && (String)tiles[poz_x, poz_y + 1].Tag != "Kamien" && (String)tiles[poz_x, poz_y + 1].Tag != "Kamien2" && !koniec_tury)
                {
                    poz_y++;
                    idz_prawo = false;
                    koniec_tury = true;
                }
                if (idz_gora && poz_x > 0 && (String)tiles[poz_x - 1, poz_y].Tag != "Kamien" && (String)tiles[poz_x - 1, poz_y].Tag != "Kamien2" && !koniec_tury)
                {
                    poz_x--;
                    idz_gora = false;
                    koniec_tury = true;
                }
                if (idz_dol && poz_x < 3 && (String)tiles[poz_x + 1, poz_y].Tag != "Kamien" && (String)tiles[poz_x + 1, poz_y].Tag != "Kamien2" && !koniec_tury)
                {
                    poz_x++;
                    idz_dol = false;
                    koniec_tury = true;
                }
                tiles[poz_x, poz_y].Image = Properties.Resources.Dwarf1; //ustawienie Gerwazego po przesunięciu go
                if ((String)tiles[poz_x,poz_y].Tag == "Diament") //Zbieranie diamentów
                {
                    punkty++;
                    tiles[poz_x, poz_y].Tag = "Empty";
                }
                if((String)tiles[poz_x,poz_y].Tag == "Serce") //zbieranie serc
                {
                    zycia++;
                    tiles[poz_x, poz_y].Tag = "Empty";
                    czy_serce = false;
                }
            }

            //niszczenie kamieni
            if (kop_lewo || kop_prawo || kop_gora || kop_dol) //następuje po aciśnięciu WASD o ile kamień znajduje sie obok Gerwazego. Zniszczony kamień zmienia się w diament
            {
                if(kop_lewo && poz_y > 0 && (String)tiles[poz_x, poz_y - 1].Tag == "Kamien" && !koniec_tury)
                {
                    tiles[poz_x, poz_y - 1].Tag = "Diament1";
                    tiles[poz_x, poz_y - 1].Image = Properties.Resources.Diament;
                    kop_lewo = false;
                    koniec_tury = true;
                }
                if (kop_gora && poz_x > 0 && (String)tiles[poz_x - 1, poz_y].Tag == "Kamien" && !koniec_tury)
                {
                    tiles[poz_x -1, poz_y].Tag = "Diament1";
                    tiles[poz_x -1, poz_y].Image = Properties.Resources.Diament;
                    kop_gora = false;
                    koniec_tury = true;
                }
                if (kop_prawo && poz_y < 3 && (String)tiles[poz_x, poz_y + 1].Tag == "Kamien" && !koniec_tury)
                {
                    tiles[poz_x, poz_y + 1].Tag = "Diament1";
                    tiles[poz_x, poz_y + 1].Image = Properties.Resources.Diament;
                    kop_prawo = false;
                    koniec_tury = true;
                }
                if (kop_dol && poz_x < 3 && (String)tiles[poz_x + 1, poz_y].Tag == "Kamien" && !koniec_tury)
                {
                    tiles[poz_x + 1, poz_y].Tag = "Diament1";
                    tiles[poz_x + 1, poz_y].Image = Properties.Resources.Diament;
                    kop_dol = false;
                    koniec_tury = true;
                }
                //niszczenie większych kamieni - jeśli się uda zmieniają sie w małe kamienie
                if (kop_lewo && poz_y > 0 && (String)tiles[poz_x, poz_y - 1].Tag == "Kamien2" && !koniec_tury)
                {
                    tiles[poz_x, poz_y - 1].Tag = "Kamien";
                    tiles[poz_x, poz_y - 1].Image = Properties.Resources.Rock1;
                    kop_lewo = false;
                    koniec_tury = true;
                }
                if (kop_gora && poz_x > 0 && (String)tiles[poz_x - 1, poz_y].Tag == "Kamien2" && !koniec_tury)
                {
                    tiles[poz_x - 1, poz_y].Tag = "Kamien";
                    tiles[poz_x - 1, poz_y].Image = Properties.Resources.Rock1;
                    kop_gora = false;
                    koniec_tury = true;
                }
                if (kop_prawo && poz_y < 3 && (String)tiles[poz_x, poz_y + 1].Tag == "Kamien2" && !koniec_tury)
                {
                    tiles[poz_x, poz_y + 1].Tag = "Kamien";
                    tiles[poz_x, poz_y + 1].Image = Properties.Resources.Rock1; 
                    kop_prawo = false;
                    koniec_tury = true;
                }
                if (kop_dol && poz_x < 3 && (String)tiles[poz_x + 1, poz_y].Tag == "Kamien2" && !koniec_tury)
                {
                    tiles[poz_x + 1, poz_y].Tag = "Kamien";
                    tiles[poz_x + 1, poz_y].Image = Properties.Resources.Rock1;
                    kop_dol = false;
                    koniec_tury = true;
                }
            }

            if (koniec_tury)
            {//zwiększenie poziomu trudności co 50 punktów
                punkty++;
                if(punkty == 100)
                {
                    trudnosc++;
                }
                if(punkty == 200)
                {
                    trudnosc2++;
                }
                if(punkty == 300)
                {
                    trudnosc++;
                }
                if(punkty == 400)
                {
                    trudnosc2++;
                }
                nowaTura();
                koniec_tury = false;
            }

            if (koniecGry)
            {
                bool czy_rekord = false;
                for (int i = 9; i > -1; i--) //sprawdzamy czy nowy wynik znajduje się w pierwszej dziesiątce ijeśli tak to aktualizujemy tablicę wyników
                {
                    
                    if(tablica_wynikow[i]<punkty)
                    {
                        czy_rekord = true;
                        if(i!=9)
                        {
                            tablica_wynikow[i + 1] = tablica_wynikow[i];
                        }
                        tablica_wynikow[i] = punkty;
                    }
                }
                if(czy_rekord) //jeśli zaktualizowaliśmy tablicę to zapisujmy ją do pliku
                {
                    zapisz_wyniki();
                }
                koniecGry = false;
                //wyświetlenie gratulacji i informacji o wyniku
                String message = czy_rekord ? "Brawo! To nowy rekord!" : "Brawo!";
                DialogResult result = MessageBox.Show($"{message}\n Twój wynik to: {punkty} \nCzy chcesz zacząć nową grę?", "Koniec Gry", MessageBoxButtons.YesNo);
                timer1.Stop();
                //rozpoczęcie nowej gry lub zamknięcie aplikacji
                if(result == DialogResult.Yes)
                {
                    nowaGra();
                }
                else
                {
                    this.Close();
                }
            }

        }

        private void zapisz_wyniki()
        {
            System.Xml.Serialization.XmlSerializer writer = new System.Xml.Serialization.XmlSerializer(typeof(int[]));

            var path = Directory.GetCurrentDirectory() + "//WYNIKI.xml";
            System.IO.FileStream file = System.IO.File.Create(path);

            writer.Serialize(file, tablica_wynikow);
            file.Close();
            
        }

        private void wczytaj_wyniki()
        {
            System.Xml.Serialization.XmlSerializer reader =
                    new System.Xml.Serialization.XmlSerializer(typeof(int[]));
            var path = Directory.GetCurrentDirectory() + "//WYNIKI.xml";
            System.IO.StreamReader file = new System.IO.StreamReader(path);
            tablica_wynikow = (int[])reader.Deserialize(file);
            file.Close();
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Left && !idz_lewo)
            {
                idz_lewo = true;
            }
            if(e.KeyCode == Keys.Right && !idz_prawo)
            {
                idz_prawo = true;
            }
            if(e.KeyCode == Keys.Up && !idz_gora)
            {
                idz_gora = true;
            }
            if(e.KeyCode == Keys.Down && !idz_dol)
            {
                idz_dol = true;
            }
            
            if(e.KeyCode == Keys.W && !kop_gora)
            {
                kop_gora = true;
            }
            if(e.KeyCode == Keys.A && !kop_lewo)
            {
                kop_lewo = true;
            }
            if(e.KeyCode == Keys.S && !kop_dol)
            {
                kop_dol = true;
            }
            if(e.KeyCode == Keys.D && !kop_prawo)
            {
                kop_prawo = true;
            }
            
        }

        //private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        //{

        //}

        public Form1()
        {
            InitializeComponent();
        }

        private void pomocToolStripMenuItem_Click(object sender, EventArgs e) //Instrukcja do gry
        {
            MessageBox.Show("Po długim dniu pracy w kopalni, krasnolud Gerwazy zorientował się, że się zgubił. Trafił do dużej jaskini z bardzo niestabilnym sufitem. Wejście niestety zostało zablokowane. Pomóż Gerwazemu przeżyć jak najdłużej. Może ktoś przyjdzie go uratować. \n\n\n Użyj strzałek do poruszania się, oraz WASD do niszczenia kamieni kilofem. Ale uważaj! Po każdym ruchu część sufitu ma szansę spaść.\n Diamenty zwiększają Twój wynik, a serca dają Ci dodatkowe życie", "Instukcja gry");
        }

        //private void nowaGraToolStripMenuItem_Click(object sender, EventArgs e)
        //{

           
        //}

        private void Form1_Load(object sender, EventArgs e)
        {
            //przypisanie pól na mapie do tablicy
            tiles[0, 0] = pictureBox1;
            tiles[0, 1] = pictureBox2;
            tiles[0, 2] = pictureBox3;
            tiles[0, 3] = pictureBox4;
            tiles[1, 0] = pictureBox5;
            tiles[1, 1] = pictureBox6;
            tiles[1, 2] = pictureBox7;
            tiles[1, 3] = pictureBox8;
            tiles[2, 0] = pictureBox9;
            tiles[2, 1] = pictureBox10;
            tiles[2, 2] = pictureBox11;
            tiles[2, 3] = pictureBox12;
            tiles[3, 0] = pictureBox13;
            tiles[3, 1] = pictureBox14;
            tiles[3, 2] = pictureBox15;
            tiles[3, 3] = pictureBox16;
            wczytaj_wyniki(); //wczytanie tablicy wyników z pliku
        }


        private void nowaTura()
        {
            foreach(PictureBox pb in tiles)
            {
                if((String)pb.Tag == "Diament") //diament znika po jednej turze
                {
                    pb.Tag = "Empty";
                    pb.Image = null;
                }
                if((String)pb.Tag == "Diament1")
                {
                    pb.Tag = "Diament";
                }
                if((String)pb.Tag == "Zagrozenie")//na płytki zagrożenie spada kamień
                {
                    if (pb == tiles[poz_x, poz_y]) //jesli kamien ma spasc na Gerwazego, to Gerwazy traci życie
                    {
                        zycia--;
                        pb.Tag = "Empty";
                        if(zycia == 0)
                        {
                            koniecGry = true;
                            pb.Image = Properties.Resources.RIP;
                        }                        
                    }
                    else //jesli nie było tam Gerwazego to kamień spada
                    {
                        int random2 = random.Next(10);
                        if (punkty > 50 && random2 < trudnosc2)
                        {
                            pb.Tag = "Kamien2";
                            pb.Image = Properties.Resources.Rock2;
                        }
                        else
                        {
                            pb.Tag = "Kamien";
                            pb.Image = Properties.Resources.Rock1;

                        }
                    }                    
                                      
                    
                    
                    pb.BackColor = Color.Transparent;
                    
                    
                }
                if((String)pb.Tag == "Empty") //Losowanie, czy na puste pole spadnie kamień lub serduszko
                {
                    int randomNumber = random.Next(25);
                    if(randomNumber < trudnosc)
                    {
                        pb.Tag = "Zagrozenie";
                        pb.BackColor = Color.Salmon;
                    }
                    else
                    {
                        randomNumber = random.Next(500);
                        if(randomNumber < 1 && pb != tiles[poz_x, poz_y] && !czy_serce)
                        {
                            pb.Tag = "Serce";
                            pb.Image = Properties.Resources.Serce;
                            czy_serce = true;
                        }
                    }
                }
            }
        }

        private void nowaGra() //restartowanie gry
        {
            foreach(PictureBox pb in tiles)
            {
                pb.Tag = "Empty";
                pb.BackColor = Color.Transparent;
                pb.Image = null;
            }
            poz_x = 2;
            poz_y = 1;
            punkty = 0;
            zycia = 1;
            trudnosc = 1;
            trudnosc2 = 1;
            czy_serce = false;
            tiles[poz_x, poz_y].Image = Properties.Resources.Dwarf1;
            timer1.Start();

        }
    }
}
