using System.Linq;

namespace SudokuSolver
{
    static class SearchSystem
    {
        public static bool StartSolve()
        {
            Sudoku.ClearData();

            return Solve();
        }

        public static bool Solve()
        {
            if(InsertionCheck())
            {
                if(!DoTheEasySearch())
                {
                    return false;
                }

                while (!Sudoku.Solved && MidSearch())
                {
                    if (!DoTheEasySearch())
                    {
                        return false;
                    }
                }

                while (!Sudoku.Solved && HardSearch())
                {
                    while (!Sudoku.Solved && MidSearch())
                    {
                        if (!DoTheEasySearch())
                        {
                            return false;
                        }
                    }
                }
                
                if (!Sudoku.Solved)
                {
                    if (!Backup.Initialized)
                    {
                        Backup.Initialize();
                    }
                    DoTheGuess();
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        private static void DoTheGuess()
        {
            bool broken = false;
            int attempt = 1;
            for (int i = 2; !broken; i++)
            {
                foreach (SudokuPiece piece in Sudoku.Pieces)
                {
                    if(!piece.IsFilled)
                    {
                        if (piece.PossibleNumbers.Count == i)
                        {
                            Backup.SetBackup(piece);
                            piece.InsertValue(piece.PossibleNumbers.First());
                            broken = true;
                            break;
                        }
                    }
                }
            }
            
            while(!Solve())
            {
                SudokuPiece changedPiece = Backup.RestoreLatestBackup();
                changedPiece.InsertValue(changedPiece.PossibleNumbers.ElementAt(attempt));
                attempt++;
            }

            Backup.DeleteLatestBackup();
        }

        private static bool DoTheEasySearch()
        {
            bool? easySearchResult;
            do
            {
                easySearchResult = EasySearch();

            } while (easySearchResult == true);

            if(easySearchResult == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public static bool InsertionCheck()
        {
            foreach (SudokuPiece[] group in Sudoku.AllGroups)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (group[i].Value != "")
                    {
                        if(!group[i].IsFilled)
                        {
                            group[i].IsFilled = true;
                        }

                        for (int j = i + 1; j < 9; j++)
                        {
                            if (group[i].Value == group[j].Value)
                            {
                                return false;
                            }
                        }

                        group[i].UpdateGroups();
                    }

                }
            }

            return true;
        }


        private static bool? EasySearch()
        {
            bool? found = false;
            foreach(SudokuPiece[] group in Sudoku.AllGroups)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (!group[i].IsFilled)
                    {
                        if (group[i].PossibleNumbers.Count == 1)
                        {
                            group[i].InsertValue(group[i].PossibleNumbers.First());
                        }
                        else if (group[i].PossibleNumbers.Count == 0)
                        {
                            return null;
                        }
                    }
                }
            }

            return found;
        }


        private static bool MidSearch()
        {
            bool found = false;
            int[][] numberOccurencies = new int[9][];
            for(int i=0; i<9; i++)
            {
                numberOccurencies[i] = new int[]
                {
                    0, 0
                };
            }

            foreach (SudokuPiece[] group in Sudoku.AllGroups)
            {
                for (int i = 0; i < 9; i++)
                {
                    if (!group[i].IsFilled)
                    {
                        foreach (string number in group[i].PossibleNumbers)
                        {
                            numberOccurencies[int.Parse(number) - 1][0]++;
                            numberOccurencies[int.Parse(number) - 1][1] = i;
                        }
                    }
                }

                for (int i = 0; i < 9; i++)
                {
                    if (numberOccurencies[i][0] == 1)
                    {
                        group[numberOccurencies[i][1]].InsertValue((i + 1).ToString());
                        found = true;
                    }

                    numberOccurencies[i][0] = 0;
                }
            }

            return found;
        }

        private static bool HardSearch()
        {
            bool found = false;
            foreach(SudokuPiece[] group in Sudoku.AllGroups)
            {
                for(int i=0; i<9; i++)
                {
                    if(!group[i].IsFilled)
                    {
                        int piecesNeeded = group[i].PossibleNumbers.Count - 1;
                        int[] indexesOfEqualPieces = new int[piecesNeeded + 1];
                        indexesOfEqualPieces[piecesNeeded] = i;
                        for(int j=i+1; j<9; j++)
                        {
                            if(!group[j].IsFilled)
                            {
                                if (group[i].PossibleNumbers.SequenceEqual(group[j].PossibleNumbers))
                                {
                                    indexesOfEqualPieces[--piecesNeeded] = j;
                                }
                            }
                        }

                        if(piecesNeeded == 0)
                        {
                            string[] numbersToRemove = group[i].PossibleNumbers.ToArray();
                            for(int j=0; j<9; j++)
                            {
                                if(!indexesOfEqualPieces.Contains(j) && !group[j].IsFilled)
                                {
                                    foreach(string number in numbersToRemove)
                                    {
                                        if(group[j].PossibleNumbers.Remove(number))
                                        {
                                            found = true;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return found;
        }
    }
}