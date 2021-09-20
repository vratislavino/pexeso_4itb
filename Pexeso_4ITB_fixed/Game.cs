using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pexeso
{
    public partial class Game : Form
    {
        SettingsData settings;

        public Game() {
            InitializeComponent();
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

        private void InitGame() {
            //MessageBox.Show(settings.numberOfPairs.ToString());
            //int cards = settings.numberOfPairs;

            Card c = new Card(0, "imagines/adam_budis.jpg");
            Card c2 = new Card(0, "imagines/matej_rajtora.jpg");
            c2.Location = new Point(150, 0);

            this.Controls.Add(c);
            this.Controls.Add(c2);

            c.Flip();
            c2.Flip();
        }
    }
}
