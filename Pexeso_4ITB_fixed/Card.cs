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
    public partial class Card : UserControl, IEquatable<Card>
    {
        public event Action<Card> CardClicked;

        private Image image;
        private bool isFlipped;
        private int id;

        private bool IsFlipped {
            get { return isFlipped; }
            set {
                isFlipped = value;
                if(isFlipped) {
                    this.BackgroundImage = image;
                } else {
                    this.BackgroundImage = null;
                }
            }
        }

        public Card() {
            InitializeComponent();
        }

        public Card(int id, string imgPath) : this() {
            this.id = id;
            this.isFlipped = false;
            this.image = Image.FromFile(imgPath);
        }

        public void Flip() {
            IsFlipped = !IsFlipped;
        }

        private void Card_Click(object sender, EventArgs e) {
            CardClicked?.Invoke(this);
        }

        public bool Equals(Card other) {
            return this.id == other.id;
        }
    }
}
