
using System.Reflection.Metadata.Ecma335;

public class ExprBalanceWOD
{
    public static void Main(string[] args)
    {
        List<string> exprs = new()
        {
            "{()}[{}]", // balanced
            "{(x+1)}[x}]", // Not balanced
            "{(x+1)}{[x+2]}", // Balanced
            "{(x+1)}[{x+2]}"  // Not balanced
        };
        foreach (var expr in exprs)
        {
            if (IsBalParenthesis(expr))
            {
                Console.WriteLine("YES - The expression:{0} is balanced", expr);
            }
            else
            {
                Console.WriteLine("NO The expression:{0} is NOT balanced", expr);
            }
        }
    }
    private static bool IsBalParenthesis(string expr)
    {
        // Use a stack to check if expression is balanced
        
        Stack<char> stack = new();
        foreach (char c in expr)
        {
            if (c.Equals('[') || c.Equals('{') || c.Equals('('))
                stack.Push(c);
            else if (c.Equals(']'))
                if (stack.Peek().Equals('['))
                    stack.Pop();
                else return false; 
            else if (c.Equals('}'))
                if (stack.Peek().Equals('{'))
                    stack.Pop();
                else return false;
            else if (c.Equals(')'))
                if (stack.Peek().Equals('('))
                    stack.Pop();
                else return false;
        }
        return stack.Count == 0;
    }
}

