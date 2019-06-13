using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SudokuSolver
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            #region initializing Sudoku
            Sudoku.Initialize(new SudokuPiece[]
            {
                new SudokuPiece(textBox1),
                new SudokuPiece(textBox2),
                new SudokuPiece(textBox3),
                new SudokuPiece(textBox4),
                new SudokuPiece(textBox5),
                new SudokuPiece(textBox6),
                new SudokuPiece(textBox7),
                new SudokuPiece(textBox8),
                new SudokuPiece(textBox9),
                new SudokuPiece(textBox10),
                new SudokuPiece(textBox11),
                new SudokuPiece(textBox12),
                new SudokuPiece(textBox13),
                new SudokuPiece(textBox14),
                new SudokuPiece(textBox15),
                new SudokuPiece(textBox16),
                new SudokuPiece(textBox17),
                new SudokuPiece(textBox18),
                new SudokuPiece(textBox19),
                new SudokuPiece(textBox20),
                new SudokuPiece(textBox21),
                new SudokuPiece(textBox22),
                new SudokuPiece(textBox23),
                new SudokuPiece(textBox24),
                new SudokuPiece(textBox25),
                new SudokuPiece(textBox26),
                new SudokuPiece(textBox27),
                new SudokuPiece(textBox28),
                new SudokuPiece(textBox29),
                new SudokuPiece(textBox30),
                new SudokuPiece(textBox31),
                new SudokuPiece(textBox32),
                new SudokuPiece(textBox33),
                new SudokuPiece(textBox34),
                new SudokuPiece(textBox35),
                new SudokuPiece(textBox36),
                new SudokuPiece(textBox37),
                new SudokuPiece(textBox38),
                new SudokuPiece(textBox39),
                new SudokuPiece(textBox40),
                new SudokuPiece(textBox41),
                new SudokuPiece(textBox42),
                new SudokuPiece(textBox43),
                new SudokuPiece(textBox44),
                new SudokuPiece(textBox45),
                new SudokuPiece(textBox46),
                new SudokuPiece(textBox47),
                new SudokuPiece(textBox48),
                new SudokuPiece(textBox49),
                new SudokuPiece(textBox50),
                new SudokuPiece(textBox51),
                new SudokuPiece(textBox52),
                new SudokuPiece(textBox53),
                new SudokuPiece(textBox54),
                new SudokuPiece(textBox55),
                new SudokuPiece(textBox56),
                new SudokuPiece(textBox57),
                new SudokuPiece(textBox58),
                new SudokuPiece(textBox59),
                new SudokuPiece(textBox60),
                new SudokuPiece(textBox61),
                new SudokuPiece(textBox62),
                new SudokuPiece(textBox63),
                new SudokuPiece(textBox64),
                new SudokuPiece(textBox65),
                new SudokuPiece(textBox66),
                new SudokuPiece(textBox67),
                new SudokuPiece(textBox68),
                new SudokuPiece(textBox69),
                new SudokuPiece(textBox70),
                new SudokuPiece(textBox71),
                new SudokuPiece(textBox72),
                new SudokuPiece(textBox73),
                new SudokuPiece(textBox74),
                new SudokuPiece(textBox75),
                new SudokuPiece(textBox76),
                new SudokuPiece(textBox77),
                new SudokuPiece(textBox78),
                new SudokuPiece(textBox79),
                new SudokuPiece(textBox80),
                new SudokuPiece(textBox81)
            });
            #endregion
        }

        bool skipTextChangedEvent = false;

        private void SolveButton_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch stopWatch = new Stopwatch();
            stopWatch.Start();
            bool solveResult = SearchSystem.StartSolve();
            stopWatch.Stop();

            if (solveResult)
            {
                MessageBox.Show("Sudoku solved in " + stopWatch.Elapsed.ToString());
            }
            else
            {
                MessageBox.Show("Sorry, this sodoku is unsolvable :(");
            }
        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            foreach(SudokuPiece piece in Sudoku.Pieces)
            {
                piece.ClearText();
            }
        }

        private void TextBox_GotMouseCapture(object sender, MouseEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox curTextBox = sender as TextBox;

            if (skipTextChangedEvent)
            {
                skipTextChangedEvent = false;
            }
            else if (curTextBox.Text == "")
            {
            }
            else if(curTextBox.Text[0]>'0' && curTextBox.Text[0]<='9')
            {
                curTextBox.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
            }
            else
            {
                skipTextChangedEvent = true;
                curTextBox.Text = "";
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            ((TextBox)sender).SelectAll();
        }
    }
}
