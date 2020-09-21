using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MemoryGame
{
    public class Program
    {
        [STAThread]
        static void Main()
        {
            MemoryGameForm gameForm = new MemoryGameForm();
        }
    }
}
