using System.Collections.Generic;
using System.Windows.Controls;

namespace SudokuSolver
{
    class SudokuPiece
    {
        public string Value => TextBox.Text;
        public bool IsFilled
        {
            get => _isFilled;
            set
            {
                _isFilled = value;
                if (value)
                {
                    Sudoku.PiecesFilled++;
                }
            }
        }
        private bool _isFilled;
        private TextBox TextBox;
        public List<string> PossibleNumbers;
        public SudokuPiece[] Row = null;
        public SudokuPiece[] Column;
        public SudokuPiece[] Square;

        public SudokuPiece(TextBox textBox)
        {
            TextBox = textBox;
            Reinitialize();
        }

        public void Reinitialize()
        {
            _isFilled = false;
            PossibleNumbers = new List<string>
            {
                "1", "2", "3", "4", "5",
                "6", "7", "8", "9"
            };
        }

        public void ClearText()
        {
            TextBox.Text = "";
        }

        public void RestoreToUnfilled(List<string> possibleNumbers)
        {
            ClearText();
            _isFilled = false;
            PossibleNumbers.Clear();
            PossibleNumbers.AddRange(possibleNumbers);
        }

        public void InsertValue(string value)
        {
            IsFilled = true;
            PossibleNumbers = null;
            TextBox.Text = value;

            UpdateGroups();
        }

        public void UpdateGroups()
        {
            foreach(SudokuPiece piece in Row)
            {
                if(!piece.IsFilled)
                {
                    piece.PossibleNumbers.Remove(TextBox.Text);
                }
            }

            foreach (SudokuPiece piece in Column)
            {
                if (!piece.IsFilled)
                {
                    piece.PossibleNumbers.Remove(TextBox.Text);
                }
            }

            foreach (SudokuPiece piece in Square)
            {
                if (!piece.IsFilled)
                {
                    piece.PossibleNumbers.Remove(TextBox.Text);
                }
            }
        }
    }
}
