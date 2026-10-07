using System;
using System.IO;

namespace LibMas
{
    public static class Massiv
    {
        public static void InitMatrix(out int[,] matrix, int rows, int columns, int randMax)
        {
            Random rnd = new Random();
            matrix = new int[rows, columns];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    matrix[i, j] = rnd.Next(randMax);
                }
            }
        }

        public static void SaveMatrix(int[,] matrix, string filePath)
        {
            if (matrix == null)
            {
                return;
            }

            StreamWriter file = new StreamWriter(filePath);
            file.WriteLine(matrix.GetLength(0));
            file.WriteLine(matrix.GetLength(1));

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    file.WriteLine(matrix[i, j]);
                }
            }

            file.Close();
        }

        public static void LoadMatrix(out int[,] matrix, string filePath)
        {
            if (!File.Exists(filePath))
            {
                matrix = new int[0,0];
                return;
            }

            StreamReader file = new StreamReader(filePath);
            int.TryParse(file.ReadLine(), out int rows);
            int.TryParse(file.ReadLine(), out int cols);
            matrix = new int[rows, cols];

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    int.TryParse(file.ReadLine(), out matrix[i, j]);
                }
            }

            file.Close();
        }
    }
}