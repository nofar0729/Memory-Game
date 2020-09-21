using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public partial class MemoryGameForm : Form
    {
        private readonly SettingsForm r_SettingsForm;
        private List<Image> m_Images = new List<Image>();
        private readonly MemoryGameLogic r_MemoryGame;
        private readonly Dictionary<Button, Card> r_PictureCard;
        private readonly Color r_Player1Color = Color.LightGreen;
        private readonly Color r_Player2Color = Color.FromArgb(192, 192, 255);
        private Player m_CurrentPlayer;
        private Button m_FirstPickedCard;
        private Button m_SecondPickedCard;
        private const int k_OneSecond = 1000;

        public MemoryGameForm()
        {
            r_SettingsForm = new SettingsForm();
            r_MemoryGame = new MemoryGameLogic(r_SettingsForm.Settings);
            r_PictureCard = new Dictionary<Button, Card>();
            InitializeComponent();
            initializeForm();
            this.ShowDialog();
        }

        public MemoryGameForm(SettingsForm i_Settings)
        {
            r_MemoryGame = new MemoryGameLogic(i_Settings.Settings);
            r_PictureCard = new Dictionary<Button, Card>();
            InitializeComponent();
            initializeForm();
            this.ShowDialog();
        }
        // $G$ CSS-999 (-3) missing blank lines
        private Image downloadImageFromUrl(string i_ImageUrl)
        {
            Image image = null;

            try
            {
                System.Net.HttpWebRequest webRequest = (System.Net.HttpWebRequest)System.Net.HttpWebRequest.Create(i_ImageUrl);
                webRequest.AllowWriteStreamBuffering = true;
                webRequest.Timeout = 30000;

                System.Net.WebResponse webResponse = webRequest.GetResponse();

                System.IO.Stream stream = webResponse.GetResponseStream();

                image = System.Drawing.Image.FromStream(stream);

                webResponse.Close();
            }
            catch (Exception ex)
            {
                return null;
            }

            return image;
        }

        private void addImagesToList()
        {
            for (int i = 0; i < r_MemoryGame.Board.NumbeOfBoardRows * r_MemoryGame.Board.NumberOfBoardColumns / 2; i++)
            {
                Image image = downloadImageFromUrl("https://picsum.photos/80");

                while (m_Images.Contains(image))
                {
                    image = downloadImageFromUrl("https://picsum.photos/80");
                }

                m_Images.Add(image);
            }
        }

        private void initializeBoard()
        {
            addImagesToList();

            foreach (Card card in r_MemoryGame.LegalCards)
            {
                Button currentButton = new Button();
                currentButton.BackColor = prototypeButton.BackColor;
                currentButton.Size = prototypeButton.Size;
                int i = card.RowIndex;
                int j = card.ColumnIndex;
                int leftPosition = (j + 1) * 10 + j * prototypeButton.Width;
                int topPosition = (i + 1) * 10 + i * prototypeButton.Height;
                currentButton.Left = leftPosition;
                currentButton.Top = topPosition;
                currentButton.Click += card_Click;
                currentButton.TabStop = false;
                r_PictureCard.Add(currentButton, card);
                Controls.Add(currentButton);
            }
        }

        private void hideCard(Button i_CardToHide)
        {
            bool getValue = r_PictureCard.TryGetValue(i_CardToHide, out Card o_CurrentCard);
            r_MemoryGame.FlipCard(o_CurrentCard);
            i_CardToHide.BackgroundImage = null;
            i_CardToHide.BackColor = prototypeButton.BackColor;
            i_CardToHide.Enabled = !i_CardToHide.Enabled;
        }

        private void showCard(Button i_CardToShow)
        {
            bool getValue = r_PictureCard.TryGetValue(i_CardToShow, out Card o_CurrentCard);
            r_MemoryGame.FlipCard(o_CurrentCard);
            i_CardToShow.BackgroundImage = m_Images[o_CurrentCard.Value - 'A'];
            i_CardToShow.BackColor = m_CurrentPlayer.Equals(r_MemoryGame.Player1) ? r_Player1Color : r_Player2Color;
            i_CardToShow.Enabled = !i_CardToShow.Enabled;
        }

        private void initializeForm()
        {
            initializeBoard();
            int boardHeight = r_MemoryGame.NumbeOfBoardRows * 90;
            int boardWidth = r_MemoryGame.NumbeOfBoardColumns * 90;
            CurrentPlayerLabel.Top = boardHeight + 12;
            CurrentPlayerLabel.Left = 12;
            Player1Label.Top = CurrentPlayerLabel.Top + 25;
            Player1Label.Left = CurrentPlayerLabel.Left;
            Player2Label.Top = Player1Label.Top + 25;
            Player2Label.Left = CurrentPlayerLabel.Left;
            Height = Player2Label.Top + 70;
            Width = boardWidth + 25;
            setPlayers();
        }

        private void setPlayers()
        {
            m_CurrentPlayer = r_MemoryGame.Player1;
            CurrentPlayerLabel.Text = string.Format("Current Player: {0}", m_CurrentPlayer.Name);
            CurrentPlayerLabel.BackColor = r_Player1Color;
            Player1Label.Text = string.Format("{0}: {1} {2}", r_MemoryGame.Player1.Name, 0, "pairs");
            Player2Label.Text = string.Format("{0}: {1} {2}", r_MemoryGame.Player2.Name, 0, "pairs");
            Player1Label.BackColor = r_Player1Color;
            Player2Label.BackColor = r_Player2Color;
        }

        private void card_Click(object sender, EventArgs e)
        {
            Button senderCard = sender as Button;
            bool getValue = r_PictureCard.TryGetValue(senderCard, out Card o_CurrentCard);
            showCard(senderCard);

            if (m_FirstPickedCard == null)
            {
                m_FirstPickedCard = senderCard;
                r_MemoryGame.FirstCardHandler(o_CurrentCard);
            }
            else
            {
                m_SecondPickedCard = senderCard;
                secondCardHandler();
            }
        }



        private void secondCardHandler()
        {
            bool getFirstCard = r_PictureCard.TryGetValue(m_FirstPickedCard, out Card o_FirstCard);
            bool getSecondCard = r_PictureCard.TryGetValue(m_SecondPickedCard, out Card o_SecondCard);
            r_MemoryGame.SecondCardHandler(o_FirstCard, o_SecondCard);

            if (r_MemoryGame.IsWinning(o_FirstCard, o_SecondCard))
            {
                r_MemoryGame.updateScore(m_CurrentPlayer);
                updateLabels();
                this.Update();
                m_FirstPickedCard = null;
                m_SecondPickedCard = null;

                if (r_MemoryGame.AreAllCardsMatched())
                {
                    replay();
                }
                else
                {
                    if (m_CurrentPlayer.IsComputer)
                    {
                        Thread.Sleep(k_OneSecond);
                        playComputersTurn();
                    }
                }
            }
            else
            {
                Thread.Sleep(k_OneSecond * 2);
                hideCard(m_FirstPickedCard);
                hideCard(m_SecondPickedCard);
                m_FirstPickedCard = null;
                m_SecondPickedCard = null;
                swapPlayers();
            }

            Thread.Sleep(k_OneSecond);
        }

        private void swapPlayers()
        {
            m_CurrentPlayer = m_CurrentPlayer.Equals(r_MemoryGame.Player1) ? r_MemoryGame.Player2 : r_MemoryGame.Player1;
            CurrentPlayerLabel.Text = string.Format("Current Player: {0}", m_CurrentPlayer.Name);
            CurrentPlayerLabel.BackColor = m_CurrentPlayer.Equals(r_MemoryGame.Player1) ? r_Player1Color : r_Player2Color;
            this.Update();

            if (m_CurrentPlayer.IsComputer)
            {
                playComputersTurn();
            }
        }



        private void playComputersTurn()
        {
            Card firstPickedCard = r_MemoryGame.GetFirstCompuerCard();
            m_FirstPickedCard = getPictureBoxByCard(firstPickedCard);
            r_MemoryGame.FirstCardHandler(firstPickedCard);
            showCard(m_FirstPickedCard);
            Thread.Sleep(k_OneSecond);
            Card secondPickedCard = r_MemoryGame.GetSecondCompuerCard(firstPickedCard);
            m_SecondPickedCard = getPictureBoxByCard(secondPickedCard);
            showCard(m_SecondPickedCard);
            Thread.Sleep(k_OneSecond);
            secondCardHandler();
        }

        private Button getPictureBoxByCard(Card i_Card)
        {
            Button theMatchedButton = null;

            foreach (Button button in r_PictureCard.Keys)
            {
                bool getValue = r_PictureCard.TryGetValue(button, out Card o_CurrentCard);

                if (o_CurrentCard.Equals(i_Card))
                {
                    theMatchedButton = button;
                    break;
                }
            }

            return theMatchedButton;
        }

        private void updateLabels()
        {
            if (m_CurrentPlayer.Equals(r_MemoryGame.Player1))
            {
                Player1Label.Text = string.Format("{0}: {1} Pairs", m_CurrentPlayer.Name, m_CurrentPlayer.Score);
            }
            else
            {
                Player2Label.Text = string.Format("{0}: {1} Pairs", m_CurrentPlayer.Name, m_CurrentPlayer.Score);
            }
        }

        private void replay()
        {
            StringBuilder endMessgae = new StringBuilder();

            if (r_MemoryGame.isTie())
            {
                endMessgae.Append("Oh no, there's a tie!");
            }
            else
            {
                endMessgae.AppendFormat("And the winner is...{0}! with a score of {1}", r_MemoryGame.GetWinner().Name, r_MemoryGame.GetWinner().Score);
            }

            endMessgae.AppendLine();
            endMessgae.AppendLine("Would you like to play again?");
            DialogResult answer = MessageBox.Show(endMessgae.ToString(), "Game Over", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (answer.Equals(DialogResult.Yes))
            {
                MemoryGameForm newGame = new MemoryGameForm(r_SettingsForm);
                Dispose();
            }
            else
            {
                Dispose();
                Close();
            }
        }
    }
}
