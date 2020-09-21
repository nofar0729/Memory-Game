using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class SettingsForm : Form
    {
        private readonly List<Tuple<int, int>> r_BoardSizes;
        private int m_CurrentIndexOfBoardSize;
        private readonly MemoryGameSettings r_Settings;

        public SettingsForm()
        {
            InitializeComponent();
            r_Settings = new MemoryGameSettings();
            r_BoardSizes = new List<Tuple<int, int>>();
            r_BoardSizes.Add(new Tuple<int, int>(4, 4));
            r_BoardSizes.Add(new Tuple<int, int>(4, 5));
            r_BoardSizes.Add(new Tuple<int, int>(4, 6));
            r_BoardSizes.Add(new Tuple<int, int>(5, 4));
            r_BoardSizes.Add(new Tuple<int, int>(5, 6));
            r_BoardSizes.Add(new Tuple<int, int>(6, 4));
            r_BoardSizes.Add(new Tuple<int, int>(6, 5));
            r_BoardSizes.Add(new Tuple<int, int>(6, 6));
            m_CurrentIndexOfBoardSize = 1;
            this.ShowDialog();
        }

        internal MemoryGameSettings Settings { get { return r_Settings; } }

        private void SizeButton_Click(object sender, EventArgs e)
        {
            int currentHeight = r_BoardSizes[m_CurrentIndexOfBoardSize].Item1;
            int currentWidth = r_BoardSizes[m_CurrentIndexOfBoardSize].Item2;
            m_CurrentIndexOfBoardSize = (m_CurrentIndexOfBoardSize + 1) % r_BoardSizes.Count;
            int newHeight = r_BoardSizes[m_CurrentIndexOfBoardSize].Item1;
            int newWidth = r_BoardSizes[m_CurrentIndexOfBoardSize].Item2;
            SizeButton.Text = String.Format("{0} x {1}", newHeight, newWidth);
            SizeButton.Height = SizeButton.Height + (newHeight - currentHeight) * 10;
            SizeButton.Width = SizeButton.Width + (newWidth - currentWidth) * 10;
        }

        private void AgainstButton_Click(object sender, EventArgs e)
        {
            Button theSender = sender as Button;

            if (theSender.Text == "Against a Friend")
            {
                AgainstButton.Text = "Against Computer";
                Player2TextBox.Clear();
                Player2TextBox.Enabled = true;
            }
            else
            {
                AgainstButton.Text = "Against a Friend";
                Player2TextBox.Text = "-computer-";
                Player2TextBox.Enabled = false;
            }
        }

        private void StartButton_Click(object sender, EventArgs e)
        {
            r_Settings.Player1Name = Player1TextBox.Text;
            r_Settings.AgainstFriend = AgainstButton.Text.Equals("Against Computer");
            r_Settings.Player2Name = r_Settings.AgainstFriend ? Player2TextBox.Text : "Computer";
            r_Settings.NumbeOfBoardRows = r_BoardSizes[m_CurrentIndexOfBoardSize].Item1;
            r_Settings.NumbeOfBoardColumns = r_BoardSizes[m_CurrentIndexOfBoardSize].Item2;
            this.Close();
        }
    }
}
