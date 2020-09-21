using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    internal class Board
    {
        private readonly int r_NumbeOfBoardColumns;
        private readonly int r_NumbeOfBoardRows;
        private readonly Card[,] r_CardBoard;
        private int m_NumberOfMatchPairs = 0;

        public Board(int i_NumberOfRows, int i_NumberOfColumns)
        {
            r_NumbeOfBoardColumns = i_NumberOfColumns;
            r_NumbeOfBoardRows = i_NumberOfRows;
            r_CardBoard = new Card[r_NumbeOfBoardRows, r_NumbeOfBoardColumns];
        }

        internal int NumberOfBoardColumns
        {
            get
            {
                return r_NumbeOfBoardColumns;
            }
        }

        internal int NumbeOfBoardRows
        {
            get
            {
                return r_NumbeOfBoardRows;
            }
        }

        public int NumberOfMatchedPairs
        {
            get
            {
                return m_NumberOfMatchPairs;
            }
            set
            {
                m_NumberOfMatchPairs = value;
            }
        }

        internal Card GetCardFromBoard(int i_RowIndex, int i_ColumnIndex)
        {
            return r_CardBoard[i_RowIndex, i_ColumnIndex];
        }

        internal void SetCardOnBoard(Card i_Card, int i_RowIndex, int i_ColumnIndex)
        {
            r_CardBoard[i_RowIndex, i_ColumnIndex] = i_Card;
        }
    }
}