namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    // Abstract Product A
    public interface IButton
    {
        void Render();
    }

    // Abstract Product B
    public interface ICheckbox
    {
        void Render();
    }

    // Abstract Factory
    public interface IUiFactory
    {
        IButton CreateButton();
        ICheckbox CreateCheckbox();
    }

    // Concrete Factories
    public class LightUiFactory : IUiFactory
    {
        public IButton CreateButton() => new LightButton();
        public ICheckbox CreateCheckbox() => new LightCheckbox();
    }

    public class DarkUiFactory : IUiFactory
    {
        public IButton CreateButton() => new DarkButton();
        public ICheckbox CreateCheckbox() => new DarkCheckbox();
    }

    // Concrete Products
    public class LightButton : IButton
    {
        public void Render() => Console.WriteLine("Light Button");
    }
    public class LightCheckbox : ICheckbox
    {
        public void Render() => Console.WriteLine("Light Checkbox");
    }
    public class DarkButton : IButton
    {
        public void Render() => Console.WriteLine("Dark Button");
    }
    public class DarkCheckbox : ICheckbox
    {
        public void Render() => Console.WriteLine("Dark Checkbox");
    }
}