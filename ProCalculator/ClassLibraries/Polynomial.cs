using System.Collections.Generic;
using System.Globalization;
using System;

class Polynomial
{
    public static int Polynomial2Degree(double a, double b, double c, ref double x1, ref double x2)
    {
        double delta = b * b - 4 * a * c;
        if (delta < 0)
        {
            x1 = x2 = 0.0;
            return 0;
        }
        else if (delta == 0)
        {
            x1 = x2 = -b / (2 * a);
            return 1;
        }
        else
        {
            delta = System.Math.Sqrt(delta);
            x1 = (-b + delta) / (2 * a);
            x2 = (-b - delta) / (2 * a);
            return 2;
        }
    }

    public static int Polynomial3Degree(double a, double b, double c, double d, ref double x1, ref double x2, ref double x3)
    {
        double dt = System.Math.Pow(b, 2) - 3 * a * c;//delta=b*b-3*a*c
        double k = (9 * a * b * c - 2 * System.Math.Pow(b, 3) - 27 * System.Math.Pow(a, 2) * d) / (2 * System.Math.Sqrt(System.Math.Pow(System.Math.Abs(dt), 3)));
        if (dt > 0)
        {
            if (System.Math.Abs(k) <= 1)
            {
                x1 = (2 * System.Math.Sqrt(dt) * System.Math.Cos(System.Math.Acos(k) / 3) - b) / (3 * a);
                x2 = (2 * System.Math.Sqrt(dt) * System.Math.Cos(System.Math.Acos(k) / 3 - (2 * System.Math.PI / 3)) - b) / (3 * a);
                x3 = (2 * System.Math.Sqrt(dt) * System.Math.Cos(System.Math.Acos(k) / 3 + (2 * System.Math.PI / 3)) - b) / (3 * a);
                return 1;
                //3 nghiem phan bien
            }
            if (System.Math.Abs(k) > 1)
            {
                x1 = ((System.Math.Sqrt(dt) * System.Math.Abs(k)) / (3 * a * k)) * (System.Math.Pow((System.Math.Abs(k) + System.Math.Sqrt(System.Math.Pow(k, 2) - 1)), 1.0 / 3) + System.Math.Pow((System.Math.Abs(k) - System.Math.Sqrt(System.Math.Pow(k, 2) - 1)), 1.0 / 3)) - (b / (3 * a));
                return 2;
                //1 nghiem
            }
        }
        else if (dt == 0)
        {
            x1 = x2 = (-b - System.Math.Pow(-(System.Math.Pow(b, 3) - 27 * a * a * d), 1.0 / 3)) / (3 * a);//do ham pow khong dung doi so am nen ta phai doi dau.Ct goc:x=(-b+pow(pow(b,3)-27*a*a*d),1.0/3))/(3*a)
            return 3;
            //nghiem kep
        }
        else
        {
            x1 = (System.Math.Sqrt(System.Math.Abs(dt)) / (3 * a)) * (System.Math.Pow((k + System.Math.Sqrt(k * k + 1)), 1.0 / 3) - System.Math.Pow(-(k - System.Math.Sqrt(k * k + 1)), 1.0 / 3)) - (b / (3 * a));
            return 4;
            //1 nghiem
        }
        return 0;
    }

    public static List<int> Factor(int number)
    {
        var factors = new List<int>();
        int max = (int)System.Math.Sqrt(number);  // Round down

        for (int factor = 1; factor <= max; ++factor) // EquationSolving from 1 to the square root, or the int below it, inclusive.
        {
            if (number % factor == 0)
            {
                factors.Add(factor);
                factors.Add(-factor);
                if (factor != number / factor) // Get rid of duplicated root
                    factors.Add(number / factor);
            }
        }
        return factors;
    }
    /*public static int Polynomial4Degree(double a, double b, double c, double d, double e, ref double x1, ref double x2, ref double x3, ref double x4)
    {
        //Find all possible rational root
        List<int> Degree4Factors = Factor(Convert.ToInt32(a));
        List<int> ConstantFactors = Factor(Convert.ToInt32(e));
        List<int> PossibleRoots;
        //Find a root
        for(int i = 0; i < Degree4Factors.Count; i++)
        {

        }
        //Solve polynomial 3 degree
    }*/
}
class SimulEquation
{
    public static int SimulEquation2Unknows(double x1, double x2, double y1, double y2, double z1, double z2, ref double x, ref double y)
    {
        double D, Dx, Dy;
        D = x1 * y2 - x2 * y1;
        Dx = z1 * y2 - z2 * y1;
        Dy = x1 * z2 - x2 * z1;
        if (D == 0)
        {
            if (Dx + Dy == 0)
                return 0; //Vo nghiem
            else
                return 1; //Vo so nghiem
        }
        else
        {
            x = Dx / D;
            y = Dy / D;
            return 2;
        }
    }

    public static (int, double[,], double[]) GaussianElimination(int size, double[,] originalMatrix, double[] RHSMatrix)
    {
        //Entry of matrix
        for (int j = 0; j < size; j++)
        {
            if (originalMatrix[j, j] == 0)
            {
                double big = 0.0;
                int kRow = j;
                //Outer loop to search for a new pivot
                for (int k = j + 1; k < size; k++)
                {
                    //If a suitable element is found
                    if (System.Math.Abs(originalMatrix[k, j]) > big)
                    {
                        big = System.Math.Abs(originalMatrix[k, j]);
                        kRow = k;
                    }
                }

                //Swap rows j and kRow
                for (int l = j; l < size; l++)
                {
                    (originalMatrix[kRow, l], originalMatrix[j, l]) = (originalMatrix[j, l], originalMatrix[kRow, l]);
                }

                //Swap jth and kth row of the RHS vector
                (RHSMatrix[kRow], RHSMatrix[j]) = (RHSMatrix[j], RHSMatrix[kRow]);

                //Checking
                for (int z = 0; z < 3; z++)
                {
                    for (int x = 0; x < 3; x++)
                    {
                        Console.Write(originalMatrix[z, x] + " ");
                    }
                    Console.WriteLine("\n");
                }

                for (int c = 0; c < 3; c++)
                    Console.WriteLine(RHSMatrix[c] + " ");
            }


            double pivot = originalMatrix[j, j];
            //Vo nghiem
            if (pivot == 0)
            {
                if (RHSMatrix[size - 1] == 0)
                    return (0, originalMatrix, RHSMatrix);  //Infinite solution
                else
                    return (1, originalMatrix, RHSMatrix);  //No solution
            }

            //Next, reduce the rows
            for (int i = j + 1; i < size; i++)
            {
                double mult = originalMatrix[i, j] / pivot;
                if (mult >= 0)
                {
                    for (int l = j; l < size; l++)
                        originalMatrix[i, l] -= mult * originalMatrix[j, l];
                    RHSMatrix[i] -= mult * RHSMatrix[j];
                }
                else
                {
                    for (int l = j; l < size; l++)
                        originalMatrix[i, l] = originalMatrix[j, l] - (originalMatrix[i, l] / mult);
                    RHSMatrix[i] = RHSMatrix[j] - (RHSMatrix[i] / mult);
                }
            }

            //Checking
            for (int z = 0; z < 3; z++)
            {
                for (int x = 0; x < 3; x++)
                {
                    Console.Write(originalMatrix[z, x] + " ");
                }
                Console.WriteLine("\n");
            }

            for (int c = 0; c < 3; c++)
                Console.WriteLine(RHSMatrix[c] + " ");
        }
        return (2, originalMatrix, RHSMatrix);
    }

    public static double[] BackCalculation(int size, double[,] UpperTriangular, double[] vectorC)
    {
        double[] result = new double[size];
        for (int i = size - 1; i >= 0; i--)
        {
            double sum = 0;
            for (int j = i + 1; j < size; j++)
                sum += result[j] * UpperTriangular[i, j];
            result[i] = 1 / UpperTriangular[i, i] * (vectorC[i] - sum);
        }
        return result;
    }
}