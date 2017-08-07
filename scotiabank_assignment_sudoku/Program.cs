﻿﻿﻿﻿using System;
using System.IO;

namespace scotiabank_assignment_sudoku
{
    class MainClass
    {
        private const int DEFAULT_GRID_SIZE = 9;
        private const int DEFAULT_BLOCK_SIZE = 3;

        public static void Main(string[] args)
        {
            var filename = AppContext.BaseDirectory + "input_sudoku.txt";
            if (args.Length > 0)
            {
                filename = args[0];
            }

			if (!File.Exists(filename)) 
            {
				throw new System.IO.FileNotFoundException();
            }

            Console.WriteLine("");
            try {
                var reader = new SudokuReader(filename, DEFAULT_GRID_SIZE);
                var solution = reader.LoadSolution();

                var validator = new SudokuValidator(DEFAULT_GRID_SIZE);

                validator.ValidateSolution(solution);
                Console.WriteLine("Congratulations, the solution is valid!");

            }
            catch(System.IO.FileNotFoundException)
            {
                Console.WriteLine("Error: " + filename + " not found");
            }
            catch (SudokuReaderException ex)
            {
                Console.WriteLine("Error in input file: " + ex.Message);
            }
            catch (SudokuValidatorException ex)
            {
                Console.WriteLine("Sudoku puzzle is invalid: " + ex.Message);
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.Read();

        }
    }
}
