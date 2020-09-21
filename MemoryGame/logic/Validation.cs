using System;
using System.Linq;


namespace MemoryGame
{
    internal static class Validation
    {
        private const int k_MinimumSize = 4;
        private const int k_MaximumSize = 6;

        internal static bool IsNumber(String i_Input, out int io_Size)
        {
            bool IsNumber = int.TryParse(i_Input, out io_Size);
            return IsNumber;
        }

        internal static bool IsNumberInRange(int i_Input)
        {
            return i_Input >= k_MinimumSize && i_Input <= k_MaximumSize;
        }

        internal static bool IsBoardSizeValid(int i_Width, int i_Height)
        {
            return (i_Width * i_Height) % 2 == 0;
        }

        internal static bool IsValidName(String i_Name,  out String io_ValidName)
        {
            bool isValid = i_Name.All(Char.IsLetter) && !String.IsNullOrEmpty(i_Name);
            io_ValidName = isValid ? i_Name : "";

            return isValid;
        }

        internal static bool IsYesOrNoAnswer(String i_Input)
        {
            bool isEmpty = String.IsNullOrEmpty(i_Input);
            bool length = false;

            if (!isEmpty)
            {
                length = i_Input.Length == 1;
            }

            return !isEmpty && length && (Char.ToUpper(i_Input[0]).Equals('Y') || Char.ToUpper(i_Input[0]).Equals('N'));
        }

        internal static bool IsYesAnswer(String i_Input)
        {
            return Char.ToUpper(i_Input[0]).Equals('Y');
        }

        internal static bool IsQuitting(String i_Card)
        {
            return i_Card.Length == 1 && Char.ToUpper(i_Card[0]).Equals('Q');
        }

        internal static bool IsCardInFormat(String i_Card, out int io_RowIndex, out int io_ColumnIndex)
        {
            bool validColumnIndex = false;
            bool validRowIndex = false;
            bool validLength = i_Card.Length == 2;
            bool isCapitalLetter = false;

            if (validLength)
            {
                validColumnIndex = Char.IsLetter(i_Card[0]);
                isCapitalLetter = Char.IsUpper(i_Card[0]);
                validRowIndex= Char.IsNumber(i_Card[1]);
            }

            bool isCardInFormat = validLength && validColumnIndex && validRowIndex && isCapitalLetter;
            io_RowIndex = isCardInFormat ? i_Card[1] - 49 : -1;
            io_ColumnIndex = isCardInFormat ? Char.ToUpper(i_Card[0]) - 65 : -1;

            return isCardInFormat;
        }

        internal static bool IsCardPickedInBoardBoundries(int i_RowIndex, int i_ColumnIndex, int i_Width, int i_Height)
        {
            bool validRowIndex = i_RowIndex >= 0 && i_RowIndex < i_Height;
            bool validColumnIndex = i_ColumnIndex >= 0 && i_ColumnIndex < i_Width;
            return validRowIndex && validColumnIndex;
        }

        internal static bool IsQ(Card i_Card)
        {
            return i_Card.Value.Equals(' ');
        }

    }
}