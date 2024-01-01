using System.Collections.Generic;
using System.Globalization;
using System;

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
class Expression_Helper
{
    static public void init()
    {
        FunctionHeaders = new List<string> {
                "!",

                "sinh", "cosh", "tanh",

                "asin","acos","atan",

                "sin", "cos", "tan",

                "log10","ln", "sqrt",

                "abs", "fact",

                "GCD", "LCM", "log",

                "NOT","~",

                "poly","comb"
           };
        List<string> Prior1 = new List<string>()
            {
                "*", "/","^"
            };
        List<string> Prior2 = new List<string>()
            {
                "+",
            };
        List<string> Prior3 = new List<string>()
            {
                ">>", "<<"
            };
        List<string> Prior4 = new List<string>()
            {
                "AND", "OR", "NOT",
                "<=", ">=", "==", "!=","<", ">"
            };
        List<string> Prior5 = new List<string>()
            {
                "&", "|","$"
            };
        Operators = new List<string>[5];
        for (int i = 0; i < 5; i++)
        {
            Operators[i] = new List<string>();
        }
        Operators[0].AddRange(Prior1);
        Operators[1].AddRange(Prior2);
        Operators[2].AddRange(Prior3);
        Operators[3].AddRange(Prior4);
        Operators[4].AddRange(Prior5);

    }
    static public List<string> FunctionHeaders;
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
    static public int getNumberOfOperands(string functionHeader)
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
        while (Expression_Helper.peelOffExtraBracket(ref input)) ;
        int i = 0;
        int outOfExpression = 0;

        while (i < input.Length)
        {
            if (checkThereIsAPatternStartHere(input, i, op))
            {
                if (outOfExpression == 0)
                {
                    return i;
                }
            }
            if (input[i] == '(')
            {
                outOfExpression++;
            }
            else if (input[i] == ')')
            {
                outOfExpression--;
            }
            i++;
        }
        return -1;
    }
};
//Node
abstract class BaseNode
{
    public BaseNode left, right;
    public int numberOfChild;
    public BaseNode()
    {
        left = right = null;
        numberOfChild = 0;
    }
    public abstract double computeResult(Dictionary<char, double> variablePair, ref bool soFarSoGood, bool unitForTrigonometryFunction);


};
class NumberNode : BaseNode
{
    public NumberNode(double val = 0)
    {
        data = val;
        numberOfChild = 0;
    }
    override public double computeResult(Dictionary<char, double> variablePair, ref bool soFarSoGood, bool unitForTrigonometryFunction)
    {
        return data;
    }
    double data;
};
class VariableNode : BaseNode
{
    public VariableNode(char c = 'x')
    {
        variable = c;
        numberOfChild = 0;
    }
    override public double computeResult(Dictionary<char, double> variablePair, ref bool soFarSoGood, bool unitForTrigonometryFunction)
    {
        if (variablePair.ContainsKey(variable))
        {
            return variablePair[variable];
        }
        //log error
        return -1;
    }
    char variable;
};
class FunctionNode : BaseNode
{
    public FunctionNode(string header = "NONE")
    {
        functionHeader = header;
        numberOfChild = 0;
    }
    override public double computeResult(Dictionary<char, double> variablePair, ref bool soFarSoGood, bool unitForTrigonometryFunction)
    {
        if (numberOfChild == 1)
        {
            double result = left.computeResult(variablePair, ref soFarSoGood, unitForTrigonometryFunction);
            double resultInRadian = result * (System.Math.PI / 180);
            if (functionHeader == "sin")
            {
                if (!unitForTrigonometryFunction)
                {
                    return System.Math.Sin(resultInRadian);
                }
                else
                {
                    return System.Math.Sin(result);
                }
            }
            else if (functionHeader == "cos")
            {
                if (!unitForTrigonometryFunction)
                {
                    return System.Math.Cos(resultInRadian);
                }
                else
                {
                    return System.Math.Cos(result);
                }
            }
            else if (functionHeader == "tan")
            {
                if (!unitForTrigonometryFunction)
                {
                    return System.Math.Tan(resultInRadian);
                }
                else
                {
                    return System.Math.Tan(result);
                }
            }
            else if (functionHeader == "asin")
            {
                if (!unitForTrigonometryFunction)
                {
                    return System.Math.Asin(result) * (System.Math.PI / 180);
                }
                else
                {
                    return System.Math.Asin(result);
                }
            }
            else if (functionHeader == "acos")
            {
                if (!unitForTrigonometryFunction)
                {
                    return System.Math.Acos(result) * (System.Math.PI / 180);
                }
                else
                {
                    return System.Math.Acos(result);
                }
            }
            else if (functionHeader == "atan")
            {
                if (!unitForTrigonometryFunction)
                {
                    return System.Math.Atan(result) * (System.Math.PI / 180);
                }
                else
                {
                    return System.Math.Atan(result);
                }
            }
            else if (functionHeader == "sinh") return System.Math.Sinh(result);
            else if (functionHeader == "cosh") return System.Math.Cosh(result);
            else if (functionHeader == "tanh") return System.Math.Tanh(result);

            else if (functionHeader == "log10") return System.Math.Log10(result);
            else if (functionHeader == "ln") return System.Math.Log(result);
            else if (functionHeader == "sqrt")
            {
                if (result < 0)
                {
                    soFarSoGood = false;
                }
                else
                {
                    return System.Math.Sqrt(result);
                }

            }
            else if (functionHeader == "abs") return System.Math.Abs(result);


            int iResult = (int)result;
            if (iResult != result)
            {
                soFarSoGood = false;
                return -1;
            }

            else if (functionHeader == "NOT") return Math.NOT(iResult);
            else if (functionHeader == "~") return Math.BitwiseNegate(iResult);
            else if (functionHeader == "fact"|| functionHeader == "!") return Math.Fact(iResult);
        }
        else if (numberOfChild == 2)
        {
            double result1 = left.computeResult(variablePair, ref soFarSoGood, unitForTrigonometryFunction);
            double result2 = right.computeResult(variablePair, ref soFarSoGood, unitForTrigonometryFunction);
            if (functionHeader == "log") return System.Math.Log(result2, result1);

            int iResult1 = (int)result1;
            int iResult2 = (int)result2;
            if (iResult1 != result1 || iResult2 != result2)
            {
                soFarSoGood = false;
                return -1;
            }
            if (functionHeader == "GCD")
            {
                return Math.GCD(iResult1, iResult2);
            }
            else if (functionHeader == "LCM")
            {
                if (iResult1 != result1 || (int)result2 != result2)
                {
                    soFarSoGood = false;
                }
                else
                {
                    return Math.LCM(iResult1, iResult2);
                }
            }
            else if (functionHeader == "poly")
            {
                if (iResult1 <= 0 || iResult2 <= 0 || iResult1 < iResult2)
                {
                    soFarSoGood = false;
                    return -1;
                }
                return Math.Poly(iResult1, iResult2);
            }
            else if (functionHeader == "comb")
            {
                if (iResult1 <= 0 || iResult2 <= 0 || iResult1 < iResult2)
                {
                    soFarSoGood = false;
                    return -1;
                }
                return Math.Comb(iResult1, iResult2);
            }
        }
        soFarSoGood = false;
        return -1;

    }
    string functionHeader;
};
class OperatorNode : BaseNode
{
    public OperatorNode(string c = "x")
    {
        oper = c;
    }
    override public double computeResult(Dictionary<char, double> variablePair, ref bool soFarSoGood, bool unitForTrigonometryFunction)
    {
        double leftResult = left.computeResult(variablePair, ref soFarSoGood, unitForTrigonometryFunction);
        double rightResult = right.computeResult(variablePair, ref soFarSoGood, unitForTrigonometryFunction);
        //normal op
        if (oper == "+")
        {
            return leftResult + rightResult;
        }
        if (oper == "-")
        {
            return leftResult - rightResult;
        }
        if (oper == "*")
        {
            return leftResult * rightResult;
        }
        if (oper == "/")
        {
            return leftResult / rightResult;
        }
        if (oper == "^")
        {
            return System.Math.Pow(leftResult, rightResult);
        }

        //compare op
        if (oper == ">")
        {
            return Convert.ToDouble(leftResult > rightResult);
        }
        if (oper == "<")
        {
            return Convert.ToDouble(leftResult < rightResult);
        }
        if (oper == ">=")
        {
            return Convert.ToDouble(leftResult >= rightResult);
        }
        if (oper == "<=")
        {
            return Convert.ToDouble(leftResult <= rightResult);
        }
        if (oper == "==")
        {
            return Convert.ToDouble(leftResult == rightResult);
        }
        if (oper == "!=")
        {
            return Convert.ToDouble(leftResult != rightResult);
        }

        //logic op
        bool bLeft = Math.convertToBoolean(leftResult);
        bool bRight = Math.convertToBoolean(rightResult);

        if (oper == "AND")
        {
            return Convert.ToDouble(bLeft && bRight);
        }
        if (oper == "OR")
        {
            return Convert.ToDouble(bLeft || bRight);
        }

        //bitwise op
        int iLeft = (int)leftResult, iRight = (int)rightResult;
        if (iLeft != leftResult || iRight != rightResult)
        {
            soFarSoGood = false;
            return -1;
        }
        if (oper == ">>")
        {
            return iLeft >> iRight;
        }
        if (oper == "<<")
        {
            return iLeft << iRight;
        }
        if (oper == "|")
        {
            return iLeft | iRight;
        }
        if (oper == "&")
        {
            return iLeft & iRight;
        }
        if (oper == "$")
        {
            return iLeft ^ iRight;
        }
        return -1;

    }

    string oper;
};
class Expression_BinaryTree
{
    public Expression_BinaryTree()
    {
        root = null;
        builtSucceeded = true;
        computedSucceeded = true;
    }
    public void buildTreeFromExpression(string s, Dictionary<char, double> variablePair)
    {
        buildTreeFromExpression_Helper(ref root, s, variablePair);
    }
    public double computeResult(Dictionary<char, double> variablePair, bool unitForTrigonometryFunction)
    {
        return root.computeResult(variablePair, ref computedSucceeded, unitForTrigonometryFunction);
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
    public void buildTreeFromExpression_Helper(ref BaseNode root, string s, Dictionary<char, double> variablePairs)
    {
        if (s.Length == 0)
        {
            builtSucceeded = false;
            errorMessage = "Can't identify Expression";
        }
        if (!builtSucceeded)
        {
            return;
        }
        //Console.WriteLine("Working with: {0}",s);
        //remove extra bracket
        while (Expression_Helper.peelOffExtraBracket(ref s)) ;

        //check if s is a number;
        if (Expression_Helper.isNumber(s))
        {
            root = new NumberNode(double.Parse(s));
            return;
        }
        //check if s is a variable
        if (s.Length == 1)
        {
            if (variablePairs.ContainsKey(s[0]))
            {
                root = new VariableNode(s[0]);
                return;
            }
        }
        //check for function header
        //special case for !
        if (s[s.Length - 1] == '!')
        {
            root = new FunctionNode("!");
            s = s.Substring(0,s.Length - 1);
            while (Expression_Helper.peelOffExtraBracket(ref s)) ;
            root.numberOfChild = 1;
            buildTreeFromExpression_Helper(ref root.left, s, variablePairs);
            return;
        }
        string header = Expression_Helper.getFunctionHeader(s);
        if (header != "NONE")
        {
            root = new FunctionNode(header);
            //Console.WriteLine("     Header: {0}", header);
            s = s.Substring(header.Length, s.Length - header.Length);
            while (Expression_Helper.peelOffExtraBracket(ref s)) ;
            root.numberOfChild = Expression_Helper.getNumberOfOperands(header);
            if (root.numberOfChild == 1)
            {
                buildTreeFromExpression_Helper(ref root.left, s, variablePairs);
                return;
            }
            else
            {
                string param1, param2;
                int commaPos = Expression_Helper.findOperatorNotEnclosed(s, ",");

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
                    //Console.WriteLine("     param1: {0}", param1);
                    //Console.WriteLine("     param2: {0}", param2);
                    buildTreeFromExpression_Helper(ref root.left, param1, variablePairs);
                    buildTreeFromExpression_Helper(ref root.right, param2, variablePairs);
                    return;
                }
                //// dect ,
            }
        }
        int prior = Expression_Helper.Operators.Length - 1;
        for (int i = prior; i >= 0; i--)
        {
            foreach (string op_holder in Expression_Helper.Operators[i])
            {
                string op = op_holder;
                int startIndex = Expression_Helper.findOperatorNotEnclosed(s, op);
                if (startIndex != -1)
                {
                    if (op == ">" && Expression_Helper.checkThereIsAPatternStartHere(s, startIndex, ">>"))
                    {
                        op = ">>";
                    }
                    root = new OperatorNode(op);
                    //Console.WriteLine("     Operator: {0}", op);
                    //left
                    if (op == "+" && startIndex == 0)
                    {
                        root.left = new NumberNode(0);
                    }
                    else
                    {
                        buildTreeFromExpression_Helper(ref root.left, s.Substring(0, startIndex), variablePairs);
                    }
                    //right, renem to accout for op length
                    buildTreeFromExpression_Helper(ref root.right, s.Substring(startIndex + op.Length), variablePairs);
                    return;
                }
            }
        }
        errorMessage = "Can't identify expression";
        builtSucceeded = false;
    }

    private BaseNode root;
    private bool builtSucceeded;
    private bool computedSucceeded;
    private string errorMessage;
};

//Expression
class Expression
{
    public Expression()
    {
        convertingRules = new Dictionary<string, string>();
        variablePairs = new Dictionary<char, double>();
        eB = new Expression_BinaryTree();
        convertingRules.Add("e", "2.718281828");
        convertingRules.Add("pi", "3.141592654");
        convertingRules.Add("π", "3.141592654");
        convertingRules.Add("√", "sqrt");
        convertingRules.Add("log", "log10");
        convertingRules.Add("-", "+(-1)*");
    }
    //built-in function supported

    //pre-process
    public void preProcess()
    {
        foreach (KeyValuePair<string, string> convert in convertingRules)
        {
            expression = expression.Replace(convert.Key, convert.Value);
        }
    }
    public bool checkOverallValidity()
    {
        string temp = expression;
        foreach (KeyValuePair<string, string> convert in convertingRules)
        {
            temp = temp.Replace(convert.Key, Convert.ToString(convert.Value));
        }
        if (!Expression_Helper.checkValidBracket(temp))
        {
            return false;
        }
        return eB.isOverallValid();
    }

    public void buildComputingTree()
    {
        eB.buildTreeFromExpression(getStringContent(), variablePairs);
    }
    //debug
    //calculate result
    public void makeVariable(char c)
    {
        variablePairs.Add(c, 0);
    }
    public void unMakeVariable(char c)
    {
        variablePairs.Remove(c);
    }
    public void setVariableValue(char c, double value)
    {
        variablePairs[c] = value;
    }
    public bool checkComputingValidity()
    {
        return eB.isComputingValid();
    }
    public string getErrorMessage()
    {
        return eB.getErrorMessage();
    }
    public double computeResult()
    {
        if (eB.isEmpty())
        {
            //error log
            return -1;
        }
        return eB.computeResult(variablePairs, unitForTrigonometryFunction);
    }

    //get, set methods
    public void addConvertingRule(string from, string to)
    {
        convertingRules.Add(from, to);
    }
    public string getStringContent()
    {
        return expression;
    }
    public void setContent(string content)
    {
        expression = content;
    }


    //private
    //friend class Expression_BinaryTree;
    public bool unitForTrigonometryFunction = false;
    public string expression;
    Expression_BinaryTree eB;

    Dictionary<string, string> convertingRules;
    Dictionary<char, double> variablePairs;

};