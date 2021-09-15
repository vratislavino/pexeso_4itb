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
    public partial class Settings : Form
    {
        private const int MAX_PLAYERS = 4;

        private List<NameInput> nameInputs;

        private SettingsData settingsData;

        public SettingsData SettingsData { 
            get {
                return settingsData;
            } 
            set { 
                settingsData = value;
                InitValues();
            } 
        }

        public Settings() {
            InitializeComponent();
            nameInputs = new List<NameInput>();
            CreateNewNameInput();
        }

        private void InitValues() {
            for(int i = 0; i < settingsData.names.Length; i++) {
                nameInputs.Last().InputValue = settingsData.names[i];
            }
            numericUpDown1.Value = settingsData.numberOfPairs;
        }

        private void NameInputChanged(NameInput sender, bool isEmpty) {
            if(!isEmpty) {
                CreateNewNameInput();
            }
        }

        private void CreateNewNameInput() {

            if (IsMaximumReached())
                return;

            if (!IsThereAnyEmptyNameInput()) {
                NameInput n = new NameInput();
                nameInputs.Add(n);
                n.OnTextChanged += NameInputChanged;
                flowLayoutPanel1.Controls.Add(n);
            }
        }

        private bool IsThereAnyEmptyNameInput() {
            return nameInputs.Exists(x => x.IsEmpty);
        }

        private bool IsMaximumReached() {
            return nameInputs.Count >= MAX_PLAYERS;
        }

        private void button1_Click(object sender, EventArgs e) {
            var names = nameInputs.Where(x => !x.IsEmpty).Select(x => x.InputValue).ToList();
            if(names.Count > 1) {
                settingsData.names = names.ToArray();
                settingsData.numberOfPairs = (int)numericUpDown1.Value;
            } else {
                MessageBox.Show("Nastavení nebylo uloženo, nebyl dostatečný počet hráčů!");
            }
        }
    }
}
