using System.Collections.Generic;
using System.Globalization;
using System;
using ProCalculator.ClassLibraries;

//math function
namespace MatrixExpression
{
    partial class Math
    {
        public static long GCD(long a, long b)
        {
            while (b != 0)
            {
                long temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
        public static long LCM(long x, long y)
        {
            long a;
            // a is greater number
            a = (x > y) ? x : y;

            while (true)
            {
                if (a % x == 0 && a % y == 0)
                    return a;
                ++a;
            }
        }
        public static long Fact(long n)
        {
            if (n == 0)
            {
                return 0;
            }
            long res = 1;
            for (int i = 1; i <= n; ++i)
            {
                res *= i;
            }
            return res;
        }
        public static double NOT(int n)
        {
            return n != 0 ? 0 : 1;
        }
        public static double BitwiseNegate(long n)
        {
            return ~n;
        }
        public static bool convertToBoolean(double d)
        {
            long i = (long)d;
            if (d != i)
            {
                return false;
            }
            if (i != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static long Poly(long a, long b)
        {
            long top = Fact(a), bot = Fact(a - b);
            return top / bot;
        }
        public static long Comb(long a, long b)
        {
            long top = Fact(a), bot = Fact(b) * Fact(a - b);
            return top / bot;
        }
    }
    public class MatrixMath
    {
        static public MyMatrix Add(MyMatrix a, MyMatrix b, ref bool succeeded, ref string errorMessage)
        {
            if (a == null || b == null)
            {
                succeeded = false;
                return null;
            }
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
        static public MyMatrix MultiplyWithMatrix(MyMatrix a, MyMatrix b, ref bool succeeded, ref string errorMessage)
        {
            if (a == null || b == null)
            {
                succeeded = false;
                return null;
            }
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
        static public MyMatrix MultiplyWithScalar(MyMatrix a, double scalar)
        {
            if (a == null)
            {
                return null;
            }
            MyMatrix result = new MyMatrix(a._row, a._col, 0);
            for (int i = 0; i < a._row; i++)
            {
                for (int j = 0; j < a._col; j++)
                {
                    result._matrix[i, j] = a._matrix[i, j] * scalar;
                }
            }
            return result;
        }
        static public MyMatrix Transpose(MyMatrix a)
        {
            if (a == null)
            {
                return null;
            }
            MyMatrix result = new MyMatrix(a._col, a._row, 0);
            for (int i = 0; i < a._row; i++)
            {
                for (int j = 0; j < a._col; j++)
                {
                    result._matrix[j, i] = a._matrix[i, j];
                }
            }
            return result;
        }
        static public MyMatrix Power(MyMatrix a, int power, ref bool succeeded, ref string errorMessage)
        {
            if (a == null)
            {
                succeeded = false;
                return null;
            }
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
        static public MyMatrix Inverse(MyMatrix a, ref bool succeeded, ref string errorMessage)
        {

            if (a._row != a._col)
            {
                succeeded = false;
                errorMessage = "MyMatrix is not square, cannot compute inverse.";
                return null; // Return null or handle error case accordingly
            }

            int n = a._row;
            MyMatrix identity = new MyMatrix(n, n, 0);
            for (int i = 0; i < n; i++)
            {
                identity._matrix[i, i] = 1;
            }

            MyMatrix augmentedMatrix = new MyMatrix(n, 2 * n, 0);

            // Augmenting the MyMatrix with identity MyMatrix
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    augmentedMatrix._matrix[i, j] = a._matrix[i, j];
                    augmentedMatrix._matrix[i, j + n] = identity._matrix[i, j];
                }
            }

            // Applying Gauss-Jordan elimination
            double det = Det(a, ref succeeded, ref errorMessage);
            if (det == 0)
            {
                succeeded = false;
                errorMessage = "The matrix is singular//Its determinant is 0, the matrix is not invertible.";
                return null; // Return null or handle error case accordingly
            }
            for (int i = 0; i < n; i++)
            {
                if (augmentedMatrix._matrix[i, i] == 0)
                {
                    succeeded = false;
                    errorMessage = "The matrix is singular//its Determinant is 0, the matrix is not invertible.";
                    return null; // Return null or handle error case accordingly
                }

                for (int j = 0; j < n; j++)
                {
                    if (i != j)
                    {
                        double ratio = augmentedMatrix._matrix[j, i] / augmentedMatrix._matrix[i, i];
                        for (int k = 0; k < 2 * n; k++)
                        {
                            augmentedMatrix._matrix[j, k] -= ratio * augmentedMatrix._matrix[i, k];
                        }
                    }
                }
            }

            // Scaling rows to make diagonal elements 1
            for (int i = 0; i < n; i++)
            {
                double div = augmentedMatrix._matrix[i, i];
                for (int j = 0; j < 2 * n; j++)
                {
                    augmentedMatrix._matrix[i, j] /= div;
                }
            }

            // Extracting the inverse MyMatrix
            MyMatrix inverseMatrix = new MyMatrix(n, n, 0);
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    inverseMatrix._matrix[i, j] = augmentedMatrix._matrix[i, j + n];
                }
            }

            succeeded = true;
            return inverseMatrix;
        }
        static public MyMatrix GetRowReducedEchelonForm(MyMatrix a)
        {
            if (a == null)
            {
                return null;
            }
            int lead = 0, rowCount = a._row, columnCount = a._col;

            MyMatrix result = new MyMatrix(a);
            for (int r = 0; r < rowCount; r++)
            {
                if (columnCount <= lead) break;
                int i = r;
                while (result._matrix[i, lead] == 0)
                {
                    i++;
                    if (i == rowCount)
                    {
                        i = r;
                        lead++;
                        if (columnCount == lead)
                        {
                            lead--;
                            break;
                        }
                    }
                }
                for (int j = 0; j < columnCount; j++)
                {
                    double temp = result._matrix[r, j];
                    result._matrix[r, j] = result._matrix[i, j];
                    result._matrix[i, j] = temp;
                }
                double div = result._matrix[r, lead];
                if (div != 0)
                    for (int j = 0; j < columnCount; j++) result._matrix[r, j] /= div;
                for (int j = 0; j < rowCount; j++)
                {
                    if (j != r)
                    {
                        double sub = result._matrix[j, lead];
                        for (int k = 0; k < columnCount; k++) result._matrix[j, k] -= (sub * result._matrix[r, k]);
                    }
                }
                lead++;
            }
            return result;
        }

        //correct
        static public double Det(MyMatrix a, ref bool succeeded, ref string errorMessage)
        {
            if (a == null)
            {
                succeeded = false;
                return 0;
            }
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

        static private double DeterminantRecursive(double[,] MyMatrix)
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

        static private double[,] CreateSubMatrix(double[,] MyMatrix, int excludingRow, int excludingCol)
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
    class MatrixExpression_Helper
    {
        static public void init()
        {
            FunctionHeaders = new List<string> {
                "sinh", "cosh", "tanh",

                "asin","acos","atan",

                "sin", "cos", "tan",

                "log10","ln", "sqrt",

                "abs",

                "GCD", "LCM", "log",

                "poly","comb",

                "Inv", "RREF", "Det", "Trans"
           };
            MatrixSymbols = new List<string> { "A", "B", "C", "D", "E", "F", "G", "H" };
            List<string> Prior1 = new List<string>()
            {
                "*", "/"
            };
            List<string> Prior2 = new List<string>()
            {
                "^",
            };
            List<string> Prior3 = new List<string>()
            {
                "+","-"
            };
            Operators = new List<string>[5];
            for (int i = 0; i < 5; i++)
            {
                Operators[i] = new List<string>();
            }
            Operators[0].AddRange(Prior1);
            Operators[1].AddRange(Prior2);
            Operators[2].AddRange(Prior3);

        }
        static public List<string> FunctionHeaders;
        static public List<string> MatrixSymbols;
        static public List<string>[] Operators;
        static public bool checkThereIsAPatternStartHere(string input, int pos, string pattern)
        {
            if (pos + pattern.Length > input.Length)
            {
                return false;
            }
            for (int i = 0; i < pattern.Length; i++)
            {
                if (input[i + pos] != pattern[i])
                {
                    return false;
                }
            }
            return true;
        }
        static public bool checkThereIsAPatternEndHere(string input, int pos, string pattern)
        {
            if (pos - (int)pattern.Length + 1 < 0)
            {
                return false;
            }
            if (!checkThereIsAPatternStartHere(input, pos - (int)pattern.Length + 1, pattern))
            {
                return false;
            }
            return true;
        }

        static public string getFunctionHeader(string input)
        {
            if (input.Length == 0)
            {
                return "NONE";
            }
            foreach (string content in FunctionHeaders)
            {
                if (checkThereIsAPatternStartHere(input, 0, content))
                {
                    int start = content.Length + 1;
                    int length = input.Length - content.Length - 2;
                    if (length <= 0 || start >= input.Length)
                    {
                        continue;
                    }
                    if (!checkValidBracket(input.Substring(start, length)))
                    {
                        continue;
                    }
                    return content;
                }
            }
            return "NONE";
        }
        static public int getNumberOfNumberNodes(string functionHeader)
        {
            if (functionHeader == "GCD" || functionHeader == "LCM" || functionHeader == "log" || functionHeader == "poly" || functionHeader == "comb")
            {
                return 2;
            }
            return 1;
        }
        static public bool peelOffExtraBracket(ref string input)
        {
            if (input.Length == 0)
            {
                return false;
            }
            if (!(input[0] == '(' && input[input.Length - 1] == ')'))
            {
                return false;
            }
            if (!checkValidBracket(input.Substring(1, input.Length - 2)))
            {
                return false;
            }
            input = input.Substring(1, input.Length - 2);
            return true;
        }
        static public bool isNumber(string input)
        {
            if (input.Length == 0)
            {
                return false;
            }
            if (input[0] == '-' || input[0] == '+')
            {
                input = input.Substring(1);
            }
            if (double.TryParse(input, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double result))
            {
                return true;
            }
            return false;
        }
        static public bool checkValidBracket(string input)
        {
            int valid = 0;
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '(')
                {
                    valid++;
                }
                else if (input[i] == ')')
                {
                    valid--;
                    if (valid < 0)
                    {
                        return false;
                    }
                }
            }
            return valid == 0;
        }

        //return start index of op if it appears in input, else return -1.
        static public int findOperatorNotEnclosed(string input, string op)
        {
            while (MatrixExpression_Helper.peelOffExtraBracket(ref input)) ;
            int i = 0;
            int outOfMatrixExpression = 0;

            while (i < input.Length)
            {
                if (checkThereIsAPatternStartHere(input, i, op))
                {
                    if (outOfMatrixExpression == 0)
                    {
                        return i;
                    }
                }
                if (input[i] == '(')
                {
                    outOfMatrixExpression++;
                }
                else if (input[i] == ')')
                {
                    outOfMatrixExpression--;
                }
                i++;
            }
            return -1;
        }
    };
    class MatrixStorage
    {
        static public MyMatrix[] matricesInStorage = null;
        static public void init(MyMatrix[] _m)
        {
            matricesInStorage = _m;
        }
    }
    //Node

}
//node
namespace MatrixExpression {
    abstract class BaseNode
    {
        public BaseNode left, right;
        public int numberOfChild;
        public BaseNode()
        {
            left = right = null;
            numberOfChild = 0;
        }
        public abstract Result computeResult(ref bool soFarSoGood, ref string errorMessage);


    };
    class NumberNode : BaseNode
    {
        public NumberNode(double val = 0)
        {
            numberValue = val;
            numberOfChild = 0;
        }
        override public Result computeResult(ref bool soFarSoGood, ref string errorMessage)
        {
            Result result = new Result();
            result.type = true;
            result.number_val = numberValue;
            return result;
        }

        public double numberValue;
    };
    class MatrixNode : BaseNode
    {
        public MatrixNode(int matrixIndex)
        {
            this.matrixIndex = matrixIndex;
            numberOfChild = 0;
        }
        override public Result computeResult(ref bool soFarSoGood, ref string errorMessage)
        {
            Result result = new Result();
            result.type = false;
            result.matrix_val = MatrixStorage.matricesInStorage[matrixIndex];
            return result;
        }

        public int matrixIndex;
    };
    class FunctionNode : BaseNode
    {
        public FunctionNode(string header = "NONE")
        {
            functionHeader = header;
            numberOfChild = 0;
        }
        override public Result computeResult(ref bool soFarSoGood, ref string errorMessage)
        {
            if (!soFarSoGood)
            {
                return null;
            }
            if (numberOfChild == 1)
            {
                Result result = left.computeResult(ref soFarSoGood, ref errorMessage);
                if (!soFarSoGood)
                {
                    return null;
                }
                if (result == null)
                {
                    soFarSoGood = false;
                    return null;
                }
                //number case
                if (functionHeader == "sin")
                {
                    if (result.type)
                    {
                        result.number_val = System.Math.Sin(result.number_val);
                    }
                    else
                    {
                        soFarSoGood = false;
                        errorMessage = "sin only work for number, not MyMatrix!\n";
                    }

                }
                else if (functionHeader == "cos")
                {
                    if (result.type)
                    {
                        result.number_val = System.Math.Cos(result.number_val);
                    }
                    else
                    {
                        soFarSoGood = false;
                        errorMessage = "cos only work for number, not MyMatrix!\n";
                    }
                }
                else if (functionHeader == "tan")
                {
                    if (result.type)
                    {
                        result.number_val = System.Math.Tan(result.number_val);
                    }
                    else
                    {
                        soFarSoGood = false;
                        errorMessage = "tan only work for number, not MyMatrix!\n";
                    }
                }
                else if (functionHeader == "asin")
                {
                    if (result.type)
                    {
                        result.number_val = System.Math.Asin(result.number_val);
                    }
                    else
                    {
                        soFarSoGood = false;
                        errorMessage = "asin only work for number, not MyMatrix!\n";
                    }
                }
                else if (functionHeader == "acos")
                {
                    if (result.type)
                    {
                        result.number_val = System.Math.Acos(result.number_val);
                    }
                    else
                    {
                        soFarSoGood = false;
                        errorMessage = "acos only work for number, not MyMatrix!\n";
                    }
                }
                else if (functionHeader == "atan")
                {
                    if (result.type)
                    {
                        result.number_val = System.Math.Atan(result.number_val);
                    }
                    else
                    {
                        soFarSoGood = false;
                        errorMessage = "atan only work for number, not MyMatrix!\n";
                    }
                }
                else if (functionHeader == "sinh")
                {
                    if (result.type)
                    {
                        result.number_val = System.Math.Sinh(result.number_val);
                    }
                    else
                    {
                        soFarSoGood = false;
                        errorMessage = "sinh only work for number, not MyMatrix!\n";
                    }
                }
                else if (functionHeader == "cosh")
                {
                    if (result.type)
                    {
                        result.number_val = System.Math.Cosh(result.number_val);
                    }
                    else
                    {
                        soFarSoGood = false;
                        errorMessage = "cosh only work for number, not MyMatrix!\n";
                    }
                }
                else if (functionHeader == "tanh")
                {
                    if (result.type)
                    {
                        result.number_val = System.Math.Tanh(result.number_val);
                    }
                    else
                    {
                        soFarSoGood = false;
                        errorMessage = "tanh only work for number, not MyMatrix!\n";
                    }
                }
                else if (functionHeader == "log10")
                {
                    if (result.type)
                    {
                        result.number_val = System.Math.Tan(result.number_val);
                    }
                    else
                    {
                        soFarSoGood = false;
                        errorMessage = "log10 only work for number, not MyMatrix!\n";
                    }
                }
                else if (functionHeader == "ln")
                {
                    if (result.type)
                    {
                        result.number_val = System.Math.Tan(result.number_val);
                    }
                    else
                    {
                        soFarSoGood = false;
                        errorMessage = "ln only work for number, not MyMatrix!\n";
                    }
                }
                else if (functionHeader == "abs")
                {
                    if (result.type)
                    {
                        result.number_val = System.Math.Tan(result.number_val);
                    }
                    else
                    {
                        soFarSoGood = false;
                        errorMessage = "abs only work for number, not MyMatrix!\n";
                    }
                }
                else if (functionHeader == "sqrt")
                {
                    if (result.type)
                    {
                        if (result.number_val < 0)
                        {
                            soFarSoGood = false;
                            errorMessage = "negative input for square root\n";
                        }
                        else
                        {
                            result.number_val = System.Math.Sqrt(result.number_val);
                        }
                    }
                    else
                    {
                        soFarSoGood = false;
                        errorMessage = "sqrt only work for number, not MyMatrix!\n";
                    }

                }

                //MyMatrix case
                if (functionHeader == "Inv")
                {
                    if (!result.type)
                    {
                        result.matrix_val = MatrixMath.Inverse(result.matrix_val, ref soFarSoGood, ref errorMessage);
                    }
                    else
                    {
                        soFarSoGood = false;
                        errorMessage = "Inverse is for MyMatrix only";
                    }

                }
                else if (functionHeader == "RREF")
                {
                    if (!result.type)
                    {
                        result.matrix_val = MatrixMath.GetRowReducedEchelonForm(result.matrix_val);
                        result.type = false;
                    }
                    else
                    {
                        soFarSoGood = false;
                        errorMessage = "Row echelon is for matrix only";
                    }

                }
                else if (functionHeader == "Det")
                {
                    if (!result.type)
                    {
                        result.number_val = MatrixMath.Det(result.matrix_val, ref soFarSoGood, ref errorMessage);
                        result.type = true;
                    }
                    else
                    {
                        soFarSoGood = false;
                        errorMessage = "Determina is for MyMatrix only";
                    }

                }
                else if (functionHeader == "Trans")
                {
                    if (!result.type)
                    {
                        result.matrix_val = MatrixMath.Transpose(result.matrix_val);
                    }
                    else
                    {
                        soFarSoGood = false;
                        errorMessage = "Transpose is for MyMatrix only";
                    }
                }
                if (soFarSoGood)
                {
                    return result;
                }
                else
                {
                    return null;
                }
            }
            else if (numberOfChild == 2)
            {
                Result result1 = left.computeResult(ref soFarSoGood, ref errorMessage);
                Result result2 = right.computeResult(ref soFarSoGood, ref errorMessage);
                if (!soFarSoGood)
                {
                    return null;
                }
                Result result = new Result();
                result.type = true;
                if (!result1.type || !result2.type)
                {
                    errorMessage = "MyMatrix version for log, GCD, LCM, poly not supported!\n";
                    soFarSoGood = false;
                    return null;
                }
                if (functionHeader == "log")
                {
                    result.number_val = System.Math.Log(result2.number_val, result1.number_val);
                }
                int iResult1 = (int)result1.number_val;
                int iResult2 = (int)result2.number_val;
                if (iResult1 != result1.number_val || iResult2 != result2.number_val)
                {
                    soFarSoGood = false;
                    errorMessage = "Incorrect input format for GCD/LCM/Poly/Comb\n";
                    return null;
                }
                if (functionHeader == "GCD")
                {
                    result.number_val = Math.GCD(iResult1, iResult2);
                }
                else if (functionHeader == "LCM")
                {
                    result.number_val = Math.LCM(iResult1, iResult2);
                }
                else if (functionHeader == "poly")
                {
                    if (iResult1 <= 0 || iResult2 <= 0 || iResult1 < iResult2)
                    {
                        soFarSoGood = false;
                        errorMessage = "Incorrect input for Polynonimal \n";
                    }
                    result.number_val = Math.Poly(iResult1, iResult2);
                }
                else if (functionHeader == "comb")
                {
                    if (iResult1 <= 0 || iResult2 <= 0 || iResult1 < iResult2)
                    {
                        soFarSoGood = false;
                        errorMessage = "Incorrect input for Combinational \n";
                    }
                    result.number_val = Math.Comb(iResult1, iResult2);
                }
            }
            soFarSoGood = false;
            errorMessage = "You shouldn't be here! \n";
            return null;

        }
        string functionHeader;
    };
    class OperatorNode : BaseNode
    {
        public OperatorNode(string c = "x")
        {
            oper = c;
        }
        override public Result computeResult(ref bool soFarSoGood, ref string errorMessage)
        {
            if (!soFarSoGood)
            {
                return null;
            }
            Result leftResult = left.computeResult(ref soFarSoGood, ref errorMessage);
            Result rightResult = right.computeResult(ref soFarSoGood, ref errorMessage);
            if (!soFarSoGood)
            {
                return null;
            }
            if (leftResult == null || rightResult == null)
            {
                soFarSoGood = false;
                return null;
            }
            //normal op
            if (oper == "+")
            {
                if (leftResult.type != rightResult.type)
                {
                    soFarSoGood = false;
                    errorMessage = "Operands type for + not match\n";
                    return null;
                }
                //check for num case 
                if (leftResult.type)
                {
                    Result result = new Result();
                    result.type = true;
                    result.number_val = leftResult.number_val + rightResult.number_val;
                    return result;

                }
                else
                {
                    Result result = new Result();
                    result.type = false;
                    result.matrix_val = MatrixMath.Add(leftResult.matrix_val, rightResult.matrix_val, ref soFarSoGood, ref errorMessage);
                    return result;
                }
            }
            if (oper == "*")
            {
                if (leftResult.type == rightResult.type)
                {
                    if (leftResult.type)
                    {
                        Result result = new Result();
                        result.type = true;
                        result.number_val = leftResult.number_val * rightResult.number_val;
                        return result;
                    }
                    else
                    {
                        Result result = new Result();
                        result.type = false;
                        result.matrix_val = MatrixMath.MultiplyWithMatrix(leftResult.matrix_val, rightResult.matrix_val,
                                                        ref soFarSoGood, ref errorMessage);
                        if (result.matrix_val == null)
                        {
                            return null;
                        }
                        else
                        {
                            return result;
                        }
                    }
                }
                //multiply a MyMatrix with a scalar
                if (leftResult.type != rightResult.type)
                {
                    //left is scalar
                    if (leftResult.type)
                    {
                        Result result = new Result();
                        result.type = false;
                        result.matrix_val = MatrixMath.MultiplyWithScalar(rightResult.matrix_val, leftResult.number_val);
                        return result;
                    }
                    else
                    {
                        Result result = new Result();
                        result.type = false;
                        result.matrix_val = MatrixMath.MultiplyWithScalar(leftResult.matrix_val, rightResult.number_val);
                        return result;
                    }

                }
            }
            if (oper == "^")
            {
                if (leftResult.type == false && rightResult.type == true)
                {
                    Result result = new Result();
                    result.type = false;
                    if ((int)rightResult.number_val != rightResult.number_val)
                    {
                        soFarSoGood = false;
                        errorMessage = "Floating power for MyMatrix not supported!";
                    }
                    result.matrix_val = MatrixMath.Power(leftResult.matrix_val, (int)rightResult.number_val, ref soFarSoGood, ref errorMessage);
                    return result;
                }
                else if (leftResult.type == rightResult.type)
                {
                    Result result = new Result();
                    result.type = true;
                    result.number_val = System.Math.Pow(leftResult.number_val, rightResult.number_val);
                    return result;
                }
                soFarSoGood = false;
                errorMessage = "Operand type for power not match\n";
                return null;
            }
            if (oper == "/")
            {
                if (leftResult.type == true && rightResult.type == true)
                {
                    Result result = new Result();
                    result.type = true;
                    result.number_val = leftResult.number_val / rightResult.number_val;
                    return result;
                }
                soFarSoGood = false;
                errorMessage = "Division is only for numbers\n";
                return null;
            }
            return null;

        }

        string oper;
    };

}
//expression and result
namespace MatrixExpression {
    class Result
    {
        //false: MyMatrix, 1: number
        public bool type;

        public double number_val;
        public MyMatrix matrix_val = null;
    }
    class MatrixExpression_BinaryTree
    {
        public MatrixExpression_BinaryTree()
        {
            errorMessage = string.Empty;
            root = null;
            builtSucceeded = true;
            computedSucceeded = true;
        }
        public void buildTreeFromMatrixExpression(string s)
        {
            root = null;
            errorMessage = string.Empty;
            builtSucceeded = true;
            computedSucceeded = true;
            buildTreeFromMatrixExpression_Helper(ref root, s);
        }
        public Result computeResult()
        {
            return root.computeResult(ref computedSucceeded, ref errorMessage);
        }

        public bool isEmpty()
        {
            return root == null;
        }
        public bool isOverallValid()
        {
            return builtSucceeded;
        }
        public bool isComputingValid()
        {
            return computedSucceeded;
        }
        public string getErrorMessage()
        {
            return errorMessage;
        }
        public void buildTreeFromMatrixExpression_Helper(ref BaseNode root, string s)
        {
            if (s.Length == 0)
            {
                builtSucceeded = false;
                errorMessage = "Can't identify MatrixExpression";
            }
            if (!builtSucceeded)
            {
                return;
            }
            Console.WriteLine("Working with: {0}", s);
            //remove extra bracket
            while (MatrixExpression_Helper.peelOffExtraBracket(ref s)) ;

            //check if s is a MyMatrix notation
            if (s[0] == '+')
            {
                s = s.Substring(1);
            }
            if (s.Length == 1)
            {
                if (MatrixExpression_Helper.MatrixSymbols.Contains(s))
                {
                    root = new MatrixNode(s[0] - 'A');
                    //Console.WriteLine("     MyMatrix : {0}", s[0]);
                    return;
                }
            }

            //check if s is a number;
            if (MatrixExpression_Helper.isNumber(s))
            {
                root = new NumberNode(double.Parse(s));
                //Console.WriteLine("     Number : {0}", s);
                return;
            }


            //check for function header
            string header = MatrixExpression_Helper.getFunctionHeader(s);
            if (header != "NONE")
            {
                root = new FunctionNode(header);
                ////Console.WriteLine("     Header: {0}", header);
                s = s.Substring(header.Length, s.Length - header.Length);
                while (MatrixExpression_Helper.peelOffExtraBracket(ref s)) ;
                root.numberOfChild = MatrixExpression_Helper.getNumberOfNumberNodes(header);
                if (root.numberOfChild == 1)
                {
                    buildTreeFromMatrixExpression_Helper(ref root.left, s);
                    return;
                }
                else
                {
                    string param1, param2;
                    int commaPos = MatrixExpression_Helper.findOperatorNotEnclosed(s, ",");

                    if (commaPos == -1)
                    {
                        //can't find enough param
                        builtSucceeded = false;
                        errorMessage = "Invalid use of Functions";
                        return;
                    }
                    else
                    {
                        param1 = s.Substring(0, commaPos);
                        param2 = s.Substring(commaPos + 1);
                        ////Console.WriteLine("     param1: {0}", param1);
                        ////Console.WriteLine("     param2: {0}", param2);
                        buildTreeFromMatrixExpression_Helper(ref root.left, param1);
                        buildTreeFromMatrixExpression_Helper(ref root.right, param2);
                        return;
                    }
                    //// dect ,
                }
            }
            int prior = MatrixExpression_Helper.Operators.Length - 1;
            for (int i = prior; i >= 0; i--)
            {
                foreach (string op_holder in MatrixExpression_Helper.Operators[i])
                {
                    string op = op_holder;
                    int startIndex = MatrixExpression_Helper.findOperatorNotEnclosed(s, op);
                    if (startIndex != -1)
                    {
                        root = new OperatorNode(op);
                        //Console.WriteLine("     Operator: {0}", op);
                        //left
                        buildTreeFromMatrixExpression_Helper(ref root.left, s.Substring(0, startIndex));
                        //right, renem to accout for op length
                        buildTreeFromMatrixExpression_Helper(ref root.right, s.Substring(startIndex + op.Length));
                        return;
                    }
                }
            }
            errorMessage = "Can't identify MatrixExpression";
            builtSucceeded = false;
        }


        private BaseNode root;
        private bool builtSucceeded;
        private bool computedSucceeded;
        private string errorMessage;
    };
    //Expression
    class MatrixExpression
    {
        public MatrixExpression()
        {
            convertingRules = new Dictionary<string, string>();
            eB = new MatrixExpression_BinaryTree();
            convertingRules.Add("-", "+(-1)*");
        }
        //built-in function supported

        //pre-process
        public void preProcess()
        {
            foreach (KeyValuePair<string, string> convert in convertingRules)
            {
                MatrixExpressionContent = MatrixExpressionContent.Replace(convert.Key, convert.Value);
            }
        }
        public bool checkOverallValidity()
        {
            string temp = MatrixExpressionContent;
            foreach (KeyValuePair<string, string> convert in convertingRules)
            {
                temp = temp.Replace(convert.Key, Convert.ToString(convert.Value));
            }
            if (!MatrixExpression_Helper.checkValidBracket(temp))
            {
                return false;
            }
            return eB.isOverallValid();
        }

        public void buildComputingTree()
        {
            eB.buildTreeFromMatrixExpression(MatrixExpressionContent);
        }
        public bool checkComputingValidity()
        {
            return eB.isComputingValid();
        }
        public string getErrorMessage()
        {
            return eB.getErrorMessage();
        }
        public Result computeResult()
        {
            if (eB.isEmpty())
            {
                //error log
                return null;
            }
            return eB.computeResult();
        }
        //get, set methods
        public void addConvertingRule(string from, string to)
        {
            convertingRules.Add(from, to);
        }

        //private
        //friend class MatrixExpression_BinaryTree;
        public string MatrixExpressionContent;
        MatrixExpression_BinaryTree eB;

        Dictionary<string, string> convertingRules;

    };

}
