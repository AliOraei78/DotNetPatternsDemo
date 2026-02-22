// Composite Pattern
namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    // Component
    public abstract class MenuComponent
    {
        public string Name { get; protected set; }

        public abstract void Display(int depth = 0);
        public virtual void Add(MenuComponent component) => throw new NotSupportedException();
        public virtual void Remove(MenuComponent component) => throw new NotSupportedException();
    }

    // Leaf
    public class MenuItem : MenuComponent
    {
        public MenuItem(string name)
        {
            Name = name;
        }

        public override void Display(int depth = 0)
        {
            Console.WriteLine(new string('-', depth * 2) + Name);
        }
    }

    // Composite
    public class MenuGroup : MenuComponent
    {
        private readonly List<MenuComponent> _children = new();

        public MenuGroup(string name)
        {
            Name = name;
        }

        public override void Add(MenuComponent component) => _children.Add(component);
        public override void Remove(MenuComponent component) => _children.Remove(component);

        public override void Display(int depth = 0)
        {
            Console.WriteLine(new string('-', depth * 2) + Name + " (group) ");
            foreach (var child in _children)
            {
                child.Display(depth + 1);
            }
        }
    }
}