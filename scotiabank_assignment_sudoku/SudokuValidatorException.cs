using System;
namespace scotiabank_assignment_sudoku
{
    public class SudokuValidatorException : Exception
    {
        public SudokuValidatorException() : base()
        {
        }

        public SudokuValidatorException(String message) : base(message) 
        {
        }

    }
}
