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
        List<PlayerOverview> players = new List<PlayerOverview>();
        int currentPlayer = 0;

        List<string> imgPaths = new List<string>();

        Card first = null;
        Card second = null;


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
            CreatePlayers();

            players[currentPlayer].IsPlaying = true;
        }

        private void CreatePlayers() {
            for(int i = 0; i < settings.names.Length; i++) {
                PlayerOverview po = new PlayerOverview(settings.names[i]);
                players.Add(po);
                playersPanel.Controls.Add(po);
            }
        }

        private void SwitchPlayer() {
            players[currentPlayer].IsPlaying = false;
            currentPlayer = (currentPlayer + 1) % players.Count;
            players[currentPlayer].IsPlaying = true;
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
            if(!timer1.Enabled) {
                if (first == null) {
                    first = card;
                    card.Flip();
                    return;
                }

                if (second == null) {
                    if (card == first)
                        return;
                    second = card;
                    card.Flip();
                    timer1.Start();
                    return;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e) {
            timer1.Stop();
            Console.WriteLine(first);
            Console.WriteLine(second);
            if(first.Equals(second)) {
                players[currentPlayer].Score++;
                panel1.Controls.Remove(first);
                panel1.Controls.Remove(second);

                cards.Remove(first);
                cards.Remove(second);

                first = null;
                second = null;

                CheckForWinner();
            } else {
                first.Flip();
                second.Flip();

                first = null;
                second = null;
                SwitchPlayer();
            }
        }
        private void CheckForWinner() {
            if (cards.Count == 0) {
                List<PlayerOverview> best = new List<PlayerOverview>();
                int max = players.Max(x => x.Score);
                best = players.Where(x=>x.Score == max).ToList();
                if(best.Count > 1) {
                    MessageBox.Show("Je to nerozhodně, vítězí " + string.Join(", ", best.Select(x=>x.PlayerName)));
                    this.Close();
                } else {
                    MessageBox.Show("Vítězem je " + best[0].PlayerName);
                    this.Close();
                }
            }
        }
    }
}
