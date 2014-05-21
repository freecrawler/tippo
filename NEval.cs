namespace client
{
    using System;
    using System.Collections;
    using System.Runtime.InteropServices;

    public class NEval
    {
        private Random random_0;

        public double Eval(string string_0)
        {
            try
            {
                string str = string_0.ToLower().Trim().Replace(" ", string.Empty);
                return this.method_0(str);
            }
            catch
            {
                return 0.0;
            }
        }

        private double method_0(string string_0)
        {
            Stack stack = this.method_2(string_0);
            Stack stack2 = new Stack();
            while (stack.Count > 0)
            {
                stack2.Push(stack.Pop());
            }
            Stack stack3 = new Stack();
            while (stack2.Count > 0)
            {
                string str = stack2.Pop().ToString();
                char ch = str[0];
                LetterType type = this.method_5(str);
                if (type == LetterType.Number)
                {
                    stack3.Push(str);
                    continue;
                }
                if (type == LetterType.SimpleOperator)
                {
                    double y = double.Parse(stack3.Pop().ToString());
                    double x = double.Parse(stack3.Pop().ToString());
                    double num3 = 0.0;
                    switch (ch)
                    {
                        case '+':
                            num3 = x + y;
                            break;

                        case '-':
                            num3 = x - y;
                            break;

                        case '*':
                            num3 = x * y;
                            break;

                        case '/':
                            num3 = x / y;
                            break;

                        default:
                            if (ch != '^')
                            {
                                throw new Exception("不支持操作符:" + ch.ToString());
                            }
                            num3 = Math.Pow(x, y);
                            break;
                    }
                    stack3.Push(num3);
                    continue;
                }
                if (type == LetterType.Function)
                {
                    string[] strArray;
                    double d = 0.0;
                    double a = 0.0;
                    double newBase = 0.0;
                    int index = str.IndexOf('(');
                    string str2 = str.Substring(0, index);
                    switch (str2)
                    {
                        case "asin":
                        {
                            this.method_1(str, 1, out strArray);
                            d = double.Parse(strArray[0]);
                            stack3.Push(Math.Asin(d).ToString());
                            continue;
                        }
                        case "acos":
                        {
                            this.method_1(str, 1, out strArray);
                            d = double.Parse(strArray[0]);
                            stack3.Push(Math.Acos(d).ToString());
                            continue;
                        }
                        case "atan":
                        {
                            this.method_1(str, 1, out strArray);
                            d = double.Parse(strArray[0]);
                            stack3.Push(Math.Atan(d).ToString());
                            continue;
                        }
                        case "acot":
                        {
                            this.method_1(str, 1, out strArray);
                            d = double.Parse(strArray[0]);
                            stack3.Push((1.0 / Math.Atan(d)).ToString());
                            continue;
                        }
                        case "sin":
                        {
                            this.method_1(str, 1, out strArray);
                            d = double.Parse(strArray[0]);
                            stack3.Push(Math.Sin(d).ToString());
                            continue;
                        }
                        case "cos":
                        {
                            this.method_1(str, 1, out strArray);
                            d = double.Parse(strArray[0]);
                            stack3.Push(Math.Cos(d).ToString());
                            continue;
                        }
                        case "tan":
                        {
                            this.method_1(str, 1, out strArray);
                            d = double.Parse(strArray[0]);
                            stack3.Push(Math.Tan(d).ToString());
                            continue;
                        }
                        case "cot":
                        {
                            this.method_1(str, 1, out strArray);
                            d = double.Parse(strArray[0]);
                            stack3.Push((1.0 / Math.Tan(d)).ToString());
                            continue;
                        }
                        case "log":
                        {
                            this.method_1(str, 2, out strArray);
                            a = double.Parse(strArray[0]);
                            newBase = double.Parse(strArray[1]);
                            stack3.Push(Math.Log(a, newBase).ToString());
                            continue;
                        }
                        case "ln":
                        {
                            this.method_1(str, 1, out strArray);
                            d = double.Parse(strArray[0]);
                            stack3.Push(Math.Log(d, 2.7182818284590451).ToString());
                            continue;
                        }
                        case "abs":
                        {
                            this.method_1(str, 1, out strArray);
                            d = double.Parse(strArray[0]);
                            stack3.Push(Math.Abs(d).ToString());
                            continue;
                        }
                        case "round":
                        {
                            this.method_1(str, 2, out strArray);
                            a = double.Parse(strArray[0]);
                            newBase = double.Parse(strArray[1]);
                            stack3.Push(Math.Round(a, (int) newBase).ToString());
                            continue;
                        }
                        case "int":
                        {
                            this.method_1(str, 1, out strArray);
                            d = double.Parse(strArray[0]);
                            stack3.Push((int) d);
                            continue;
                        }
                        case "trunc":
                        {
                            this.method_1(str, 1, out strArray);
                            d = double.Parse(strArray[0]);
                            stack3.Push(Math.Truncate(d).ToString());
                            continue;
                        }
                        case "floor":
                        {
                            this.method_1(str, 1, out strArray);
                            d = double.Parse(strArray[0]);
                            stack3.Push(Math.Floor(d).ToString());
                            continue;
                        }
                        case "ceil":
                        {
                            this.method_1(str, 1, out strArray);
                            d = double.Parse(strArray[0]);
                            stack3.Push(Math.Ceiling(d).ToString());
                            continue;
                        }
                        case "random":
                        {
                            if (this.random_0 == null)
                            {
                                this.random_0 = new Random();
                            }
                            d = this.random_0.NextDouble();
                            stack3.Push(d.ToString());
                            continue;
                        }
                        case "exp":
                        {
                            this.method_1(str, 1, out strArray);
                            d = double.Parse(strArray[0]);
                            stack3.Push(Math.Exp(d).ToString());
                            continue;
                        }
                        case "pow":
                        {
                            this.method_1(str, 2, out strArray);
                            a = double.Parse(strArray[0]);
                            newBase = double.Parse(strArray[1]);
                            stack3.Push(Math.Pow(a, newBase).ToString());
                            continue;
                        }
                        case "sqrt":
                        {
                            this.method_1(str, 1, out strArray);
                            d = double.Parse(strArray[0]);
                            stack3.Push(Math.Sqrt(d).ToString());
                            continue;
                        }
                    }
                    throw new Exception("未定义的函数：" + str2);
                }
            }
            return double.Parse(stack3.Pop().ToString());
        }

        private void method_1(string string_0, int int_0, out string[] string_1)
        {
            string_1 = new string[int_0];
            int index = string_0.IndexOf('(', 0);
            string str = string_0.Substring(index + 1, (string_0.Length - index) - 2);
            if (int_0 == 1)
            {
                string_1[0] = str;
            }
            else
            {
                int num2 = 0;
                int startIndex = 0;
                int num4 = 0;
                for (int j = 0; j <= (str.Length - 1); j++)
                {
                    if (str[j] == '(')
                    {
                        num2++;
                    }
                    else if (str[j] == ')')
                    {
                        num2--;
                    }
                    else if ((str[j] == ',') && (num2 == 0))
                    {
                        string_1[num4] = str.Substring(startIndex, j - startIndex);
                        num4++;
                        startIndex = j + 1;
                    }
                }
                if (startIndex < str.Length)
                {
                    string_1[num4] = str.Substring(startIndex);
                }
            }
            for (int i = 0; i <= (int_0 - 1); i++)
            {
                double num7;
                if (!double.TryParse(string_1[i], out num7))
                {
                    num7 = new NEval().Eval(string_1[i]);
                    string_1[i] = num7.ToString();
                }
            }
        }

        private Stack method_2(string string_0)
        {
            Stack stack = new Stack();
            Stack stack2 = new Stack();
            for (int i = 0; i <= (string_0.Length - 1); i++)
            {
                string str;
                int num;
                object obj2;
                char ch = string_0[i];
                LetterType type = this.method_3(ch, string_0, i);
                switch (type)
                {
                    case LetterType.Number:
                    {
                        this.method_8(string_0, i, out str, out num);
                        stack.Push(str);
                        i = num;
                        continue;
                    }
                    case LetterType.OpeningParenthesis:
                    {
                        stack2.Push(ch);
                        continue;
                    }
                    default:
                    {
                        if (type != LetterType.ClosingParenthesis)
                        {
                            goto Label_00A6;
                        }
                        while (stack2.Count > 0)
                        {
                            if (stack2.Peek().ToString() == "(")
                            {
                                break;
                            }
                            stack.Push(stack2.Pop());
                        }
                        continue;
                    }
                }
                stack2.Pop();
                continue;
            Label_00A6:
                if (type != LetterType.SimpleOperator)
                {
                    goto Label_0164;
                }
                if (stack2.Count == 0)
                {
                    stack2.Push(ch);
                }
                else
                {
                    char ch2 = (char) stack2.Peek();
                    if (ch2 == '(')
                    {
                        stack2.Push(ch);
                    }
                    else
                    {
                        if (this.method_6(ch) < this.method_6(ch2))
                        {
                            goto Label_014C;
                        }
                        stack2.Push(ch);
                    }
                }
                continue;
            Label_010E:
                obj2 = stack2.Peek();
                if ((this.method_6((char) obj2) <= this.method_6(ch)) || !(obj2.ToString() != "("))
                {
                    goto Label_0155;
                }
                stack.Push(stack2.Pop());
            Label_014C:
                if (stack2.Count > 0)
                {
                    goto Label_010E;
                }
            Label_0155:
                stack2.Push(ch);
                continue;
            Label_0164:
                if (type == LetterType.Function)
                {
                    this.method_7(string_0, i, out str, out num);
                    stack.Push(str);
                    i = num;
                }
            }
            while (stack2.Count > 0)
            {
                stack.Push(stack2.Pop());
            }
            return stack;
        }

        private LetterType method_3(char char_0, string string_0, int int_0)
        {
            string str = "*/^";
            if (((char_0 > '9') || (char_0 < '0')) && (char_0 != '.'))
            {
                if (char_0 == '(')
                {
                    return LetterType.OpeningParenthesis;
                }
                if (char_0 == ')')
                {
                    return LetterType.ClosingParenthesis;
                }
                if (str.IndexOf(char_0) >= 0)
                {
                    return LetterType.SimpleOperator;
                }
                if ((char_0 != '-') && (char_0 != '+'))
                {
                    return LetterType.Function;
                }
                if (int_0 != 0)
                {
                    char ch = string_0[int_0 - 1];
                    if ((ch <= '9') && (ch >= '0'))
                    {
                        return LetterType.SimpleOperator;
                    }
                    if (ch == ')')
                    {
                        return LetterType.SimpleOperator;
                    }
                }
            }
            return LetterType.Number;
        }

        private LetterType method_4(char char_0)
        {
            string str = "+-*/^";
            if (((char_0 <= '9') && (char_0 >= '0')) || (char_0 == '.'))
            {
                return LetterType.Number;
            }
            if (char_0 == '(')
            {
                return LetterType.OpeningParenthesis;
            }
            if (char_0 == ')')
            {
                return LetterType.ClosingParenthesis;
            }
            if (str.IndexOf(char_0) >= 0)
            {
                return LetterType.SimpleOperator;
            }
            return LetterType.Function;
        }

        private LetterType method_5(string string_0)
        {
            char ch = string_0[0];
            switch (ch)
            {
                case '-':
                case '+':
                    if (string_0.Length > 1)
                    {
                        return LetterType.Number;
                    }
                    return LetterType.SimpleOperator;
            }
            string str = "+-*/^";
            if (((ch <= '9') && (ch >= '0')) || (ch == '.'))
            {
                return LetterType.Number;
            }
            switch (ch)
            {
                case '(':
                    return LetterType.OpeningParenthesis;

                case ')':
                    return LetterType.ClosingParenthesis;
            }
            if (str.IndexOf(ch) >= 0)
            {
                return LetterType.SimpleOperator;
            }
            return LetterType.Function;
        }

        private int method_6(char char_0)
        {
            if ((char_0 == '+') || (char_0 == '-'))
            {
                return 0;
            }
            if (char_0 == '*')
            {
                return 1;
            }
            if (char_0 == '/')
            {
                return 2;
            }
            return 2;
        }

        private void method_7(string string_0, int int_0, out string string_1, out int int_1)
        {
            int num = 0;
            for (int i = int_0; i <= (string_0.Length - 1); i++)
            {
                char ch = string_0[i];
                switch (this.method_4(ch))
                {
                    case LetterType.OpeningParenthesis:
                        num++;
                        break;

                    case LetterType.ClosingParenthesis:
                        num--;
                        if (num == 0)
                        {
                            int_1 = i;
                            string_1 = string_0.Substring(int_0, (int_1 - int_0) + 1);
                            return;
                        }
                        break;
                }
            }
            string_1 = "";
            int_1 = -1;
        }

        private void method_8(string string_0, int int_0, out string string_1, out int int_1)
        {
            char ch1 = string_0[int_0];
            for (int i = int_0 + 1; i <= (string_0.Length - 1); i++)
            {
                char ch = string_0[i];
                if (this.method_4(ch) != LetterType.Number)
                {
                    int_1 = i - 1;
                    string_1 = string_0.Substring(int_0, (int_1 - int_0) + 1);
                    return;
                }
            }
            string_1 = string_0.Substring(int_0);
            int_1 = string_0.Length - 1;
        }
    }
}

