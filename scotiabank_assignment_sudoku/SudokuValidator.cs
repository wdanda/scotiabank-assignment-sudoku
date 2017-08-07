﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿﻿using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace scotiabank_assignment_sudoku
{
    public class SudokuValidator
    {
        private int gridSize;

        public SudokuValidator(int _gridSize)
        {
        	this.gridSize = _gridSize;
        }

        public void ValidateSolution(int[,] solution)
        {
            ValidateRows(solution);
            ValidateColumns(solution);
            ValidateBlocks(solution);
        }

        public void ValidateRows(int[,] solution)
        {
        	for(int i = 0; i < this.gridSize; i++)
        	{
        		var row = solution.GetRow(i);

                // validate numbers
                //
                if (!(row.Count(p => p < 1) == 0))
                {
                    throw new SudokuValidatorException(
                        String.Format("Row {0} contains invalid numbers (smaller than 1)", i+1));
                }

                if (!(row.Count(p => p > this.gridSize) == 0))
                {
                	throw new SudokuValidatorException(
                        String.Format("Row {0} contains invalid numbers (bigger than {1})", i+1, this.gridSize));
                }

        		// validate duplicates
                //
                if( !HasUniqueNumbers(row) ) 
                {
                	throw new SudokuValidatorException(
                        String.Format("Row {0} contains duplicated number => {1}", i+1, String.Join("", row)));
                }
        	}
        }

		public void ValidateColumns(int[,] solution)
        {
            for (int i = 0; i < this.gridSize; i++)
            {
                var col = solution.GetColumn(i);

                // validate duplicates
                //
                if( !HasUniqueNumbers(col) ) 
                {
                    throw new SudokuValidatorException(
                        String.Format("Column {0} contains duplicated number => {1}", i+1, String.Join("", col)));
                }

            }
		}

		public void ValidateBlocks(int[,] solution)
		{
            var blockSize = Convert.ToInt32(Math.Floor(Math.Sqrt(this.gridSize)));

			for(int i = 0; i < blockSize; i++)
			{
                for (int j = 0; j < blockSize; j++)
                {
                    // flatten numbers in block into an array
                    var block = new int[blockSize * blockSize];
                    var counter = 0;
                    for (int row = 0; row < blockSize; row++)
                    {
                        for (int col = 0; col < blockSize; col++)
                        {
                            block[counter++] = solution[i * blockSize + row, j * blockSize + col];
                        }
                    }

            		// check array for duplicates
                    //
                	if( !HasUniqueNumbers(block) ) 
                    {
                    	throw new SudokuValidatorException(
                            String.Format("Block ({0},{1}) contains duplicated number => {2}", i+1, j+1, String.Join("", block)));
                    }
                    
                }
            }

		}

		public bool HasUniqueNumbers(int[] row)
		{
			return row.Distinct().Count() == this.gridSize;
		}
	}

    public static class ArrayExt
    {
        public static T[] GetRow<T>(this T[,] array, int row)
        {
            if (!typeof(T).IsPrimitive)
                throw new InvalidOperationException("Not supported for managed types.");

            if (array == null)
                throw new ArgumentNullException("array");

            int cols = array.GetUpperBound(1) + 1;
            T[] result = new T[cols];
            int size = Marshal.SizeOf<T>();

            Buffer.BlockCopy(array, row*cols*size, result, 0, cols*size);

            return result;
        }

		public static T[] GetColumn<T>(this T[,] array, int col)
        {
            if (!typeof(T).IsPrimitive)
                throw new InvalidOperationException("Not supported for managed types.");

            if (array == null)
                throw new ArgumentNullException("array");
            
        	var rows = array.GetLength(1);
            T[] result = new T[rows];

            for(int i=0; i<rows; i++)
            {
            	result[i] = array[i, col];
            }

            return result;
        }
   }
}
