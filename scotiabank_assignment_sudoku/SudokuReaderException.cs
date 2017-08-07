using System;
namespace scotiabank_assignment_sudoku
{
    public class SudokuReaderException : Exception
    {
        public SudokuReaderException() : base()
        {
        }

        public SudokuReaderException(String message) : base(message) 
        {
        }

    }
}
