using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MemoryGame
{
    public class MemoryGameSettings
    {
        private String m_Player1Name = "";
        private bool m_AgainstFriend = false;
        private String m_Player2Name = "Computer";
        private int m_NumbeOfBoardRows = 4;
        private int m_NumbeOfBoardColumns = 5;

        internal String Player1Name { get { return m_Player1Name; } set { m_Player1Name = value; } }
        internal String Player2Name { get { return m_Player2Name; } set { m_Player2Name = value; } }
        internal bool AgainstFriend { get { return m_AgainstFriend; } set { m_AgainstFriend = value; } }
        internal int NumbeOfBoardRows { get { return m_NumbeOfBoardRows; } set { m_NumbeOfBoardRows = value; } }
        internal int NumbeOfBoardColumns { get { return m_NumbeOfBoardColumns; } set { m_NumbeOfBoardColumns = value; } }
    }
}
