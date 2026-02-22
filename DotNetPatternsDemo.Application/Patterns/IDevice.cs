// Bridge Pattern
namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    // Abstraction
    public abstract class RemoteControl
    {
        protected readonly IDevice _device;

        protected RemoteControl(IDevice device)
        {
            _device = device;
        }

        public abstract void TurnOn();
        public abstract void TurnOff();
        public abstract void SetVolume(int volume);
    }

    // Refined Abstraction
    public class AdvancedRemoteControl : RemoteControl
    {
        public AdvancedRemoteControl(IDevice device) : base(device) { }

        public void Mute()
        {
            Console.WriteLine("Sound muted (Advanced)");
            _device.SetVolume(0);
        }

        public override void TurnOn() => _device.TurnOn();
        public override void TurnOff() => _device.TurnOff();
        public override void SetVolume(int volume) => _device.SetVolume(volume);
    }

    // Implementor interface
    public interface IDevice
    {
        void TurnOn();
        void TurnOff();
        void SetVolume(int volume);
    }

    // Concrete Implementors
    public class Tv : IDevice
    {
        public void TurnOn() => Console.WriteLine("TV turned on");
        public void TurnOff() => Console.WriteLine("TV turned off");
        public void SetVolume(int volume) => Console.WriteLine($"TV volume: {volume}");
    }

    public class Radio : IDevice
    {
        public void TurnOn() => Console.WriteLine("Radio turned on");
        public void TurnOff() => Console.WriteLine("Radio turned off");
        public void SetVolume(int volume) => Console.WriteLine($"Radio volume: {volume}");
    }
}