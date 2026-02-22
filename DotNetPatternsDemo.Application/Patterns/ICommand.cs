namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    // Receiver
    public class Light
    {
        public void TurnOn() => Console.WriteLine("Light turned on");
        public void TurnOff() => Console.WriteLine("Light turned off");
    }

    // Command interface
    public interface ICommand
    {
        void Execute();
        void Undo();  // For undo support
    }

    // Concrete Commands
    public class LightOnCommand : ICommand
    {
        private readonly Light _light;

        public LightOnCommand(Light light) => _light = light;

        public void Execute() => _light.TurnOn();
        public void Undo() => _light.TurnOff();
    }

    public class LightOffCommand : ICommand
    {
        private readonly Light _light;

        public LightOffCommand(Light light) => _light = light;

        public void Execute() => _light.TurnOff();
        public void Undo() => _light.TurnOn();
    }

    // Invoker
    public class RemoteController
    {
        private ICommand? _command;

        public void SetCommand(ICommand command) => _command = command;

        public void PressButton() => _command?.Execute();

        public void PressUndo() => _command?.Undo();
    }
}