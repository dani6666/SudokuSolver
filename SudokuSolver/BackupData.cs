using System.Collections.Generic;

namespace SudokuSolver
{
    class BackupData
    {
        public Dictionary<int, string> PiecesFilled;
        public Dictionary<int, List<string>> PiecesUnfilled;
        public SudokuPiece ChangedPiece;
        public BackupData(int piecesToBackup, SudokuPiece changedPiece)
        {
            ChangedPiece = changedPiece;
            PiecesFilled = new Dictionary<int, string>(piecesToBackup);
            PiecesUnfilled = new Dictionary<int, List<string>>(81 - piecesToBackup);
        }
    }
}
