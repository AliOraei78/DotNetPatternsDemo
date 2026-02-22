// Property Injection
public class ReportGenerator
{
    public ILoggerService? Logger { get; set; }  // Property Injection

    public void Generate()
    {
        Logger?.Log("Report generated");
        // Works even without Logger (optional)
    }
}