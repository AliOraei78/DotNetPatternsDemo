// State Pattern
namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    public interface IVendingMachineState
    {
        void InsertCoin(VendingMachine machine, decimal amount);
        void SelectProduct(VendingMachine machine, string product);
        void Dispense(VendingMachine machine);
    }

    public class NoCoinState : IVendingMachineState
    {
        public void InsertCoin(VendingMachine machine, decimal amount)
        {
            machine.AddBalance(amount);
            machine.ChangeState(new HasCoinState());
            Console.WriteLine($"Coin inserted. Balance: {machine.Balance}");
        }

        public void SelectProduct(VendingMachine machine, string product)
            => Console.WriteLine("Please insert a coin first.");
        public void Dispense(VendingMachine machine)
            => Console.WriteLine("Please insert a coin first.");
    }

    public class HasCoinState : IVendingMachineState
    {
        public void InsertCoin(VendingMachine machine, decimal amount)
        {
            machine.AddBalance(amount);
            Console.WriteLine($"Additional coin inserted. Balance: {machine.Balance}");
        }

        public void SelectProduct(VendingMachine machine, string product)
        {
            if (machine.Balance >= 5000) // example price
            {
                machine.SetSelectedProduct(product);
                machine.ChangeState(new ProductSelectedState());
                Console.WriteLine($"Product selected: {product}");
            }
            else
            {
                Console.WriteLine("Insufficient balance.");
            }
        }

        public void Dispense(VendingMachine machine)
            => Console.WriteLine("Please select a product first.");
    }

    public class ProductSelectedState : IVendingMachineState
    {
        public void InsertCoin(VendingMachine machine, decimal amount)
            => Console.WriteLine("Product already selected.");
        public void SelectProduct(VendingMachine machine, string product)
            => Console.WriteLine("Product has already been selected.");
        public void Dispense(VendingMachine machine)
        {
            Console.WriteLine($"Product dispensed: {machine.SelectedProduct}");
            machine.DeductBalance(5000);
            machine.ChangeState(new NoCoinState());
        }
    }

    public class VendingMachine
    {
        public IVendingMachineState State { get; private set; }
        public decimal Balance { get; private set; }
        public string? SelectedProduct { get; private set; }

        public VendingMachine()
        {
            State = new NoCoinState();
        }

        public void ChangeState(IVendingMachineState state) => State = state;
        public void AddBalance(decimal amount) => Balance += amount;
        public void DeductBalance(decimal amount) => Balance -= amount;
        public void SetSelectedProduct(string product) => SelectedProduct = product;

        public void InsertCoin(decimal amount) => State.InsertCoin(this, amount);
        public void SelectProduct(string product) => State.SelectProduct(this, product);
        public void Dispense() => State.Dispense(this);
    }
}