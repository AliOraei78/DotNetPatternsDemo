// Chain of Responsibility Pattern
namespace AdvancedDotNetPatternsDemo.Application.Patterns
{
    public class LeaveRequest
    {
        public int Days { get; set; }
        public string Employee { get; set; }
    }

    // Base Handler
    public abstract class LeaveApprover
    {
        protected LeaveApprover? NextApprover { get; set; }

        public void SetNext(LeaveApprover approver)
        {
            NextApprover = approver;
        }

        public abstract void ProcessRequest(LeaveRequest request);
    }

    // Concrete Handlers
    public class Supervisor : LeaveApprover
    {
        public override void ProcessRequest(LeaveRequest request)
        {
            if (request.Days <= 3)
            {
                Console.WriteLine($"Supervisor: {request.Days}-day leave approved for {request.Employee}.");
            }
            else if (NextApprover != null)
            {
                NextApprover.ProcessRequest(request);
            }
        }
    }

    public class Manager : LeaveApprover
    {
        public override void ProcessRequest(LeaveRequest request)
        {
            if (request.Days <= 7)
            {
                Console.WriteLine($"Manager: {request.Days}-day leave approved for {request.Employee}.");
            }
            else if (NextApprover != null)
            {
                NextApprover.ProcessRequest(request);
            }
        }
    }

    public class HRDirector : LeaveApprover
    {
        public override void ProcessRequest(LeaveRequest request)
        {
            Console.WriteLine($"HR Director: {request.Days}-day leave approved for {request.Employee} (even more than 7 days).");
        }
    }
}