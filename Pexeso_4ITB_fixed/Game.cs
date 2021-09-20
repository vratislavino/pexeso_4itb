using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pexeso
{
    public partial class Game : Form
    {
        SettingsData settings;
        List<Card> cards = new List<Card>();

        List<string> imgPaths = new List<string>();

        public Game() {
            InitializeComponent();
            LoadInitPaths();
            settings = new SettingsData();
        }

        public Game(SettingsData settings) : this() {
            if (settings != null) {
                this.settings = settings;
            }
        }

        private void Game_Load(object sender, EventArgs e) {
            InitGame();
        }

        private void LoadInitPaths() {
            imgPaths = Directory.GetFiles("imagines").ToList();
        }

        private void InitGame() {
            Random r = new Random();

            for (int i = 0; i < settings.numberOfPairs; i++) {
                int c = r.Next(imgPaths.Count);
                string path = imgPaths[c];
                imgPaths.RemoveAt(c);
                Card c1 = new Card(i, path);
                Card c2 = new Card(i, path);

                cards.Insert(r.Next(cards.Count), c1);
                cards.Insert(r.Next(cards.Count), c2);
            }

            RepositionCards();
        }

        private void RepositionCards() {
            int vRadku = (int)(Math.Sqrt(cards.Count) + 0.9999);
            int vel = cards.First().Width;
            int offset = 5;

            for(int i = 0; i < vRadku; i++) {
                for (int j = 0; j < vRadku; j++) {
                    if (i * vRadku + j < cards.Count) {
                        cards[i * vRadku + j].Location = new Point(i * (vel+offset), j * (vel+offset));
                        panel1.Controls.Add(cards[i * vRadku + j]);
                        cards[i * vRadku + j].CardClicked += OnCardClicked;
                    }
                }
            }

            ShuffleCards();
        }

        private void ShuffleCards() {
            Random r = new Random();
            for (int i = 0; i < 1000; i++) {
                int a = r.Next(cards.Count);
                int b = r.Next(cards.Count);
                if (a != b) {
                    Point p = cards[a].Location;
                    cards[a].Location = cards[b].Location;
                    cards[b].Location = p;
                }
            }
            panel1.Refresh();
        }

        private void OnCardClicked(Card card) {
            card.Flip();
        }
    }
}
