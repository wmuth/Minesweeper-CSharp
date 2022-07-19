using System;
using System.Windows.Forms;

namespace MineSweeper
{
    public partial class DifficultyForm : Form
    {
        public DifficultyForm()
        {
            InitializeComponent();
        }

        private void easyButton_Click(object sender, EventArgs e)
        {
            MineSweeper.difficulty = 1;
            this.Close();
        }

        private void mediumButton_Click(object sender, EventArgs e)
        {
            MineSweeper.difficulty = 2;
            this.Close();
        }

        private void hardButton_Click(object sender, EventArgs e)
        {
            MineSweeper.difficulty = 3;
            this.Close();
        }

        private void DifficultyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MineSweeper.difficulty == 0)
            {
                Application.Exit();
            }
        }
    }
}
