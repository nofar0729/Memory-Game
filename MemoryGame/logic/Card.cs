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
    internal class Card
    {
        private readonly char r_Value;
        private bool m_IsFlipped;
        private int m_RowIndex;
        private int m_ColumnIndex;

        internal Card(char i_Value)
        {
            r_Value = i_Value;
            m_IsFlipped = false;
        }

        internal char Value
        {
            get
            {
                return r_Value;
            }
        }

        internal bool IsFlipped
        {
            get
            {
                return m_IsFlipped;
            }
            set
            {
                m_IsFlipped = value;
            }
        }

        public int RowIndex
        {
            get
            {
                return m_RowIndex;
            }
            set
            {
                m_RowIndex = value;
            }
        }

        public int ColumnIndex
        {
            get
            {
                return m_ColumnIndex;
            }
            set
            {
                m_ColumnIndex = value;
            }
        }

        public override String ToString()
        {
            String valueCard = r_Value.ToString();
            String showCard = m_IsFlipped ? valueCard : " ";
            return String.Format("{0}{1}{0}", " ", showCard);
        }
    }
}