using System;
using System.IO;
using System.Collections.Generic;
using System.Xml;

namespace DS_IP92_LR7._2_ZalizchukD
{
    class Program
    {
        static void Main(string[] args)
        {
            string file = "input.txt";
            Graph graph = new Graph(file);
            graph.Output();
        }
    }

    class Graph
    {
        private int[,] mSmezh;
        private int[,] pairs;
        private int m;

        public Graph(string file)
        {
            StreamReader sr = new StreamReader(file);
            string read = sr.ReadLine();
            string[] temp = read.Split(' ');
            m = Convert.ToInt32(temp[1]);
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
            
            mSmezh = new int[max1, max2];

            for (int i = 0; i < vertexReading.GetLength(1); i++)
            {
                mSmezh[vertexReading[0, i] - 1, vertexReading[1, i] - 1] = 1;
            }

        }

        public void Output()
        {
            for (int i = 0; i < mSmezh.GetLength(0); i++)
            {
                for (int j = 0; j < mSmezh.GetLength(1); j++)
                {
                    Console.Write("{0,4}", mSmezh[i, j]);
                }
                Console.WriteLine();
            }
        }
        
    }
    
}