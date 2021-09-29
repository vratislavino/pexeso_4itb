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
    public partial class Menu : Form
    {
        SettingsData settingsData;

        public Menu() {
            InitializeComponent();
            settingsData = new SettingsData();
        }

        private void button1_Click(object sender, EventArgs e) {
            Game g = new Game(settingsData);
            g.Show();
            this.Hide();
            g.FormClosing += (snd, evt) => {
                this.Show();
            };
        }

        private void button2_Click(object sender, EventArgs e) {
            MessageBox.Show("Pexeso snad znáte :)");
        }

        private void button3_Click(object sender, EventArgs e) {
            Settings settings = new Settings();

            settings.SettingsData = settingsData;
            
            var dr = settings.ShowDialog();
            if(dr == DialogResult.OK) {
                MessageBox.Show("Uloženo!");
            }
        }

        private void button4_Click(object sender, EventArgs e) {
            Application.Exit();
        }
    }
}
