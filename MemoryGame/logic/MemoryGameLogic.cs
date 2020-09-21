using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    internal class MemoryGameLogic
    {
        private readonly Player r_Player1;
        private readonly Player r_Player2;
        private readonly Board r_Board;
        private readonly List<Card> r_UnseenCards;
        private readonly List<Card> r_SeenCards;
        private readonly List<Card> r_LegalCards;
        private Random m_RandomNumber = new Random();

        public MemoryGameLogic(MemoryGameSettings i_Settings)
        {
            r_Player1 = new Player(i_Settings.Player1Name, false);
            r_Player2 = new Player(i_Settings.Player2Name, !i_Settings.AgainstFriend);
            r_Board = new Board(i_Settings.NumbeOfBoardRows, i_Settings.NumbeOfBoardColumns);
            r_UnseenCards = new List<Card>();
            r_SeenCards = new List<Card>();
            r_LegalCards = new List<Card>();
            BuildShuffledBoard();
        }

        internal List<Card> LegalCards { get { return r_LegalCards; } }

        internal Player Player1 { get { return r_Player1; } }

        internal Player Player2 { get { return r_Player2; } }

        internal Board Board { get { return r_Board; } }

        internal int NumbeOfBoardColumns { get { return r_Board.NumberOfBoardColumns; } }

        internal int NumbeOfBoardRows { get { return r_Board.NumbeOfBoardRows; } }

        internal Card getCardFromMemoryBoard(int i_RowIndex, int i_ColumnIndex)
        {
            return r_Board.GetCardFromBoard(i_RowIndex, i_ColumnIndex);
        }

        public void BuildShuffledBoard()
        {
            Card[] listOfCards = new Card[r_Board.NumberOfBoardColumns * r_Board.NumbeOfBoardRows];
            char currentChar = 'A';
            int totalAmountOfCards = listOfCards.Length;

            for (int i = 0; i < totalAmountOfCards - 1; i = i + 2)
            {
                Card firstCard = new Card(currentChar);
                Card secondCard = new Card(currentChar);
                listOfCards[i] = firstCard;
                listOfCards[i + 1] = secondCard;
                currentChar++;
            }

            for (int i = 0; i < listOfCards.Length; i++)
            {
                int randomIndex = m_RandomNumber.Next(i, listOfCards.Length);
                Card swap = listOfCards[i];
                listOfCards[i] = listOfCards[randomIndex];
                listOfCards[randomIndex] = swap;
            }

            int currentIndexCard = 0;

            for (int i = 0; i < r_Board.NumbeOfBoardRows; i++)
            {
                for (int j = 0; j < r_Board.NumberOfBoardColumns; j++)
                {
                    Card currentCard = listOfCards[currentIndexCard];
                    r_Board.SetCardOnBoard(currentCard, i, j);
                    currentCard.RowIndex = i;
                    currentCard.ColumnIndex = j;
                    currentIndexCard++;
                    r_UnseenCards.Add(currentCard);
                    r_LegalCards.Add(currentCard);
                }
            }
        }

        internal void updateScore(Player i_CurrentPlayer)
        {
            r_Board.NumberOfMatchedPairs++;

            if (i_CurrentPlayer.Equals(r_Player1))
            {
                r_Player1.Score++;
            }
            else
            {
                r_Player2.Score++;
            }
        }

        internal bool AreAllCardsMatched()
        {
            return r_Board.NumberOfMatchedPairs == r_Board.NumbeOfBoardRows * r_Board.NumberOfBoardColumns / 2;
        }

        internal bool isTie()
        {
            return r_Player1.Score == r_Player2.Score;
        }

        internal Player GetWinner()
        {
            return r_Player1.Score < r_Player2.Score ? r_Player2 : r_Player1;
        }

        internal bool isFlipped(int i_RowIndex, int i_ColumnIndex)
        {
            return r_Board.GetCardFromBoard(i_RowIndex, i_ColumnIndex).IsFlipped;
        }

        private bool isMatchedCard(out Card io_firstCard)
        {
            bool isMatched = false;
            io_firstCard = null;

            foreach (Card firstCard in r_SeenCards)
            {
                foreach (Card secondCard in r_SeenCards)
                {
                    if (firstCard != secondCard && firstCard.Value == secondCard.Value)
                    {
                        io_firstCard = firstCard;
                        isMatched = true;
                    }
                }
            }

            return isMatched;
        }

        internal Card GetFirstCompuerCard()
        {
            Card computerPick;
            bool matched = isMatchedCard(out computerPick);
            if (!matched)
            {
                if (r_UnseenCards.Count > 0)
                {
                    int randomCardInList = m_RandomNumber.Next(r_UnseenCards.Count);
                    computerPick = r_UnseenCards[randomCardInList];
                }
                else
                {
                    int randomCardInList = m_RandomNumber.Next(r_LegalCards.Count);
                    computerPick = r_LegalCards[randomCardInList];
                }
            }

            return computerPick;
        }

        private bool getMatchedCard(Card i_FirstCard, out Card io_MatchedCard)
        {
            bool isMatched = false;
            io_MatchedCard = null;

            foreach (Card matchedCard in r_SeenCards)
            {
                if (i_FirstCard != matchedCard && i_FirstCard.Value == matchedCard.Value)
                {
                    io_MatchedCard = matchedCard;
                    isMatched = true;
                    break;
                }
            }

            return isMatched;
        }

        internal Card GetSecondCompuerCard(Card i_FirstPickedCard)
        {
            Card computerPick;
            bool matched = getMatchedCard(i_FirstPickedCard, out computerPick);

            if (!matched)
            {
                if (r_UnseenCards.Count > 0)
                {
                    int randomCardInList = m_RandomNumber.Next(r_UnseenCards.Count);
                    computerPick = r_UnseenCards[randomCardInList];
                }
                else
                {
                    int randomCardInList = m_RandomNumber.Next(r_LegalCards.Count);
                    computerPick = r_LegalCards[randomCardInList];
                }
            }

            return computerPick;
        }

        internal bool IsWinning(Card i_FirstPickedCard, Card i_SecondPickedCard)
        {
            return i_FirstPickedCard.Value.Equals(i_SecondPickedCard.Value);
        }

        internal void FlipCard(Card i_CardToFlip)
        {
            i_CardToFlip.IsFlipped = !i_CardToFlip.IsFlipped;
        }
        internal void FlipCards(Card i_FirstPickedCard, Card i_SecondPickedCard)
        {
            FlipCard(i_FirstPickedCard);
            FlipCard(i_SecondPickedCard);
        }

        private bool cardInList(List<Card> i_List, Card i_Card)
        {
            bool isCardInList = false;

            foreach (Card card in i_List)
            {
                if (card == i_Card)
                {
                    isCardInList = true;
                    break;
                }
            }

            return isCardInList;
        }

        internal void FirstCardHandler(Card i_FirstPickedCard)
        {
            if (cardInList(r_UnseenCards, i_FirstPickedCard))
            {
                r_UnseenCards.Remove(i_FirstPickedCard);
                r_SeenCards.Add(i_FirstPickedCard);
            }

            r_LegalCards.Remove(i_FirstPickedCard);
        }

        internal void SecondCardHandler(Card i_FirstPickedCard, Card i_SecondPickedCard)
        {
            if (i_FirstPickedCard.Value.Equals(i_SecondPickedCard.Value))
            {
                if (cardInList(r_UnseenCards, i_SecondPickedCard))
                {
                    r_UnseenCards.Remove(i_SecondPickedCard);
                }
                else
                {
                    r_SeenCards.Remove(i_SecondPickedCard);
                }

                r_LegalCards.Remove(i_SecondPickedCard);
                r_SeenCards.Remove(i_FirstPickedCard);
            }
            else
            {
                r_LegalCards.Add(i_FirstPickedCard);

                if (cardInList(r_UnseenCards, i_SecondPickedCard))
                {
                    r_UnseenCards.Remove(i_SecondPickedCard);
                    r_SeenCards.Add(i_SecondPickedCard);
                }
            }
        }
    }
}