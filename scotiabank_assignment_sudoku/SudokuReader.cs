﻿using System;
using System.IO;
using System.Linq;

namespace scotiabank_assignment_sudoku
{
    public class SudokuReader
    {

        private string filename;
        private int gridSize;

        public SudokuReader(string _filename, int _gridSize)
        {
            this.filename = _filename;
            this.gridSize = _gridSize;
        }

        public int[,] LoadSolution()
        {
            int[,] data = new int[this.gridSize, this.gridSize];

            if (!File.Exists(this.filename))
            {
                throw new System.IO.FileNotFoundException();
            }

            var reader = File.OpenText(filename);

            var lineCounter = 0;
            var line = reader.ReadLine();

            while(line != null)
            {
                if (line == string.Empty)
                {
                    line = reader.ReadLine();
                    continue;
                }

                if (lineCounter >= gridSize)
                {
                    throw new SudokuReaderException("Invalid input -> more than "+this.gridSize+" lines found");
                }

                if (line.Length < this.gridSize)
                {
                    throw new SudokuReaderException("Invalid input -> less than "+this.gridSize+" numbers (per row) found");
                }

                if (line.Length > this.gridSize)
                {
                    throw new SudokuReaderException("Invalid input -> more than "+this.gridSize+" numbers (per row) found");
                }

                var colCounter = 0;
                foreach(var c in line.ToCharArray())
                {
                    if (!Char.IsNumber(c))
                    {
                        throw new SudokuReaderException("Invalid input -> must be numbers");
                    }
                    data[lineCounter, colCounter] = (int) Char.GetNumericValue(c);
                    colCounter++;
                }
                lineCounter++;

                line = reader.ReadLine();
            }

            if( lineCounter < this.gridSize )
            {
                throw new SudokuReaderException("Invalid input -> less than "+this.gridSize+" lines found");
            }

            return data;
        }


    }
}
