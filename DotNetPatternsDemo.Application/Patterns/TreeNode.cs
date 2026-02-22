// Iterator Pattern
using System.Collections;

namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    public class TreeNode
    {
        public int Value { get; }
        public List<TreeNode> Children { get; } = new();

        public TreeNode(int value) => Value = value;

        public void Add(TreeNode child) => Children.Add(child);
    }

    public class DepthFirstIterator : IEnumerator<TreeNode>
    {
        private readonly Stack<TreeNode> _stack = new();
        private TreeNode? _current;

        public DepthFirstIterator(TreeNode root)
        {
            _stack.Push(root);
        }

        public TreeNode Current => _current ?? throw new InvalidOperationException();

        object IEnumerator.Current => Current;

        public bool MoveNext()
        {
            if (_stack.Count == 0) return false;

            _current = _stack.Pop();

            foreach (var child in _current.Children.AsEnumerable().Reverse())
            {
                _stack.Push(child);
            }

            return true;
        }

        public void Reset() => throw new NotSupportedException();

        public void Dispose() { }
    }
}