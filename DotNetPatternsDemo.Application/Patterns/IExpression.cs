namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    public interface IExpression
    {
        int Interpret(Dictionary<string, int> context);
    }

    public class Number : IExpression
    {
        private readonly int _value;
        public Number(int value) => _value = value;
        public int Interpret(Dictionary<string, int> context) => _value;
    }

    public class Plus : IExpression
    {
        private readonly IExpression _left;
        private readonly IExpression _right;

        public Plus(IExpression left, IExpression right)
        {
            _left = left;
            _right = right;
        }

        public int Interpret(Dictionary<string, int> context)
            => _left.Interpret(context) + _right.Interpret(context);
    }

    public class Minus : IExpression
    {
        private readonly IExpression _left;
        private readonly IExpression _right;

        public Minus(IExpression left, IExpression right)
        {
            _left = left;
            _right = right;
        }

        public int Interpret(Dictionary<string, int> context)
            => _left.Interpret(context) - _right.Interpret(context);
    }

    public class Variable : IExpression
    {
        private readonly string _name;

        public Variable(string name) => _name = name;

        public int Interpret(Dictionary<string, int> context)
            => context.TryGetValue(_name, out var value) ? value : 0;
    }
}