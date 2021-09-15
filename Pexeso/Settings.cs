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
                // dostat data z NameInputs a vložit do SettingsData 
                // settingsData.names = 
                return settingsData;
            } 
            set { settingsData = value; } 
        }

        public Settings() {
            InitializeComponent();
            nameInputs = new List<NameInput>();
            CreateNewNameInput();
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
    }
}
