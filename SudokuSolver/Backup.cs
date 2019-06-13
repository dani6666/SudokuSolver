using System.Collections.Generic;
using System.Linq;

namespace SudokuSolver
{
    static class Backup
    {
        private static Stack<BackupData> BackupDataStack { get; set; }
        public static bool Initialized { get; set; } = false;

        public static void Initialize()
        {
            Initialized = true;
            BackupDataStack = new Stack<BackupData>();
        }

        public static void SetBackup(SudokuPiece changedPiece)
        {
            BackupData backup = new BackupData(Sudoku.PiecesFilled, changedPiece);
            for (int i = 0; i < 81; i++)
            {
                if(Sudoku.Pieces[i].IsFilled)
                {
                    backup.PiecesFilled.Add(i, Sudoku.Pieces[i].Value);
                }
                else
                {
                    List<string> possibleNumbers = new List<string>();
                    possibleNumbers.AddRange(Sudoku.Pieces[i].PossibleNumbers);
                    backup.PiecesUnfilled.Add(i, possibleNumbers);
                }
            }

            BackupDataStack.Push(backup);
        }

        public static SudokuPiece RestoreLatestBackup()
        {
            Sudoku.ClearData();
            BackupData backup = BackupDataStack.Peek();

            for (int i = 0; i < 81; i++)
            {
                if(backup.PiecesFilled.Keys.Contains(i))
                {
                    Sudoku.Pieces[i].InsertValue(backup.PiecesFilled[i]);
                }
                else
                {
                    Sudoku.Pieces[i].RestoreToUnfilled(backup.PiecesUnfilled[i]);
                }
            }

            return backup.ChangedPiece;
        }

        internal static void DeleteLatestBackup()
        {
            BackupDataStack.Pop();
        }
    }
}
