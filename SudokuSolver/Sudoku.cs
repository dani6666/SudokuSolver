
namespace SudokuSolver
{
    static class Sudoku
    {
        public static bool Solved { get; private set; } = false;
        public static int PiecesFilled
        {
            get => _piecesFilled;
            set
            {
                if (value == 81)
                {
                    Solved = true;
                }
                _piecesFilled = value;
            }
        }
        private static int _piecesFilled = 0;
        public static SudokuPiece[] Pieces { get; private set; }
        public static SudokuPiece[][] Rows { get; private set; }
        public static SudokuPiece[][] Columns { get; private set; }
        public static SudokuPiece[][] Squares { get; private set; }
        public static SudokuPiece[][] AllGroups { get; private set; }

        public static void Initialize(SudokuPiece[] pieces)
        {
            Pieces = pieces;
            Rows = new SudokuPiece[9][];
            Columns = new SudokuPiece[9][];
            Squares = new SudokuPiece[9][];
            for (int i = 0; i < 9; i++)
            {
                Rows[i] = new SudokuPiece[9];
                Columns[i] = new SudokuPiece[9];
                Squares[i] = new SudokuPiece[9];
            }

            for(int i = 0; i < Pieces.Length; i++)
            {

                int rowIndex, columnIndex,
                squareIndex, indexInsideSquare;

                rowIndex = i / 9;
                columnIndex = i % 9;
                squareIndex = ((rowIndex) / 3) * 3 + 
                    (columnIndex) / 3;
                indexInsideSquare = (columnIndex) % 3 * 3 + 
                    (rowIndex) % 3;
                
                Rows[rowIndex][columnIndex] = Pieces[i];
                Pieces[i].Row = Rows[rowIndex]; 

                Columns[columnIndex][rowIndex] = Pieces[i];
                Pieces[i].Column = Columns[columnIndex];

                Squares[squareIndex][indexInsideSquare] = Pieces[i];
                Pieces[i].Square = Squares[squareIndex];
            }

            AllGroups = new SudokuPiece[27][];

            Rows.CopyTo(AllGroups, 0);
            Columns.CopyTo(AllGroups, 9);
            Squares.CopyTo(AllGroups, 18);
        }

        public static void ClearData()
        {
            foreach(SudokuPiece piece in Pieces)
            {
                piece.Reinitialize();
            }
            _piecesFilled = 0;
            Solved = false;
        }
    }
}
