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
    public partial class PlayerOverview : UserControl
    {
        int score;
        public int Score {
            get { return score; }
            set { 
                score = value; 
                scoreLabel.Text = score.ToString(); 
            }
        }

        public string PlayerName => nameLabel.Text;

        bool isPlaying = false;
        public bool IsPlaying {
            get { return isPlaying; }
            set {
                isPlaying = value;
                this.BackColor = isPlaying ? Color.YellowGreen : Color.White;
            }
        }

        public PlayerOverview() {
            InitializeComponent();
        }

        public PlayerOverview(string name) : this() {
            nameLabel.Text = name;
            IsPlaying = false;
            Score = 0;
        }
    }
}
