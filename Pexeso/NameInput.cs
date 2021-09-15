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
    public delegate void ChangeDelegate(NameInput sender, bool isEmpty);

    public partial class NameInput : UserControl
    {
        public new event ChangeDelegate OnTextChanged;

        public bool IsEmpty {
            get { return string.IsNullOrEmpty(textBox1.Text); }
        }

        public string InputValue {
            get { return textBox1.Text; }
            set { textBox1.Text = value; }
        }

        //public bool IsEmpty => string.IsNullOrEmpty(textBox1.Text);

        private static string[] names = new string[] {
            "Alois", "Karel", "Pepa", "Antonín", "Otto", "Hans", "Adam", "Adolf", "Karolína", "Bára", "Lucie"
        };


        public NameInput() {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e) { 
            Random r = new Random();
            string jmeno = names[r.Next(names.Length)];
            textBox1.Text = jmeno;
        }

        private void textBox1_TextChanged(object sender, EventArgs e) {
            OnTextChanged?.Invoke(this, string.IsNullOrEmpty(textBox1.Text));
        }
    }
}
