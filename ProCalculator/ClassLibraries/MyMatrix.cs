using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProCalculator.ClassLibraries
{
    public class MyMatrix
    {
        public double[,] _matrix;
        public int _row, _col;
        public MyMatrix(int row = 2, int col = 2, int initValue = 0)
        {
            _matrix = new double[row, col];
            _row = row;
            _col = col;
            for(int i = 0;i<row;i++)
            {
                for(int j = 0;j<col;j++)
                {
                    _matrix[i,j] = initValue;
                }
            }
        }
        public MyMatrix(MyMatrix matrix)
        {
            this._row = matrix._row;
            this._col = matrix._col;
            _matrix = new double[this._row, this._col];
            for (int i = 0; i < this._row; i++)
            {
                for (int j = 0; j < this._col; j++)
                {
                    this._matrix[i, j] = matrix._matrix[i, j];
                }
            }
        }
    }
    internal class Matrix_Helper
    {
        public MyMatrix Add(MyMatrix a, MyMatrix b, ref bool succeeded, ref string errorMessage)
        {
            if (a._row != b._row || a._col != b._col)
            {
                succeeded = false;
                errorMessage = "Matrices dimensions mismatch for addition.";
                return null; // Return null or handle error case accordingly
            }

            MyMatrix result = new MyMatrix(a._row, a._col, 0);
            for (int i = 0; i < a._row; i++)
            {
                for (int j = 0; j < a._col; j++)
                {
                    result._matrix[i, j] = a._matrix[i, j] + b._matrix[i, j];
                }
            }

            succeeded = true;
            return result;
        }

        public MyMatrix Subtract(MyMatrix a, MyMatrix b, ref bool succeeded, ref string errorMessage)
        {
            if (a._row != b._row || a._col != b._col)
            {
                succeeded = false;
                errorMessage = "Matrices dimensions mismatch for subtraction.";
                return null; // Return null or handle error case accordingly
            }

            MyMatrix result = new MyMatrix(a._row, a._col, 0);
            for (int i = 0; i < a._row; i++)
            {
                for (int j = 0; j < a._col; j++)
                {
                    result._matrix[i, j] = a._matrix[i, j] - b._matrix[i, j];
                }
            }

            succeeded = true;
            return result;
        }

        public MyMatrix MultiplyWithMatrix(MyMatrix a, MyMatrix b, ref bool succeeded, ref string errorMessage)
        {
            if (a._col != b._row)
            {
                succeeded = false;
                errorMessage = "Matrices dimensions mismatch for multiplication.";
                return null; // Return null or handle error case accordingly
            }

            MyMatrix result = new MyMatrix(a._row, b._col, 0);
            for (int i = 0; i < a._row; i++)
            {
                for (int j = 0; j < b._col; j++)
                {
                    for (int k = 0; k < a._col; k++)
                    {
                        result._matrix[i, j] += a._matrix[i, k] * b._matrix[k, j];
                    }
                }
            }

            succeeded = true;
            return result;
        }
        public MyMatrix MultiplyWithScalar(MyMatrix a, double scalar)
        {
            MyMatrix result = new MyMatrix(a._row, a._col, 0);
            for(int i = 0; i < a._row; i++)
            {
                for(int j = 0; j < a._col; j++)
                {
                    result._matrix[i,j] = a._matrix[i,j]*scalar;
                }
            }
            return result;
        }
        public MyMatrix Power(MyMatrix a, int power, ref bool succeeded, ref string errorMessage)
        {
            if (a._row != a._col)
            {
                succeeded = false;
                errorMessage = "MyMatrix is not square, power operation cannot be performed.";
                return null; // Return null or handle error case accordingly
            }

            if (power < 0)
            {
                succeeded = false;
                errorMessage = "Negative powers are not supported for matrices.";
                return null; // Return null or handle error case accordingly
            }

            MyMatrix result = new MyMatrix(a._row, a._col, 0);
            MyMatrix baseMatrix = a;

            if (power == 0)
            {
                for (int i = 0; i < a._row; i++)
                {
                    result._matrix[i, i] = 1; // Identity MyMatrix for power 0
                }
                succeeded = true;
                return result;
            }

            result = a;


            for (int p = 1; p < power; p++)
            {
                result = MultiplyWithMatrix(result, baseMatrix, ref succeeded, ref errorMessage);
                if (!succeeded)
                {
                    // Handle error from multiplication if needed
                    return null;
                }
            }

            succeeded = true;
            return result;
        }
        public MyMatrix Inverse(MyMatrix a, ref bool succeeded, ref string errorMessage)
        {
            if (a._row != a._col)
            {
                succeeded = false;
                errorMessage = "MyMatrix is not square, cannot compute inverse.";
                return null; // Return null or handle error case accordingly
            }

            double det = Det(a, ref succeeded, ref errorMessage);
            if (!succeeded || det == 0)
            {
                succeeded = false;
                errorMessage = "MyMatrix is singular, cannot compute inverse.";
                return null; // Return null or handle error case accordingly
            }

            int n = a._row;
            MyMatrix identity = new MyMatrix(n, n, 0);
            for (int i = 0; i < n; i++)
            {
                identity._matrix[i, i] = 1;
            }

            MyMatrix result = new MyMatrix(n, n, 0);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    result._matrix[i, j] = identity._matrix[i, j] / det;
                }
            }

            succeeded = true;
            return result;
        }

        public double Det(MyMatrix a, ref bool succeeded, ref string errorMessage)
        {
            if (a._row != a._col)
            {
                succeeded = false;
                errorMessage = "MyMatrix is not square, determinant cannot be computed.";
                return 0; // Return appropriate value for error case
            }

            int n = a._row;
            double det = 0;

            if (n == 1)
            {
                det = a._matrix[0, 0];
            }
            else if (n == 2)
            {
                det = a._matrix[0, 0] * a._matrix[1, 1] - a._matrix[0, 1] * a._matrix[1, 0];
            }
            else
            {
                det = DeterminantRecursive(a._matrix);
            }

            succeeded = true;
            return det;
        }

        private double DeterminantRecursive(double[,] MyMatrix)
        {
            int n = (int)System.Math.Sqrt(MyMatrix.Length);
            double det = 0;

            if (n == 2)
            {
                det = MyMatrix[0, 0] * MyMatrix[1, 1] - MyMatrix[0, 1] * MyMatrix[1, 0];
            }
            else
            {
                for (int i = 0; i < n; i++)
                {
                    det += System.Math.Pow(-1, i) * MyMatrix[0, i] * DeterminantRecursive(CreateSubMatrix(MyMatrix, 0, i));
                }
            }

            return det;
        }

        private double[,] CreateSubMatrix(double[,] MyMatrix, int excludingRow, int excludingCol)
        {
            int n = (int)System.Math.Sqrt(MyMatrix.Length);
            double[,] subMatrix = new double[n - 1, n - 1];
            int r = -1;

            for (int i = 0; i < n; i++)
            {
                if (i == excludingRow)
                    continue;

                r++;
                int c = -1;
                for (int j = 0; j < n; j++)
                {
                    if (j == excludingCol)
                        continue;

                    subMatrix[r, ++c] = MyMatrix[i, j];
                }
            }

            return subMatrix;
        }
    }
}

