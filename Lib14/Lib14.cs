using System;
namespace Lib_14
{
    public static class Calculation
    {
        public static int GetSumLessThan8(int[,] matrix)
        {
            if (matrix == null) return 0;

            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            int sum = 0;

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    if (matrix[i, j] < 8)
                    {
                        sum += matrix[i, j];
                    }
                }
            }

            return sum;
        }
    }
}