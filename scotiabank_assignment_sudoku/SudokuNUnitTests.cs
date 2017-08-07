﻿using NUnit.Framework;
using System;

namespace scotiabank_assignment_sudoku
{
    [TestFixture()]
    public class SudokuNUnitTests
    {
		private SudokuValidator validator = new SudokuValidator(9);

		protected static int[,] validPuzzle = new int[,]
		{
			{ 5,3,4,6,7,8,9,1,2 },
			{ 6,7,2,1,9,5,3,4,8 },
			{ 1,9,8,3,4,2,5,6,7 },
			{ 8,5,9,7,6,1,4,2,3 },
			{ 4,2,6,8,5,3,7,9,1 },
			{ 7,1,3,9,2,4,8,5,6 },
			{ 9,6,1,5,3,7,2,8,4 },
			{ 2,8,7,4,1,9,6,3,5 },
			{ 3,4,5,2,8,6,1,7,9 }
		};

		[Test()]
        public void should_validate_correct_puzzle() 
        {
            Assert.DoesNotThrow(() =>
                validator.ValidateSolution(validPuzzle)
            );
        }

        [Test()]
        public void should_throw_exception_for_invalid_puzzle() 
        {
            var puzzle = validPuzzle.Clone() as int[,];
            puzzle[0, 0] = 1;

            Assert.Throws<SudokuValidatorException>(() =>
                validator.ValidateSolution(puzzle)
            );
        }

		[Test()]
		public void should_throw_exception_due_to_duplicate_number_in_row()
		{
			var puzzle = validPuzzle.Clone() as int[,];
			puzzle[0, 0] = 1;
			puzzle[0, 1] = 1;

			Assert.Throws<SudokuValidatorException>(() =>
                validator.ValidateRows(puzzle)
			);
		}

        [Test()]
        public void should_throw_exception_due_to_duplicate_number_in_column()
        {
            var puzzle = validPuzzle.Clone() as int[,];
            puzzle[0, 0] = 1;
            puzzle[1, 0] = 1;

            Assert.Throws<SudokuValidatorException>(() =>
                validator.ValidateColumns(puzzle)
            );
        }

		[Test()]
		public void should_throw_exception_due_to_duplicate_number_in_block()
		{
			var puzzle = validPuzzle.Clone() as int[,];
			puzzle[0, 0] = 1;
			puzzle[1, 1] = 1;

			Assert.Throws<SudokuValidatorException>(() =>
                validator.ValidateBlocks(puzzle)
			);
		}
		
        [Test()]
        public void should_detect_duplicate_numbers()
        {
            var row = new int[] { 1, 1, 3, 4, 5, 6, 7, 8, 9 };
            Assert.IsFalse(validator.HasUniqueNumbers(row));
        }
    
        [Test()]
        public void should_validate_no_duplicate()
        {
            var row = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            Assert.IsTrue(validator.HasUniqueNumbers(row));
        }

	}
}
