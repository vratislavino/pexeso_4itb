﻿using System;
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
           // int cards = settings.numberOfPairs;

        }
    }
}
