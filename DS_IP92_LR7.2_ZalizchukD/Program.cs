using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;

namespace AnotherLR72
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = "input.txt";
            int[,] mSmezh = ReadGraph(file);
            int[,] mPair = PairGraph(mSmezh);
            Console.WriteLine("Матрицы совершенных паросочетаний:");
            OutputPairs(mPair, 0);
        }

        // ======================== ФУНКЦИИ ========================
        
        // ====================== ЧТЕНИЕ ГРАФА ======================
        
        public static int[,] ReadGraph(string file)
        {
            StreamReader sr = new StreamReader(file);
            string read = sr.ReadLine();
            string[] temp = read.Split(' ');
            int m = Convert.ToInt32(temp[1]);
            int[,] vertexReading = new int[2, m];

            for (int i = 0; i < m; i++)
            {
                read = sr.ReadLine();
                temp = read.Split(' ');
                vertexReading[0, i] = Convert.ToInt32(temp[0]);
                vertexReading[1, i] = Convert.ToInt32(temp[1]);
            }

            int max1 = vertexReading[0, 0], max2 = vertexReading[1, 0];
            
            for (int i = 1; i < m; i++)
            {
                if (vertexReading[0, i] > max1)
                    max1 = vertexReading[0, i];
                if (vertexReading[1, i] > max2)
                    max2 = vertexReading[1, i];
            }
            
            int[,] mSmezh = new int[max1, max2];

            for (int i = 0; i < vertexReading.GetLength(1); i++)
            {
                mSmezh[vertexReading[0, i] - 1, vertexReading[1, i] - 1] = 1;
            }

            return mSmezh;
        }

        // ====================== ЗАМЕНА 0 НА -1, 1 НА 0 ======================
        
        public static int[,] PairGraph(int[,] mSmezh)
        {
            int[,] newM = mSmezh;
            for (int i = 0; i < newM.GetLength(0); i++)
            {
                for (int j = 0; j < newM.GetLength(1); j++)
                {
                    if (newM[i, j] == 0)
                        newM[i, j] = -1;
                    else newM[i, j] = 0;
                }
            }
            return newM;
        }

        // ====================== ВЫВОД МАТРИЦЫ ======================
        
        public static void OutputMatrix(int[,] matrix)
        {
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    if (matrix[i, j] == -1)
                        Console.Write("{0,4}", '*');
                    else
                        Console.Write("{0,4}", matrix[i, j]);
                }
                Console.WriteLine();
            }
        }

        // ====================== КОПИРОВАНИЕ МАТРИЦЫ ======================
        
        public static int[,] CopyArray(int[,] matrix)
        {
            int[,] newMatrix = new int[matrix.GetLength(0), matrix.GetLength(1)];
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                for (int j = 0; j < matrix.GetLength(1); j++)
                {
                    newMatrix[i, j] = matrix[i, j];
                }
            }

            return newMatrix;
        }

        // ====================== ПРОВЕРКА НА СОВЕРШЕННОСТЬ ПАРОСОЧЕТАНИЯ ======================
        
        public static void Check(int[,] matrix)
        {
            List<int> arr = new List<int>();
            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                int count = 0;
                for (int j = 0; j < matrix.GetLength(0); j++)
                {
                    if (matrix[j, i] == 1)
                        count++;
                }
                arr.Add(count);
            }

            int counter = 0;

            for (int i = 0; i < matrix.GetLength(0); i++)
            {
                if (arr[i] == 1)
                    counter++;
            }

            if (counter == matrix.GetLength(0))
            {
                OutputMatrix(matrix);
                Console.WriteLine();
                arr = new List<int>();
            }
            else
                arr = new List<int>();
        }
        
        // ====================== ВЫВОД СОВЕРШЕННЫХ ПАРОСОЧЕТАНИЙ ======================
        
        public static void OutputPairs(int[,] matrix, int number)
        {
            if (number >= matrix.GetLength(0))
            {
                Check(matrix);
                return;
            }

            for (int i = 0; i < matrix.GetLength(1); i++)
            {
                if (matrix[number, i] != -1)
                {
                    int[,] newM = CopyArray(matrix);
                    if (newM[number, i] != -1)
                    {
                        newM[number, i] = 1;
                        OutputPairs(newM, number + 1);
                    }
                }
            }
        }

    }
}