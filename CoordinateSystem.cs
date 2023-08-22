using System;

namespace Traveling_CoordinateSystem
{
    public class CoordinateSystem
    {
        private double[,] points;
        private double[,] dm;

        public double[,] Points { get => points; set => points = value; }
        public double[,] DM { get => dm; set => dm = value; }

        public CoordinateSystem(int n, double width, double height)
        {
            Points = new double[n, 2];
            DM = new double[n, n];

            for (int i = 0; i < Points.GetLength(0); i++)
            {
                Random random = new Random();
                Points[i, 0] = Math.Round(random.NextDouble() * width, 1);
                Points[i, 1] = Math.Round(random.NextDouble() * height, 1);
            }
        }

        public void printPoints()
        {
            Console.Write("\n---------------------\n|Point|X_axis|Y_axis|\n---------------------");

            for (int i = 0; i < Points.GetLength(0); i++)
            {
                Console.Write("\n|");
                if (i < 10) Console.Write(" ");

                Console.Write(" {0}  | ", i);

                for (int j = 0; j < Points.GetLength(1); j++)
                    Console.Write(Points[i, j].ToString("F1").PadLeft(4) + " | ");

                Console.Write("\n---------------------");
            }
            Console.WriteLine();
        }

        public void createDM()
        {
            for (int i = 0; i < Points.GetLength(0); i++)
            {
                for (int j = 0; j < Points.GetLength(0); j++)
                    DM[i, j] = (i == j) ? 0.0 : calculateDistance(GetArrayByRowIndex(Points, i), GetArrayByRowIndex(points, j));
            }
        }

        private double calculateDistance(double[] p1, double[] p2)
        {
            return Math.Sqrt(Math.Pow(p2[0] - p1[0], 2) + Math.Pow(p2[1] - p1[1], 2));
        }

        private double[] GetArrayByRowIndex(double[,] matrix, int rowIndex)
        {
            int colCount = matrix.GetLength(1);
            double[] rowArray = new double[colCount];

            for (int j = 0; j < colCount; j++)
                rowArray[j] = matrix[rowIndex, j];

            return rowArray;
        }

        public void printDM()
        {
            Console.WriteLine("\n Distance Matrix (DM)\n**********************\n");

            for (int i = 0; i < 10 && i < DM.GetLength(1); i++)
                Console.Write("      " + i);

            for (int i = 10; i < DM.GetLength(1); i++)
                Console.Write("     " + i);

            Console.WriteLine();

            for (int i = 0; i < DM.GetLength(0); i++)
            {
                Console.WriteLine();

                if (i < 10 && i < DM.GetLength(0))
                    Console.Write(" ");

                Console.Write(i + "   ");

                for (int j = 0; j < DM.GetLength(1); j++)
                    Console.Write("{0:0.0}   ", DM[i, j]);

                Console.WriteLine();
            }
            Console.WriteLine();
        }

        public void traverse()
        {
            string[] rightOfOrigin = new string[Points.GetLength(0)];
            string[] passedPoints = new string[Points.GetLength(0)];

            for (int i = 0; i < rightOfOrigin.Length; i++) rightOfOrigin[i] = " ";

            printDM();

            for (int tour = 1; tour <= 10; tour++)
            {
                for (int i = 0; i < passedPoints.Length; i++) passedPoints[i] = " ";

                string path = "";

                int startIndex = originIndex(rightOfOrigin);
                rightOfOrigin[startIndex] = "X";

                path += Convert.ToString(startIndex) + " - ";
                passedPoints[startIndex] = "X";

                Console.Write("\n\n TOUR {0}\n********", tour);
                if (tour > 9) Console.Write("*");

                Console.WriteLine();

                double totalDistance = 0.0;

                int currentIndex = startIndex;

                for (int i = 0; i < Points.GetLength(0) - 1; i++)
                {
                    int index = nearestNeighborIndex(currentIndex, passedPoints);

                    path += Convert.ToString(index);
                    passedPoints[index] = "X";

                    if (i != Points.GetLength(0) - 2) path += " - ";

                    totalDistance += DM[currentIndex, index];

                    currentIndex = index;
                }

                Console.WriteLine("\nPath: " + path);
                Console.WriteLine("Total distance: {0:0.0}", totalDistance);
            }
        }

        private int originIndex(string[] rightOfOrigin)
        {
            int index = 0;

            Random random2 = new Random();
            while (rightOfOrigin[index] == "X") index = random2.Next(0, rightOfOrigin.Length);

            return index;
        }

        private int nearestNeighborIndex(int currentIndex, string[] passedPoints)
        {
            int index = 0; double shortestDistance = double.MaxValue;

            for (int i = 0; i < DM.GetLength(0); i++)
            {
                if (i != currentIndex && passedPoints[i] != "X" && DM[currentIndex, i] < shortestDistance)
                {
                    index = i;
                    shortestDistance = DM[currentIndex, i];
                }
            }

            return index;
        }
    }
}
